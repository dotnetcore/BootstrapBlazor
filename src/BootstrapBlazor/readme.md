# Bootstrap Blazor Component

## A set of enterprise-class UI components based on Bootstrap and Blazor.

[![License](https://img.shields.io/github/license/argozhang/bootstrapblazor.svg?logo=git&logoColor=red)](https://github.com/ArgoZhang/BootstrapBlazor/blob/main/LICENSE)
[![Github build](https://img.shields.io/github/workflow/status/ArgoZhang/BootstrapBlazor/Build%20Project/main?label=main&logo=github&logoColor=green)](https://github.com/ArgoZhang/BootstrapBlazor/actions?query=workflow%3A%22Build+Project%22+branch%3Amain)
[![Repo Size](https://img.shields.io/github/repo-size/ArgoZhang/BootstrapBlazor.svg?logo=github&logoColor=green&label=repo)](https://github.com/ArgoZhang/BootstrapBlazor)
[![Commit Date](https://img.shields.io/github/last-commit/ArgoZhang/BootstrapBlazor/main.svg?logo=github&logoColor=green&label=commit)](https://github.com/ArgoZhang/BootstrapBlazor)
[![codecov](https://codecov.io/gh/dotnetcore/BootstrapBlazor/branch/main/graph/badge.svg?token=5SXIWHXZC3)](https://codecov.io/gh/dotnetcore/BootstrapBlazor)

---

## Features
- Enterprise-class UI designed for web applications.
- A set of high-quality Blazor components out of the box.
- Supports WebAssembly-based client-side and SignalR-based server-side UI event interaction.
- Supports Progressive Web Applications (PWA).
- Build with C#, a multi-paradigm static language for an efficient development experience.
- .NET Standard 2.1 based, with direct reference to the rich .NET ecosystem.
- Supports NET5. (Server-Side, WASM)
- Seamless integration with existing ASP.NET Core MVC and Razor Pages projects.

## Online Examples
[![website](https://img.shields.io/badge/China-https://www.blazor.zone-success.svg?color=red&logo=buzzfeed&logoColor=red)](https://www.blazor.zone)
[![website](https://img.shields.io/badge/Github-https://argozhang.github.io-success.svg?logo=buzzfeed&logoColor=green)](https://argozhang.github.io)

## Installation Guide

- Install .net core sdk [Offical website](https://dotnet.microsoft.com/download)
- Install Visual Studio 2019 lastest [Offical website](https://visualstudio.microsoft.com/vs/getting-started/)

## Create a new project from the dotnet new template

1. Install the template

`dotnet new -i Bootstrap.Blazor.Templates::*`

2. Create the Boilerplate project with the template

`dotnet new bbapp`

## Install Bootstrap Blazor Project Template

1. Download Project Template

Microsoft Market [link](https://marketplace.visualstudio.com/items?itemName=Longbow.BootstrapBlazorUITemplate)

2. Double Click **BootstrapBlazor.UITemplate.vsix**

## Import Bootstrap Blazor into an existing project

1. Go to the project folder of the application and install the Nuget package reference

`dotnet add package BootstrapBlazor`

2.  **Add** the `stylesheet` `javascripts` file to your main index file - `Pages/_Host.cshtml (Server)` or `wwwroot/index.html (WebAssembly)`

 **HTML**

```
<!DOCTYPE html>
<html lang="en">
<head>
    . . .
    <link rel="stylesheet" href="_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css">
</head>
<body>
    . . .
    <script src="_framework/blazor.server.js"></script>
    <script src="_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js"></script>
</body>
</html>
```

3. Open the `~/Startup.cs` file in the and register the `Bootstrap Blazor` service:

 **C#**

```
namespace BootstrapBlazorAppName
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //more code may be present here
            services.AddBootstrapBlazor();
        }

        //more code may be present here
    }
}
```

## Visual Studio Integration

To create a new `Bootstrap Blazor` UI for Blazor application, use the Create New Project Wizard. The wizard detects all installed versions of `Bootstrap Blazor` for Blazor and lists them in the Version combobox—this enables you to start your project with the desired version. You can also get the latest version to make sure you are up to date.

1. Get the Wizard

To use the Create New Project Wizard, install the `Bootstrap Blazor` UI for Blazor Visual Studio Extensions. You can get it from the:

- Visual Studio Marketplace (for Windows)

2. Start the Wizard

To start the wizard, use either of the following approaches

### Using the Project menu:

- Click File > New > Project.
- Find and click the C# Blazor Application option (you can use the search, or filter by Blazor templates).
- Follow the wizard.
