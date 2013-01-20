# Dynamic proxies

To demonstrate the basic dynamic proxy functionality lets assume we have a class named `Foo` which will present the base class for our new proxy.

```csharp
public class Foo
{
    public virtual void Bar()
    {
    }
}
```

For simplicity we create an `IInvocationHandler` which just logs all invocations to the console.

```csharp
public sealed class LoggingInvocationHandler : IInvocationHandler
{
    #region IInvocationHandler Members

    public object Invoke(object proxy, MethodInfo methodInfo, object[] parameters)
    {
        Console.WriteLine("Before invoke: {0}", methodInfo);

        try
        {
            return methodInfo.Invoke(proxy, parameters);
        }
        finally
        {
            Console.WriteLine("After invoke: {0}", methodInfo);
        }
    }
 
    #endregion
}
```

Finally we instantiate a new instance of the `ProxyFactory` class and create a new dynamic proxy by calling the `CreateProxy` method.

```csharp
var proxyFactory = new ProxyFactory();
var foo = proxyFactory.CreateProxy<Foo>(Type.EmptyTypes, new LoggingInvocationHandler());

foo.Bar();
```

# Interceptors

To demonstrate the interception functionality lets assume we want to implement a lazy loading mechanism in an aspect oriented manner.

```csharp
public interface IPerson
{
    [Lazy]
    string Name { get; set; }
}

public sealed class Person : IPerson
{
    #region IPerson Members

    public string Name { get; set; }

    #endregion
}
```

The `Name` property is annotated with a custom attribute which is defined as follows.

```csharp
[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public sealed class LazyAttribute : Attribute, IInterceptionBehavior
{
    #region IInterceptionBehavior Members

    public void Apply(MemberInfo memberInfo, ICollection<IInterceptor> interceptors)
    {
        interceptors.Add(LazyInterceptor.Instance);
    }

    public void Validate(MemberInfo memberInfo)
    {
    }

    #endregion
}
```

The `IInterceptor` which actually implements the lazy loading mechanism is shown below.

```csharp
internal sealed class LazyInterceptor : IInterceptor
{
    public static readonly IInterceptor Instance = new LazyInterceptor();

    #region IInterceptor Members

    public object Intercept(IInvocationContext invocationContext)
    {
        var lazy = invocationContext.Target as ILazy;

        if (lazy != null)
        {
            if (!lazy.Loaded)
            {
                lazy.Loaded = true;

                // Perform lazy loading...
            }
        }

        return invocationContext.Proceed();
    }

    #endregion
}
```

To remember if an object was already loaded we introduce a mixin which implements an interface named `ILazy`.

```csharp
internal interface ILazy
{
    bool Loaded { get; set; }
}

internal sealed class LazyMixin : ILazy
{
    private bool _loaded;

    /// <summary>
    /// The Loaded property is "write-once" -
    /// after you have set it to true you cannot set
    /// it to false again
    /// </summary>
    bool ILazy.Loaded
    {
        get { return _loaded; }
        set
        {
            if (_loaded)
                return;

            _loaded = value;
        }
    }
}
```

Finally we create a proxy with our previously implemented lazy loading mechanism as follows.

```csharp
var person = proxyFactory.NewProxy<IPerson>()
                         .Extends<LazyMixin>()
                         .Targets<Person>();


```
