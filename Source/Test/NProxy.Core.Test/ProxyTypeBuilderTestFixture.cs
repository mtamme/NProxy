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
using System.Linq;
using System.Reflection;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    public sealed class ProxyTypeBuilderTestFixture
    {
        private ProxyTypeBuilderFactory _proxyTypeBuilderFactory;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _proxyTypeBuilderFactory = new ProxyTypeBuilderFactory(true, false);
        }

        #region Add Interface Test Cases

        [TestCase(typeof (object), typeof (IEmpty))]
        [TestCase(typeof (List<object>), typeof (IEmpty))]

        #endregion

        public void AddInterfaceTest(Type parentType, Type interfaceType)
        {
            // Arrange
            var typeBuilder = _proxyTypeBuilderFactory.CreateBuilder(parentType);

            // Act
            typeBuilder.AddInterface(interfaceType);

            var type = typeBuilder.CreateType();

            // Assert
            Assert.DoesNotThrow(() => Activator.CreateInstance(type));

            foreach (var parentInterfaceType in parentType.GetInterfaces())
            {
                Assert.That(parentInterfaceType.IsAssignableFrom(type), Is.True);
            }

            Assert.That(interfaceType.IsAssignableFrom(type), Is.True);
            Assert.That(parentType.IsAssignableFrom(type), Is.True);
        }

        #region Build Constructor From Abstract Class Test Cases

        [TestCase(typeof (EnumArrayConstructorBase), typeof (EnumArrayConstructorBase), typeof (EnumArrayConstructor))]
        [TestCase(typeof (EnumConstructorBase), typeof (EnumConstructorBase), typeof (EnumConstructor))]
        [TestCase(typeof (GenericArrayConstructorBase<object>), typeof (GenericArrayConstructorBase<object>), typeof (GenericArrayConstructor<object>))]
        [TestCase(typeof (GenericJaggedArrayConstructorBase<object>), typeof (GenericJaggedArrayConstructorBase<object>), typeof (GenericJaggedArrayConstructor<object>))]
        [TestCase(typeof (GenericRankArrayConstructorBase<object>), typeof (GenericRankArrayConstructorBase<object>), typeof (GenericRankArrayConstructor<object>))]
        [TestCase(typeof (GenericListConstructorBase<object>), typeof (GenericListConstructorBase<object>), typeof (GenericListConstructor<object>))]
        [TestCase(typeof (GenericConstructorBase<object>), typeof (GenericConstructorBase<object>), typeof (GenericConstructor<object>))]
        [TestCase(typeof (IntArrayConstructorBase), typeof (IntArrayConstructorBase), typeof (IntArrayConstructor))]
        [TestCase(typeof (IntConstructorBase), typeof (IntConstructorBase), typeof (IntConstructor))]
        [TestCase(typeof (StringArrayConstructorBase), typeof (StringArrayConstructorBase), typeof (StringArrayConstructor))]
        [TestCase(typeof (StringConstructorBase), typeof (StringConstructorBase), typeof (StringConstructor))]
        [TestCase(typeof (StructArrayConstructorBase), typeof (StructArrayConstructorBase), typeof (StructArrayConstructor))]
        [TestCase(typeof (StructConstructorBase), typeof (StructConstructorBase), typeof (StructConstructor))]
        [TestCase(typeof (EnumArrayRefConstructorBase), typeof (EnumArrayRefConstructorBase), typeof (EnumArrayRefConstructor))]
        [TestCase(typeof (EnumRefConstructorBase), typeof (EnumRefConstructorBase), typeof (EnumRefConstructor))]
        [TestCase(typeof (GenericArrayRefConstructorBase<object>), typeof (GenericArrayRefConstructorBase<object>), typeof (GenericArrayRefConstructor<object>))]
        [TestCase(typeof (GenericJaggedArrayRefConstructorBase<object>), typeof (GenericJaggedArrayRefConstructorBase<object>), typeof (GenericJaggedArrayRefConstructor<object>))]
        [TestCase(typeof (GenericRankArrayRefConstructorBase<object>), typeof (GenericRankArrayRefConstructorBase<object>), typeof (GenericRankArrayRefConstructor<object>))]
        [TestCase(typeof (GenericListRefConstructorBase<object>), typeof (GenericListRefConstructorBase<object>), typeof (GenericListRefConstructor<object>))]
        [TestCase(typeof (GenericRefConstructorBase<object>), typeof (GenericRefConstructorBase<object>), typeof (GenericRefConstructor<object>))]
        [TestCase(typeof (IntArrayRefConstructorBase), typeof (IntArrayRefConstructorBase), typeof (IntArrayRefConstructor))]
        [TestCase(typeof (IntRefConstructorBase), typeof (IntRefConstructorBase), typeof (IntRefConstructor))]
        [TestCase(typeof (StringArrayRefConstructorBase), typeof (StringArrayRefConstructorBase), typeof (StringArrayRefConstructor))]
        [TestCase(typeof (StringRefConstructorBase), typeof (StringRefConstructorBase), typeof (StringRefConstructor))]
        [TestCase(typeof (StructArrayRefConstructorBase), typeof (StructArrayRefConstructorBase), typeof (StructArrayRefConstructor))]
        [TestCase(typeof (StructRefConstructorBase), typeof (StructRefConstructorBase), typeof (StructRefConstructor))]
        [TestCase(typeof (EnumArrayOutConstructorBase), typeof (EnumArrayOutConstructorBase), typeof (EnumArrayOutConstructor))]
        [TestCase(typeof (EnumOutConstructorBase), typeof (EnumOutConstructorBase), typeof (EnumOutConstructor))]
        [TestCase(typeof (GenericArrayOutConstructorBase<object>), typeof (GenericArrayOutConstructorBase<object>), typeof (GenericArrayOutConstructor<object>))]
        [TestCase(typeof (GenericJaggedArrayOutConstructorBase<object>), typeof (GenericJaggedArrayOutConstructorBase<object>), typeof (GenericJaggedArrayOutConstructor<object>))]
        [TestCase(typeof (GenericRankArrayOutConstructorBase<object>), typeof (GenericRankArrayOutConstructorBase<object>), typeof (GenericRankArrayOutConstructor<object>))]
        [TestCase(typeof (GenericListOutConstructorBase<object>), typeof (GenericListOutConstructorBase<object>), typeof (GenericListOutConstructor<object>))]
        [TestCase(typeof (GenericOutConstructorBase<object>), typeof (GenericOutConstructorBase<object>), typeof (GenericOutConstructor<object>))]
        [TestCase(typeof (IntArrayOutConstructorBase), typeof (IntArrayOutConstructorBase), typeof (IntArrayOutConstructor))]
        [TestCase(typeof (IntOutConstructorBase), typeof (IntOutConstructorBase), typeof (IntOutConstructor))]
        [TestCase(typeof (StringArrayOutConstructorBase), typeof (StringArrayOutConstructorBase), typeof (StringArrayOutConstructor))]
        [TestCase(typeof (StringOutConstructorBase), typeof (StringOutConstructorBase), typeof (StringOutConstructor))]
        [TestCase(typeof (StructArrayOutConstructorBase), typeof (StructArrayOutConstructorBase), typeof (StructArrayOutConstructor))]
        [TestCase(typeof (StructOutConstructorBase), typeof (StructOutConstructorBase), typeof (StructOutConstructor))]

        #endregion

        #region Build Constructor From Class Test Cases

        [TestCase(typeof (EnumArrayConstructor), typeof (EnumArrayConstructor), typeof (EnumArrayConstructor))]
        [TestCase(typeof (EnumConstructor), typeof (EnumConstructor), typeof (EnumConstructor))]
        [TestCase(typeof (GenericArrayConstructor<object>), typeof (GenericArrayConstructor<object>), typeof (GenericArrayConstructor<object>))]
        [TestCase(typeof (GenericJaggedArrayConstructor<object>), typeof (GenericJaggedArrayConstructor<object>), typeof (GenericJaggedArrayConstructor<object>))]
        [TestCase(typeof (GenericRankArrayConstructor<object>), typeof (GenericRankArrayConstructor<object>), typeof (GenericRankArrayConstructor<object>))]
        [TestCase(typeof (GenericListConstructor<object>), typeof (GenericListConstructor<object>), typeof (GenericListConstructor<object>))]
        [TestCase(typeof (GenericConstructor<object>), typeof (GenericConstructor<object>), typeof (GenericConstructor<object>))]
        [TestCase(typeof (IntArrayConstructor), typeof (IntArrayConstructor), typeof (IntArrayConstructor))]
        [TestCase(typeof (IntConstructor), typeof (IntConstructor), typeof (IntConstructor))]
        [TestCase(typeof (StringArrayConstructor), typeof (StringArrayConstructor), typeof (StringArrayConstructor))]
        [TestCase(typeof (StringConstructor), typeof (StringConstructor), typeof (StringConstructor))]
        [TestCase(typeof (StructArrayConstructor), typeof (StructArrayConstructor), typeof (StructArrayConstructor))]
        [TestCase(typeof (StructConstructor), typeof (StructConstructor), typeof (StructConstructor))]
        [TestCase(typeof (EnumArrayRefConstructor), typeof (EnumArrayRefConstructor), typeof (EnumArrayRefConstructor))]
        [TestCase(typeof (EnumRefConstructor), typeof (EnumRefConstructor), typeof (EnumRefConstructor))]
        [TestCase(typeof (GenericArrayRefConstructor<object>), typeof (GenericArrayRefConstructor<object>), typeof (GenericArrayRefConstructor<object>))]
        [TestCase(typeof (GenericJaggedArrayRefConstructor<object>), typeof (GenericJaggedArrayRefConstructor<object>), typeof (GenericJaggedArrayRefConstructor<object>))]
        [TestCase(typeof (GenericRankArrayRefConstructor<object>), typeof (GenericRankArrayRefConstructor<object>), typeof (GenericRankArrayRefConstructor<object>))]
        [TestCase(typeof (GenericListRefConstructor<object>), typeof (GenericListRefConstructor<object>), typeof (GenericListRefConstructor<object>))]
        [TestCase(typeof (GenericRefConstructor<object>), typeof (GenericRefConstructor<object>), typeof (GenericRefConstructor<object>))]
        [TestCase(typeof (IntArrayRefConstructor), typeof (IntArrayRefConstructor), typeof (IntArrayRefConstructor))]
        [TestCase(typeof (IntRefConstructor), typeof (IntRefConstructor), typeof (IntRefConstructor))]
        [TestCase(typeof (StringArrayRefConstructor), typeof (StringArrayRefConstructor), typeof (StringArrayRefConstructor))]
        [TestCase(typeof (StringRefConstructor), typeof (StringRefConstructor), typeof (StringRefConstructor))]
        [TestCase(typeof (StructArrayRefConstructor), typeof (StructArrayRefConstructor), typeof (StructArrayRefConstructor))]
        [TestCase(typeof (StructRefConstructor), typeof (StructRefConstructor), typeof (StructRefConstructor))]
        [TestCase(typeof (EnumArrayOutConstructor), typeof (EnumArrayOutConstructor), typeof (EnumArrayOutConstructor))]
        [TestCase(typeof (EnumOutConstructor), typeof (EnumOutConstructor), typeof (EnumOutConstructor))]
        [TestCase(typeof (GenericArrayOutConstructor<object>), typeof (GenericArrayOutConstructor<object>), typeof (GenericArrayOutConstructor<object>))]
        [TestCase(typeof (GenericJaggedArrayOutConstructor<object>), typeof (GenericJaggedArrayOutConstructor<object>), typeof (GenericJaggedArrayOutConstructor<object>))]
        [TestCase(typeof (GenericRankArrayOutConstructor<object>), typeof (GenericRankArrayOutConstructor<object>), typeof (GenericRankArrayOutConstructor<object>))]
        [TestCase(typeof (GenericListOutConstructor<object>), typeof (GenericListOutConstructor<object>), typeof (GenericListOutConstructor<object>))]
        [TestCase(typeof (GenericOutConstructor<object>), typeof (GenericOutConstructor<object>), typeof (GenericOutConstructor<object>))]
        [TestCase(typeof (IntArrayOutConstructor), typeof (IntArrayOutConstructor), typeof (IntArrayOutConstructor))]
        [TestCase(typeof (IntOutConstructor), typeof (IntOutConstructor), typeof (IntOutConstructor))]
        [TestCase(typeof (StringArrayOutConstructor), typeof (StringArrayOutConstructor), typeof (StringArrayOutConstructor))]
        [TestCase(typeof (StringOutConstructor), typeof (StringOutConstructor), typeof (StringOutConstructor))]
        [TestCase(typeof (StructArrayOutConstructor), typeof (StructArrayOutConstructor), typeof (StructArrayOutConstructor))]
        [TestCase(typeof (StructOutConstructor), typeof (StructOutConstructor), typeof (StructOutConstructor))]

        #endregion

        public void BuildConstructorTest(Type parentType, Type declaringType, Type classType)
        {
            // Arrange
            var typeBuilder = _proxyTypeBuilderFactory.CreateBuilder(parentType);

            if (declaringType.IsInterface)
                typeBuilder.AddInterface(declaringType);

            var constructorInfos = declaringType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            Assert.That(constructorInfos.Length, Is.EqualTo(1));

            // Act
            typeBuilder.BuildConstructor(constructorInfos.First());

            var type = typeBuilder.CreateType();

            // Assert
            foreach (var interfaceType in declaringType.GetInterfaces())
            {
                Assert.That(interfaceType.IsAssignableFrom(type), Is.True);
            }

            Assert.That(parentType.IsAssignableFrom(type), Is.True);
            Assert.That(declaringType.IsAssignableFrom(type), Is.True);

            var actualConstructorInfos = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var expectedConstructorInfos = classType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            Assert.That(actualConstructorInfos.Length, Is.EqualTo(expectedConstructorInfos.Length));

            MemberAssert.AreEquivalent(actualConstructorInfos.Single(), expectedConstructorInfos.Single());
        }

        #region Build Event From Interface Test Cases

        [TestCase(typeof (object), typeof (IActionEvent), typeof (ActionEvent))]

        #endregion

        #region Build Event From Abstract Class Test Cases

        [TestCase(typeof (ActionEventBase), typeof (ActionEventBase), typeof (ActionEvent))]

        #endregion

        #region Build Event From Class Test Cases

        [TestCase(typeof (ActionEvent), typeof (ActionEvent), typeof (ActionEvent))]

        #endregion

        public void BuildEventTest(Type parentType, Type declaringType, Type classType)
        {
            // Arrange
            var typeBuilder = _proxyTypeBuilderFactory.CreateBuilder(parentType);

            if (declaringType.IsInterface)
                typeBuilder.AddInterface(declaringType);

            var eventInfo = declaringType.GetEvent("Event");

            Assert.That(eventInfo, Is.Not.Null);

            // Act
            typeBuilder.BuildEvent(eventInfo);

            var type = typeBuilder.CreateType();

            // Assert
            Assert.DoesNotThrow(() => Activator.CreateInstance(type));

            foreach (var interfaceType in declaringType.GetInterfaces())
            {
                Assert.That(interfaceType.IsAssignableFrom(type), Is.True);
            }

            Assert.That(parentType.IsAssignableFrom(type), Is.True);
            Assert.That(declaringType.IsAssignableFrom(type), Is.True);

            var isExplicit = declaringType.IsInterface;
            EventInfo actual;

            if (isExplicit)
                actual = type.GetEvent(eventInfo.GetFullName(), BindingFlags.NonPublic | BindingFlags.Instance);
            else
                actual = type.GetEvent(eventInfo.Name);

            var expected = classType.GetEvent(eventInfo.Name);

            MemberAssert.AreEquivalent(actual, expected, isExplicit);
        }

        #region Build Property From Interface Test Cases

        [TestCase(typeof (object), typeof (IObjectGetProperty), typeof (ObjectGetProperty))]
        [TestCase(typeof (object), typeof (IObjectGetSetProperty), typeof (ObjectGetSetProperty))]
        [TestCase(typeof (object), typeof (IObjectSetProperty), typeof (ObjectSetProperty))]

        #endregion

        #region Build Property From Abstract Class Test Cases

        [TestCase(typeof (ObjectGetPropertyBase), typeof (ObjectGetPropertyBase), typeof (ObjectGetProperty))]
        [TestCase(typeof (ObjectGetSetPropertyBase), typeof (ObjectGetSetPropertyBase), typeof (ObjectGetSetProperty))]
        [TestCase(typeof (ObjectSetPropertyBase), typeof (ObjectSetPropertyBase), typeof (ObjectSetProperty))]

        #endregion

        #region Build Property From Class Test Cases

        [TestCase(typeof (ObjectGetProperty), typeof (ObjectGetProperty), typeof (ObjectGetProperty))]
        [TestCase(typeof (ObjectGetSetProperty), typeof (ObjectGetSetProperty), typeof (ObjectGetSetProperty))]
        [TestCase(typeof (ObjectSetProperty), typeof (ObjectSetProperty), typeof (ObjectSetProperty))]

        #endregion

        public void BuildPropertyTest(Type parentType, Type declaringType, Type classType)
        {
            // Arrange
            var typeBuilder = _proxyTypeBuilderFactory.CreateBuilder(parentType);

            if (declaringType.IsInterface)
                typeBuilder.AddInterface(declaringType);

            var propertyInfo = declaringType.GetProperty("Property");

            Assert.That(propertyInfo, Is.Not.Null);

            // Act
            typeBuilder.BuildProperty(propertyInfo);

            var type = typeBuilder.CreateType();

            // Assert
            Assert.DoesNotThrow(() => Activator.CreateInstance(type));

            foreach (var interfaceType in declaringType.GetInterfaces())
            {
                Assert.That(interfaceType.IsAssignableFrom(type), Is.True);
            }

            Assert.That(parentType.IsAssignableFrom(type), Is.True);
            Assert.That(declaringType.IsAssignableFrom(type), Is.True);

            var isExplicit = declaringType.IsInterface;
            PropertyInfo actual;

            if (isExplicit)
                actual = type.GetProperty(propertyInfo.GetFullName(), BindingFlags.NonPublic | BindingFlags.Instance);
            else
                actual = type.GetProperty(propertyInfo.Name);

            var expected = classType.GetProperty(propertyInfo.Name);

            MemberAssert.AreEquivalent(actual, expected, isExplicit);
        }

        #region Build Method From Interface Test Cases

        [TestCase(typeof (object), typeof (IEnumArrayParameter), typeof (EnumArrayParameter))]
        [TestCase(typeof (object), typeof (IEnumParameter), typeof (EnumParameter))]
        [TestCase(typeof (object), typeof (IGenericArrayParameter), typeof (GenericArrayParameter))]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayParameter), typeof (GenericJaggedArrayParameter))]
        [TestCase(typeof (object), typeof (IGenericRankArrayParameter), typeof (GenericRankArrayParameter))]
        [TestCase(typeof (object), typeof (IGenericListParameter), typeof (GenericListParameter))]
        [TestCase(typeof (object), typeof (IGenericParameter), typeof (GenericParameter))]
        [TestCase(typeof (object), typeof (IIntArrayParameter), typeof (IntArrayParameter))]
        [TestCase(typeof (object), typeof (IIntParameter), typeof (IntParameter))]
        [TestCase(typeof (object), typeof (IStringArrayParameter), typeof (StringArrayParameter))]
        [TestCase(typeof (object), typeof (IStringParameter), typeof (StringParameter))]
        [TestCase(typeof (object), typeof (IStructArrayParameter), typeof (StructArrayParameter))]
        [TestCase(typeof (object), typeof (IStructParameter), typeof (StructParameter))]
        [TestCase(typeof (object), typeof (IEnumArrayRefParameter), typeof (EnumArrayRefParameter))]
        [TestCase(typeof (object), typeof (IEnumRefParameter), typeof (EnumRefParameter))]
        [TestCase(typeof (object), typeof (IGenericArrayRefParameter), typeof (GenericArrayRefParameter))]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayRefParameter), typeof (GenericJaggedArrayRefParameter))]
        [TestCase(typeof (object), typeof (IGenericRankArrayRefParameter), typeof (GenericRankArrayRefParameter))]
        [TestCase(typeof (object), typeof (IGenericListRefParameter), typeof (GenericListRefParameter))]
        [TestCase(typeof (object), typeof (IGenericRefParameter), typeof (GenericRefParameter))]
        [TestCase(typeof (object), typeof (IIntArrayRefParameter), typeof (IntArrayRefParameter))]
        [TestCase(typeof (object), typeof (IIntRefParameter), typeof (IntRefParameter))]
        [TestCase(typeof (object), typeof (IStringArrayRefParameter), typeof (StringArrayRefParameter))]
        [TestCase(typeof (object), typeof (IStringRefParameter), typeof (StringRefParameter))]
        [TestCase(typeof (object), typeof (IStructArrayRefParameter), typeof (StructArrayRefParameter))]
        [TestCase(typeof (object), typeof (IStructRefParameter), typeof (StructRefParameter))]
        [TestCase(typeof (object), typeof (IEnumArrayOutParameter), typeof (EnumArrayOutParameter))]
        [TestCase(typeof (object), typeof (IEnumOutParameter), typeof (EnumOutParameter))]
        [TestCase(typeof (object), typeof (IGenericArrayOutParameter), typeof (GenericArrayOutParameter))]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayOutParameter), typeof (GenericJaggedArrayOutParameter))]
        [TestCase(typeof (object), typeof (IGenericRankArrayOutParameter), typeof (GenericRankArrayOutParameter))]
        [TestCase(typeof (object), typeof (IGenericListOutParameter), typeof (GenericListOutParameter))]
        [TestCase(typeof (object), typeof (IGenericOutParameter), typeof (GenericOutParameter))]
        [TestCase(typeof (object), typeof (IIntArrayOutParameter), typeof (IntArrayOutParameter))]
        [TestCase(typeof (object), typeof (IIntOutParameter), typeof (IntOutParameter))]
        [TestCase(typeof (object), typeof (IStringArrayOutParameter), typeof (StringArrayOutParameter))]
        [TestCase(typeof (object), typeof (IStringOutParameter), typeof (StringOutParameter))]
        [TestCase(typeof (object), typeof (IStructArrayOutParameter), typeof (StructArrayOutParameter))]
        [TestCase(typeof (object), typeof (IStructOutParameter), typeof (StructOutParameter))]
        [TestCase(typeof (object), typeof (IEnumArrayReturnValue), typeof (EnumArrayReturnValue))]
        [TestCase(typeof (object), typeof (IEnumReturnValue), typeof (EnumReturnValue))]
        [TestCase(typeof (object), typeof (IGenericArrayReturnValue), typeof (GenericArrayReturnValue))]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayReturnValue), typeof (GenericJaggedArrayReturnValue))]
        [TestCase(typeof (object), typeof (IGenericRankArrayReturnValue), typeof (GenericRankArrayReturnValue))]
        [TestCase(typeof (object), typeof (IGenericListReturnValue), typeof (GenericListReturnValue))]
        [TestCase(typeof (object), typeof (IGenericReturnValue), typeof (GenericReturnValue))]
        [TestCase(typeof (object), typeof (IIntArrayReturnValue), typeof (IntArrayReturnValue))]
        [TestCase(typeof (object), typeof (IIntReturnValue), typeof (IntReturnValue))]
        [TestCase(typeof (object), typeof (IStringArrayReturnValue), typeof (StringArrayReturnValue))]
        [TestCase(typeof (object), typeof (IStringReturnValue), typeof (StringReturnValue))]
        [TestCase(typeof (object), typeof (IStructArrayReturnValue), typeof (StructArrayReturnValue))]
        [TestCase(typeof (object), typeof (IStructReturnValue), typeof (StructReturnValue))]
        [TestCase(typeof (object), typeof (IVoidReturnValue), typeof (VoidReturnValue))]

        #endregion

        #region Build Method From Abstract Class Test Cases

        [TestCase(typeof (EnumArrayParameterBase), typeof (EnumArrayParameterBase), typeof (EnumArrayParameter))]
        [TestCase(typeof (EnumParameterBase), typeof (EnumParameterBase), typeof (EnumParameter))]
        [TestCase(typeof (GenericArrayParameterBase), typeof (GenericArrayParameterBase), typeof (GenericArrayParameter))]
        [TestCase(typeof (GenericJaggedArrayParameterBase), typeof (GenericJaggedArrayParameterBase), typeof (GenericJaggedArrayParameter))]
        [TestCase(typeof (GenericRankArrayParameterBase), typeof (GenericRankArrayParameterBase), typeof (GenericRankArrayParameter))]
        [TestCase(typeof (GenericListParameterBase), typeof (GenericListParameterBase), typeof (GenericListParameter))]
        [TestCase(typeof (GenericParameterBase), typeof (GenericParameterBase), typeof (GenericParameter))]
        [TestCase(typeof (IntArrayParameterBase), typeof (IntArrayParameterBase), typeof (IntArrayParameter))]
        [TestCase(typeof (IntParameterBase), typeof (IntParameterBase), typeof (IntParameter))]
        [TestCase(typeof (StringArrayParameterBase), typeof (StringArrayParameterBase), typeof (StringArrayParameter))]
        [TestCase(typeof (StringParameterBase), typeof (StringParameterBase), typeof (StringParameter))]
        [TestCase(typeof (StructArrayParameterBase), typeof (StructArrayParameterBase), typeof (StructArrayParameter))]
        [TestCase(typeof (StructParameterBase), typeof (StructParameterBase), typeof (StructParameter))]
        [TestCase(typeof (EnumArrayRefParameterBase), typeof (EnumArrayRefParameterBase), typeof (EnumArrayRefParameter))]
        [TestCase(typeof (EnumRefParameterBase), typeof (EnumRefParameterBase), typeof (EnumRefParameter))]
        [TestCase(typeof (GenericArrayRefParameterBase), typeof (GenericArrayRefParameterBase), typeof (GenericArrayRefParameter))]
        [TestCase(typeof (GenericJaggedArrayRefParameterBase), typeof (GenericJaggedArrayRefParameterBase), typeof (GenericJaggedArrayRefParameter))]
        [TestCase(typeof (GenericRankArrayRefParameterBase), typeof (GenericRankArrayRefParameterBase), typeof (GenericRankArrayRefParameter))]
        [TestCase(typeof (GenericListRefParameterBase), typeof (GenericListRefParameterBase), typeof (GenericListRefParameter))]
        [TestCase(typeof (GenericRefParameterBase), typeof (GenericRefParameterBase), typeof (GenericRefParameter))]
        [TestCase(typeof (IntArrayRefParameterBase), typeof (IntArrayRefParameterBase), typeof (IntArrayRefParameter))]
        [TestCase(typeof (IntRefParameterBase), typeof (IntRefParameterBase), typeof (IntRefParameter))]
        [TestCase(typeof (StringArrayRefParameterBase), typeof (StringArrayRefParameterBase), typeof (StringArrayRefParameter))]
        [TestCase(typeof (StringRefParameterBase), typeof (StringRefParameterBase), typeof (StringRefParameter))]
        [TestCase(typeof (StructArrayRefParameterBase), typeof (StructArrayRefParameterBase), typeof (StructArrayRefParameter))]
        [TestCase(typeof (StructRefParameterBase), typeof (StructRefParameterBase), typeof (StructRefParameter))]
        [TestCase(typeof (EnumArrayOutParameterBase), typeof (EnumArrayOutParameterBase), typeof (EnumArrayOutParameter))]
        [TestCase(typeof (EnumOutParameterBase), typeof (EnumOutParameterBase), typeof (EnumOutParameter))]
        [TestCase(typeof (GenericArrayOutParameterBase), typeof (GenericArrayOutParameterBase), typeof (GenericArrayOutParameter))]
        [TestCase(typeof (GenericJaggedArrayOutParameterBase), typeof (GenericJaggedArrayOutParameterBase), typeof (GenericJaggedArrayOutParameter))]
        [TestCase(typeof (GenericRankArrayOutParameterBase), typeof (GenericRankArrayOutParameterBase), typeof (GenericRankArrayOutParameter))]
        [TestCase(typeof (GenericListOutParameterBase), typeof (GenericListOutParameterBase), typeof (GenericListOutParameter))]
        [TestCase(typeof (GenericOutParameterBase), typeof (GenericOutParameterBase), typeof (GenericOutParameter))]
        [TestCase(typeof (IntArrayOutParameterBase), typeof (IntArrayOutParameterBase), typeof (IntArrayOutParameter))]
        [TestCase(typeof (IntOutParameterBase), typeof (IntOutParameterBase), typeof (IntOutParameter))]
        [TestCase(typeof (StringArrayOutParameterBase), typeof (StringArrayOutParameterBase), typeof (StringArrayOutParameter))]
        [TestCase(typeof (StringOutParameterBase), typeof (StringOutParameterBase), typeof (StringOutParameter))]
        [TestCase(typeof (StructArrayOutParameterBase), typeof (StructArrayOutParameterBase), typeof (StructArrayOutParameter))]
        [TestCase(typeof (StructOutParameterBase), typeof (StructOutParameterBase), typeof (StructOutParameter))]
        [TestCase(typeof (EnumArrayReturnValueBase), typeof (EnumArrayReturnValueBase), typeof (EnumArrayReturnValue))]
        [TestCase(typeof (EnumReturnValueBase), typeof (EnumReturnValueBase), typeof (EnumReturnValue))]
        [TestCase(typeof (GenericArrayReturnValueBase), typeof (GenericArrayReturnValueBase), typeof (GenericArrayReturnValue))]
        [TestCase(typeof (GenericJaggedArrayReturnValueBase), typeof (GenericJaggedArrayReturnValueBase), typeof (GenericJaggedArrayReturnValue))]
        [TestCase(typeof (GenericRankArrayReturnValueBase), typeof (GenericRankArrayReturnValueBase), typeof (GenericRankArrayReturnValue))]
        [TestCase(typeof (GenericListReturnValueBase), typeof (GenericListReturnValueBase), typeof (GenericListReturnValue))]
        [TestCase(typeof (GenericReturnValueBase), typeof (GenericReturnValueBase), typeof (GenericReturnValue))]
        [TestCase(typeof (IntArrayReturnValueBase), typeof (IntArrayReturnValueBase), typeof (IntArrayReturnValue))]
        [TestCase(typeof (IntReturnValueBase), typeof (IntReturnValueBase), typeof (IntReturnValue))]
        [TestCase(typeof (StringArrayReturnValueBase), typeof (StringArrayReturnValueBase), typeof (StringArrayReturnValue))]
        [TestCase(typeof (StringReturnValueBase), typeof (StringReturnValueBase), typeof (StringReturnValue))]
        [TestCase(typeof (StructArrayReturnValueBase), typeof (StructArrayReturnValueBase), typeof (StructArrayReturnValue))]
        [TestCase(typeof (StructReturnValueBase), typeof (StructReturnValueBase), typeof (StructReturnValue))]
        [TestCase(typeof (VoidReturnValueBase), typeof (VoidReturnValueBase), typeof (VoidReturnValue))]

        #endregion

        #region Build Method From Class Test Cases

        [TestCase(typeof (EnumArrayParameter), typeof (EnumArrayParameter), typeof (EnumArrayParameter))]
        [TestCase(typeof (EnumParameter), typeof (EnumParameter), typeof (EnumParameter))]
        [TestCase(typeof (GenericArrayParameter), typeof (GenericArrayParameter), typeof (GenericArrayParameter))]
        [TestCase(typeof (GenericJaggedArrayParameter), typeof (GenericJaggedArrayParameter), typeof (GenericJaggedArrayParameter))]
        [TestCase(typeof (GenericRankArrayParameter), typeof (GenericRankArrayParameter), typeof (GenericRankArrayParameter))]
        [TestCase(typeof (GenericListParameter), typeof (GenericListParameter), typeof (GenericListParameter))]
        [TestCase(typeof (GenericParameter), typeof (GenericParameter), typeof (GenericParameter))]
        [TestCase(typeof (IntArrayParameter), typeof (IntArrayParameter), typeof (IntArrayParameter))]
        [TestCase(typeof (IntParameter), typeof (IntParameter), typeof (IntParameter))]
        [TestCase(typeof (StringArrayParameter), typeof (StringArrayParameter), typeof (StringArrayParameter))]
        [TestCase(typeof (StringParameter), typeof (StringParameter), typeof (StringParameter))]
        [TestCase(typeof (StructArrayParameter), typeof (StructArrayParameter), typeof (StructArrayParameter))]
        [TestCase(typeof (StructParameter), typeof (StructParameter), typeof (StructParameter))]
        [TestCase(typeof (EnumArrayRefParameter), typeof (EnumArrayRefParameter), typeof (EnumArrayRefParameter))]
        [TestCase(typeof (EnumRefParameter), typeof (EnumRefParameter), typeof (EnumRefParameter))]
        [TestCase(typeof (GenericArrayRefParameter), typeof (GenericArrayRefParameter), typeof (GenericArrayRefParameter))]
        [TestCase(typeof (GenericJaggedArrayRefParameter), typeof (GenericJaggedArrayRefParameter), typeof (GenericJaggedArrayRefParameter))]
        [TestCase(typeof (GenericRankArrayRefParameter), typeof (GenericRankArrayRefParameter), typeof (GenericRankArrayRefParameter))]
        [TestCase(typeof (GenericListRefParameter), typeof (GenericListRefParameter), typeof (GenericListRefParameter))]
        [TestCase(typeof (GenericRefParameter), typeof (GenericRefParameter), typeof (GenericRefParameter))]
        [TestCase(typeof (IntArrayRefParameter), typeof (IntArrayRefParameter), typeof (IntArrayRefParameter))]
        [TestCase(typeof (IntRefParameter), typeof (IntRefParameter), typeof (IntRefParameter))]
        [TestCase(typeof (StringArrayRefParameter), typeof (StringArrayRefParameter), typeof (StringArrayRefParameter))]
        [TestCase(typeof (StringRefParameter), typeof (StringRefParameter), typeof (StringRefParameter))]
        [TestCase(typeof (StructArrayRefParameter), typeof (StructArrayRefParameter), typeof (StructArrayRefParameter))]
        [TestCase(typeof (StructRefParameter), typeof (StructRefParameter), typeof (StructRefParameter))]
        [TestCase(typeof (EnumArrayOutParameter), typeof (EnumArrayOutParameter), typeof (EnumArrayOutParameter))]
        [TestCase(typeof (EnumOutParameter), typeof (EnumOutParameter), typeof (EnumOutParameter))]
        [TestCase(typeof (GenericArrayOutParameter), typeof (GenericArrayOutParameter), typeof (GenericArrayOutParameter))]
        [TestCase(typeof (GenericJaggedArrayOutParameter), typeof (GenericJaggedArrayOutParameter), typeof (GenericJaggedArrayOutParameter))]
        [TestCase(typeof (GenericRankArrayOutParameter), typeof (GenericRankArrayOutParameter), typeof (GenericRankArrayOutParameter))]
        [TestCase(typeof (GenericListOutParameter), typeof (GenericListOutParameter), typeof (GenericListOutParameter))]
        [TestCase(typeof (GenericOutParameter), typeof (GenericOutParameter), typeof (GenericOutParameter))]
        [TestCase(typeof (IntArrayOutParameter), typeof (IntArrayOutParameter), typeof (IntArrayOutParameter))]
        [TestCase(typeof (IntOutParameter), typeof (IntOutParameter), typeof (IntOutParameter))]
        [TestCase(typeof (StringArrayOutParameter), typeof (StringArrayOutParameter), typeof (StringArrayOutParameter))]
        [TestCase(typeof (StringOutParameter), typeof (StringOutParameter), typeof (StringOutParameter))]
        [TestCase(typeof (StructArrayOutParameter), typeof (StructArrayOutParameter), typeof (StructArrayOutParameter))]
        [TestCase(typeof (StructOutParameter), typeof (StructOutParameter), typeof (StructOutParameter))]
        [TestCase(typeof (EnumArrayReturnValue), typeof (EnumArrayReturnValue), typeof (EnumArrayReturnValue))]
        [TestCase(typeof (EnumReturnValue), typeof (EnumReturnValue), typeof (EnumReturnValue))]
        [TestCase(typeof (GenericArrayReturnValue), typeof (GenericArrayReturnValue), typeof (GenericArrayReturnValue))]
        [TestCase(typeof (GenericJaggedArrayReturnValue), typeof (GenericJaggedArrayReturnValue), typeof (GenericJaggedArrayReturnValue))]
        [TestCase(typeof (GenericRankArrayReturnValue), typeof (GenericRankArrayReturnValue), typeof (GenericRankArrayReturnValue))]
        [TestCase(typeof (GenericListReturnValue), typeof (GenericListReturnValue), typeof (GenericListReturnValue))]
        [TestCase(typeof (GenericReturnValue), typeof (GenericReturnValue), typeof (GenericReturnValue))]
        [TestCase(typeof (IntArrayReturnValue), typeof (IntArrayReturnValue), typeof (IntArrayReturnValue))]
        [TestCase(typeof (IntReturnValue), typeof (IntReturnValue), typeof (IntReturnValue))]
        [TestCase(typeof (StringArrayReturnValue), typeof (StringArrayReturnValue), typeof (StringArrayReturnValue))]
        [TestCase(typeof (StringReturnValue), typeof (StringReturnValue), typeof (StringReturnValue))]
        [TestCase(typeof (StructArrayReturnValue), typeof (StructArrayReturnValue), typeof (StructArrayReturnValue))]
        [TestCase(typeof (StructReturnValue), typeof (StructReturnValue), typeof (StructReturnValue))]
        [TestCase(typeof (VoidReturnValue), typeof (VoidReturnValue), typeof (VoidReturnValue))]

        #endregion

        public void BuildMethodOverrideTest(Type parentType, Type declaringType, Type classType)
        {
            // Arrange
            var typeBuilder = _proxyTypeBuilderFactory.CreateBuilder(parentType);

            if (declaringType.IsInterface)
                typeBuilder.AddInterface(declaringType);

            var methodInfo = declaringType.GetMethod("Method");

            Assert.That(methodInfo, Is.Not.Null);

            // Act
            typeBuilder.BuildMethod(methodInfo);

            var type = typeBuilder.CreateType();

            // Assert
            Assert.DoesNotThrow(() => Activator.CreateInstance(type));

            foreach (var interfaceType in declaringType.GetInterfaces())
            {
                Assert.That(interfaceType.IsAssignableFrom(type), Is.True);
            }

            Assert.That(parentType.IsAssignableFrom(type), Is.True);
            Assert.That(declaringType.IsAssignableFrom(type), Is.True);

            var isExplicit = declaringType.IsInterface;
            MethodInfo actual;

            if (isExplicit)
                actual = type.GetMethod(methodInfo.GetFullName(), BindingFlags.NonPublic | BindingFlags.Instance);
            else
                actual = type.GetMethod(methodInfo.Name);

            var expected = classType.GetMethod(methodInfo.Name);

            MemberAssert.AreEquivalent(actual, expected, isExplicit);
        }

        #region Build Method From Action Delegate Test Cases

        [TestCase(typeof (object), typeof (Action<EnumType[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Action<EnumType>), "Invoke")]
        [TestCase(typeof (object), typeof (Action<int[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Action<int>), "Invoke")]
        [TestCase(typeof (object), typeof (Action<string[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Action<string>), "Invoke")]
        [TestCase(typeof (object), typeof (Action<StructType[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Action<StructType>), "Invoke")]

        #endregion

        #region Build Method From Function Delegate Test Cases

        [TestCase(typeof (object), typeof (Func<EnumType[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Func<EnumType>), "Invoke")]
        [TestCase(typeof (object), typeof (Func<int[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Func<int>), "Invoke")]
        [TestCase(typeof (object), typeof (Func<string[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Func<string>), "Invoke")]
        [TestCase(typeof (object), typeof (Func<StructType[]>), "Invoke")]
        [TestCase(typeof (object), typeof (Func<StructType>), "Invoke")]

        #endregion

        #region Build Method From Method Test Cases

        [TestCase(typeof (object), typeof (IEnumArrayParameter), "Method")]
        [TestCase(typeof (object), typeof (IEnumParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericArrayParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericRankArrayParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericListParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericParameter), "Method")]
        [TestCase(typeof (object), typeof (IIntArrayParameter), "Method")]
        [TestCase(typeof (object), typeof (IIntParameter), "Method")]
        [TestCase(typeof (object), typeof (IStringArrayParameter), "Method")]
        [TestCase(typeof (object), typeof (IStringParameter), "Method")]
        [TestCase(typeof (object), typeof (IStructArrayParameter), "Method")]
        [TestCase(typeof (object), typeof (IStructParameter), "Method")]
        [TestCase(typeof (object), typeof (IEnumArrayRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IEnumRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericArrayRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericRankArrayRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericListRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IIntArrayRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IIntRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IStringArrayRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IStringRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IStructArrayRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IStructRefParameter), "Method")]
        [TestCase(typeof (object), typeof (IEnumArrayOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IEnumOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericArrayOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericRankArrayOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericListOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IGenericOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IIntArrayOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IIntOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IStringArrayOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IStringOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IStructArrayOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IStructOutParameter), "Method")]
        [TestCase(typeof (object), typeof (IEnumArrayReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IEnumReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IGenericArrayReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IGenericJaggedArrayReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IGenericRankArrayReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IGenericListReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IGenericReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IIntArrayReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IIntReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IStringArrayReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IStringReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IStructArrayReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IStructReturnValue), "Method")]
        [TestCase(typeof (object), typeof (IVoidReturnValue), "Method")]

        #endregion

        public void BuildMethodTest(Type parentType, Type declaringType, string methodName)
        {
            // Arrange
            var typeBuilder = _proxyTypeBuilderFactory.CreateBuilder(parentType);

            var methodInfo = declaringType.GetMethod(methodName);

            Assert.That(methodInfo, Is.Not.Null);

            // Act
            typeBuilder.BuildMethod(methodInfo);

            // Assert
            var type = typeBuilder.CreateType();

            Assert.DoesNotThrow(() => Activator.CreateInstance(type));

            var actual = type.GetMethod(methodName);
            var expected = declaringType.GetMethod(methodName);

            MemberAssert.AreEquivalent(actual, expected, false);
        }

        #region Build Method From Non Overridable Method Test Cases

        [TestCase(typeof (object), typeof (object), "GetType")]
        [TestCase(typeof (object), typeof (object), "MemberwiseClone")]

        #endregion

        public void BuildMethodWithNonOverridableMethodTest(Type parentType, Type declaringType, string methodName)
        {
            // Arrange
            var typeBuilder = _proxyTypeBuilderFactory.CreateBuilder(parentType);
            var methodInfo = declaringType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => typeBuilder.BuildMethod(methodInfo));
        }
    }
}