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
using System.Reflection;
using System.Reflection.Emit;

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Provides <see cref="TypeBuilder"/> extension methods.
    /// </summary>
    internal static class TypeBuilderExtensions
    {
        /// <summary>
        /// Defines the generic type parameters.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <returns>The generic parameter types.</returns>
        /// <remarks>
        /// Custom attributes are not considered by this method.
        /// </remarks>
        public static Type[] DefineGenericParameters(this TypeBuilder typeBuilder, Type[] genericTypes)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (genericTypes == null)
                throw new ArgumentNullException("genericTypes");

            if (genericTypes.Length == 0)
                return Type.EmptyTypes;

            var genericParameterNames = Array.ConvertAll(genericTypes, t => t.Name);
            var genericParameterBuilders = typeBuilder.DefineGenericParameters(genericParameterNames);

            foreach (var genericParameterBuilder in genericParameterBuilders)
            {
                var genericType = genericTypes[genericParameterBuilder.GenericParameterPosition];

                // Set generic parameter attributes.
                genericParameterBuilder.SetGenericParameterAttributes(genericType.GenericParameterAttributes);

                // Set generic parameter constraints.
                var constraints = genericType.GetGenericParameterConstraints();

                if (constraints.Length == 0)
                    continue;

                var interfaceConstraints = new List<Type>();

                foreach (var constraint in constraints)
                {
                    if (constraint.IsInterface)
                        interfaceConstraints.Add(constraint);
                    else
                        genericParameterBuilder.SetBaseTypeConstraint(constraint);
                }

                genericParameterBuilder.SetInterfaceConstraints(interfaceConstraints.ToArray());
            }

            return Array.ConvertAll(genericParameterBuilders, b => (Type) b);
        }

        /// <summary>
        /// Defines a constructor.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodAttributes">The method attributes.</param>
        /// <param name="callingConvention">The calling convention.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="parameterNames">The parameter names.</param>
        /// <returns>The constructor builder.</returns>
        public static ConstructorBuilder DefineConstructor(this TypeBuilder typeBuilder,
            MethodAttributes methodAttributes,
            CallingConventions callingConvention,
            Type[] parameterTypes,
            string[] parameterNames)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (parameterTypes == null)
                throw new ArgumentNullException("parameterTypes");

            if (parameterNames == null)
                throw new ArgumentNullException("parameterNames");

            if (parameterTypes.Length != parameterNames.Length)
                throw new ArgumentException(Resources.NumberOfParameterTypesAndNamesMustBeEqual);

            // Define constructor.
            var constructorBuilder = typeBuilder.DefineConstructor(
                methodAttributes,
                callingConvention,
                parameterTypes);

            // Define constructor parameters.
            constructorBuilder.DefineParameters(parameterNames);

            return constructorBuilder;
        }

        /// <summary>
        /// Defines the constructor parameters.
        /// </summary>
        /// <param name="constructorBuilder">The constructor builder.</param>
        /// <param name="parameterNames">The parameter names.</param>
        private static void DefineParameters(this ConstructorBuilder constructorBuilder, IEnumerable<string> parameterNames)
        {
            var position = 1;

            // Define additional constructor parameters.
            foreach (var parameterName in parameterNames)
            {
                constructorBuilder.DefineParameter(position++, ParameterAttributes.None, parameterName);
            }
        }

        /// <summary>
        /// Defines a constructor based on the specified constructor.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <param name="additionalParameterTypes">The additional parameter types.</param>
        /// <param name="additionalParameterNames">The additional parameter names.</param>
        /// <returns>The constructor builder.</returns>
        public static ConstructorBuilder DefineConstructor(this TypeBuilder typeBuilder,
            ConstructorInfo constructorInfo,
            IEnumerable<Type> additionalParameterTypes,
            IEnumerable<string> additionalParameterNames)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            if (additionalParameterTypes == null)
                throw new ArgumentNullException("additionalParameterTypes");

            if (additionalParameterNames == null)
                throw new ArgumentNullException("additionalParameterNames");

            var methodAttributes = MethodAttributes.Public |
                                   constructorInfo.Attributes & (MethodAttributes.HideBySig |
                                                                 MethodAttributes.SpecialName |
                                                                 MethodAttributes.ReservedMask);
            var parameterTypes = new List<Type>();

            parameterTypes.AddRange(additionalParameterTypes);

            var parentParameterTypes = constructorInfo.GetParameterTypes();

            parameterTypes.AddRange(parentParameterTypes);

            // Define constructor.
            var constructorBuilder = typeBuilder.DefineConstructor(
                methodAttributes,
                constructorInfo.CallingConvention,
                parameterTypes.ToArray());

            // Define constructor parameters.
            constructorBuilder.DefineParameters(constructorInfo, additionalParameterNames);

            return constructorBuilder;
        }

        /// <summary>
        /// Defines the constructor parameters based on the specified constructor.
        /// </summary>
        /// <param name="constructorBuilder">The constructor builder.</param>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <param name="additionalParameterNames">The additional parameter names.</param>
        private static void DefineParameters(this ConstructorBuilder constructorBuilder,
            ConstructorInfo constructorInfo,
            IEnumerable<string> additionalParameterNames)
        {
            var position = 1;

            // Define additional constructor parameters.
            foreach (var additionalParameterName in additionalParameterNames)
            {
                constructorBuilder.DefineParameter(position++, ParameterAttributes.None, additionalParameterName);
            }

            // Define base constructor parameters.
            var parameterInfos = constructorInfo.GetParameters();

            foreach (var parameterInfo in parameterInfos)
            {
                constructorBuilder.DefineParameter(parameterInfo.Position + position, parameterInfo.Attributes, parameterInfo.Name);
            }
        }

        /// <summary>
        /// Defines an event based on the specified event.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="isExplicit">A value indicating whether the specified event should be implemented explicitly.</param>
        /// <param name="methodBuilderFactory">The method builder factory function.</param>
        /// <returns>The event builder.</returns>
        public static void DefineEvent(this TypeBuilder typeBuilder,
            EventInfo eventInfo,
            bool isExplicit,
            Func<MethodInfo, bool, MethodBuilder> methodBuilderFactory)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            if (methodBuilderFactory == null)
                throw new ArgumentNullException("methodBuilderFactory");

            // Define event.
            var eventName = isExplicit ? eventInfo.GetFullName() : eventInfo.Name;

            var eventBuilder = typeBuilder.DefineEvent(
                eventName,
                eventInfo.Attributes,
                eventInfo.EventHandlerType);

            // Build event add method.
            var addMethodInfo = eventInfo.GetAddMethod();
            var addMethodBuilder = methodBuilderFactory(addMethodInfo, isExplicit);

            eventBuilder.SetAddOnMethod(addMethodBuilder);

            // Build event remove method.
            var removeMethodInfo = eventInfo.GetRemoveMethod(true);
            var removeMethodBuilder = methodBuilderFactory(removeMethodInfo, isExplicit);

            eventBuilder.SetRemoveOnMethod(removeMethodBuilder);

            // Build event raise method.
            var raiseMethodInfo = eventInfo.GetRaiseMethod(true);

            if (raiseMethodInfo != null)
            {
                var methodBuilder = methodBuilderFactory(raiseMethodInfo, isExplicit);

                eventBuilder.SetRaiseMethod(methodBuilder);
            }
        }

        /// <summary>
        /// Defines a property based on the specified property.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="isExplicit">A value indicating whether the specified property should be implemented explicitly.</param>
        /// <param name="methodBuilderFactory">The method builder factory function.</param>
        /// <returns>The property builder.</returns>
        public static void DefineProperty(this TypeBuilder typeBuilder,
            PropertyInfo propertyInfo,
            bool isExplicit,
            Func<MethodInfo, bool, MethodBuilder> methodBuilderFactory)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            if (methodBuilderFactory == null)
                throw new ArgumentNullException("methodBuilderFactory");

            // Define property.
            var propertyName = isExplicit ? propertyInfo.GetFullName() : propertyInfo.Name;
            var parameterTypes = propertyInfo.GetIndexParameterTypes();

            var propertyBuilder = typeBuilder.DefineProperty(
                propertyName,
                propertyInfo.Attributes,
                CallingConventions.HasThis,
                propertyInfo.PropertyType,
                null,
                null,
                parameterTypes,
                null,
                null);

            // Build property get method.
            var getMethodInfo = propertyInfo.GetGetMethod(true);

            if (getMethodInfo != null)
            {
                var methodBuilder = methodBuilderFactory(getMethodInfo, isExplicit);

                propertyBuilder.SetGetMethod(methodBuilder);
            }

            // Build property set method.
            var setMethodInfo = propertyInfo.GetSetMethod(true);

            if (setMethodInfo != null)
            {
                var methodBuilder = methodBuilderFactory(setMethodInfo, isExplicit);

                propertyBuilder.SetSetMethod(methodBuilder);
            }
        }

        /// <summary>
        /// Defines a method based on the specified method.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="isExplicit">A value indicating whether the specified method should be implemented explicitly.</param>
        /// <param name="isOverride">A value indicating whether the specified method should be overridden.</param>
        /// <returns>The method builder.</returns>
        public static MethodBuilder DefineMethod(this TypeBuilder typeBuilder,
            MethodInfo methodInfo,
            bool isExplicit,
            bool isOverride)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            // Define method attributes.
            var methodAttributes = methodInfo.Attributes & (MethodAttributes.HideBySig |
                                                            MethodAttributes.SpecialName |
                                                            MethodAttributes.ReservedMask);

            if (isExplicit)
                methodAttributes |= MethodAttributes.Private;
            else
                methodAttributes |= methodInfo.Attributes & MethodAttributes.MemberAccessMask;

            if (isOverride)
            {
                methodAttributes |= MethodAttributes.Virtual;

                var declaringType = methodInfo.DeclaringType;

                if (declaringType.IsInterface)
                    methodAttributes |= MethodAttributes.NewSlot;
                else
                    methodAttributes |= MethodAttributes.ReuseSlot;
            }

            var methodName = isExplicit ? methodInfo.GetFullName() : methodInfo.Name;

            // Define method.
            return typeBuilder.DefineMethod(
                methodName,
                methodAttributes,
                methodInfo.CallingConvention);
        }

        /// <summary>
        /// Sets the specified custom attribute.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <param name="arguments">The constructor arguments.</param>
        public static void SetCustomAttribute(this TypeBuilder typeBuilder, ConstructorInfo constructorInfo, object[] arguments)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, arguments);

            typeBuilder.SetCustomAttribute(customAttributeBuilder);
        }
    }
}