//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using NProxy.Core.Internal.Generators;
using NProxy.Core.Internal.Reflection;

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
        private static readonly MethodInfo InvokeMethodInfo = typeof (IInvocationHandler).GetMethod(
            "Invoke",
            BindingFlags.Public | BindingFlags.Instance,
            typeof (object), typeof (MethodInfo), typeof (object[]));

        /// <summary>
        /// The parent type.
        /// </summary>
        private readonly Type _parentType;

        /// <summary>
        /// The type builder.
        /// </summary>
        private readonly TypeBuilder _typeBuilder;

        /// <summary>
        /// The method information type provider.
        /// </summary>
        private readonly ITypeProvider<MethodInfo> _methodInfoTypeProvider;

        /// <summary>
        /// The invocation handler field information.
        /// </summary>
        private readonly FieldInfo _invocationHandlerFieldInfo;

        /// <summary>
        /// The interface types.
        /// </summary>
        private readonly HashSet<Type> _interfaceTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeBuilder"/> class.
        /// </summary>
        /// <param name="parentType">The parent type.</param>
        /// <param name="typeEmitter">The type emitter.</param>
        /// <param name="methodInfoTypeProvider">The method information type provider.</param>
        public ProxyTypeBuilder(Type parentType, ITypeEmitter typeEmitter, ITypeProvider<MethodInfo> methodInfoTypeProvider)
        {
            if (parentType == null)
                throw new ArgumentNullException("parentType");

            if (parentType.IsSealed)
                throw new ArgumentException("Parent type must not be sealed", "parentType");

            if (parentType.IsGenericTypeDefinition)
                throw new ArgumentException("Parent type must not be a generic type definition", "parentType");

            if (typeEmitter == null)
                throw new ArgumentNullException("typeEmitter");

            if (methodInfoTypeProvider == null)
                throw new ArgumentNullException("methodInfoTypeProvider");

            _parentType = parentType;
            _methodInfoTypeProvider = methodInfoTypeProvider;

            _typeBuilder = typeEmitter.DefineType("Proxy", parentType);

            _invocationHandlerFieldInfo = _typeBuilder.DefineField(
                "_invocationHandler",
                typeof (IInvocationHandler),
                FieldAttributes.Private | FieldAttributes.InitOnly);

            _interfaceTypes = new HashSet<Type>();
        }

        /// <summary>
        /// Builds an intercepted method based on the specified method information.
        /// </summary>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        /// <param name="isExplicit">A value indicating weather the specified method should be implemented explicitly.</param>
        /// <returns>The intercepted method builder.</returns>
        private MethodBuilder BuildInterceptedMethod(MethodInfo declaringMethodInfo, bool isExplicit)
        {
            if (!declaringMethodInfo.CanOverride())
                throw new ArgumentException(String.Format("Method '{0}' is not overridable", declaringMethodInfo.Name), "declaringMethodInfo");

            var isOverride = IsOverrideMethod(declaringMethodInfo);

            // Define method.
            var methodBuilder = _typeBuilder.DefineMethod(declaringMethodInfo, isExplicit, isOverride);

            // Define generic parameters.
            var genericParameterTypes = methodBuilder.DefineGenericParameters(declaringMethodInfo);

            // Define parameters.
            methodBuilder.DefineParameters(declaringMethodInfo, genericParameterTypes);

            // Only define method override if method is implemented explicitly and is an override.
            if (isExplicit && isOverride)
                _typeBuilder.DefineMethodOverride(methodBuilder, declaringMethodInfo);

            // Implement method.
            var ilGenerator = methodBuilder.GetILGenerator();

            // Load parameters.
            var parameterTypes = declaringMethodInfo.MapGenericParameterTypes(genericParameterTypes);
            var parametersLocalBuilder = ilGenerator.NewArray(typeof (object), parameterTypes.Length);

            LoadParameters(ilGenerator, parameterTypes, parametersLocalBuilder);

            // Load invocation handler.
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, _invocationHandlerFieldInfo);

            // Load proxy object.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Get method information constructor.
            var methodInfoConstructorInfo = GetMethodInfoConstructor(declaringMethodInfo, genericParameterTypes);

            // Create and load method information.
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(isOverride ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Newobj, methodInfoConstructorInfo);

            // Load parameters.
            ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);

            // Call invocation handler method.
            ilGenerator.EmitCall(InvokeMethodInfo);

            // Restore by reference parameters.
            RestoreByReferenceParameters(ilGenerator, parameterTypes, parametersLocalBuilder);

            // Handle return value.
            var returnType = declaringMethodInfo.MapGenericReturnType(genericParameterTypes);

            if (returnType.IsVoid())
                ilGenerator.Emit(OpCodes.Pop);
            else
                ilGenerator.EmitUnbox(returnType);

            ilGenerator.Emit(OpCodes.Ret);

            return methodBuilder;
        }

        /// <summary>
        /// Returns a method information constructor for the specified declaring method information.
        /// </summary>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <returns>The method information constructor information.</returns>
        private ConstructorInfo GetMethodInfoConstructor(MethodInfo declaringMethodInfo, Type[] genericParameterTypes)
        {
            var type = _methodInfoTypeProvider.GetType(declaringMethodInfo);
            var constructorInfo = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance,
                                                      typeof (object), typeof (bool));

            if (!type.IsGenericTypeDefinition)
                return constructorInfo;

            var genericType = type.MakeGenericType(genericParameterTypes);

            return TypeBuilder.GetConstructor(genericType, constructorInfo);
        }

        /// <summary>
        /// Loads the parameters.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parametersLocalBuilder">The parameters local builder.</param>
        private static void LoadParameters(ILGenerator ilGenerator, IList<Type> parameterTypes, LocalBuilder parametersLocalBuilder)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);
                ilGenerator.Emit(OpCodes.Ldc_I4, index);
                ilGenerator.Emit(OpCodes.Ldarg, index + 1);

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
        /// Restores the by reference parameters.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parametersLocalBuilder">The parameters local builder.</param>
        private static void RestoreByReferenceParameters(ILGenerator ilGenerator, IList<Type> parameterTypes, LocalBuilder parametersLocalBuilder)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var argumentType = parameterTypes[index];

                if (!argumentType.IsByRef)
                    continue;

                var elementType = argumentType.GetElementType();

                ilGenerator.Emit(OpCodes.Ldarg, index + 1);
                ilGenerator.Emit(OpCodes.Ldloc, parametersLocalBuilder);
                ilGenerator.Emit(OpCodes.Ldc_I4, index);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                ilGenerator.EmitUnbox(elementType);
                ilGenerator.EmitStoreIndirect(argumentType);
            }
        }

        /// <summary>
        /// Returns a value indicating weather the specified method should be overridden.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>A value indicating weather the specified method should be overridden.</returns>
        private bool IsOverrideMethod(MethodBase methodBase)
        {
            if (!methodBase.IsVirtual)
                return false;

            var declaringType = methodBase.GetDeclaringType();

            if (declaringType.IsInterface)
                return _interfaceTypes.Contains(declaringType);

            return declaringType.IsAssignableFrom(_parentType);
        }

        /// <summary>
        /// Returns a value indicating weather the specified member should be implemented explicitly.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>A value indicating weather the specified member should be implemented explicitly.</returns>
        private bool IsExplicitMember(MemberInfo memberInfo)
        {
            var declaringType = memberInfo.GetDeclaringType();

            // Implement interface members always explicitly.
            return _interfaceTypes.Contains(declaringType);
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
                throw new ArgumentException(String.Format("Type '{0}' is not an interface type", interfaceType), "interfaceType");

            if (interfaceType.IsGenericTypeDefinition)
                throw new ArgumentException("Interface type must not be a generic type definition", "interfaceType");

            _typeBuilder.AddInterfaceImplementation(interfaceType);

            _interfaceTypes.Add(interfaceType);
        }

        /// <inheritdoc/>
        public void BuildConstructor(ConstructorInfo declaringConstructorInfo)
        {
            if (declaringConstructorInfo == null)
                throw new ArgumentNullException("declaringConstructorInfo");

            // Define constructor.
            var constructorBuilder = _typeBuilder.DefineConstructor(
                declaringConstructorInfo,
                new[] {typeof (IInvocationHandler)},
                new[] {"invocationHandler"});

            // Implement constructor.
            var ilGenerator = constructorBuilder.GetILGenerator();
            var parameterInfos = declaringConstructorInfo.GetParameters();

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load arguments.
            ilGenerator.EmitLoadArguments(2, parameterInfos.Length);

            // Call parent constructor.
            ilGenerator.Emit(OpCodes.Call, declaringConstructorInfo);

            // Check for null invocation handler.
            var invocationHandlerNotNullLabel = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Brtrue, invocationHandlerNotNullLabel);
            ilGenerator.ThrowException(typeof (ArgumentNullException), "invocationHandler");
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
        public void BuildEvent(EventInfo declaringEventInfo)
        {
            if (declaringEventInfo == null)
                throw new ArgumentNullException("declaringEventInfo");

            var isExplicit = IsExplicitMember(declaringEventInfo);

            _typeBuilder.DefineEvent(declaringEventInfo, isExplicit, BuildInterceptedMethod);
        }

        /// <inheritdoc/>
        public void BuildProperty(PropertyInfo declaringPropertyInfo)
        {
            if (declaringPropertyInfo == null)
                throw new ArgumentNullException("declaringPropertyInfo");

            var isExplicit = IsExplicitMember(declaringPropertyInfo);

            _typeBuilder.DefineProperty(declaringPropertyInfo, isExplicit, BuildInterceptedMethod);
        }

        /// <inheritdoc/>
        public void BuildMethod(MethodInfo declaringMethodInfo)
        {
            if (declaringMethodInfo == null)
                throw new ArgumentNullException("declaringMethodInfo");

            var isExplicit = IsExplicitMember(declaringMethodInfo);

            BuildInterceptedMethod(declaringMethodInfo, isExplicit);
        }

        /// <inheritdoc/>
        public Type CreateType()
        {
            return _typeBuilder.CreateType();
        }

        #endregion
    }
}