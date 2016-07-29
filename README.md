# NProxy
https://ci.appveyor.com/api/projects/status/github/mtamme/NProxy?branch=master&svg=true

NProxy is a library for the .NET framework to create lightweight dynamic proxies.

## Motivation

There are already a few libraries out there which deal with dynamic proxy generation so why another dynamic proxy library?
The answers which lead to the goals of the NProxy project can be summarized as follows:

* Provide an API to generate dynamic proxies based upon unsealed classes, abstract classes, interfaces and delegates in a unified way.
* General focus on quality attributes like performance, extensibility and lightweightness.
* Treat generic methods not as aliens and massively improve their invocation performance.
* Support the invocation of intercepted base methods.
* Make a library available which can be used as a base for AOP frameworks, mocking libraries, ...
* Dynamic proxy creation must be thread-safe.

## Dynamic proxies

Dynamic proxies provide an alternate, dynamic mechanism for implementing many common design patterns,
including the Facade, Bridge, Interceptor, Decorator, Proxy, and Adapter patterns. While all of these
patterns can be easily implemented using ordinary classes instead of dynamic proxies, in many cases the
dynamic proxy approach is more convenient and compact and can eliminate a lot of handwritten or generated
classes.

At the heart of the dynamic proxy mechanism is the `IInvocationHandler` interface, shown below.

```csharp
public interface IInvocationHandler
{
    object Invoke(object target, MethodInfo methodInfo, object[] parameters);
}
```

The job of an invocation handler is to actually perform the requested method invocation on behalf of a dynamic
proxy. The invocation handler is passed a target object, a `MethodInfo` object (from the `System.Reflection` namespace)
and an array of parameters; in the simplest case, it could simply call the method `MethodInfo.Invoke()` and return the
result. `MethodInfo.Invoke()` directly invokes the target method without utilizing reflection.

Every proxy has an associated invocation handler that is called whenever one of the proxy's methods is called.
Proxies can be created from unsealed classes, abstract classes, interfaces and delegates, and can implement
an arbitrary number of interfaces. All interfaces are implemented explicitly to avoid member name conflicts.

There are some known limitation when it comes to event, property or method interception. For class based proxies
only non-static and only abstract or virtual members can be intercepted.

To exclude events, properties and methods from beeing intercepted just apply the `NonInterceptedAttribute` on the
member which should not be intercepted. This attribute has no effect when applied to abstract members.
You can also implement your own `IInterceptionFilter` if you need full control about which member is going to be
intercepted.

```csharp
public interface IInterceptionFilter
{
    bool AcceptEvent(EventInfo eventInfo);

    bool AcceptProperty(PropertyInfo propertyInfo);

    bool AcceptMethod(MethodInfo methodInfo);
}
```

To create dynamic proxies of only internally visible types, just add the following assembly attribute to your project.

```csharp
[assembly: InternalsVisibleTo("NProxy.Dynamic, PublicKey=002400000480000094000000060200000024000052534131000400000100010031d0e185f342141fb582a63c5c3706ee107a49b7c4c988587512e9cf2d02473280bd9d5cf129d118978bb753339b1819c5f836a0940a0c3ec153ccad71b4786a388da0b4b9531b405d57ce00ac02ee019001eb1bcfdaa0afa1d1542adec526e1165ce740dd2d31ad682c4c8d9b305bc64c3ebb029dffa773d1f9e0e9a5847885")]
```

## Latest version

To get the latest version of NProxy just add it to your project using [NuGet](http://nuget.org/packages/NProxy.Core).

```
PM> Install-Package NProxy.Core
```

Detailed release notes can be found [here](https://github.com/mtamme/NProxy/blob/master/Documentation/RELEASE-NOTES.md).

## Examples

See [here](https://github.com/mtamme/NProxy/blob/master/Documentation/EXAMPLES.md) for details.

## Benchmarks

See [here](https://github.com/mtamme/NProxy/blob/master/Documentation/BENCHMARKS.md) for details.

# Copyright

Copyright © Martin Tamme. See LICENSE for details.
