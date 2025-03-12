<h1 align="center">Bootstrap Blazor Component</h1>

<div align="center">
<h2>Bootstrap Blazor is an enterprise-level UI component library based on Bootstrap and Blazor.</h2>

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
## .NET Foundation
[<img align="right" src="https://github.com/dotnet-foundation/swag/blob/main/logo/dotnetfoundation_v4.png?raw=true" width="68px" />](https://dotnetfoundation.org/projects/project-detail/bootstrap-blazor)
<p>This project is part of the <a href="https://www.dotnetfoundation.org/">.NET Foundation</a> and operates under their <a href="https://www.dotnetfoundation.org/code-of-conduct">code of conduct</a>. </p>

## Online Examples
[![website](https://img.shields.io/badge/online-https://www.blazor.zone-success.svg?color=&logo=buzzfeed&logoColor=green)](https://www.blazor.zone)

## Environment

- Install .net core sdk [Official website](https://dotnet.microsoft.com/download)
- Install Visual Studio 2022 latest [Official website](https://visualstudio.microsoft.com/vs/getting-started/)

## Quick Installation Guide

### Install Package
```
dotnet add package BootstrapBlazor
```

### Add the following to `_Imports.razor`
```
@using BootstrapBlazor.Components
```

### Add the following to the `MainLayout.razor`
```html
<BootstrapBlazorRoot>
    @Body
</BootstrapBlazorRoot>
```

### Add the following to your HTML head section
it's either **index.html** or **_Layout.cshtml/_Host.cshtml/App.razor** depending on whether you're running WebAssembly or Server
```html
<link rel="stylesheet" href="_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css" />
```

### Add the following script at the end of the body
```html
<script src="_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js"></script>
```

### Add the following to the relevant sections of `Program.cs`
```csharp
builder.Services.AddBootstrapBlazor();
```

## Usage
```razor
<Display Value="@_text"></Display>
<Button Text="Button" OnClick="@ClickButton"></Button>

@code {
    private string? _text;
    private void ClickButton(MouseEventArgs e)
    {
        _text = DateTime.Now.ToString();
    }
}
```

## Install CLI Template
1. Install the template
```
dotnet new install Bootstrap.Blazor.Templates::*
```

2. Create the Boilerplate project with the template
```
dotnet new bbapp
```

## Contribution
1. Fork
2. Create Feat_xxx branch
3. Commit
4. Create Pull Request

## Code of Conduct
This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## Donate
If this project is helpful to you, please scan the QR code below for a cup of coffee.

<img src="https://raw.githubusercontent.com/ArgoZhang/Images/master/Donate/BarCode%402x.png" width="382px;" />

## Sponsor
Thanks to [JetBrains](https://jb.gg/OpenSourceSupport) for providing free open source license
<img src="https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg" width="100px" align="right" />
