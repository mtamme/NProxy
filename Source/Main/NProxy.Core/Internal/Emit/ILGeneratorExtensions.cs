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
using System.Reflection;
using System.Reflection.Emit;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal.Emit
{
    /// <summary>
    /// Provides <see cref="ILGenerator"/> extension methods.
    /// </summary>
    internal static class ILGeneratorExtensions
    {
        /// <summary>
        /// Calls the specified method.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="methodInfo">The method information.</param>
        public static void EmitCall(this ILGenerator ilGenerator, MethodInfo methodInfo)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            ilGenerator.Emit(methodInfo.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, methodInfo);
        }

        /// <summary>
        /// Loads the arguments onto the stack.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="offset">The argument offset.</param>
        /// <param name="count">The argument count.</param>
        public static void EmitLoadArguments(this ILGenerator ilGenerator, int offset, int count)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            var end = offset + count;

            for (var index = offset; index < end; index++)
            {
                ilGenerator.EmitLoadArgument(index);
            }
        }

        /// <summary>
        /// Loads an argument onto the stack.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="index">The argument index.</param>
        public static void EmitLoadArgument(this ILGenerator ilGenerator, int index)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            switch (index)
            {
                case 0:
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    break;
                case 1:
                    ilGenerator.Emit(OpCodes.Ldarg_1);
                    break;
                case 2:
                    ilGenerator.Emit(OpCodes.Ldarg_2);
                    break;
                case 3:
                    ilGenerator.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    ilGenerator.Emit(OpCodes.Ldarg, index);
                    break;
            }
        }

        /// <summary>
        /// Loads a value onto the stack.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="value">The value.</param>
        public static void EmitLoadValue(this ILGenerator ilGenerator, int value)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            switch (value)
            {
                case 0:
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    break;
                case 1:
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2:
                    ilGenerator.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3:
                    ilGenerator.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4:
                    ilGenerator.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5:
                    ilGenerator.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6:
                    ilGenerator.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7:
                    ilGenerator.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8:
                    ilGenerator.Emit(OpCodes.Ldc_I4_8);
                    break;
                default:
                    ilGenerator.Emit(OpCodes.Ldc_I4, value);
                    break;
            }
        }

        /// <summary>
        /// Throws a new exception of the specified exception type.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="exceptionType">The exception type.</param>
        /// <param name="message">The error message.</param>
        public static void ThrowException(this ILGenerator ilGenerator, Type exceptionType, string message)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            if (exceptionType == null)
                throw new ArgumentNullException("exceptionType");

            var constructorInfo = exceptionType.GetConstructor(BindingFlags.Public | BindingFlags.Instance,
                                                               typeof (string));

            ilGenerator.Emit(OpCodes.Ldstr, message);
            ilGenerator.Emit(OpCodes.Newobj, constructorInfo);
            ilGenerator.Emit(OpCodes.Throw);
        }

        /// <summary>
        /// Creates a new array of the specified element type.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="elementType">The element type.</param>
        /// <param name="size">The array size.</param>
        /// <returns>The new array.</returns>
        public static LocalBuilder NewArray(this ILGenerator ilGenerator, Type elementType, int size)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            if (elementType == null)
                throw new ArgumentNullException("elementType");

            var localBuilder = ilGenerator.DeclareLocal(elementType.MakeArrayType());

            ilGenerator.EmitLoadValue(size);
            ilGenerator.Emit(OpCodes.Newarr, elementType);
            ilGenerator.Emit(OpCodes.Stloc, localBuilder);

            return localBuilder;
        }

        /// <summary>
        /// Converts a value type to an object reference.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="type">The type.</param>
        public static void EmitBox(this ILGenerator ilGenerator, Type type)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsValueType || type.IsGenericParameter)
                ilGenerator.Emit(OpCodes.Box, type);
        }

        /// <summary>
        /// Converts the boxed representation of a specified type to its unboxed form.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="type">The type.</param>
        public static void EmitUnbox(this ILGenerator ilGenerator, Type type)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsValueType || type.IsGenericParameter)
                ilGenerator.Emit(OpCodes.Unbox_Any, type);
        }

        /// <summary>
        /// Loads a value from an address.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="type">The type.</param>
        public static void EmitLoadIndirect(this ILGenerator ilGenerator, Type type)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            if (type == null)
                throw new ArgumentNullException("type");

            if (!type.IsByRef)
                return;

            var elementType = type.GetElementType();

            if (elementType.IsValueType || elementType.IsGenericParameter)
                ilGenerator.Emit(OpCodes.Ldobj, elementType);
            else
                ilGenerator.Emit(OpCodes.Ldind_Ref);
        }

        /// <summary>
        /// Stores a value to an address.
        /// </summary>
        /// <param name="ilGenerator">The intermediate language generator.</param>
        /// <param name="type">The type.</param>
        public static void EmitStoreIndirect(this ILGenerator ilGenerator, Type type)
        {
            if (ilGenerator == null)
                throw new ArgumentNullException("ilGenerator");

            if (type == null)
                throw new ArgumentNullException("type");

            if (!type.IsByRef)
                return;

            var elementType = type.GetElementType();

            if (elementType.IsValueType || elementType.IsGenericParameter)
                ilGenerator.Emit(OpCodes.Stobj, elementType);
            else
                ilGenerator.Emit(OpCodes.Stind_Ref);
        }
    }
}