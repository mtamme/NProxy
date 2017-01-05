using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NProxy.Core.Test
{
    public interface ITestAcessor
    {
        NProxy.Core.IProxyFactory _GetValue();
    }

    public class TestAcessor : ITestAcessor
    {
        IProxyFactory _accessor = null;

        public IProxyFactory _GetValue()
        {
            return _accessor;
        }
    }

}
