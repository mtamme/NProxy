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
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Provides <see cref="MethodBuilder"/> extension methods.
    /// </summary>
    internal static class MethodBuilderExtensions
    {
        /// <summary>
        /// Defines the method parameters based on the specified method.
        /// </summary>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="methodInfo">The method information.</param>
        public static void DefineParameters(this MethodBuilder methodBuilder, MethodInfo methodInfo)
        {
            if (methodBuilder == null)
                throw new ArgumentNullException("methodBuilder");

            var genericParameterTypes = methodBuilder.DefineGenericParameters(methodInfo);

            methodBuilder.DefineParameters(methodInfo, genericParameterTypes);
        }

        /// <summary>
        /// Defines the method parameters based on the specified method.
        /// </summary>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="genericTypes">The generic types.</param>
        public static void DefineParameters(this MethodBuilder methodBuilder, MethodInfo methodInfo, Type[] genericTypes)
        {
            if (methodBuilder == null)
                throw new ArgumentNullException("methodBuilder");

            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            // Set parameters and return type.
            var parameterTypes = methodInfo.MapGenericParameterTypes(genericTypes);
            var returnType = methodInfo.MapGenericReturnType(genericTypes);

            methodBuilder.SetReturnType(returnType);
            methodBuilder.SetParameters(parameterTypes);

            // Define method parameters. No need to define parameter 0 for return type.
            var parameterInfos = methodInfo.GetParameters();

            foreach (var parameterInfo in parameterInfos)
            {
                methodBuilder.DefineParameter(parameterInfo.Position + 1, parameterInfo.Attributes, parameterInfo.Name);
            }
        }

        /// <summary>
        /// Defines the generic method parameters based on the specified method.
        /// </summary>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="methodBase">The method base.</param>
        /// <returns>The generic parameter types.</returns>
        /// <remarks>
        /// Custom attributes are not considered by this method.
        /// </remarks>
        public static Type[] DefineGenericParameters(this MethodBuilder methodBuilder, MethodBase methodBase)
        {
            if (methodBuilder == null)
                throw new ArgumentNullException("methodBuilder");

            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            if (!methodBase.IsGenericMethodDefinition)
                return Type.EmptyTypes;

            var genericParameterTypes = methodBase.GetGenericArguments();
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