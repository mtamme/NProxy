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
using NProxy.Core.Test.Common;
using NProxy.Core.Test.Common.Types;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    public sealed class ProxyFactoryTestFixture
    {
        private ProxyTypeBuilderFactory _proxyTypeBuilderFactory;

        private ProxyFactory _proxyFactory;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _proxyTypeBuilderFactory = new ProxyTypeBuilderFactory(true);
            _proxyFactory = new ProxyFactory(_proxyTypeBuilderFactory);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _proxyTypeBuilderFactory.SaveAssembly("NProxy.Dynamic.dll");
        }

        #region Interface Tests

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {EnumType.Two});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(EnumType.Two);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {"Two"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {new[] {"Two"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRankArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[,] {{"Two", "Two"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new List<string> {"Two"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method("Two");

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {2});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(2);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {"2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method("2");

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {EnumType.Two};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = EnumType.Two;

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {new[] {"Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRankArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[,] {{"Two", "Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new List<string> {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = "Two";

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {2};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = 2;

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {"2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = "2";

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {new StructType {Integer = 2, String = "2"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new StructType {Integer = 2, String = "2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {EnumType.Two}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            EnumType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {EnumType.Two}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumOutParameter>(Type.EmptyTypes, invocationHandler);
            EnumType value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {"Two"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {new[] {"Two"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericRankArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[,] {{"Two", "Two"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new List<string> {"Two"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListOutParameter>(Type.EmptyTypes, invocationHandler);
            List<string> value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {"Two"}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericOutParameter>(Type.EmptyTypes, invocationHandler);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {2}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {2}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntOutParameter>(Type.EmptyTypes, invocationHandler);
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {"2"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {"2"}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringOutParameter>(Type.EmptyTypes, invocationHandler);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {new StructType {Integer = 2, String = "2"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            StructType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new StructType {Integer = 2, String = "2"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructOutParameter>(Type.EmptyTypes, invocationHandler);
            StructType value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {EnumType.Two});

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithEnumReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(EnumType.Two);

            // Act
            var proxy = _proxyFactory.CreateProxy<IEnumReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {"Two"});

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericJaggedArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {new[] {"Two"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithRankJaggedArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[,] {{"Two", "Two"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericListReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new List<string> {"Two"});

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithGenericReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler("Two");

            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {2});

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithIntReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(2);

            // Act
            var proxy = _proxyFactory.CreateProxy<IIntReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {"2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStringReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler("2");

            // Act
            var proxy = _proxyFactory.CreateProxy<IStringReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithStructReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<IStructReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromInterfaceWithVoidReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(null);

            // Act
            var proxy = _proxyFactory.CreateProxy<IVoidReturnValue>(Type.EmptyTypes, invocationHandler);

            proxy.Method();
        }

        #endregion

        #region Abstract Class Tests

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {EnumType.Two});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(EnumType.Two);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {"Two"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {new[] {"Two"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[,] {{"Two", "Two"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new List<string> {"Two"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method("Two");

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {2});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(2);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {"2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method("2");

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructParameterBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new[] {EnumType.Two};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = EnumType.Two;

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new[] {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new[] {new[] {"Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new[,] {{"Two", "Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new List<string> {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = "Two";

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new[] {2};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = 2;

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new[] {"2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = "2";

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new[] {new StructType {Integer = 2, String = "2"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructRefParameterBase>(Type.EmptyTypes, invocationHandler);
            var value = new StructType {Integer = 2, String = "2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {EnumType.Two}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayOutParameterBase>(Type.EmptyTypes, invocationHandler);
            EnumType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {EnumType.Two}};

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumOutParameterBase>(Type.EmptyTypes, invocationHandler);
            EnumType value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {"Two"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayOutParameterBase>(Type.EmptyTypes, invocationHandler);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {new[] {"Two"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayOutParameterBase>(Type.EmptyTypes, invocationHandler);
            string[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[,] {{"Two", "Two"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayOutParameterBase>(Type.EmptyTypes, invocationHandler);
            string[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new List<string> {"Two"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListOutParameterBase>(Type.EmptyTypes, invocationHandler);
            List<string> value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {"Two"}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericOutParameterBase>(Type.EmptyTypes, invocationHandler);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {2}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayOutParameterBase>(Type.EmptyTypes, invocationHandler);
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {2}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IntOutParameterBase>(Type.EmptyTypes, invocationHandler);
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {"2"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayOutParameterBase>(Type.EmptyTypes, invocationHandler);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {"2"}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StringOutParameterBase>(Type.EmptyTypes, invocationHandler);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {new StructType {Integer = 2, String = "2"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayOutParameterBase>(Type.EmptyTypes, invocationHandler);
            StructType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new StructType {Integer = 2, String = "2"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StructOutParameterBase>(Type.EmptyTypes, invocationHandler);
            StructType value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {EnumType.Two});

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithEnumReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(EnumType.Two);

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {"Two"});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericJaggedArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {new[] {"Two"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericRankArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[,] {{"Two", "Two"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericListReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new List<string> {"Two"});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithGenericReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler("Two");

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {2});

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithIntReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(2);

            // Act
            var proxy = _proxyFactory.CreateProxy<IntReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {"2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStringReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler("2");

            // Act
            var proxy = _proxyFactory.CreateProxy<StringReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithStructReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<StructReturnValueBase>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithVoidReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(null);

            // Act
            var proxy = _proxyFactory.CreateProxy<VoidReturnValueBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method();
        }

        #endregion

        #region Class Tests

        [Test]
        public void CreateProxyFromClassWithEnumArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {EnumType.Two});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(EnumType.Two);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {"Two"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {new[] {"Two"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[,] {{"Two", "Two"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new List<string> {"Two"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method("Two");

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {2});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(2);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {"2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method("2");

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructParameter>(Type.EmptyTypes, invocationHandler);

            proxy.Method(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {EnumType.Two};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = EnumType.Two;

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {new[] {"Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[,] {{"Two", "Two"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new List<string> {"Two"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = "Two";

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {2};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<IntRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = 2;

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {"2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StringRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = "2";

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new[] {new StructType {Integer = 2, String = "2"}};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructRefParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<StructRefParameter>(Type.EmptyTypes, invocationHandler);
            var value = new StructType {Integer = 2, String = "2"};

            proxy.Method(ref value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {EnumType.Two}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            EnumType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {EnumType.Two}};

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumOutParameter>(Type.EmptyTypes, invocationHandler);
            EnumType value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {"Two"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {new[] {"Two"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[,] {{"Two", "Two"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new List<string> {"Two"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListOutParameter>(Type.EmptyTypes, invocationHandler);
            List<string> value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {"Two"}};

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericOutParameter>(Type.EmptyTypes, invocationHandler);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {2}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {2}};

            // Act
            var proxy = _proxyFactory.CreateProxy<IntOutParameter>(Type.EmptyTypes, invocationHandler);
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {"2"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            string[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {"2"}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StringOutParameter>(Type.EmptyTypes, invocationHandler);
            string value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new[] {new StructType {Integer = 2, String = "2"}}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayOutParameter>(Type.EmptyTypes, invocationHandler);
            StructType[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructOutParameterTest()
        {
            // Arrange
            var invocationHandler = new SetParametersInvocationHandler {Parameters = new object[] {new StructType {Integer = 2, String = "2"}}};

            // Act
            var proxy = _proxyFactory.CreateProxy<StructOutParameter>(Type.EmptyTypes, invocationHandler);
            StructType value;

            proxy.Method(out value);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {EnumType.Two});

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromClassWithEnumReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(EnumType.Two);

            // Act
            var proxy = _proxyFactory.CreateProxy<EnumReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromClassWithGenericArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {"Two"});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericJaggedArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {new[] {"Two"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericJaggedArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new[] {"Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericRankArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[,] {{"Two", "Two"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericRankArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new[,] {{"Two", "Two"}}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericListReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new List<string> {"Two"});

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericListReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<string> {"Two"}));
        }

        [Test]
        public void CreateProxyFromClassWithGenericReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler("Two");

            // Act
            var proxy = _proxyFactory.CreateProxy<GenericReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method<string>();

            // Assert
            Assert.That(value, Is.EqualTo("Two"));
        }

        [Test]
        public void CreateProxyFromClassWithIntArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {2});

            // Act
            var proxy = _proxyFactory.CreateProxy<IntArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromClassWithIntReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(2);

            // Act
            var proxy = _proxyFactory.CreateProxy<IntReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromClassWithStringArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {"2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<StringArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromClassWithStringReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler("2");

            // Act
            var proxy = _proxyFactory.CreateProxy<StringReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromClassWithStructArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<StructArrayReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromClassWithStructReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<StructReturnValue>(Type.EmptyTypes, invocationHandler);
            var value = proxy.Method();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromClassWithVoidReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(null);

            // Act
            var proxy = _proxyFactory.CreateProxy<VoidReturnValue>(Type.EmptyTypes, invocationHandler);

            proxy.Method();
        }

        #endregion

        #region Delegate Tests

        [Test]
        public void CreateProxyFromDelegateWithEnumArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<EnumType[]>>(Type.EmptyTypes, invocationHandler);

            proxy(new[] {EnumType.Two});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromDelegateWithEnumParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<EnumType>>(Type.EmptyTypes, invocationHandler);

            proxy(EnumType.Two);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<int[]>>(Type.EmptyTypes, invocationHandler);

            proxy(new[] {2});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<int>>(Type.EmptyTypes, invocationHandler);

            proxy(2);

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<string[]>>(Type.EmptyTypes, invocationHandler);

            proxy(new[] {"2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<string>>(Type.EmptyTypes, invocationHandler);

            proxy("2");

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructArrayParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<StructType[]>>(Type.EmptyTypes, invocationHandler);

            proxy(new[] {new StructType {Integer = 2, String = "2"}});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructParameterTest()
        {
            // Arrange
            var invocationHandler = new GetParametersInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action<StructType>>(Type.EmptyTypes, invocationHandler);

            proxy(new StructType {Integer = 2, String = "2"});

            // Assert
            Assert.That(invocationHandler.Parameters[0], Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        [Test]
        public void CreateProxyFromDelegateWithEnumArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {EnumType.Two});

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<EnumType[]>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {EnumType.Two}));
        }

        [Test]
        public void CreateProxyFromDelegateWithEnumReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(EnumType.Two);

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<EnumType>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(EnumType.Two));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {2});

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<int[]>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {2}));
        }

        [Test]
        public void CreateProxyFromDelegateWithIntReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(2);

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<int>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {"2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<string[]>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {"2"}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStringReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler("2");

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<string>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo("2"));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructArrayReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new[] {new StructType {Integer = 2, String = "2"}});

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<StructType[]>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new[] {new StructType {Integer = 2, String = "2"}}));
        }

        [Test]
        public void CreateProxyFromDelegateWithStructReturnValueTest()
        {
            // Arrange
            var invocationHandler = new SetReturnValueInvocationHandler(new StructType {Integer = 2, String = "2"});

            // Act
            var proxy = _proxyFactory.CreateProxy<Func<StructType>>(Type.EmptyTypes, invocationHandler);
            var value = proxy();

            // Assert
            Assert.That(value, Is.EqualTo(new StructType {Integer = 2, String = "2"}));
        }

        #endregion

        #region Target Object Tests

        [Test]
        public void CreateProxyFromInterfaceAndNullTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromInterfaceAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInvocationHandler(p => p));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromAbstractClassAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(Type.EmptyTypes, new TargetInvocationHandler(p => p));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromClassAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IntParameter>(Type.EmptyTypes, new TargetInvocationHandler(p => p));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromDelegateAndProxyTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Action<int>>(Type.EmptyTypes, new TargetInvocationHandler(p => p));

            // Assert
            Assert.Throws<TargetException>(() => proxy(default(int)));
        }

        [Test]
        public void CreateProxyFromInterfaceAndInvalidTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new StringParameter()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyFromInterfaceAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new IntParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericArrayParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericJaggedArrayParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericRankArrayParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new int[0,0]));
        }

        [Test]
        public void CreateProxyWithGenericListParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericListParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericParameter()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayRefParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericArrayRefParameter()));
            var value = new int[0];

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayRefParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericJaggedArrayRefParameter()));
            var value = new int[0][];

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayRefParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericRankArrayRefParameter()));
            var value = new int[0,0];

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0,0]));
        }

        [Test]
        public void CreateProxyWithGenericListRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListRefParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericListRefParameter()));
            var value = new List<int>();

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericRefParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRefParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericRefParameter()));
            int value = default(int);

            proxy.Method(ref value);

            // Assert
            Assert.That(value, Is.EqualTo(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayOutParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericArrayOutParameter()));
            int[] value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayOutParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericJaggedArrayOutParameter()));
            int[][] value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayOutParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericRankArrayOutParameter()));
            int[,] value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new int[0,0]));
        }

        [Test]
        public void CreateProxyWithGenericListOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListOutParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericListOutParameter()));
            List<int> value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericOutParameterAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericOutParameter>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericOutParameter()));
            int value;

            proxy.Method(out value);

            // Assert
            Assert.That(value, Is.EqualTo(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericArrayReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericArrayReturnValue>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericArrayReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new int[0]));
        }

        [Test]
        public void CreateProxyWithGenericJaggedArrayReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericJaggedArrayReturnValue>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericJaggedArrayReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new int[0][]));
        }

        [Test]
        public void CreateProxyWithGenericRankArrayReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericRankArrayReturnValue>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericRankArrayReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new int[0,0]));
        }

        [Test]
        public void CreateProxyWithGenericListReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericListReturnValue>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericListReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(new List<int>()));
        }

        [Test]
        public void CreateProxyWithGenericReturnValueAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericReturnValue>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericReturnValue()));
            var value = proxy.Method<int>();

            // Assert
            Assert.That(value, Is.EqualTo(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericInterfaceAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGeneric<int>>(Type.EmptyTypes, new TargetInvocationHandler(_ => new Generic<int>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericAbstractClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<GenericBase<int>>(Type.EmptyTypes, new TargetInvocationHandler(_ => new Generic<int>()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Generic<int>>(Type.EmptyTypes, new TargetInvocationHandler(_ => new Generic<int>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(int)));
        }

        [Test]
        public void CreateProxyWithGenericParameterInterfaceAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IGenericParameter<string>>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericParameter<string>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(string), default(int)));
        }

        [Test]
        public void CreateProxyWithGenericParameterAbstractClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<GenericParameterBase<string>>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericParameter<string>()));

            // Assert
            Assert.Throws<TargetException>(() => proxy.Method(default(string), default(int)));
        }

        [Test]
        public void CreateProxyWithGenericParameterClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<GenericParameter<string>>(Type.EmptyTypes, new TargetInvocationHandler(_ => new GenericParameter<string>()));

            // Assert
            Assert.DoesNotThrow(() => proxy.Method(default(string), default(int)));
        }

        #endregion

        #region Additional Interface Tests

        [Test]
        public void CreateProxyWithAdditionalInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IBase>(new[] {typeof (IOther)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOther>());
        }

        [Test]
        public void CreateProxyWithExtendedInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IBase>(new[] {typeof (IOne)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOne>());
        }

        [Test]
        public void CreateProxyWithPartialInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IOne>(new[] {typeof (IBase)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IOne>());
            Assert.That(proxy, Is.InstanceOf<IBase>());
        }

        [Test]
        public void CreateProxyWithEquivalentInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IBase>(new[] {typeof (IBase)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
        }

        [Test]
        public void CreateProxyWithSimilarInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IOne>(new[] {typeof (ITwo)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOne>());
            Assert.That(proxy, Is.InstanceOf<ITwo>());
        }

        [Test]
        public void CreateProxyWithDuplicateInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IOneTwo>(new[] {typeof (IOne), typeof (ITwo)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IOne>());
            Assert.That(proxy, Is.InstanceOf<ITwo>());
            Assert.That(proxy, Is.InstanceOf<IOneTwo>());
        }

        [Test]
        public void CreateProxyWithHiddenInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IBase>(new[] {typeof (IHideBase)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<IHideBase>());
        }

        [Test]
        public void CreateProxyWithNestedGenericInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IBase>(new[] {typeof (Class.INested<int>), typeof (Class.INested<string>)}, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
            Assert.That(proxy, Is.InstanceOf<Class.INested<int>>());
            Assert.That(proxy, Is.InstanceOf<Class.INested<string>>());
        }

        [Test]
        public void CreateProxyWithoutAdditionalInterfaceTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IBase>(Type.EmptyTypes, new TargetInvocationHandler(_ => null));

            // Assert
            Assert.That(proxy, Is.InstanceOf<IBase>());
        }

        #endregion

        #region Non Intercepted Attribute Tests

        [Test]
        public void CreateProxyFromInterfaceWithNonInterceptedEventTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<INonIntercepted>(Type.EmptyTypes, invocationHandler);

            proxy.Event += () => { };

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromInterfaceWithNonInterceptedPropertyTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<INonIntercepted>(Type.EmptyTypes, invocationHandler);

            proxy.Property = default(int);

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromInterfaceWithNonInterceptedMethodTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<INonIntercepted>(Type.EmptyTypes, invocationHandler);

            proxy.Method();

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithNonInterceptedEventTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<NonInterceptedBase>(Type.EmptyTypes, invocationHandler);

            proxy.Event += () => { };

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithNonInterceptedPropertyTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<NonInterceptedBase>(Type.EmptyTypes, invocationHandler);

            proxy.Property = default(int);

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromAbstractClassWithNonInterceptedMethodTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<NonInterceptedBase>(Type.EmptyTypes, invocationHandler);

            proxy.Method();

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromClassWithNonInterceptedEventTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<NonIntercepted>(Type.EmptyTypes, invocationHandler);

            proxy.Event += () => { };

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(0));
        }

        [Test]
        public void CreateProxyFromClassWithNonInterceptedPropertyTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<NonIntercepted>(Type.EmptyTypes, invocationHandler);

            proxy.Property = default(int);

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(0));
        }

        [Test]
        public void CreateProxyFromClassWithNonInterceptedMethodTest()
        {
            // Arrange
            var invocationHandler = new CountingInvocationHandler();

            // Act
            var proxy = _proxyFactory.CreateProxy<NonIntercepted>(Type.EmptyTypes, invocationHandler);

            proxy.Method();

            // Assert
            Assert.That(invocationHandler.InvocationCount, Is.EqualTo(0));
        }

        #endregion
    }
}