# Change log

All notable changes to the LaunchDarkly.CommonSdk package will be documented in this file. For full release notes for the projects that depend on this project, see their respective changelogs. This file describes changes only to the common code. This project adheres to [Semantic Versioning](http://semver.org).

## [2.9.2] - 2019-11-12
### Fixed:
- `LdValue.Equals()` incorrectly returned true for object (dictionary) values that were not equal.
- Summary events incorrectly had `unknown:true` for all evaluation errors, rather than just for "flag not found" errors (bug introduced in 2.9.0, not used in any current SDK).

## [2.9.1] - 2019-11-08
### Fixed:
- Fixed an exception when serializing user custom attributes in events (bug in 2.9.0).

## [2.9.0] - 2019-11-08
### Added:
- `EvaluationReason` static methods and properties for creating reason instances.
- `LdValue` helpers for dealing with array/object values, without having to use an intermediate `List` or `Dictionary`: `BuildArray`, `BuildObject`, `Count`, `Get`.
- `LdValue.Parse()`. It is also possible to use `Newtonsoft.Json.JsonConvert` to parse or serialize `LdValue`, but since the implementation may change in the future, using the type's own methods is preferable.

### Changed:
- `EvaluationReason` properties all exist on the base class now, so for instance you do not need to cast to `RuleMatch` to get the `RuleId` property. This is in preparation for a future API change in which `EvaluationReason` will become a struct instead of a base class.

### Fixed:
- Improved memory usage and performance when processing analytics events: the SDK now encodes event data to JSON directly, instead of creating intermediate objects and serializing them via reflection.

### Deprecated:
- `EvaluationReason` subclasses. Use only the base class properties and methods to ensure compatibility with future versions.

## [2.8.0] - 2019-10-10
### Added:
- Added `LaunchDarkly.Logging.ConsoleAdapter` as a convenience for quickly enabling console logging; this is equivalent to `Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter`, but the latter is not available on some platforms.

## [2.7.0] - 2019-10-03
### Added:
- `IUserBuilder.AnonymousOptional` allows setting the `Anonymous` property to `null` (necessary for consistency with other SDKs). See note about this under Fixed.

### Fixed:
- `IUserBuilder` was incorrectly setting the user's `Anonymous` property to `null` even if it had been explicitly set to `false`. Null and false behave the same in terms of LaunchDarkly's user indexing behavior, but currently it is possible to create a feature flag rule that treats them differently. So `IUserBuilder.Anonymous(false)` now correctly sets it to `false`, just as the deprecated method `UserExtensions.WithAnonymous(false)` would.
- `LdValue.Convert.Long` was mistakenly converting to an `int` rather than a `long`. ([#32](https://github.com/launchdarkly/dotnet-sdk-common/issues/32))

## [2.6.1] - 2019-09-12
### Fixed:
- A packaging error made the `LaunchDarkly.CommonSdk.StrongName` package unusable in 2.6.0.

## [2.6.0] - 2019-09-12
### Added:
- Value type `LdValue`, to be used in place of `JToken` whenever possible.

### Changed:
- All event-related code except for public properties now uses `LdValue`.

### Removed:
- Internal helper type `ValueType`, unnecessary now because we can use `LdValue.Convert`.

## [2.5.1] - 2019-08-30
### Fixed:
- Many improvements to XML documentation comments.

## [2.5.0] - 2019-08-30
### Added:
- Internal helper types `ValueType` and `ValueTypes`.
- XML documentation comments are now included in the package for all target frameworks. Previously they were only included for .NET Standard 1.4.

### Changed:
- Internal types are now sealed.
- Changed some internal classes to structs for efficiency.

### Deprecated:
- `IBaseConfiguration` and `ICommonLdClient` interfaces.

## [2.4.0] - 2019-07-31
### Added:
- `IBaseConfiguration.EventCapacity` and `IBaseConfiguration.EventFlushInterval`.
- `UserBuilder.Key` setter.

### Deprecated:
- `IBaseConfiguration.SamplingInterval`.
- `IBaseConfiguration.EventQueueCapacity` (now a synonym for `EventCapacity`).
- `IBaseConfiguration.EventQueueFrequency` (now a synonym for `EventFlushInterval`).

## [2.3.0] - 2019-07-23
### Deprecated:
- `User` constructors.
- `User.Custom` and `User.PrivateAttributeNames` will be changed to immutable collections in the future.

## [2.2.0] - 2019-07-23
### Added:
- `User.Builder` provides a fluent builder pattern for constructing `User` objects. This is now the preferred method for building a user, rather than setting `User` properties directly or using `UserExtension` methods like `AndName()` that modify the existing user object.
- `User.IPAddress` is equivalent to `User.IpAddress`, but has the standard .NET capitalization for two-letter acronyms.

### Deprecated:
- `User.IpAddress` (use `IPAddress`).
- All `UserExtension` methods are now deprecated. The setters for all `User` properties should also be considered deprecated, although C# does not allow these to be marked with `[Obsolete]`.

## [2.1.2] - 2019-05-10
### Fixed:
- Fixed a build error that caused classes to be omitted from `LaunchDarkly.CommonSdk.StrongName`.

## [2.1.1] - 2019-05-10
### Changed:
- The package and assembly name are now `LaunchDarkly.CommonSdk`, and the `InternalsVisibleTo` directives now refer to `LaunchDarkly.ServerSdk` and `LaunchDarkly.XamarinSdk`. There are no other changes. All future releases of the LaunchDarkly server-side .NET SDK and client-side Xamarin SDK will use the new package names, and no further updates of the old `LaunchDarkly.Common` package will be published.

## [2.1.0] - 2019-04-16
### Added:
- Added support for planned future LaunchDarkly features related to analytics events and experimentation (metric values).

## [2.0.0] - 2019-03-26
### Added:
- Added support for planned future LaunchDarkly features related to analytics events and experimentation.
- It is now possible to deserialize evaluation reasons from JSON (this is used by the Xamarin client).

### Changed:
- The `IFlagEventProperties` interface was extended and modified to support the aforementioned features.

### Fixed:
- Under some circumstances, a `CancellationTokenSource` might not be disposed of after making an HTTP request, which could cause a timer object to be leaked.

## [1.2.3] - 2018-01-14
### Fixed:
- The assemblies in this package now have Authenticode signatures.

## [1.2.2] - 2018-01-09

This release was an error. It works, but there are no changes from 1.2.1 except for using a newer version of `dotnet-eventsource`, which was also an unintended re-release of the previous version.

## [1.2.1] - 2018-12-17

### Changed
The only changes in this version are to the build:

- What is published to NuGet is now the Release configuration, without debug information.
- The Debug configuration (the default) no longer performs strong-name signing. This makes local development easier.
- `LaunchDarkly.Common` now has an `InternalsVisibleTo` directive for an _unsigned_ version of the `LaunchDarkly.Client` unit tests. Again this is to support local development, since the client will be unsigned by default as well.

## [1.2.0] - 2018-10-24

### Changed
- The non-strong-named version of this library (`LaunchDarkly.Common`) can now be used with a non-strong-named version of `LaunchDarkly.Client`, which does not normally exist but could be built as part of a fork of the SDK.

- Previously, the delay before stream reconnect attempts would increase exponentially only if the previous connection could not be made at all or returned an HTTP error; if it received an HTTP 200 status, the delay would be reset to the minimum even if the connection then immediately failed. Now, if the stream connection fails after it has been up for less than a minute, the reconnect delay will continue to increase. (changed in `LaunchDarkly.EventSource` 3.2.0)

### Fixed

- Fixed an [unobserved exception](https://blogs.msdn.microsoft.com/pfxteam/2011/09/28/task-exception-handling-in-net-4-5/) that could occur following a stream timeout, which could cause a crash in .NET 4.0. (fixed in `LaunchDarkly.EventSource` 3.2.0)

- A `NullReferenceException` could sometimes be logged if a stream connection failed. (fixed in `LaunchDarkly.EventSource` 3.2.0)

## [1.1.1] - 2018-08-29

Incorporates the fix from 1.0.6 that was not included in 1.1.0.

## [1.1.0] - 2018-08-22

### Added
- New `EvaluationDetail` and `EvaluationReason` classes will be used in future SDK versions that support capturing evaluation reasons.

## [1.0.6] - 2018-08-30

### Fixed
- Updated LaunchDarkly.EventSource to fix a bug that prevented the client from reconnecting to the stream if it received an HTTP error status from the server (as opposed to simply losing the connection).

## [1.0.5] - 2018-08-14

### Fixed
- The reconnection attempt counter is no longer shared among all StreamManager instances. Previously, if you connected to more than one stream, all but the first would behave as if they were reconnecting and would have a backoff delay.

## [1.0.4] - 2018-08-02

### Changed
- Updated the dependency on `LaunchDarkly.EventSource`, which no longer has package references to System assemblies.

## [1.0.3] - 2018-07-27

### Changed
- The package `LaunchDarkly.Common` is no longer strong-named. Instead, we are now building two packages: `LaunchDarkly.Common` and `LaunchDarkly.Common.StrongName`. This is because the Xamarin project requires an unsigned version of the package, whereas the main .NET SDK uses the signed one.
- The project now uses a framework reference (`Reference`) instead of a package reference (`PackageReference`) to refer to `System.Net.Http`. An unnecessary reference to `System.Runtime` was removed.
- The stream processor now propagates an exception out of its initialization `Task` if it encounters an unrecoverable error.

## [1.0.2] - 2018-07-24

''This release is broken and should not be used.''

## [1.0.1] - 2018-07-02

### Changed
- When targeting .NET 4.5, the dependency on `Newtonsoft.Json` now has a minimum version of 6.0.1 rather than 9.0.1. This should not affect any applications that specify a higher version for this assembly.

### Removed
- The `Identify` method is no longer part of `ILdCommonClient`, since it does not have the same signature in the Xamarin client as in the server-side .NET SDK.

## [1.0.0] - 2018-06-26

Initial release, corresponding to .net-client version 5.1.0.
