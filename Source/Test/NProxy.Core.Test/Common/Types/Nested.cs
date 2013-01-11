namespace NProxy.Core.Test.Common.Types
{
    internal sealed class Nested
    {
        public sealed class Generic<TInner>
        {
            public void Method(TInner value)
            {
            }
        }

        public sealed class NonGeneric
        {
            public void Method()
            {
            }
        }

        public void Method()
        {
        }
    }
}
