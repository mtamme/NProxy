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
using NUnit.Framework;

namespace NProxy.Core.Test
{
    /// <summary>
    /// Represents member verification methods.
    /// </summary>
    internal static class MemberAssert
    {
        /// <summary>
        /// Verifies that two constructors are equivalent.
        /// </summary>
        /// <param name="actual">The actual constructor information.</param>
        /// <param name="expected">The expected constructor information.</param>
        public static void AreEquivalent(ConstructorInfo actual, ConstructorInfo expected)
        {
            // Check member properties.
            AreMembersEquivalent(actual, expected);

            // Check method attributes.
            Assert.That(actual.Attributes & MethodAttributes.ReservedMask, Is.EqualTo(expected.Attributes & MethodAttributes.ReservedMask));
            Assert.That(actual.Attributes & MethodAttributes.SpecialName, Is.EqualTo(expected.Attributes & MethodAttributes.SpecialName));
            Assert.That(actual.Attributes & MethodAttributes.VtableLayoutMask, Is.EqualTo(expected.Attributes & MethodAttributes.VtableLayoutMask));

            // Check calling convention.
            Assert.That(actual.CallingConvention, Is.EqualTo(expected.CallingConvention));

            // Check generic parameters.
            Assert.That(actual.ContainsGenericParameters, Is.EqualTo(expected.ContainsGenericParameters));

            if (actual.ContainsGenericParameters)
                AreTypesEquivalent(actual.GetGenericArguments(), expected.GetGenericArguments());
        }

        /// <summary>
        /// Verifies that two events are equivalent.
        /// </summary>
        /// <param name="actual">The actual event information.</param>
        /// <param name="expected">The expected event information.</param>
        /// <param name="isExplicit">A value indicating whether the actual event is implemented explicitly.</param>
        public static void AreEquivalent(EventInfo actual, EventInfo expected, bool isExplicit)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(expected, Is.Not.Null);

            // Check member properties.
            AreMembersEquivalent(actual, expected);

            // Check event properties.
            Assert.That(actual.Attributes, Is.EqualTo(expected.Attributes));
            Assert.That(actual.IsMulticast, Is.EqualTo(expected.IsMulticast));
            Assert.That(actual.IsSpecialName, Is.EqualTo(expected.IsSpecialName));

            // Check event handler type.
            AreTypesEquivalent(actual.EventHandlerType, expected.EventHandlerType);

            // Check event methods.
            InternalAreEquivalent(actual.GetAddMethod(), expected.GetAddMethod(), isExplicit);
            InternalAreEquivalent(actual.GetRemoveMethod(), expected.GetRemoveMethod(), isExplicit);
            InternalAreEquivalent(actual.GetRaiseMethod(), expected.GetRaiseMethod(), isExplicit);
        }

        /// <summary>
        /// Verifies that two properties are equivalent.
        /// </summary>
        /// <param name="actual">The actual property information.</param>
        /// <param name="expected">The expected property information.</param>
        /// <param name="isExplicit">A value indicating whether the actual property is implemented explicitly.</param>
        public static void AreEquivalent(PropertyInfo actual, PropertyInfo expected, bool isExplicit)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(expected, Is.Not.Null);

            // Check member properties.
            AreMembersEquivalent(actual, expected);

            // Check property properties.
            Assert.That(actual.Attributes, Is.EqualTo(expected.Attributes));
            Assert.That(actual.CanRead, Is.EqualTo(expected.CanRead));
            Assert.That(actual.CanWrite, Is.EqualTo(expected.CanWrite));
            Assert.That(actual.IsSpecialName, Is.EqualTo(expected.IsSpecialName));

            // Check property type.
            AreTypesEquivalent(actual.PropertyType, expected.PropertyType);

            // Check index parameters.
            AreParametersEquivalent(actual.GetIndexParameters(), expected.GetIndexParameters());

            // Check property methods.
            InternalAreEquivalent(actual.GetGetMethod(), expected.GetGetMethod(), isExplicit);
            InternalAreEquivalent(actual.GetSetMethod(), expected.GetSetMethod(), isExplicit);
        }

        /// <summary>
        /// Verifies that two methods are equivalent.
        /// </summary>
        /// <param name="actual">The actual method information.</param>
        /// <param name="expected">The expected method information.</param>
        /// <param name="isExplicit">A value indicating whether the actual method is implemented explicitly.</param>
        private static void InternalAreEquivalent(MethodInfo actual, MethodInfo expected, bool isExplicit)
        {
            if ((actual == null) || (expected == null))
                return;

            AreEquivalent(actual, expected, isExplicit);
        }

        /// <summary>
        /// Verifies that two methods are equivalent.
        /// </summary>
        /// <param name="actual">The actual method information.</param>
        /// <param name="expected">The expected method information.</param>
        /// <param name="isExplicit">A value indicating whether the actual method is implemented explicitly.</param>
        public static void AreEquivalent(MethodInfo actual, MethodInfo expected, bool isExplicit)
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(expected, Is.Not.Null);

            // Check member properties.
            AreMembersEquivalent(actual, expected);

            // Check method attributes.
            var methodAttributes = isExplicit ? MethodAttributes.Private : expected.Attributes & MethodAttributes.MemberAccessMask;

            Assert.That(actual.Attributes & MethodAttributes.MemberAccessMask, Is.EqualTo(methodAttributes));
            Assert.That(actual.Attributes & MethodAttributes.ReservedMask, Is.EqualTo(expected.Attributes & MethodAttributes.ReservedMask));
            Assert.That(actual.Attributes & MethodAttributes.SpecialName, Is.EqualTo(expected.Attributes & MethodAttributes.SpecialName));

            // Check calling convention.
            Assert.That(actual.CallingConvention, Is.EqualTo(expected.CallingConvention));

            // Check generic parameters.
            Assert.That(actual.ContainsGenericParameters, Is.EqualTo(expected.ContainsGenericParameters));

            if (actual.ContainsGenericParameters)
                AreTypesEquivalent(actual.GetGenericArguments(), expected.GetGenericArguments());

            // Check parameters.
            AreParametersEquivalent(actual.GetParameters(), expected.GetParameters());
            AreParametersEquivalent(actual.ReturnParameter, expected.ReturnParameter);
        }

        /// <summary>
        /// Verifies that two members are equivalent.
        /// </summary>
        /// <param name="actual">The actual member information.</param>
        /// <param name="expected">The expected member information.</param>
        private static void AreMembersEquivalent(MemberInfo actual, MemberInfo expected)
        {
            // Check member name.
            Assert.That(actual.Name, Does.EndWith(expected.Name));

            // Check member type.
            Assert.That(actual.MemberType, Is.EqualTo(expected.MemberType));
        }

        /// <summary>
        /// Verifies that two lists of parameters are equivalent.
        /// </summary>
        /// <param name="actual">The actual list of parameter informations.</param>
        /// <param name="expected">The expected list of parameter informations.</param>
        private static void AreParametersEquivalent(IList<ParameterInfo> actual, IList<ParameterInfo> expected)
        {
            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (var index = 0; index < actual.Count; index++)
            {
                AreParametersEquivalent(actual[index], expected[index]);
            }
        }

        /// <summary>
        /// Verifies that two parameters are equivalent.
        /// </summary>
        /// <param name="actual">The actual parameter information.</param>
        /// <param name="expected">The expected parameter information.</param>
        private static void AreParametersEquivalent(ParameterInfo actual, ParameterInfo expected)
        {
            // Check parameter properties.
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.IsIn, Is.EqualTo(expected.IsIn));
            Assert.That(actual.IsLcid, Is.EqualTo(expected.IsLcid));
            Assert.That(actual.IsOptional, Is.EqualTo(expected.IsOptional));
            Assert.That(actual.IsOut, Is.EqualTo(expected.IsOut));
            Assert.That(actual.IsRetval, Is.EqualTo(expected.IsRetval));
            Assert.That(actual.Position, Is.EqualTo(expected.Position));

            // Check parameter attributes.
            Assert.That(actual.Attributes, Is.EqualTo(expected.Attributes));

            // Check parameter type.
            AreTypesEquivalent(actual.ParameterType, expected.ParameterType);

            // Check default value.
            Assert.That(actual.DefaultValue, Is.EqualTo(expected.DefaultValue));
            Assert.That(actual.RawDefaultValue, Is.EqualTo(expected.RawDefaultValue));

            // Check custom modifiers.
            Assert.That(actual.GetOptionalCustomModifiers(), Is.EqualTo(expected.GetOptionalCustomModifiers()));
            Assert.That(actual.GetRequiredCustomModifiers(), Is.EqualTo(expected.GetRequiredCustomModifiers()));
        }

        /// <summary>
        /// Verifies that two lists of types are equivalent.
        /// </summary>
        /// <param name="actual">The actual list of types.</param>
        /// <param name="expected">The expected list of types.</param>
        private static void AreTypesEquivalent(IList<Type> actual, IList<Type> expected)
        {
            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (var index = 0; index < actual.Count; index++)
            {
                AreTypesEquivalent(actual[index], expected[index]);
            }
        }

        /// <summary>
        /// Verifies that two types are equivalent.
        /// </summary>
        /// <param name="actual">The actual type.</param>
        /// <param name="expected">The expected type.</param>
        private static void AreTypesEquivalent(Type actual, Type expected)
        {
            // Check type properties.
            Assert.That(actual.IsAbstract, Is.EqualTo(expected.IsAbstract));
            Assert.That(actual.IsAnsiClass, Is.EqualTo(expected.IsAnsiClass));
            Assert.That(actual.IsArray, Is.EqualTo(expected.IsArray));
            Assert.That(actual.IsAutoClass, Is.EqualTo(expected.IsAutoClass));
            Assert.That(actual.IsAutoLayout, Is.EqualTo(expected.IsAutoLayout));
            Assert.That(actual.IsByRef, Is.EqualTo(expected.IsByRef));
            Assert.That(actual.IsClass, Is.EqualTo(expected.IsClass));
            Assert.That(actual.IsCOMObject, Is.EqualTo(expected.IsCOMObject));
            Assert.That(actual.IsContextful, Is.EqualTo(expected.IsContextful));
            Assert.That(actual.IsEnum, Is.EqualTo(expected.IsEnum));
            Assert.That(actual.IsExplicitLayout, Is.EqualTo(expected.IsExplicitLayout));
            Assert.That(actual.IsInterface, Is.EqualTo(expected.IsInterface));
            Assert.That(actual.IsLayoutSequential, Is.EqualTo(expected.IsLayoutSequential));
            Assert.That(actual.IsMarshalByRef, Is.EqualTo(expected.IsMarshalByRef));
            Assert.That(actual.IsNested, Is.EqualTo(expected.IsNested));
            Assert.That(actual.IsNestedAssembly, Is.EqualTo(expected.IsNestedAssembly));
            Assert.That(actual.IsNestedFamANDAssem, Is.EqualTo(expected.IsNestedFamANDAssem));
            Assert.That(actual.IsNestedFamily, Is.EqualTo(expected.IsNestedFamily));
            Assert.That(actual.IsNestedFamORAssem, Is.EqualTo(expected.IsNestedFamORAssem));
            Assert.That(actual.IsNestedPrivate, Is.EqualTo(expected.IsNestedPrivate));
            Assert.That(actual.IsNestedPublic, Is.EqualTo(expected.IsNestedPublic));
            Assert.That(actual.IsNotPublic, Is.EqualTo(expected.IsNotPublic));
            Assert.That(actual.IsPointer, Is.EqualTo(expected.IsPointer));
            Assert.That(actual.IsPrimitive, Is.EqualTo(expected.IsPrimitive));
            Assert.That(actual.IsPublic, Is.EqualTo(expected.IsPublic));
            Assert.That(actual.IsSealed, Is.EqualTo(expected.IsSealed));
            Assert.That(actual.IsSecuritySafeCritical, Is.EqualTo(expected.IsSecuritySafeCritical));
            #pragma warning disable SYSLIB0050
            Assert.That(actual.IsSerializable, Is.EqualTo(expected.IsSerializable));
            #pragma warning restore SYSLIB0050
            Assert.That(actual.IsSpecialName, Is.EqualTo(expected.IsSpecialName));
            Assert.That(actual.IsUnicodeClass, Is.EqualTo(expected.IsUnicodeClass));
            Assert.That(actual.IsValueType, Is.EqualTo(expected.IsValueType));
            Assert.That(actual.IsVisible, Is.EqualTo(expected.IsVisible));
            Assert.That(actual.MemberType, Is.EqualTo(expected.MemberType));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.StructLayoutAttribute, Is.EqualTo(expected.StructLayoutAttribute));
            Assert.That(actual.IsGenericType, Is.EqualTo(expected.IsGenericType));

            // Ignore .NET Framework 4 properties.
            //Assert.That(actual.IsSecurityCritical, Is.EqualTo(expected.IsSecurityCritical));
            //Assert.That(actual.IsSecurityTransparent, Is.EqualTo(expected.IsSecurityTransparent));

            // Check type attributes.
            Assert.That(actual.Attributes, Is.EqualTo(expected.Attributes));

            // Check element type.
            Assert.That(actual.HasElementType, Is.EqualTo(expected.HasElementType));

            if (actual.HasElementType)
                AreTypesEquivalent(actual.GetElementType(), expected.GetElementType());

            // Check generic parameters.
            Assert.That(actual.ContainsGenericParameters, Is.EqualTo(expected.ContainsGenericParameters));

            if (actual.ContainsGenericParameters)
                AreTypesEquivalent(actual.GetGenericArguments(), expected.GetGenericArguments());

            // Check generic parameter.
            Assert.That(actual.IsGenericParameter, Is.EqualTo(expected.IsGenericParameter));

            if (actual.IsGenericParameter)
            {
                Assert.That(actual.GenericParameterAttributes, Is.EqualTo(expected.GenericParameterAttributes));
                AreTypesEquivalent(actual.GetGenericParameterConstraints(), expected.GetGenericParameterConstraints());
                Assert.That(actual.GenericParameterPosition, Is.EqualTo(expected.GenericParameterPosition));
            }

            // Check generic type definition.
            Assert.That(actual.IsGenericTypeDefinition, Is.EqualTo(expected.IsGenericTypeDefinition));

            if (actual.IsGenericTypeDefinition)
                AreTypesEquivalent(actual.GetGenericTypeDefinition(), expected.GetGenericTypeDefinition());
        }
    }
}