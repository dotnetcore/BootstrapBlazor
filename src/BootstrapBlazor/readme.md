# Bootstrap Blazor Component

## an enterprise-level UI component library based on Bootstrap and Blazor.

<h4>This project is part of the <a href="https://www.dotnetfoundation.org/">.NET Foundation</a> and operates under their <a href="https://www.dotnetfoundation.org/code-of-conduct">code of conduct</a>. </h4>

[![License](https://img.shields.io/github/license/dotnetcore/bootstrapblazor.svg?logo=git&logoColor=red)](https://github.com/dotnetcore/BootstrapBlazor/blob/main/LICENSE)
[![Github build](https://img.shields.io/github/actions/workflow/status/dotnetcore/BootstrapBlazor/build.yml?branch=main&?label=main&logo=github)](https://github.com/dotnetcore/BootstrapBlazor/actions?query=workflow%3A%22Build+Project%22+branch%3Amain)
[![Repo Size](https://img.shields.io/github/repo-size/dotnetcore/BootstrapBlazor.svg?logo=github&logoColor=green&label=repo)](https://github.com/dotnetcore/BootstrapBlazor)
[![Commit Date](https://img.shields.io/github/last-commit/dotnetcore/BootstrapBlazor/main.svg?logo=github&logoColor=green&label=commit)](https://github.com/dotnetcore/BootstrapBlazor)
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
[![website](https://img.shields.io/badge/China-https://www.blazor.zone-success.svg?color=blue&logo=buzzfeed&logoColor=green)](https://www.blazor.zone)

## Installation Guide

- Install .net core sdk [Official website](https://dotnet.microsoft.com/download?wt.mc_id=DT-MVP-5004174)
- Install Visual Studio latest [Official website](https://visualstudio.microsoft.com/vs/getting-started?wt.mc_id=DT-MVP-5004174)

```shell
git clone https://github.com/dotnetcore/BootstrapBlazor.git
cd BootstrapBlazor/src/BootstrapBlazor.Server
dotnet run
```

## Create a new project from the dotnet new template

1. Install the template

`dotnet new install Bootstrap.Blazor.Templates::*`

2. Create the Boilerplate project with the template

`dotnet new bbapp`

## Install Bootstrap Blazor Project Template

1. Download Project Template

Microsoft Market [link](https://marketplace.visualstudio.com/items?itemName=Longbow.BootstrapBlazorUITemplate&wt.mc_id=DT-MVP-5004174)

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
    <script src="_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js"></script>
</body>
</html>
```

3. Open the `Program.cs` file in the and register the `Bootstrap Blazor` service:

 **C#**

```
builder.Services.AddBootstrapBlazorServices();
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

## Code of conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
