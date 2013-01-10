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
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Threading;
using NProxy.Core.Internal.Generators;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy type builder factory.
    /// </summary>
    internal sealed class ProxyTypeBuilderFactory : ITypeBuilderFactory, ITypeEmitter
    {
        /// <summary>
        /// The dynamic assembly name.
        /// </summary>
        private const string DynamicAssemblyName = "NProxy.Dynamic";

        /// <summary>
        /// The dynamic module name.
        /// </summary>
        private const string DynamicModuleName = "NProxy.Dynamic.dll";

        /// <summary>
        /// The dynamic assembly key pair resource name.
        /// </summary>
        private const string DynamicAssemblyKeyPairResourceName = "NProxy.Core.Internal.Dynamic.snk";

        /// <summary>
        /// The method information type provider.
        /// </summary>
        private readonly ITypeProvider<MethodInfo> _methodInfoTypeProvider;

        /// <summary>
        /// The assembly builder.
        /// </summary>
        private readonly AssemblyBuilder _assemblyBuilder;

        /// <summary>
        /// The module builder.
        /// </summary>
        private readonly ModuleBuilder _moduleBuilder;

        /// <summary>
        /// The next type id.
        /// </summary>
        private long _nextTypeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeBuilderFactory"/> class.
        /// </summary>
        /// <param name="canSaveAssembly">A value indicating weather the assembly can be saved.</param>
        public ProxyTypeBuilderFactory(bool canSaveAssembly)
        {
            var methodInfoTypeProvider = new MethodInfoTypeGenerator(this);

            _methodInfoTypeProvider = new TypeCache<MethodInfo, long>(m => m.GetId(), methodInfoTypeProvider);

            var assemblyName = GetDynamicAssemblyName(DynamicAssemblyName);

            // Define dynamic assembly.
            var assemblyBuilderAccess = canSaveAssembly ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run;

            _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyBuilderAccess);

            // Define dynamic module.
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(DynamicModuleName);

            _nextTypeId = 0;
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
                    throw new MissingManifestResourceException("Dynamic assembly key pair is missing");

                var keyPair = GetBytes(stream);

                return new StrongNameKeyPair(keyPair);
            }
        }

        /// <summary>
        /// Returns all bytes read from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>All bytes read from the specified stream.</returns>
        private static byte[] GetBytes(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
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
        /// Saves the dynamic assembly to disk.
        /// </summary>
        /// <param name="path">The path of the assembly.</param>
        public void SaveAssembly(string path)
        {
            _assemblyBuilder.Save(path);
        }

        #region ITypeEmitter Members

        /// <inheritdoc/>
        public TypeBuilder DefineType(string typeName, Type parentType)
        {
            var typeId = Interlocked.Increment(ref _nextTypeId);
            var uniqueTypeName = String.Format("{0}{1}${2}{3:x}", DynamicAssemblyName, Type.Delimiter, typeName, typeId);

            return _moduleBuilder.DefineType(
                uniqueTypeName,
                TypeAttributes.Class | TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.Serializable | TypeAttributes.BeforeFieldInit,
                parentType);
        }

        #endregion

        #region ITypeBuilderFactory Members

        /// <inheritdoc/>
        public ITypeBuilder CreateBuilder(Type parentType)
        {
            if (parentType == null)
                throw new ArgumentNullException("parentType");

            return new ProxyTypeBuilder(parentType, this, _methodInfoTypeProvider);
        }

        #endregion
    }
}
