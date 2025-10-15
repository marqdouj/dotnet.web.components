# dotnet.web.components

> NOTE: This is a new repository and is released as a Preview version.

## Summary
C#  Components, classes, and extensions I find useful in my .NET web-based projects.

## Demo
A demo of all my `DotNet` packages can be found [here](https://github.com/marqdouj/dotnet.demo).

## Features

**Components**
  - **SlideShow**: Displays a list of images (i.e. Banner/Carousel).

**Extensions**
  - `Css`
    - `Guid.ToCssId()`. Creates a valid css identifier from a Guid.

**Enums**
  - `HtmlColorName`. The basic 140 named colors.
    - `HtmlColorNameListItem`. View model for a HtmlColorName.
    - `HtmlColorNameCollection`. Manages a collection of view models.
    - `Extensions`.
      - `HtmlColorName.ToHex()`. Returns the Hex string for the HtmlColorName.

 **Services**
   - `GeolocationService`. 
     - Uses the Geolocation API services to get the current position.
     - Configuration:
       - `builder.Services.AddScoped<IGeolocationService, GeolocationService>();`
     - Use:
       - `@inject IGeolocationService GeolocationService`
  
   - `JSLoggerService`. 
     - Logs messages to the browser Console.
     - It is not an implementation of ILogger, but rather mimics most methods of ILogger as async ValueTask.
     - Configuration:
       - `builder.Services.AddJSLoggerService();`
     - Use:
       - `@inject IJSLoggerService LoggerService`

   - `ResizeObserverService`. 
     - Scoped service to monitor html element resizing.
     - JSInterop wrapper to the Html [ResizeObserver](https://developer.mozilla.org/en-US/docs/Web/API/ResizeObserver).
     - Configuration: 
       - `builder.Services.AddScoped<IResizeObserverService, ResizeObserverService>();`
     - Use:
       - `@inject IResizeObserverService ResizeObserver`

**Classes**
  - **UI Model**
    -  Classes for UI presentation and element binding.
       Implements a `BindValue` string property for non-string values
       where binding requires a string. Designed for use with Blazor @bind-Value.

## Release Notes
- `10.0.0-Preview-3.0`
  - `XMLComments`. Comments are now distributed with the package.

- `10.0.0-Preview-2`
  - `GeolocationService`. Added service.
  - `JSLoggerService`.  Added service

- `10.0.0-Preview`
  - Initial release.
