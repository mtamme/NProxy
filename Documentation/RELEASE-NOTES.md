# Release notes

## Version 2.2.1

* Fixed fluent proxy creation.

## Version 2.2.0

* Fixed implemented interfaces returned by `IProxyTemplate`.
* Exposed parent type via `IProxyTemplate` interface.
* Exposed `IInterceptionFilter` interface.
* Only offer optional members to `IInterceptionFilter`.

## Version 2.1.0

* Reduced generated IL code size.
* Exposed additional metadata via `IProxyTemplate` interface.
* Changed dynamic type naming.

## Version 2.0.1

* Fixed documentation.

## Version 2.0.0

* Changed target framework to .NET 3.5.
* Separated proxy creation from proxy instantiation.
* Exposed interception metadata via `IProxyTemplate` interface.
* Removed obsolete proxy adaption method.

## Version 1.2.1

* Improved proxy creation performance.
* Fixed naming for explicit interface implementation.
* Fixed generic type method identification.
* Fixed proxy type identification.

## Version 1.2.0

* Renamed proxy interface adaption method.
* Fixed method identification inside interceptor implementation.
* Improved cache lookup performance.

## Version 1.1.0

* Added null value checks for target objects.
* Simplified IProxyFactory interface.

## Version 1.0.4

* Added license files to NuGet package.
* Fixed NuGet metadata.

## Version 1.0.3

* Fixed NuGet metadata.

## Version 1.0.2

* Fixed NuGet metadata.

## Version 1.0.1

* Fixed NuGet metadata.

## Version 1.0.0

* Initial publication.
