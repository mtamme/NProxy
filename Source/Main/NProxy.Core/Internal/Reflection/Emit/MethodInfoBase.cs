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
using System.Globalization;
using System.Reflection;

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Represents the method information base class.
    /// </summary>
    [Serializable]
    internal abstract class MethodInfoBase : MethodInfo
    {
        /// <summary>
        /// The source object.
        /// </summary>
        private readonly object _source;

        /// <summary>
        /// The declaring method information.
        /// </summary>
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// A value indicating whether the method is an override.
        /// </summary>
        private readonly bool _isOverride;

        /// <summary>
        /// The declaring type.
        /// </summary>
        private readonly Type _declaringType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodInfoBase"/> class.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="methodInfo">The declaring method information.</param>
        /// <param name="isOverride">A value indicating whether the method is an override.</param>
        protected MethodInfoBase(object source, MethodInfo methodInfo, bool isOverride)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            _source = source;
            _methodInfo = methodInfo;
            _isOverride = isOverride;

            _declaringType = methodInfo.DeclaringType;
        }

        /// <summary>
        /// Invokes the base method represented by the current instance.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The return value.</returns>
        protected virtual object InvokeBase(object target, object[] parameters)
        {
            throw new TargetException(Resources.MethodNotImplemented);
        }

        /// <summary>
        /// Invokes the virtual method represented by the current instance.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The return value.</returns>
        protected abstract object InvokeVirtual(object target, object[] parameters);

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
            // Invoke base method when target equals source object.
            if (ReferenceEquals(target, _source))
            {
                if (_isOverride)
                    return InvokeBase(target, parameters);

                throw new TargetException(Resources.MethodNotImplemented);
            }

            if (target == null)
                throw new TargetException(Resources.MethodRequiresATargetObject);

            // Check target type.
            var targetType = target.GetType();

            if (!_declaringType.IsAssignableFrom(targetType) && !targetType.IsCOMObject)
                throw new TargetException(Resources.MethodNotDeclaredOrInherited);

            // Invoke method on target object.
            return InvokeVirtual(target, parameters);
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
            get { return _declaringType; }
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