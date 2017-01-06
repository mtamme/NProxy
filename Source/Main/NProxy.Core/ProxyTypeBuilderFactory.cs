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
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Threading;
using NProxy.Core.Internal;
using NProxy.Core.Internal.Caching;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Reflection.Emit;
using NProxy.Core.Internal.Definitions;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy type builder factory.
    /// </summary>
    internal sealed class ProxyTypeBuilderFactory : ITypeBuilderFactory, ITypeRepository
    {
        /// <summary>
        /// The dynamic assembly name.
        /// </summary>
        private const string DynamicAssemblyName = "NProxy.Dynamic";

        /// <summary>
        /// The dynamic module name.
        /// </summary>
        private const string DynamicModuleName = DynamicAssemblyName + ".dll";

        /// <summary>
        /// The dynamic default namespace.
        /// </summary>
        private const string DynamicDefaultNamespace = DynamicAssemblyName;

        /// <summary>
        /// The dynamic assembly key pair resource name.
        /// </summary>
        private const string DynamicAssemblyKeyPairResourceName = "NProxy.Core.Internal.Dynamic.snk";

        /// <summary>
        /// The assembly builder.
        /// </summary>
        private readonly AssemblyBuilder _assemblyBuilder;

        /// <summary>
        /// The module builder.
        /// </summary>
        private readonly ModuleBuilder _moduleBuilder;

        /// <summary>
        /// The method information type factory.
        /// </summary>
        private readonly ITypeFactory _methodInfoTypeFactory;

        /// <summary>
        /// The method information type cache.
        /// </summary>
        private readonly ICache<MemberToken, Type> _methodInfoTypeCache;

        /// <summary>
        /// The next type identifier.
        /// </summary>
        private int _nextTypeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeBuilderFactory"/> class.
        /// </summary>
        /// <param name="canSaveAssembly">A value indicating whether the assembly can be saved.</param>
        public ProxyTypeBuilderFactory(bool canSaveAssembly)
        {
            _assemblyBuilder = DefineDynamicAssembly(DynamicAssemblyName, canSaveAssembly);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(DynamicModuleName);

            _methodInfoTypeFactory = new MethodInfoTypeFactory(this);
            _methodInfoTypeCache = new Cache<MemberToken, Type>();

            _nextTypeId = -1;
        }

        /// <summary>
        /// Defines the dynamic assembly.
        /// </summary>
        /// <param name="name">The assembly name.</param>
        /// <param name="canSaveAssembly">A value indicating whether the assembly can be saved.</param>
        /// <returns>The assembly builder.</returns>
        private static AssemblyBuilder DefineDynamicAssembly(string name, bool canSaveAssembly)
        {
            var assemblyBuilderAccess = canSaveAssembly ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run;
            var assemblyName = GetDynamicAssemblyName(name);

            return AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyBuilderAccess);
        }

        /// <summary>
        /// Returns the dynamic assembly name.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <returns>The assembly name.</returns>
        private static AssemblyName GetDynamicAssemblyName(string assemblyName)
        {
            var executingAssemblyName = GetExecutingAssemblyName();
            var keyPair = GetDynamicAssemblyKeyPair();

            return new AssemblyName(assemblyName)
            {
                KeyPair = keyPair,
                Version = executingAssemblyName.Version
            };
        }

        /// <summary>
        /// Returns the executing assembly name.
        /// </summary>
        /// <returns>The assembly name.</returns>
        private static AssemblyName GetExecutingAssemblyName()
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetName();
        }

        /// <summary>
        /// Returns the dynamic assembly key pair.
        /// </summary>
        /// <returns>The dynamic assembly key pair.</returns>
        private static StrongNameKeyPair GetDynamicAssemblyKeyPair()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(DynamicAssemblyKeyPairResourceName))
            {
                if (stream == null)
                    throw new MissingManifestResourceException(Resources.DynamicAssemblyKeyPairIsMissing);

                var keyPair = ReadToEnd(stream);

                return new StrongNameKeyPair(keyPair);
            }
        }

        /// <summary>
        /// Reads all bytes from the current position to the end of the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>All bytes from the current position to the end of the specified stream.</returns>
        private static byte[] ReadToEnd(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[512];

                while (true)
                {
                    var count = stream.Read(buffer, 0, buffer.Length);

                    if (count <= 0)
                        break;

                    memoryStream.Write(buffer, 0, count);
                }

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Saves the dynamic assembly to disk.
        /// </summary>
        /// <param name="path">The path of the assembly.</param>
        public void SaveAssembly(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            _assemblyBuilder.Save(path);
        }

        #region ITypeRepository Members

        /// <inheritdoc/>
        public TypeBuilder DefineType(string typeName, Type parentType)
        {
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            if (parentType == null)
                throw new ArgumentNullException("parentType");

            var typeId = Interlocked.Increment(ref _nextTypeId);
            var uniqueTypeName = String.Format("{0}{1}<{2}>z__{3:x}", DynamicDefaultNamespace, Type.Delimiter, typeName, typeId);

            return _moduleBuilder.DefineType(
                uniqueTypeName,
                TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Serializable | TypeAttributes.BeforeFieldInit,
                parentType);
        }

        /// <inheritdoc/>
        public Type GetType(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            var memberToken = new MemberToken(methodInfo);

            return _methodInfoTypeCache.GetOrAdd(memberToken, _ => _methodInfoTypeFactory.CreateType(methodInfo));
        }

        #endregion

        #region ITypeBuilderFactory Members

        /// <inheritdoc/>
        public ITypeBuilder CreateBuilder(IProxyDefinition proxyDefinition)
        {
            if (proxyDefinition == null)
                throw new ArgumentNullException("proxyDefinition");

            return new ProxyTypeBuilder(this, proxyDefinition);
        }

        #endregion
    }
}