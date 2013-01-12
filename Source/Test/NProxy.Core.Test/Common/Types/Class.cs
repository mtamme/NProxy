namespace NProxy.Core.Test.Common.Types
{
    internal sealed class Class
    {
        public interface INested<out TInner>
        {
            TInner Method();
        }

        public sealed class Nested<TInner>
        {
            public TInner Method()
            {
                return default(TInner);
            }
        }

        public sealed class Nested
        {
            public void Method()
            {
            }
        }

        public void Method()
        {
        }
    }

    internal sealed class Class<TOuter>
    {
        public interface INested<out TInner>
        {
            TInner Method();
        }

        public sealed class Nested<TInner>
        {
            public TInner Method()
            {
                return default(TInner);
            }
        }

        public sealed class Nested
        {
            public void Method()
            {
            }
        }

        public TOuter Method()
        {
            return default(TOuter);
        }
    }
}
