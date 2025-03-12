# Bootstrap Blazor Component

## an enterprise-level UI component library based on Bootstrap and Blazor.

This project is part of the [.NET Foundation](https://www.dotnetfoundation.org/) and operates under their [Code Of Conduct](https://www.dotnetfoundation.org/code-of-conduct)

[![License](https://img.shields.io/github/license/dotnetcore/bootstrapblazor.svg?logo=git&logoColor=red&color=)](https://github.com/dotnetcore/BootstrapBlazor/blob/main/LICENSE)
[![Github build](https://img.shields.io/github/actions/workflow/status/dotnetcore/BootstrapBlazor/build.yml?branch=main&?label=main&logo=github)](https://github.com/dotnetcore/BootstrapBlazor/actions?query=workflow%3A%22Build+Project%22+branch%3Amain)
[![Repo Size](https://img.shields.io/github/repo-size/dotnetcore/BootstrapBlazor.svg?logo=github&logoColor=green&label=repo)](https://github.com/dotnetcore/BootstrapBlazor)
[![Commit Date](https://img.shields.io/github/last-commit/dotnetcore/BootstrapBlazor/main.svg?logo=github&logoColor=green&label=commit)](https://github.com/dotnetcore/BootstrapBlazor)
[![codecov](https://codecov.io/gh/dotnetcore/BootstrapBlazor/branch/main/graph/badge.svg?token=5SXIWHXZC3)](https://codecov.io/gh/dotnetcore/BootstrapBlazor)

---

## Online Examples
[![website](https://img.shields.io/badge/online-https://www.blazor.zone-success.svg?&logo=buzzfeed&logoColor=green)](https://www.blazor.zone)

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

## Code of conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
