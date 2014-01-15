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
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal.Builders
{
    /// <summary>
    /// Represents a method information type factory.
    /// </summary>
    internal sealed class MethodInfoTypeFactory : ITypeFactory
    {
        /// <summary>
        /// The <see cref="MethodBase.GetMethodFromHandle(RuntimeMethodHandle,RuntimeTypeHandle)"/> method information.
        /// </summary>
        private static readonly MethodInfo MethodBaseGetMethodFromHandleMethodInfo = typeof (MethodBase).GetMethod(
            "GetMethodFromHandle",
            BindingFlags.Public | BindingFlags.Static,
            typeof (RuntimeMethodHandle), typeof (RuntimeTypeHandle));

        /// <summary>
        /// The <see cref="MethodInfoBase"/> constructor information.
        /// </summary>
        private static readonly ConstructorInfo MethodInfoBaseConstructorInfo = typeof (MethodInfoBase).GetConstructor(
            BindingFlags.NonPublic | BindingFlags.Instance,
            typeof (object), typeof (MethodInfo));

        /// <summary>
        /// The <see cref="MethodInfoBase.InvokeBase(object,object[])"/> method information.
        /// </summary>
        private static readonly MethodInfo MethodInfoBaseInvokeBaseMethodInfo = typeof (MethodInfoBase).GetMethod(
            "InvokeBase",
            BindingFlags.NonPublic | BindingFlags.Instance,
            typeof (object), typeof (object[]));

        /// <summary>
        /// The <see cref="MethodInfoBase.InvokeVirtual(object,object[])"/> method information.
        /// </summary>
        private static readonly MethodInfo MethodInfoBaseInvokeVirtualMethodInfo = typeof (MethodInfoBase).GetMethod(
            "InvokeVirtual",
            BindingFlags.NonPublic | BindingFlags.Instance,
            typeof (object), typeof (object[]));

        /// <summary>
        /// The type repository.
        /// </summary>
        private readonly ITypeRepository _typeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodInfoTypeFactory"/> class.
        /// </summary>
        /// <param name="typeRepository">The type repository.</param>
        public MethodInfoTypeFactory(ITypeRepository typeRepository)
        {
            if (typeRepository == null)
                throw new ArgumentNullException("typeRepository");

            _typeRepository = typeRepository;
        }

        /// <summary>
        /// Builds the type initializer.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="prototypeMethodInfo">The prototype method information.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <param name="methodFieldInfo">The method information static field information.</param>
        private static void BuildTypeInitializer(TypeBuilder typeBuilder,
                                                 MethodInfo prototypeMethodInfo,
                                                 Type[] genericParameterTypes,
                                                 FieldInfo methodFieldInfo)
        {
            // Define type initializer.
            var typeInitializer = typeBuilder.DefineConstructor(
                MethodAttributes.Private | MethodAttributes.Static | MethodAttributes.SpecialName,
                CallingConventions.Standard,
                null);

            // Implement type initializer.
            var ilGenerator = typeInitializer.GetILGenerator();

            // Get and load method information.
            var methodInfo = prototypeMethodInfo.MapGenericMethod(genericParameterTypes);
            var declaringType = methodInfo.GetDeclaringType();

            ilGenerator.Emit(OpCodes.Ldtoken, methodInfo);
            ilGenerator.Emit(OpCodes.Ldtoken, declaringType);
            ilGenerator.EmitCall(MethodBaseGetMethodFromHandleMethodInfo);

            // Store method information.
            ilGenerator.Emit(OpCodes.Castclass, typeof (MethodInfo));
            ilGenerator.Emit(OpCodes.Stsfld, methodFieldInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Builds the constructor.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodFieldInfo">The method information static field information.</param>
        /// <returns>The constructor information.</returns>
        private static void BuildConstructor(TypeBuilder typeBuilder, FieldInfo methodFieldInfo)
        {
            // Define constructor.
            var constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                MethodInfoBaseConstructorInfo.CallingConvention,
                new[] {typeof (object)},
                new[] {"source"});

            // Implement constructor.
            var ilGenerator = constructorBuilder.GetILGenerator();

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load source object.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Load method information.
            ilGenerator.Emit(OpCodes.Ldsfld, methodFieldInfo);

            // Call parent constructor.
            ilGenerator.Emit(OpCodes.Call, MethodInfoBaseConstructorInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Builds the invoke method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <param name="isVirtual">A value indicating weather the method should be called virtually.</param>
        private static void BuildInvokeMethod(TypeBuilder typeBuilder, MethodInfo methodInfo, Type[] genericParameterTypes, bool isVirtual)
        {
            var invokeMethodInfo = isVirtual ? MethodInfoBaseInvokeVirtualMethodInfo : MethodInfoBaseInvokeBaseMethodInfo;

            // Define method.
            var methodBuilder = typeBuilder.DefineMethod(invokeMethodInfo, false, true);

            methodBuilder.DefineParameters(invokeMethodInfo);

            // Implement method.
            var ilGenerator = methodBuilder.GetILGenerator();

            // Load target object.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Load parameter values.
            var parameterTypes = methodInfo.MapGenericParameterTypes(genericParameterTypes);
            var parameterValueLocalBuilders = new LocalBuilder[parameterTypes.Length];

            LoadParameterValues(ilGenerator, 2, parameterTypes, parameterValueLocalBuilders);

            // Call target method.
            var targetMethodInfo = methodInfo.MapGenericMethod(genericParameterTypes);

            ilGenerator.Emit(isVirtual ? OpCodes.Callvirt : OpCodes.Call, targetMethodInfo);

            // Restore by reference parameter values.
            RestoreByReferenceParameterValues(ilGenerator, 2, parameterTypes, parameterValueLocalBuilders);

            // Handle return value.
            var returnType = methodInfo.MapGenericReturnType(genericParameterTypes);

            if (returnType.IsVoid())
                ilGenerator.Emit(OpCodes.Ldnull);
            else
                ilGenerator.EmitBox(returnType);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Loads the parameter values onto the stack.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="argumentIndex">The argument index.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterValueLocalBuilders">The parameter value local builders.</param>
        private static void LoadParameterValues(ILGenerator ilGenerator, int argumentIndex, IList<Type> parameterTypes, IList<LocalBuilder> parameterValueLocalBuilders)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                ilGenerator.EmitLoadArgument(argumentIndex);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                if (parameterType.IsByRef)
                {
                    var elementType = parameterType.GetElementType();
                    var parameterLocalBuilder = ilGenerator.DeclareLocal(elementType);

                    ilGenerator.EmitUnbox(elementType);
                    ilGenerator.Emit(OpCodes.Stloc, parameterLocalBuilder);
                    ilGenerator.Emit(OpCodes.Ldloca, parameterLocalBuilder);

                    parameterValueLocalBuilders[index] = parameterLocalBuilder;
                }
                else
                {
                    ilGenerator.EmitUnbox(parameterType);
                }
            }
        }

        /// <summary>
        /// Restores the by reference parameter values.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="argumentIndex">The argument index.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterValueLocalBuilders">The parameter value local builders.</param>
        private static void RestoreByReferenceParameterValues(ILGenerator ilGenerator, int argumentIndex, IList<Type> parameterTypes, IList<LocalBuilder> parameterValueLocalBuilders)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                if (!parameterType.IsByRef)
                    continue;

                var parameterLocalBuilder = parameterValueLocalBuilders[index];
                var elementType = parameterType.GetElementType();

                ilGenerator.EmitLoadArgument(argumentIndex);
                ilGenerator.EmitLoadValue(index);
                ilGenerator.Emit(OpCodes.Ldloc, parameterLocalBuilder);
                ilGenerator.EmitBox(elementType);
                ilGenerator.Emit(OpCodes.Stelem_Ref);
            }
        }

        #region ITypeFactory Members

        /// <inheritdoc/>
        public Type CreateType(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            // Define type.
            var typeBuilder = _typeRepository.DefineType("MethodInfo", typeof (MethodInfoBase));

            // Define generic parameters.
            var genericParameterTypes = typeBuilder.DefineGenericParameters(methodInfo.GetGenericArguments());

            // Define method information static field.
            var methodFieldInfo = typeBuilder.DefineField(
                "Method",
                typeof (MethodInfo),
                FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly);

            // Build type initializer.
            BuildTypeInitializer(typeBuilder, methodInfo, genericParameterTypes, methodFieldInfo);

            // Build constructor.
            BuildConstructor(typeBuilder, methodFieldInfo);

            // Build base invoke method only for non abstract methods.
            if (!methodInfo.IsAbstract)
                BuildInvokeMethod(typeBuilder, methodInfo, genericParameterTypes, false);

            // Build virtual invoke method.
            BuildInvokeMethod(typeBuilder, methodInfo, genericParameterTypes, true);

            return typeBuilder.CreateType();
        }

        #endregion
    }
}