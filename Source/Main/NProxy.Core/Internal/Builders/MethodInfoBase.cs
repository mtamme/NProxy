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
using System.Globalization;
using System.Reflection;

namespace NProxy.Core.Internal.Builders
{
    /// <summary>
    /// Represents the method information base class.
    /// </summary>
    [Serializable]
    internal abstract class MethodInfoBase : MethodInfo
    {
        /// <summary>
        /// The instance object.
        /// </summary>
        private readonly object _instance;

        /// <summary>
        /// The declaring method information.
        /// </summary>
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// A value indicating weather the specified method is overridden.
        /// </summary>
        private readonly bool _isOverride;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodInfoBase"/> class.
        /// </summary>
        /// <param name="instance">The instance object.</param>
        /// <param name="methodInfo">The declaring method information.</param>
        /// <param name="isOverride">A value indicating weather the specified method is overridden.</param>
        protected MethodInfoBase(object instance, MethodInfo methodInfo, bool isOverride)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            _instance = instance;
            _methodInfo = methodInfo;
            _isOverride = isOverride;
        }

        /// <summary>
        /// Invokes the base method represented by the current instance.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The return value.</returns>
        protected virtual object BaseInvoke(object target, object[] parameters)
        {
            throw new TargetException("Method is not implemented or inherited by target object");
        }

        /// <summary>
        /// Invokes the virtual method represented by the current instance.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The return value.</returns>
        protected abstract object VirtualInvoke(object target, object[] parameters);

        #region MethodInfo Members

        /// <inheritdoc/>
        public override sealed ParameterInfo ReturnParameter
        {
            get { return _methodInfo.ReturnParameter; }
        }

        /// <inheritdoc/>
        public override sealed Type ReturnType
        {
            get { return _methodInfo.ReturnType; }
        }

        /// <inheritdoc/>
        public override sealed ICustomAttributeProvider ReturnTypeCustomAttributes
        {
            get { return _methodInfo.ReturnTypeCustomAttributes; }
        }

        /// <inheritdoc/>
        public override sealed MethodInfo GetBaseDefinition()
        {
            return _methodInfo.GetBaseDefinition();
        }

        /// <inheritdoc/>
        public override sealed MethodInfo GetGenericMethodDefinition()
        {
            return _methodInfo.GetGenericMethodDefinition();
        }

        /// <inheritdoc/>
        public override sealed MethodInfo MakeGenericMethod(params Type[] typeArguments)
        {
            return _methodInfo.MakeGenericMethod(typeArguments);
        }

        #endregion

        #region MethodBase Members

        /// <inheritdoc/>
        public override sealed MethodAttributes Attributes
        {
            get { return _methodInfo.Attributes; }
        }

        /// <inheritdoc/>
        public override sealed CallingConventions CallingConvention
        {
            get { return _methodInfo.CallingConvention; }
        }

        /// <inheritdoc/>
        public override sealed bool ContainsGenericParameters
        {
            get { return _methodInfo.ContainsGenericParameters; }
        }

        /// <inheritdoc/>
        public override sealed bool IsGenericMethod
        {
            get { return _methodInfo.IsGenericMethod; }
        }

        /// <inheritdoc/>
        public override sealed bool IsGenericMethodDefinition
        {
            get { return _methodInfo.IsGenericMethodDefinition; }
        }

        /// <inheritdoc/>
        public override sealed RuntimeMethodHandle MethodHandle
        {
            get { return _methodInfo.MethodHandle; }
        }

        /// <inheritdoc/>
        public override sealed Type[] GetGenericArguments()
        {
            return _methodInfo.GetGenericArguments();
        }

        /// <inheritdoc/>
        public override sealed MethodBody GetMethodBody()
        {
            return _methodInfo.GetMethodBody();
        }

        /// <inheritdoc/>
        public override sealed MethodImplAttributes GetMethodImplementationFlags()
        {
            return _methodInfo.GetMethodImplementationFlags();
        }

        /// <inheritdoc/>
        public override sealed ParameterInfo[] GetParameters()
        {
            return _methodInfo.GetParameters();
        }

        /// <inheritdoc/>
        public override sealed object Invoke(object target, BindingFlags bindingFlags, Binder binder, object[] parameters, CultureInfo cultureInfo)
        {
            if (target == null)
                throw new TargetException("Method requires a target object");

            // Check target type.
            var declaringType = DeclaringType;
            var targetType = target.GetType();

            if ((declaringType == null) || !declaringType.IsAssignableFrom(targetType))
                throw new TargetException("Method is not declared or inherited by target object");

            // Check target object.
            if (!ReferenceEquals(target, _instance))
                return VirtualInvoke(target, parameters);

            if (!_isOverride)
                throw new TargetException("Method is not inherited by target object");

            return BaseInvoke(target, parameters);
        }

        #endregion

        #region ICustomAttributeProvider Members

        /// <inheritdoc/>
        public override sealed object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return _methodInfo.GetCustomAttributes(attributeType, inherit);
        }

        /// <inheritdoc/>
        public override sealed object[] GetCustomAttributes(bool inherit)
        {
            return _methodInfo.GetCustomAttributes(inherit);
        }

        /// <inheritdoc/>
        public override sealed bool IsDefined(Type attributeType, bool inherit)
        {
            return _methodInfo.IsDefined(attributeType, inherit);
        }

        #endregion

        #region MemberInfo Members

        /// <inheritdoc/>
        public override sealed Type DeclaringType
        {
            get { return _methodInfo.DeclaringType; }
        }

        /// <inheritdoc/>
        public override sealed MemberTypes MemberType
        {
            get { return _methodInfo.MemberType; }
        }

        /// <inheritdoc/>
        public override sealed int MetadataToken
        {
            get { return _methodInfo.MetadataToken; }
        }

        /// <inheritdoc/>
        public override sealed Module Module
        {
            get { return _methodInfo.Module; }
        }

        /// <inheritdoc/>
        public override sealed string Name
        {
            get { return _methodInfo.Name; }
        }

        /// <inheritdoc/>
        public override sealed Type ReflectedType
        {
            get { return _methodInfo.ReflectedType; }
        }

        /// <inheritdoc/>
        public override sealed IList<CustomAttributeData> GetCustomAttributesData()
        {
            return _methodInfo.GetCustomAttributesData();
        }

        #endregion

        #region Object Members

        /// <inheritdoc/>
        public override sealed bool Equals(object obj)
        {
            return _methodInfo.Equals(obj);
        }

        /// <inheritdoc/>
        public override sealed int GetHashCode()
        {
            return _methodInfo.GetHashCode();
        }

        /// <inheritdoc/>
        public override sealed string ToString()
        {
            return _methodInfo.ToString();
        }

        #endregion
    }
}