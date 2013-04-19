# Performance / Design optimizations

* Cache MethodInfo constructor information instead of MethodInfo type.
* Don't use ITypeProvider for both ProxyTypeGenerator and MethodInfoTypeGenerator.
* Split ITypeDefinition into e.g. ITypeToken and ITypeReflector.
* Return metadata via ITypeBuilder e.g. IType.
* Make interception metadata available via IProxy.