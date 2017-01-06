using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NProxy.Core.Test
{
    public class SampleGeneratedProxy : _IProxyObject
    {
        IInvocationHandler _invocationHandler;
        Type _declaringType;
        Type _parentType;

        public Type _DeclaringType
        {
            get
            {
                return _declaringType;
            }
        }

        public Type _ParentType
        {
            get
            {
                return _parentType;
            }
        }

        public SampleGeneratedProxy()
        {
            _invocationHandler = InvocationHandlerFactoryHolder<SampleFactory>.GetFactory().CreateHandler(this);

            _declaringType = typeof(Activator);
            _parentType = typeof(object);
        }

        public IInvocationHandler _GetInvocationHandler()
        {
            return _invocationHandler;
        }
    }

    public class SampleFactory : IInvocationHandlerFactory
    {
        public IInvocationHandler CreateHandler(_IProxyObject target)
        {
            throw new NotImplementedException();
        }
    }
}
