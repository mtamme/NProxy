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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides <see cref="MethodBuilder"/> extension methods.
    /// </summary>
    internal static class MethodBuilderExtensions
    {
        /// <summary>
        /// Defines the method parameters.
        /// </summary>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        public static void DefineParameters(this MethodBuilder methodBuilder, MethodInfo declaringMethodInfo)
        {
            if (methodBuilder == null)
                throw new ArgumentNullException("methodBuilder");

            var genericParameterTypes = methodBuilder.DefineGenericParameters(declaringMethodInfo);

            methodBuilder.DefineParameters(declaringMethodInfo, genericParameterTypes);
        }

        /// <summary>
        /// Defines the method parameters.
        /// </summary>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        /// <param name="genericTypes">The generic types.</param>
        public static void DefineParameters(this MethodBuilder methodBuilder, MethodInfo declaringMethodInfo, Type[] genericTypes)
        {
            if (methodBuilder == null)
                throw new ArgumentNullException("methodBuilder");

            if (declaringMethodInfo == null)
                throw new ArgumentNullException("declaringMethodInfo");

            // Set parameters and return type.
            var parameterTypes = declaringMethodInfo.MapGenericParameterTypes(genericTypes);
            var returnType = declaringMethodInfo.MapGenericReturnType(genericTypes);

            methodBuilder.SetReturnType(returnType);
            methodBuilder.SetParameters(parameterTypes);

            // Define method parameters. No need to define parameter 0 for return type.
            var parameterInfos = declaringMethodInfo.GetParameters();

            foreach (var parameterInfo in parameterInfos)
            {
                methodBuilder.DefineParameter(parameterInfo.Position + 1, parameterInfo.Attributes, parameterInfo.Name);
            }
        }

        /// <summary>
        /// Defines the generic method parameters.
        /// </summary>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="declaringMethodBase">The declaring method base.</param>
        /// <returns>The generic parameter types.</returns>
        /// <remarks>
        /// Custom attributes are not considered by this method.
        /// </remarks>
        public static Type[] DefineGenericParameters(this MethodBuilder methodBuilder, MethodBase declaringMethodBase)
        {
            if (methodBuilder == null)
                throw new ArgumentNullException("methodBuilder");

            if (declaringMethodBase == null)
                throw new ArgumentNullException("declaringMethodBase");

            if (!declaringMethodBase.IsGenericMethodDefinition)
                return Type.EmptyTypes;

            var genericParameterTypes = declaringMethodBase.GetGenericArguments();
            var genericParameterNames = Array.ConvertAll(genericParameterTypes, t => t.Name);
            var genericParameterBuilders = methodBuilder.DefineGenericParameters(genericParameterNames);

            foreach (var genericParameterBuilder in genericParameterBuilders)
            {
                var genericParameterType = genericParameterTypes[genericParameterBuilder.GenericParameterPosition];

                // Set generic parameter attributes.
                genericParameterBuilder.SetGenericParameterAttributes(genericParameterType.GenericParameterAttributes);

                // Set generic parameter constraints.
                var genericParameterConstraints = genericParameterType.GetGenericParameterConstraints();
                var baseTypeConstraint = genericParameterConstraints.FirstOrDefault(t => t.IsClass);

                if (baseTypeConstraint != null)
                    genericParameterBuilder.SetBaseTypeConstraint(baseTypeConstraint);

                var interfaceConstraints = genericParameterConstraints.Where(t => t.IsInterface).ToArray();

                genericParameterBuilder.SetInterfaceConstraints(interfaceConstraints);
            }

            return Array.ConvertAll(genericParameterBuilders, b => (Type) b);
        }
    }
}