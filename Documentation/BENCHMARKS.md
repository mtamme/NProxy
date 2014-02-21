# Benchmarks

To get a picture how NProxy performs in comparison to other popular dynamic proxy libraries lets
define two interfaces to create dynamic proxies from.

```csharp
public interface IStandard
{
    int Invoke(int value);
}

public interface IGeneric
{
    TValue Invoke<TValue>(TValue value);
}
```

## Proxy generation

Proxy generation can be so called the worst-case scenario where each dynamic proxy creation
generates a new dynamic proxy type. The chart below shows the average proxy generation time in
milliseconds.

![Proxy generation performance](https://raw.github.com/mtamme/NProxy/master/Documentation/ProxyGeneration.png "Proxy generation performance")

## Proxy instantiation

Dynamic proxy types are usually cached by default so under normal circumstances the previous scenario does not apply.
Looking at the more common scenario where only a cache lookup following a type instantiation is performed results
in the following chart showing the average proxy instantiation time in microseconds.

![Proxy instantiation performance](https://raw.github.com/mtamme/NProxy/master/Documentation/ProxyInstantiation.png "Proxy instantiation performance")

## Method invocation

Method inocation is probably the most important performance indicator. The following chart shows
the values in microseconds for this scenario.

![Method invocation performance](https://raw.github.com/mtamme/NProxy/master/Documentation/MethodInvocation.png "Method invocation performance")

Details to the performed tests can be found [here](https://github.com/mtamme/NProxy/tree/master/Documentation/Benchmark_20140221_094332.md).
All tests have been performed under Microsoft .NET 4.0.30319 and can be found [here](https://github.com/mtamme/NProxy/tree/master/Source/Test/NProxy.Core.Benchmark/).

## Conclusion

From performance point of view NProxy beats the other dynamic proxy libraries in almost all scenarios. Only LinFu performs better
in terms of proxy instantiation. The main reason why LinFu performs better is that LinFu only supports the instantiation of class proxies
with a default constructor and the LinFu interceptor is not injected via a constructor but a property inside LinFu.