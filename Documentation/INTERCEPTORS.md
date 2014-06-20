# Interceptors

Interceptors provide the functionality of capturing calls to a target object at invocation time. The interception mechanism
is part of the NProxy library and can be depicted as follows.

![Overview](https://raw.github.com/mtamme/NProxy/master/Documentation/Overview.png "Overview")

At the heart of the interception mechanism is the `IInterceptor` and `IInvocationContext` interface,
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

The easiest way to add interceptors in an aspect oriented manner is to apply attributes on classes, interfaces, events, properties or methods which
implement the `IInterceptionBehavior` interface.

```csharp
public interface IInterceptionBehavior
{
    void Apply(MemberInfo memberInfo, ICollection<IInterceptor> interceptors);
	
    void Validate(MemberInfo memberInfo);
}
```
