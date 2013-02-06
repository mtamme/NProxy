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
|Castle.Core|  v 3.1.0|Proxy generation                      |        1000|          5325.267|            5325.267|
|LinFu.Core |  v 2.3.0|Proxy generation                      |        1000|          7166.328|            7166.328|
|NProxy.Core|  v 1.2.1|Proxy generation                      |        1000|          2277.122|            2277.122|
|Castle.Core|  v 3.1.0|Proxy generation (w/ generic param)   |        1000|          5443.368|            5443.368|
|LinFu.Core |  v 2.3.0|Proxy generation (w/ generic param)   |        1000|          8324.486|            8324.486|
|NProxy.Core|  v 1.2.1|Proxy generation (w/ generic param)   |        1000|          2248.740|            2248.740|
|Castle.Core|  v 3.1.0|Proxy instantiation                   |     1000000|          6586.562|               6.587|
|LinFu.Core |  v 2.3.0|Proxy instantiation                   |     1000000|          1346.209|               1.346|
|NProxy.Core|  v 1.2.1|Proxy instantiation                   |     1000000|          4320.666|               4.321|
|Castle.Core|  v 3.1.0|Proxy instantiation (w/ generic param)|     1000000|          6601.589|               6.602|
|LinFu.Core |  v 2.3.0|Proxy instantiation (w/ generic param)|     1000000|          1281.524|               1.282|
|NProxy.Core|  v 1.2.1|Proxy instantiation (w/ generic param)|     1000000|          4207.651|               4.208|
|Castle.Core|  v 3.1.0|Method invocation                     |    10000000|          1280.738|               0.128|
|LinFu.Core |  v 2.3.0|Method invocation                     |    10000000|         20873.542|               2.087|
|NProxy.Core|  v 1.2.1|Method invocation                     |    10000000|          1093.672|               0.109|
|Regular    |      n/a|Method invocation                     |   100000000|           348.720|               0.003|
|Castle.Core|  v 3.1.0|Method invocation (w/ generic param)  |    10000000|         18656.266|               1.866|
|LinFu.Core |  v 2.3.0|Method invocation (w/ generic param)  |    10000000|         83971.767|               8.397|
|NProxy.Core|  v 1.2.1|Method invocation (w/ generic param)  |    10000000|          1436.133|               0.144|
|Regular    |      n/a|Method invocation (w/ generic param)  |   100000000|          1762.936|               0.018|

All tests have been performed under Microsoft .NET 4.0.30319 and can be found [here](https://github.com/mtamme/NProxy/tree/master/Source/Test/NProxy.Core.Benchmark/).

## Conclusion

From performance point of view NProxy beats the other dynamic proxy libraries in almost all scenarios. Only LinFu performs better
in terms of proxy instantiation.
