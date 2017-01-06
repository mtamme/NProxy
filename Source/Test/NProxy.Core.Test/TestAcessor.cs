using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NProxy.Core.Test
{
    public class SampleGeneratedProxy : IProxyObject
    {
        IInvocationHandler _invocationHandler;
        public SampleGeneratedProxy()
        {
            _invocationHandler = InvocationHandlerFactoryHolder<SampleFactory>.GetFactory().CreateHandler(this);
        }

        public IInvocationHandler _GetInvocationHandler()
        {
            return _invocationHandler;
        }
    }

    public class SampleFactory : IInvocationHandlerFactory
    {
        public IInvocationHandler CreateHandler(object target)
        {
            throw new NotImplementedException();
        }
    }
}
