# DotNet.Web.Components

## NOTES
A new [DotNet.Web.JsInterop.Modules](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Modules/) library
has been created that replaces the `GeolocationService`, `JSLoggerService` and `ResizeObserverService`.

## Summary
C#  Components, classes, and extensions I find useful in my .NET web-based projects.

## Demo
A demo of this, and other of my `DotNet` packages, can be found [here](https://github.com/marqdouj/dotnet.demo).

## Features
**Classes**
  - **UI Models**
    -  Classes for UI presentation and element binding.
       Implements a `BindValue` string property for non-string values
       where binding requires a string. Designed for use with Blazor @bind-Value.
  - `ValueTypeStringValue<T>`. Handles conversion between value types and their string representations, supporting nullable value types.
  - `XmlDocReader`. Provides functionality to read and extract XML documentation comments from an assembly's XML documentation file.
  - `XmlUIModel`. Wrapper to UIModel that works with the `XmlDocReader` to update comments.

**Components**
  - **SlideShow**: Displays a list of images (i.e. Banner/Carousel).

**Enums**
  - `HtmlColorName`. The basic 140 named colors.
    - `HtmlColorNameListItem`. View model for a HtmlColorName.
    - `HtmlColorNameCollection`. Manages a collection of view models.
    - `Extensions`.
      - `HtmlColorName.ToHex()`. Returns the Hex string for the HtmlColorName.

**Extensions**
  - `Css`
    - `Guid.ToCssId()`. Creates a valid css identifier from a Guid.
  - `ModelExtensions`.
    - `GetDisplayName()/GetDisplayAttribute()`. Gets the display attribute/name for MemberInfo, Enum, PropertyInfo, and Type.

 **Services**
   - ### These services have been marked as Obsolete and are no longer maintained. See [DotNet.Web.JsInterop.Modules](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Modules/).

   - `GeolocationService`. 
   - `JSLoggerService`. 
   - `ResizeObserverService`. 

## Release Notes
- `10.5.0`
  - `ModelExtensions`. Update extensions; add support for MemberInfo and Type.

- `10.4.0`
  - `ModelExtensions`. New static class.
  - `UIModelValue`. Automatically resolves NameAlias, using Display attribute name, in constructor.

- `10.3.0`
  - `ValueTypeStringValue<T>`. New class.
  - Update NuGet packages.

- `10.2.1`
  - `Obsolete Services`. These items have been marked as Obsolete and are no longer maintained.
    Replaced by [DotNet.Web.JsInterop.Modules](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Modules/).
    - `JSLoggerService`.

- `10.2.0`
  - `Obsolete Services`. These items have been marked as Obsolete and are no longer maintained.
    Replaced by [DotNet.Web.JsInterop.Modules](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Modules/).
    - `GeolocationService`.
    - `ResizeObserverService`.
  - Update NuGet packages.

- `10.1.2` - `10.1.1`
  - Update NuGet packages.

- `10.1.0`
  - `UIModelValue.BindValueFlags (New)`. Applies to `BindValue.Set`.
    - `None`. No change to processing value.
    - `UseDefaultSetValueForNull`. Specifies that value types should use the Property.SetValue(null) method for null values.
    - `TreatEmptyStringAsNullForValueTypes`. Specifies that empty string values should be treated as null when converting to value types.

- `10.0.0`
  - Initial release.
