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

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides intermediate language generator extensions.
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

            for (var position = offset; position < end; position++)
            {
                ilGenerator.Emit(OpCodes.Ldarg, position);
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

            var constructorInfo = exceptionType.GetConstructor(new[] {typeof (string)});

            if (constructorInfo == null)
                throw new MissingMethodException(String.Format("Constructor on exception type '{0}' not found.", exceptionType.FullName));

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

            ilGenerator.Emit(OpCodes.Ldc_I4, size);
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
