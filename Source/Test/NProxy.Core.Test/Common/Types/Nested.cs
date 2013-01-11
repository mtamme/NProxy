namespace NProxy.Core.Test.Common.Types
{
    internal sealed class Nested
    {
        public interface IGeneric<out TInner>
        {
            TInner Method();
        }

        public sealed class Generic<TInner>
        {
            public TInner Method()
            {
                return default(TInner);
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
