<h1 align="center">Bootstrap Blazor Component</h1>

<div align="center">
<h2>A set of enterprise-class UI components based on Bootstrap and Blazor.</h2>

[![Security Status](https://www.murphysec.com/platform3/v3/badge/1619783039836532736.svg)](https://github.com/dotnetcore/BootstrapBlazor/blob/main/LICENSE)

[![oscs](https://www.oscs1024.com/platform/badge/murphysecurity/murphysec.svg)](https://github.com/dotnetcore/BootstrapBlazor/blob/main/LICENSE)
[![License](https://img.shields.io/github/license/dotnetcore/BootstrapBlazor.svg?logo=git&logoColor=red)](https://github.com/dotnetcore/BootstrapBlazor/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/BootstrapBlazor.svg?color=red&logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Nuget](https://img.shields.io/nuget/dt/BootstrapBlazor.svg?logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Repo Size](https://img.shields.io/github/repo-size/dotnetcore/BootstrapBlazor.svg?logo=github&logoColor=green&label=repo)](https://github.com/dotnetcore/BootstrapBlazor)
[![Commit Date](https://img.shields.io/github/last-commit/dotnetcore/BootstrapBlazor/main.svg?logo=github&logoColor=green&label=commit)](https://github.com/dotnetcore/BootstrapBlazor)
[![Github build](https://img.shields.io/github/actions/workflow/status/dotnetcore/BootstrapBlazor/build.yml?branch=main&?label=main&logo=github)](https://github.com/dotnetcore/BootstrapBlazor/actions?query=workflow%3A%22Build+Project%22+branch%3Amain)
[![codecov](https://codecov.io/gh/dotnetcore/BootstrapBlazor/branch/main/graph/badge.svg?token=5SXIWHXZC3)](https://codecov.io/gh/dotnetcore/BootstrapBlazor)
</div>

English | <a href="README.zh-CN.md">中文</a>

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
- Install Visual Studio 2022 lastest [Offical website](https://visualstudio.microsoft.com/vs/getting-started/)

## Create a new project from the dotnet new template

1. Install the template

`dotnet new install Bootstrap.Blazor.Templates::*`

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

```HTML
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

```csharp
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

## Supported browsers

![chrome](https://img.shields.io/badge/chrome->%3D57-success.svg?logo=google%20chrome&logoColor=red)
![firefox](https://img.shields.io/badge/firefox->522-success.svg?logo=firefox%20browser&logoColor=red)
![edge](https://img.shields.io/badge/edge->%3D16-success.svg?logo=microsoft%20edge&logoColor=blue)
![ie](https://img.shields.io/badge/ie->%3D11-success.svg?logo=internet%20explorer&logoColor=blue)
![Safari](https://img.shields.io/badge/safari->%3D14-success.svg?logo=safari&logoColor=blue)
![Andriod](https://img.shields.io/badge/andriod->%3D4.4-success.svg?logo=android)
![oper](https://img.shields.io/badge/opera->%3D4.4-success.svg?logo=opera&logoColor=red)

### Mobile devices

![ios](https://img.shields.io/badge/ios-supported-success.svg?logo=apple&logoColor=white)
![Andriod](https://img.shields.io/badge/andriod-suported-success.svg?logo=android)
![windows](https://img.shields.io/badge/windows-suported-success.svg?logo=windows&logoColor=blue)

|                        |  **Chrome**  |  **Firefox**  |  **Safari**  |  **Android Browser & WebView**  |  **Microsoft Edge**  |
| -------                | ---------    | ---------     | ------       | -------------------------       | --------------       |
|  **iOS**               | Supported    | Supported     | Supported    | N/A                             | Supported            |
|  **Android**           | Supported    | Supported     | N/A          | Android v5.0+ supported         | Supported            |
|  **Windows 10 Mobile** | N/A          | N/A           | N/A          | N/A                             | Supported            |

### Desktop browsers

![macOS](https://img.shields.io/badge/macOS-supported-success.svg?logo=apple&logoColor=white)
![linux](https://img.shields.io/badge/linux-suported-success.svg?logo=linux&logoColor=white)
![windows](https://img.shields.io/badge/windows-suported-success.svg?logo=windows)

|         | Chrome    | Firefox   | Internet Explorer | Microsoft Edge | Opera     | Safari        |
| ------- | --------- | --------- | ----------------- | -------------- | --------- | ------------- |
| Mac     | Supported | Supported | N/A               | N/A            | Supported | Supported     |
| Linux   | Supported | Supported | N/A               | N/A            | N/A       | N/A           |
| Windows | Supported | Supported | Supported, IE11+  | Supported      | Supported | Not supported |

## Screenshots

![Toggle](https://raw.githubusercontent.com/ArgoZhang/Images/master/BootstrapBlazor/Toggle.png "Toggle.png")
![Toast](https://raw.githubusercontent.com/ArgoZhang/Images/master/BootstrapBlazor/Toast.png "Toast.png")
![Upload](https://raw.githubusercontent.com/ArgoZhang/Images/master/BootstrapBlazor/Upload.png "Upload.png")
![Upload2](https://raw.githubusercontent.com/ArgoZhang/Images/master/BootstrapBlazor/Upload2.png "Upload2.png")
![Bar](https://raw.githubusercontent.com/ArgoZhang/Images/master/BootstrapBlazor/Bar.png "Bar.png")
![Pei](https://raw.githubusercontent.com/ArgoZhang/Images/master/BootstrapBlazor/Pie.png "Pei.png")
![Doughnut](https://raw.githubusercontent.com/ArgoZhang/Images/master/BootstrapBlazor/Doughnut.png "Doughnut.png")

## Contribution

1. Fork
2. Create Feat_xxx branch
3. Commit
4. Create Pull Request

## Donate

If this project is helpful to you, please scan the QR code below for a cup of coffee.

<img src="https://raw.githubusercontent.com/ArgoZhang/Images/master/Donate/BarCode%402x.png" width="382px;" />
