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
using System.Reflection;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides <see cref="Type"/> extension methods.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified type is a delegate.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A value indicating whether the specified type is a delegate.</returns>
        public static bool IsDelegate(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return typeof (Delegate).IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns a value indicating whether the specified type is void.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A value indicating whether the specified type is void.</returns>
        public static bool IsVoid(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return (type == typeof (void));
        }

        /// <summary>
        /// Returns the constructor information that reflects the constructor that matches the specified criterias.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The constructor information.</returns>
        public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingFlags, params Type[] parameterTypes)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (parameterTypes == null)
                throw new ArgumentNullException("parameterTypes");

            var constructorInfo = type.GetConstructor(bindingFlags, null, parameterTypes, null);

            if (constructorInfo == null)
                throw new MissingMethodException(String.Format(Resources.ConstructorOnTypeNotFound, type));

            return constructorInfo;
        }

        /// <summary>
        /// Returns the method that reflects the method that matches the specified criterias.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The method information.</returns>
        public static MethodInfo GetMethod(this Type type, string methodName, BindingFlags bindingFlags, params Type[] parameterTypes)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (methodName == null)
                throw new ArgumentNullException("methodName");

            if (parameterTypes == null)
                throw new ArgumentNullException("parameterTypes");

            var methodInfo = type.GetMethod(methodName, bindingFlags, null, parameterTypes, null);

            if (methodInfo == null)
                throw new MissingMethodException(String.Format(Resources.MethodOnTypeNotFound, methodName, type));

            return methodInfo;
        }

        /// <summary>
        /// Maps a type to the specified generic types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <returns>The mapped type.</returns>
        public static Type MapGenericType(this Type type, Type[] genericTypes)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (genericTypes == null)
                throw new ArgumentNullException("genericTypes");

            // Skip types without generic parameters.
            if (!type.IsGenericParameter && !type.ContainsGenericParameters)
                return type;

            // Handle generic parameter.
            if (type.IsGenericParameter)
                return genericTypes[type.GenericParameterPosition];

            // Handle generic array parameters.
            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var newElementType = elementType.MapGenericType(genericTypes);
                var rank = type.GetArrayRank();

                return (rank > 1) ? newElementType.MakeArrayType(rank) : newElementType.MakeArrayType();
            }

            // Handle generic by reference parameters.
            if (type.IsByRef)
            {
                var elementType = type.GetElementType();
                var newElementType = elementType.MapGenericType(genericTypes);

                return newElementType.MakeByRefType();
            }

            // Handle generic pointer parameters.
            if (type.IsPointer)
            {
                var elementType = type.GetElementType();
                var newElementType = elementType.MapGenericType(genericTypes);

                return newElementType.MakePointerType();
            }

            // Handle generic type parameters.
            var genericTypeDefinition = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
            var genericArguments = type.MapGenericArguments(genericTypes);

            return genericTypeDefinition.MakeGenericType(genericArguments);
        }

        /// <summary>
        /// Maps the generic arguments to the specified generic types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <returns>The mapped generic arguments.</returns>
        private static Type[] MapGenericArguments(this Type type, Type[] genericTypes)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var genericArguments = type.GetGenericArguments();

            return Array.ConvertAll(genericArguments, t => t.MapGenericType(genericTypes));
        }
    }
}