//
// Copyright © Martin Tamme
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using NProxy.Core.Internal;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Reflection.Emit;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy type builder.
    /// </summary>
    internal sealed class ProxyTypeBuilder : ITypeBuilder
    {
        /// <summary>
        /// The <c>IInvocationHandler.Invoke</c> method information.
        /// </summary>
        private static readonly MethodInfo InvocationHandlerInvokeMethodInfo = typeof(IInvocationHandler).GetMethod(
            "Invoke",
            BindingFlags.Public | BindingFlags.Instance,
            typeof(object), typeof(MethodInfo), typeof(object[]));

        /// <summary>
        /// The type repository.
        /// </summary>
        private readonly ITypeRepository _typeRepository;

        /// <summary>
        /// The parent type.
        /// </summary>
        private readonly Type _parentType;

        /// <summary>
        /// The type builder.
        /// </summary>
        private readonly TypeBuilder _typeBuilder;

        /// <summary>
        /// The invocation handler field information.
        /// </summary>
        private readonly FieldInfo _invocationHandlerFieldInfo;

        /// <summary>
        /// The interface types.
        /// </summary>
        private readonly HashSet<Type> _interfaceTypes;

        private readonly ProxyFactoryOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeBuilder"/> class.
        /// </summary>
        /// <param name="typeRepository">The type repository.</param>
        /// <param name="parentType">The parent type.</param>
        public ProxyTypeBuilder(ITypeRepository typeRepository, Type parentType, ProxyFactoryOptions options)
        {
            if (typeRepository == null)
                throw new ArgumentNullException("typeRepository");

            if (parentType == null)
                throw new ArgumentNullException("parentType");

            if (parentType.IsSealed)
                throw new ArgumentException(Resources.ParentTypeMustNotBeSealed, "parentType");

            if (parentType.IsGenericTypeDefinition)
                throw new ArgumentException(Resources.ParentTypeMustNotBeAGenericTypeDefinition, "parentType");

            _typeRepository = typeRepository;
            _parentType = parentType;
            _options = options.Clone();

            _typeBuilder = typeRepository.DefineType("Proxy", parentType);

            var attributeData = parentType.GetCustomAttributeDataCollection();
            foreach (var attData in attributeData)
            {
                _typeBuilder.SetCustomAttribute(attData);
            }

            _invocationHandlerFieldInfo = _typeBuilder.DefineField(
                "_invocationHandler",
                typeof(IInvocationHandler),
                FieldAttributes.Private | FieldAttributes.InitOnly);

            _interfaceTypes = new HashSet<Type>();
        }

        /// <summary>
        /// Builds an intercepted method based on the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="isExplicit">A value indicating whether the specified method should be implemented explicitly.</param>
        /// <returns>The intercepted method builder.</returns>
        private MethodBuilder BuildInterceptedMethod(MethodInfo methodInfo, bool isExplicit)
        {
            var isOverride = IsOverrideMember(methodInfo);

            if (isOverride && !methodInfo.CanOverride())
                throw new InvalidOperationException(String.Format(Resources.MethodNotOverridable, methodInfo.Name));

            // Define method.
            var methodBuilder = _typeBuilder.DefineMethod(methodInfo, isExplicit, isOverride);

            // Define generic parameters.
            var genericParameterTypes = methodBuilder.DefineGenericParameters(methodInfo);

            // Define parameters.
            methodBuilder.DefineParameters(methodInfo, genericParameterTypes);

            // Only define method override if method is implemented explicitly and is an override.
            if (isExplicit && isOverride)
                _typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);

            // Implement method.
            var ilGenerator = methodBuilder.GetILGenerator();

            // Load arguments.
            var parameterTypes = methodInfo.MapGenericParameterTypes(genericParameterTypes);
            var parametersLocalBuilder = ilGenerator.NewArray(typeof(object), parameterTypes.Length);

            LoadArguments(ilGenerator, parameterTypes, parametersLocalBuilder);

            // Load invocation handler.
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, _invocationHandlerFieldInfo);

            // Load source object.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Get method information constructor.
            var methodInfoConstructorInfo = GetMethodInfoConstructor(methodInfo, genericParameterTypes);

            // Create and load method information.
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(isOverride ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Newobj, methodInfoConstructorInfo);

            // Load parameters.
            ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);

            // Call invocation handler method.
            ilGenerator.EmitCall(InvocationHandlerInvokeMethodInfo);

            // Restore by reference arguments.
            RestoreByReferenceArguments(ilGenerator, parameterTypes, parametersLocalBuilder);

            // Handle return value.
            var returnType = methodInfo.MapGenericReturnType(genericParameterTypes);

            if (returnType.IsVoid())
                ilGenerator.Emit(OpCodes.Pop);
            else
                ilGenerator.EmitUnbox(returnType);

            ilGenerator.Emit(OpCodes.Ret);

            return methodBuilder;
        }

        /// <summary>
        /// Returns a constructor information for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <returns>The constructor information.</returns>
        private ConstructorInfo GetMethodInfoConstructor(MethodInfo methodInfo, Type[] genericParameterTypes)
        {
            var type = _typeRepository.GetType(methodInfo);
            var constructorInfo = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, typeof(object), typeof(bool));

            if (!type.IsGenericTypeDefinition)
                return constructorInfo;

            var genericType = type.MakeGenericType(genericParameterTypes);

            return TypeBuilder.GetConstructor(genericType, constructorInfo);
        }

        /// <summary>
        /// Loads the arguments onto the stack.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parametersLocalBuilder">The parameters local builder.</param>
        private static void LoadArguments(ILGenerator ilGenerator, IList<Type> parameterTypes, LocalBuilder parametersLocalBuilder)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.EmitLoadArgument(index + 1);

                if (parameterType.IsByRef)
                {
                    var elementType = parameterType.GetElementType();

                    ilGenerator.EmitLoadIndirect(parameterType);
                    ilGenerator.EmitBox(elementType);
                }
                else
                {
                    ilGenerator.EmitBox(parameterType);
                }

                ilGenerator.Emit(OpCodes.Stelem_Ref);
            }
        }

        /// <summary>
        /// Restores the by reference arguments.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parametersLocalBuilder">The parameters local builder.</param>
        private static void RestoreByReferenceArguments(ILGenerator ilGenerator, IList<Type> parameterTypes, LocalBuilder parametersLocalBuilder)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var argumentType = parameterTypes[index];

                if (!argumentType.IsByRef)
                    continue;

                var elementType = argumentType.GetElementType();

                ilGenerator.EmitLoadArgument(index + 1);
                ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                ilGenerator.EmitUnbox(elementType);
                ilGenerator.EmitStoreIndirect(argumentType);
            }
        }

        /// <summary>
        /// Returns a value indicating whether the specified member should be overridden.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>A value indicating whether the specified member should be overridden.</returns>
        private bool IsOverrideMember(MemberInfo memberInfo)
        {
            var declaringType = memberInfo.DeclaringType;

            if (declaringType.IsInterface)
                return _interfaceTypes.Contains(declaringType);

            return declaringType.IsAssignableFrom(_parentType);
        }

        /// <summary>
        /// Returns a value indicating whether the specified member should be implemented explicitly.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>A value indicating whether the specified member should be implemented explicitly.</returns>
        private bool IsExplicitMember(MemberInfo memberInfo)
        {
            var declaringType = memberInfo.DeclaringType;

            // Implement interface members always explicitly.
            return declaringType.IsInterface && _interfaceTypes.Contains(declaringType);
        }

        #region ITypeBuilder Members

        /// <inheritdoc/>
        public void AddCustomAttribute(ConstructorInfo constructorInfo, params object[] arguments)
        {
            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _typeBuilder.SetCustomAttribute(constructorInfo, arguments);
        }

        /// <inheritdoc/>
        public void AddInterface(Type interfaceType)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format(Resources.TypeNotAnInterfaceType, interfaceType), "interfaceType");

            if (interfaceType.IsGenericTypeDefinition)
                throw new ArgumentException(String.Format(Resources.InterfaceTypeMustNotBeAGenericTypeDefinition, interfaceType), "interfaceType");

            _typeBuilder.AddInterfaceImplementation(interfaceType);

            _interfaceTypes.Add(interfaceType);
        }

        /// <inheritdoc/>
        public void BuildConstructor(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            // Define constructor.
            var constructorBuilder = _typeBuilder.DefineConstructor(
                constructorInfo,
                new[] { typeof(IInvocationHandler) },
                new[] { "invocationHandler" });

            // Implement constructor.
            var ilGenerator = constructorBuilder.GetILGenerator();
            var parameterInfos = constructorInfo.GetParameters();

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load arguments.
            ilGenerator.EmitLoadArguments(2, parameterInfos.Length);

            // Call parent constructor.
            ilGenerator.Emit(OpCodes.Call, constructorInfo);

            // Check for null invocation handler.
            var invocationHandlerNotNullLabel = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Brtrue, invocationHandlerNotNullLabel);
            ilGenerator.ThrowException(typeof(ArgumentNullException), "invocationHandler");
            ilGenerator.MarkLabel(invocationHandlerNotNullLabel);

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load invocation handler.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Store invocation handler.
            ilGenerator.Emit(OpCodes.Stfld, _invocationHandlerFieldInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <inheritdoc/>
        public void BuildConstructor(ConstructorInfo constructorInfo, Type invocationHandlerType)
        {
            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            // Define constructor.
            var constructorBuilder = _typeBuilder.DefineConstructor(
                constructorInfo,
                Type.EmptyTypes,
                new String[0]);

            // Implement constructor.
            var ilGenerator = constructorBuilder.GetILGenerator();
            var parameterInfos = constructorInfo.GetParameters();

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load arguments.
            ilGenerator.EmitLoadArguments(2, parameterInfos.Length);

            // Call parent constructor.
            ilGenerator.Emit(OpCodes.Call, constructorInfo);

            // Check for null invocation handler.
            //var invocationHandlerNotNullLabel = ilGenerator.DefineLabel();

            //ilGenerator.Emit(OpCodes.Ldarg_1);
            //ilGenerator.Emit(OpCodes.Brtrue, invocationHandlerNotNullLabel);
            //ilGenerator.ThrowException(typeof(ArgumentNullException), "invocationHandler");
            //ilGenerator.MarkLabel(invocationHandlerNotNullLabel);            

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load invocation handler.
            //ilGenerator.Emit(OpCodes.Ldarg_1);

            ilGenerator.Emit(OpCodes.Newobj, invocationHandlerType.GetConstructor(Type.EmptyTypes));

            // Store invocation handler.
            ilGenerator.Emit(OpCodes.Stfld, _invocationHandlerFieldInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <inheritdoc/>
        public bool IsConcreteEvent(EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            var methodInfos = eventInfo.GetAccessorMethods();

            return methodInfos.All(IsConcreteMethod);
        }

        /// <inheritdoc/>
        public void BuildEvent(EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            var isExplicit = IsExplicitMember(eventInfo);

            _typeBuilder.DefineEvent(eventInfo, isExplicit, BuildInterceptedMethod);
        }

        /// <inheritdoc/>
        public bool IsConcreteProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var methodInfos = propertyInfo.GetAccessorMethods();

            return methodInfos.All(IsConcreteMethod);
        }

        /// <inheritdoc/>
        public void BuildProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var isExplicit = IsExplicitMember(propertyInfo);

            _typeBuilder.DefineProperty(propertyInfo, isExplicit, BuildInterceptedMethod);
        }

        /// <inheritdoc/>
        public bool IsConcreteMethod(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            return !methodInfo.IsAbstract && IsOverrideMember(methodInfo);
        }

        /// <inheritdoc/>
        public void BuildMethod(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            var isExplicit = IsExplicitMember(methodInfo);

            BuildInterceptedMethod(methodInfo, isExplicit);
        }

        /// <inheritdoc/>
        public Type CreateType()
        {
            return _typeBuilder.CreateType();
        }

        #endregion
    }
}