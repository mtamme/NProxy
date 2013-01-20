# NProxy

NProxy is a library for the .NET framework to create lightweight dynamic proxies.

## Latest version

To get the latest version of NProxy just add it to your project using [NuGet](http://nuget.org/packages/NProxy.Core).

```
PM> Install-Package NProxy.Core
```

## Release notes

See [here](https://github.com/mtamme/NProxy/blob/master/Documentation/RELEASE-NOTES.md) for details.

## Motivation

There are already a few libraries out there which deal with dynamic proxy generation so why another dynamic proxy library?
The answers which lead to the goals of the NProxy project can be summarized as follows:

* Provide an API to generate dynamic proxies based upon unsealed classes, abstract classes, interfaces and delegates in a unified way.
* Focus on a slim implementation which can be easily extended.
* Treat open generic methods not as aliens and massively improve their invocation performance.
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
    object Invoke(object proxy, MethodInfo methodInfo, object[] parameters);
}
```

The job of an invocation handler is to actually perform the requested method invocation on behalf of a dynamic
proxy. The invocation handler is passed a proxy object, a `MethodInfo` object (from the `System.Reflection` namespace)
and an array of parameters; in the simplest case, it could simply call the method `MethodInfo.Invoke()` and return the
result. `MethodInfo.Invoke()` directly invokes the target method without utilizing reflection.

Every proxy has an associated invocation handler that is called whenever one of the proxy's methods is called.
Proxy objects can be created from unsealed classes, abstract classes, interfaces and delegates, and can implement
an arbitrary number of interfaces. All interfaces are implemented explicitly to avoid member name conflicts.

To exclude events, properties and methods from beeing intercepted just apply the `NonInterceptedAttribute` on the
member which should not be intercepted. This attribute has no effect when applied to abstract members.

To create dynamic proxies of only internally visible types, just add the following assembly attribute to your project.

```csharp
[assembly: InternalsVisibleTo("NProxy.Dynamic")]
```

## Interceptors

Interceptors provide the functionality of capturing calls to a target object at invocation time. The interception mechanism
is part of the NProxy library and can be depicted as follows.

![Overview](https://raw.github.com/mtamme/NProxy/master/Documentation/Overview.png "Overview")

At the heart of the interception mechanism is the `IInterceptor`, `IInvocationContext` and `IInvocationTarget` interface,
shown below.

```csharp
public interface IInterceptor
{
    object Intercept(IInvocationContext invocationContext);
}
```

The `IInvocationContext` interface provides essential context information about the current invocation.

```csharp
public interface IInvocationContext
{
    object Target { get; }

    MethodInfo Method { get; }

    object[] Parameters { get; }

    object Proceed();
}
```

The `IInvocationTarget` interface enables the functionality to delegate invocations to different target objects at runtime.

```csharp
public interface IInvocationTarget
{
    object GetTarget(MethodInfo methodInfo);
}
```

The easiest way to add interceptors in an aspect oriented manner is to apply attributes on classes, interfaces, events, properties or methods which
implement the `IInterceptionBehavior` interface.

```csharp
public interface IInterceptionBehavior
{
    void Apply(MemberInfo memberInfo, ICollection<IInterceptor> interceptors);
	
	void Validate(MemberInfo memberInfo);
}
```

## Examples

See [here](https://github.com/mtamme/NProxy/blob/master/Documentation/EXAMPLES.md) for details.

## Performance

See [here](https://github.com/mtamme/NProxy/blob/master/Documentation/PERFORMANCE.md) for details.

## Building NProxy

See [here](https://github.com/mtamme/NProxy/blob/master/Documentation/BUILD.md) for details.

# Copyright

Copyright © Martin Tamme. See COPYING and COPYING.LESSER for details.
