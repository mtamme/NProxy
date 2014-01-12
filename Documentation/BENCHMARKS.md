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

Details to the performed tests can be found in the table below.

| Type      | Version | Scenario                             | Iterations | Total time in ms | Average time in µs |
|:----------|--------:|:-------------------------------------|-----------:|-----------------:|-------------------:|
|Castle.Core|    v 3.2|Method invocation                     |    10000000|           827,356|               0,083|
|Castle.Core|    v 3.2|Method invocation (w/ generic param)  |    10000000|         14577,622|               1,458|
|Castle.Core|    v 3.2|Proxy generation                      |        1000|          3426,190|            3426,190|
|Castle.Core|    v 3.2|Proxy generation (w/ generic param)   |        1000|          3401,879|            3401,879|
|Castle.Core|    v 3.2|Proxy instantiation                   |     1000000|          3839,880|               3,840|
|Castle.Core|    v 3.2|Proxy instantiation (w/ generic param)|     1000000|          3846,255|               3,846|
|LinFu.Core |    v 2.3|Method invocation                     |    10000000|         15153,302|               1,515|
|LinFu.Core |    v 2.3|Method invocation (w/ generic param)  |    10000000|         58935,538|               5,894|
|LinFu.Core |    v 2.3|Proxy generation                      |        1000|          4491,037|            4491,037|
|LinFu.Core |    v 2.3|Proxy generation (w/ generic param)   |        1000|          4804,534|            4804,534|
|LinFu.Core |    v 2.3|Proxy instantiation                   |     1000000|           926,474|               0,926|
|LinFu.Core |    v 2.3|Proxy instantiation (w/ generic param)|     1000000|           920,981|               0,921|
|NProxy.Core|    v 2.0|Method invocation                     |    10000000|           847,583|               0,085|
|NProxy.Core|    v 2.0|Method invocation (w/ generic param)  |    10000000|          1020,487|               0,102|
|NProxy.Core|    v 2.0|Proxy generation                      |        1000|          1352,218|            1352,218|
|NProxy.Core|    v 2.0|Proxy generation (w/ generic param)   |        1000|          1358,776|            1358,776|
|NProxy.Core|    v 2.0|Proxy instantiation                   |     1000000|          1501,998|               1,502|
|NProxy.Core|    v 2.0|Proxy instantiation (w/ generic param)|     1000000|          1486,243|               1,486|
|Regular    |      n/a|Method invocation                     |   100000000|           258,134|               0,003|
|Regular    |      n/a|Method invocation (w/ generic param)  |   100000000|          1217,085|               0,012|

All tests have been performed under Microsoft .NET 4.0.30319 and can be found [here](https://github.com/mtamme/NProxy/tree/master/Source/Test/NProxy.Core.Benchmark/).

## Conclusion

From performance point of view NProxy beats the other dynamic proxy libraries in almost all scenarios. Only LinFu performs better
in terms of proxy instantiation. The main reason why LinFu performs better is that LinFu only supports the instantiation of class proxies
with a default constructor and the LinFu interceptor is not injected via a constructor but a property inside LinFu.