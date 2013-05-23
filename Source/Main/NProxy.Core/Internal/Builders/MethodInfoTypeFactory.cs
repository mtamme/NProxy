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
    internal sealed class MethodInfoTypeFactory
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
            typeof (object), typeof (MethodInfo), typeof (bool));

        /// <summary>
        /// The <see cref="MethodInfoBase.BaseInvoke(object,object[])"/> method information.
        /// </summary>
        private static readonly MethodInfo MethodInfoBaseBaseInvokeMethodInfo = typeof (MethodInfoBase).GetMethod(
            "BaseInvoke",
            BindingFlags.NonPublic | BindingFlags.Instance,
            typeof (object), typeof (object[]));

        /// <summary>
        /// The <see cref="MethodInfoBase.VirtualInvoke(object,object[])"/> method information.
        /// </summary>
        private static readonly MethodInfo MethodInfoBaseVirtualInvokeMethodInfo = typeof (MethodInfoBase).GetMethod(
            "VirtualInvoke",
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
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <param name="methodFieldInfo">The method information static field information.</param>
        private static void BuildTypeInitializer(TypeBuilder typeBuilder,
                                                 MethodInfo declaringMethodInfo,
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
            var methodInfo = declaringMethodInfo.MapGenericMethod(genericParameterTypes);
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
                new[] {typeof (object), typeof (bool)},
                new[] {"proxy", "isOverride"});

            // Implement constructor.
            var ilGenerator = constructorBuilder.GetILGenerator();

            // Load this reference.
            ilGenerator.Emit(OpCodes.Ldarg_0);

            // Load proxy object.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Load method information.
            ilGenerator.Emit(OpCodes.Ldsfld, methodFieldInfo);

            // Load override value.
            ilGenerator.Emit(OpCodes.Ldarg_2);

            // Call parent constructor.
            ilGenerator.Emit(OpCodes.Call, MethodInfoBaseConstructorInfo);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Builds the invoke method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        /// <param name="genericParameterTypes">The generic parameter types.</param>
        /// <param name="isVirtual">A value indicating weather the method should be called virtually.</param>
        private static void BuildInvokeMethod(TypeBuilder typeBuilder, MethodInfo declaringMethodInfo, Type[] genericParameterTypes, bool isVirtual)
        {
            var invokeMethodInfo = isVirtual ? MethodInfoBaseVirtualInvokeMethodInfo : MethodInfoBaseBaseInvokeMethodInfo;

            // Define method.
            var methodBuilder = typeBuilder.DefineMethod(invokeMethodInfo, false, true);

            methodBuilder.DefineParameters(invokeMethodInfo);

            // Implement method.
            var ilGenerator = methodBuilder.GetILGenerator();

            // Load target object.
            ilGenerator.Emit(OpCodes.Ldarg_1);

            // Load arguments.
            var parameterTypes = declaringMethodInfo.MapGenericParameterTypes(genericParameterTypes);
            var parameterLocalBuilders = new LocalBuilder[parameterTypes.Length];

            LoadArguments(ilGenerator, parameterTypes, parameterLocalBuilders);

            // Call target method.
            var methodInfo = declaringMethodInfo.MapGenericMethod(genericParameterTypes);

            if (isVirtual && methodInfo.IsVirtual)
                ilGenerator.Emit(OpCodes.Callvirt, methodInfo);
            else
                ilGenerator.Emit(OpCodes.Call, methodInfo);

            // Restore by reference arguments.
            RestoreByReferenceArguments(ilGenerator, parameterTypes, parameterLocalBuilders);

            // Handle return value.
            var returnType = declaringMethodInfo.MapGenericReturnType(genericParameterTypes);

            if (returnType.IsVoid())
                ilGenerator.Emit(OpCodes.Ldnull);
            else
                ilGenerator.EmitBox(returnType);

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Loads the arguments.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterLocalBuilders">The parameter local builders.</param>
        private static void LoadArguments(ILGenerator ilGenerator, IList<Type> parameterTypes, IList<LocalBuilder> parameterLocalBuilders)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                ilGenerator.Emit(OpCodes.Ldarg_2);
                ilGenerator.Emit(OpCodes.Ldc_I4, index);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);

                if (parameterType.IsByRef)
                {
                    var elementType = parameterType.GetElementType();
                    var parameterLocalBuilder = ilGenerator.DeclareLocal(elementType);

                    ilGenerator.EmitUnbox(elementType);
                    ilGenerator.Emit(OpCodes.Stloc, parameterLocalBuilder);
                    ilGenerator.Emit(OpCodes.Ldloca, parameterLocalBuilder);

                    parameterLocalBuilders[index] = parameterLocalBuilder;
                }
                else
                {
                    ilGenerator.EmitUnbox(parameterType);
                }
            }
        }

        /// <summary>
        /// Restores the by reference arguments.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterLocalBuilders">The parameter local builders.</param>
        private static void RestoreByReferenceArguments(ILGenerator ilGenerator, IList<Type> parameterTypes, IList<LocalBuilder> parameterLocalBuilders)
        {
            for (var index = 0; index < parameterTypes.Count; index++)
            {
                var parameterType = parameterTypes[index];

                if (!parameterType.IsByRef)
                    continue;

                var parameterLocalBuilder = parameterLocalBuilders[index];
                var elementType = parameterType.GetElementType();

                ilGenerator.Emit(OpCodes.Ldarg_2);
                ilGenerator.Emit(OpCodes.Ldc_I4, index);
                ilGenerator.Emit(OpCodes.Ldloc, parameterLocalBuilder);
                ilGenerator.EmitBox(elementType);
                ilGenerator.Emit(OpCodes.Stelem_Ref);
            }
        }

        /// <summary>
        /// Creates a method information type.
        /// </summary>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        /// <returns>The method information type.</returns>
        public Type CreateType(MethodInfo declaringMethodInfo)
        {
            if (declaringMethodInfo == null)
                throw new ArgumentNullException("declaringMethodInfo");

            // Define type.
            var typeBuilder = _typeRepository.DefineType("MethodInfo", typeof (MethodInfoBase));

            // Define generic parameters.
            var genericParameterTypes = typeBuilder.DefineGenericParameters(declaringMethodInfo.GetGenericArguments());

            // Define method information static field.
            var methodFieldInfo = typeBuilder.DefineField(
                "MethodInfo",
                typeof (MethodInfo),
                FieldAttributes.Private | FieldAttributes.Static | FieldAttributes.InitOnly);

            // Build type initializer.
            BuildTypeInitializer(typeBuilder, declaringMethodInfo, genericParameterTypes, methodFieldInfo);

            // Build constructor.
            BuildConstructor(typeBuilder, methodFieldInfo);

            // Build base invoke method only for non abstract methods.
            if (!declaringMethodInfo.IsAbstract)
                BuildInvokeMethod(typeBuilder, declaringMethodInfo, genericParameterTypes, false);

            // Build virtual invoke method.
            BuildInvokeMethod(typeBuilder, declaringMethodInfo, genericParameterTypes, true);

            return typeBuilder.CreateType();
        }
    }
}