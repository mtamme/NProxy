using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NProxy.Core.Test
{
    public class SampleBase
    {
        public SampleBase() { }
        public SampleBase(SampleBase other) { }
    }

    public class SampleGeneratedProxy : SampleBase, IProxyObject
    {
        IInvocationHandler _invocationHandler;
        Type _declaringType;
        Type _parentType;

        public SampleGeneratedProxy() : base()
        {
            _invocationHandler = InvocationHandlerFactoryHolder<SampleFactory>.GetFactory().CreateHandler(this);

            _declaringType = typeof(Activator);
            _parentType = typeof(object);
        }

        public SampleGeneratedProxy(SampleBase other) : base(other)
        {

        }

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

        public IInvocationHandler _GetInvocationHandler()
        {
            return _invocationHandler;
        }
    }

    public class SampleFactory : IInvocationHandlerFactory
    {
        public IInvocationHandler CreateHandler(IProxyObject target)
        {
            throw new NotImplementedException();
        }
    }

    public class SampleFastInvoke
    {
        public static object FastInvoke_Action1(object target, params object[] arguments)
        {
            ((SampleFastInvokeTarget)target).Action1();
            return null;
        }

        public static object FastInvoke_Action2(object target, params object[] arguments)
        {
            ((SampleFastInvokeTarget)target).Action2((int)arguments[0], (string)arguments[1]);
            return null;
        }

        public static object FastInvoke_StaticAction1(object target, params object[] arguments)
        {
            SampleFastInvokeTarget.StaticAction1((int)arguments[0], (string)arguments[1]);
            return null;
        }

        public static object FastInvoke_Func1(object target, params object[] arguments)
        {
            return ((SampleFastInvokeTarget)target).Func1((int)arguments[0], (string)arguments[1]);
        }

        public static object FastInvoke_Func2(object target, params object[] arguments)
        {
            return ((SampleFastInvokeTarget)target).Func2((string)arguments[0], (string)arguments[1], (string)arguments[2], (int)arguments[3], (DateTime)arguments[4], 
                                (string)arguments[5], (string)arguments[6], (string)arguments[7], (int)arguments[8], (DateTime)arguments[9]);
        }       
    }

    public class SampleFastInvokeTarget
    {
        public void Action1()
        {

        }

        public void Action2(int p1, string p2)
        {
        }

        public static void StaticAction1(int p1, string p2)
        {
        }

        public int Func1(int p1, string p2)
        {
            return 0;
        }

        public string Func2(string p1, string p2, string p3, int p4, DateTime p5, string p6, string p7, string p8, int p9, DateTime p10)
        {
            return string.Empty;
        }
    }
}
