# Performance

Looking at the worst-case scenario where each proxy creation generates a new proxy type the main bottleneck is currently the `System.Reflection.Emit`
namespace. The functions with the most individual work are shown below.

![Overview](https://raw.github.com/mtamme/NProxy/master/Documentation/WithoutProxyTypeCache.png "Without proxy type cache")

Proxy types are cached by default so under normal circumstances this scenario does not apply. Enabling proxy type caching results in the following list
of functions with the most individual work.

![Overview](https://raw.github.com/mtamme/NProxy/master/Documentation/WithProxyTypeCache.png "With proxy type cache")

The follwoing table shows a performance comparison between the most popular dynamic proxy libraries and NProxy.

| Library     | Version | Test                | Iterations | Total Time in ms | Average Time in µs |
|:------------|--------:|:--------------------|-----------:|-----------------:|-------------------:|
| Castle.Core |   3.1.0 | CreateProxy         |    1000000 |         5929.160 |              5.929 |
| LinFu.Core  |   2.3.0 | CreateProxy         |    1000000 |         2006.009 |              2.006 |
| NProxy.Core |   1.2.1 | CreateProxy         |    1000000 |         3843.029 |              3.843 |
| Castle.Core |   3.1.0 | InvokeIntMethod     |   10000000 |          869.915 |              0.087 |
| LinFu.Core  |   2.3.0 | InvokeIntMethod     |   10000000 |        25743.463 |              2.574 |
| NProxy.Core |   1.2.1 | InvokeIntMethod     |   10000000 |          824.943 |              0.082 |
| Castle.Core |   3.1.0 | InvokeGenericMethod |   10000000 |        18471.089 |              1.847 |
| LinFu.Core  |   2.3.0 | InvokeGenericMethod |   10000000 |        76997.030 |              7.700 |
| NProxy.Core |   1.2.1 | InvokeGenericMethod |   10000000 |         1013.833 |              0.101 |

Tests have been performed under .NET 4.0.30319 and can be found [here](https://github.com/mtamme/NProxy/tree/master/Source/Test/NProxy.Core.Test/Performance).
