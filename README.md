# NProxy

NProxy is a library for the .NET framework to create lightweight dynamic proxies.

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
proxy. The invocation handler is passed a target object, a `MethodInfo` object (from the `System.Reflection` namespace)
and an array of parameters; in the simplest case, it could simply call the method `MethodInfo.Invoke()` and return the
result. `MethodInfo.Invoke()` directly invokes the target method without utilizing reflection.

Every proxy has an associated invocation handler that is called whenever one of the proxy's methods is called.
Proxy objects can be created from unsealed classes, abstract classes, interfaces and delegates, and can implement
an arbitrary number of interfaces. All interfaces are implemented explicitly to avoid member name conflicts.

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

    bool CanProceed { get; }

    object Proceed();
}
```

The `IInvocationTarget` interface enables the functionality to delegate invocations to different target objects at runtime.

```csharp
public interface IInvocationTarget
{
    object GetTarget(Type declaringType);
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

## Performance

Looking at the worst-case scenario where each proxy creation generates a new proxy type the main bottleneck is currently the `System.Reflection.Emit`
namespace. The functions with the most individual work are shown below.

![Overview](https://raw.github.com/mtamme/NProxy/master/Documentation/WithoutProxyTypeCache.png "Without proxy type cache")

Proxy types are cached by default so under normal circumstances this scenario does not apply. Enabling proxy type caching results in the following list
of functions with the most individual work.

![Overview](https://raw.github.com/mtamme/NProxy/master/Documentation/WithProxyTypeCache.png "With proxy type cache")

# Copyright

Copyright © Martin Tamme. See COPYING and COPYING.LESSER for details.
