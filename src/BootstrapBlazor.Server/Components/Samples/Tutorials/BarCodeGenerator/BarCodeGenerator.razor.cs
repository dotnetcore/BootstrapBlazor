// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// BarCodeGenerator 组件示例代码
/// </summary>
public partial class BarCodeGenerator
{
    private string _activeContentType = "Text";

    private readonly List<string> _contents = ["Text", "Url", "Wi-Fi", "Email"];

    private readonly WiFiContent _wifiContent = new();

    private readonly EmailContent _emailContent = new();

    private readonly TextContent _textContent = new();

    private readonly TextContent _urlContent = new();

    private string? _content;

    private string _desc => _activeContentType switch
    {
        "Text" => Localizer["TextDesc"],
        "Url" => Localizer["UrlDesc"],
        "Wi-Fi" => Localizer["WiFiDesc"],
        "Email" => Localizer["EmailDesc"],
        _ => string.Empty
    };

    private string? GetItemClassString(string item) => CssBuilder.Default("bc-type-item")
        .AddClass("active", _activeContentType == item)
        .Build();

    private void OnActiveType(string item)
    {
        _activeContentType = item;
    }

    private Task OnTextSubmit(EditContext context)
    {
        _content = _textContent.ToString();
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUrlSubmit(EditContext context)
    {
        _content = _urlContent.ToString();
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnWiFiSubmit(EditContext context)
    {
        _content = _wifiContent.ToString();
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnEmailSubmit(EditContext context)
    {
        _content = _emailContent.ToString();
        StateHasChanged();
        return Task.CompletedTask;
    }
}
