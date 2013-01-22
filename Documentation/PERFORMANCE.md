# Performance

To get a picture how NProxy performs in comparison to other popular dynamic proxy libraries lets
define two interfaces to create dynamic proxies from.

```csharp
public interface ITrivial
{
    int Method(int value);
}

public interface IGeneric
{
    TValue Method<TValue>(TValue value);
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

Details to the performed test can be found in the table below.

| Library   | Version | Scenario                             | Iterations | Total time in ms | Average time in µs |
|:----------|--------:|:-------------------------------------|-----------:|-----------------:|-------------------:|
|Castle.Core|  v 3.1.0|Proxy generation                      |        1000|          4887.234|            4887.234|
|LinFu.Core |  v 2.3.0|Proxy generation                      |        1000|          6679.920|            6679.920|
|NProxy.Core|  v 1.2.1|Proxy generation                      |        1000|          2041.065|            2041.065|
|Castle.Core|  v 3.1.0|Proxy generation (w/ generic param)   |        1000|          4900.969|            4900.969|
|LinFu.Core |  v 2.3.0|Proxy generation (w/ generic param)   |        1000|          7690.109|            7690.109|
|NProxy.Core|  v 1.2.1|Proxy generation (w/ generic param)   |        1000|          2069.943|            2069.943|
|Castle.Core|  v 3.1.0|Proxy instantiation                   |     1000000|          6222.884|               6.223|
|LinFu.Core |  v 2.3.0|Proxy instantiation                   |     1000000|          1218.432|               1.218|
|NProxy.Core|  v 1.2.1|Proxy instantiation                   |     1000000|          4015.552|               4.016|
|Castle.Core|  v 3.1.0|Proxy instantiation (w/ generic param)|     1000000|          6271.498|               6.271|
|LinFu.Core |  v 2.3.0|Proxy instantiation (w/ generic param)|     1000000|          1206.713|               1.207|
|NProxy.Core|  v 1.2.1|Proxy instantiation (w/ generic param)|     1000000|          3998.120|               3.998|
|Castle.Core|  v 3.1.0|Method invocation                     |    10000000|          1171.201|               0.117|
|LinFu.Core |  v 2.3.0|Method invocation                     |    10000000|         19358.717|               1.936|
|NProxy.Core|  v 1.2.1|Method invocation                     |    10000000|          1113.396|               0.111|
|Castle.Core|  v 3.1.0|Method invocation (w/ generic param)  |    10000000|         17030.596|               1.703|
|LinFu.Core |  v 2.3.0|Method invocation (w/ generic param)  |    10000000|         78770.864|               7.877|
|NProxy.Core|  v 1.2.1|Method invocation (w/ generic param)  |    10000000|          1371.717|               0.137|

All tests have been performed under Microsoft .NET 4.0.30319 and can be found [here](https://github.com/mtamme/NProxy/tree/master/Source/Test/NProxy.Core.Test/Performance).

## Conclusion

From performance point of view NProxy beats the other dynamic proxy libraries in almost all scenarios. Only LinFu performs better
in terms of proxy instantiation. This is due to the fact that LinFu's default dynamic proxy type cache is backed by a simple `Dictionary`
which is of cause much faster but not thread-safe.
