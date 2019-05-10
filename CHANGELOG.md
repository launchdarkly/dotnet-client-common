# Change log

For full release notes for the projects that depend on this project, see their respective changelogs. This file describes changes only to the common code. This project adheres to [Semantic Versioning](http://semver.org).

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
