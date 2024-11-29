﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
///
/// </summary>
public sealed partial class Cameras
{
    private string? ImageUrl { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private string? TraceOnInit { get; set; }

    [NotNull]
    private string? TraceOnError { get; set; }

    [NotNull]
    private string? TraceOnStar { get; set; }

    [NotNull]
    private string? TraceOnClose { get; set; }

    [NotNull]
    private string? TraceOnCapture { get; set; }

    private string? PlayText { get; set; }

    private string? StopText { get; set; }

    private string? PreviewText { get; set; }

    private string? SaveText { get; set; }

    private bool PlayDisabled { get; set; } = true;

    private bool StopDisabled { get; set; } = true;

    private bool CaptureDisabled { get; set; } = true;

    private List<SelectedItem> Devices { get; } = [];

    private string? DeviceId { get; set; }

    private string? DeviceLabel { get; set; }

    private string? PlaceHolderString { get; set; }

    [NotNull]
    private Camera? Camera { get; set; }

    private string? ImageContentData { get; set; }

    private string? ImageStyleString => CssBuilder.Default("width: 320px; border-radius: var(--bs-border-radius);")
        .AddClass("display: none;", string.IsNullOrEmpty(ImageContentData))
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        TraceOnInit = Localizer[nameof(TraceOnInit)];
        TraceOnError = Localizer[nameof(TraceOnError)];
        TraceOnStar = Localizer[nameof(TraceOnStar)];
        TraceOnClose = Localizer[nameof(TraceOnClose)];
        TraceOnCapture = Localizer[nameof(TraceOnCapture)];
        DeviceLabel = Localizer["DeviceLabel"];
        PlaceHolderString = Localizer["InitDevicesString"];
        PlayText = Localizer["PlayText"];
        StopText = Localizer["StopText"];
        PreviewText = Localizer["PreviewText"];
        SaveText = Localizer["SaveText"];
    }

    private Task OnInit(List<DeviceItem> devices)
    {
        if (devices.Any())
        {
            Devices.AddRange(devices.Select(d => new SelectedItem(d.DeviceId, d.Label)));
            PlayDisabled = false;
        }
        else
        {
            PlaceHolderString = Localizer["NotFoundDevicesString"];
        }

        foreach (var item in devices)
        {
            Logger.Log($"{TraceOnInit} {item.Label}-{item.DeviceId}");
        }

        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task OnClickOpen()
    {
        await Camera.Open();
        PlayDisabled = true;
        StopDisabled = false;
        CaptureDisabled = false;
    }

    private async Task OnClickClose()
    {
        await Camera.Close();
        ImageContentData = null;
        PlayDisabled = false;
        StopDisabled = true;
        CaptureDisabled = true;
    }

    private async Task OnClickPreview()
    {
        ImageContentData = null;
        var stream = await Camera.Capture();
        if (stream != null)
        {
            var reader = new StreamReader(stream);
            ImageContentData = await reader.ReadToEndAsync();
            reader.Close();
        }
    }

    private Task OnClickSave() => Camera.SaveAndDownload($"capture_{DateTime.Now:hhmmss}.png");

    private Task OnApply(int width, int height) => Camera.Resize(width, height);

    private Task OnError(string err)
    {
        PlayDisabled = false;
        StopDisabled = true;
        CaptureDisabled = true;
        PlaceHolderString = Localizer["NotFoundDevicesString"];
        Logger.Log($"{TraceOnError} {err}");

        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnOpen()
    {
        ImageUrl = null;
        Logger.Log(TraceOnStar);
        return Task.CompletedTask;
    }

    private Task OnClose()
    {
        Logger.Log(TraceOnClose);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(BootstrapBlazor.Components.Camera.VideoWidth),
            Description = Localizer["VideoWidth"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "320"
        },
        new()
        {
            Name = nameof(BootstrapBlazor.Components.Camera.VideoHeight),
            Description = Localizer["VideoHeight"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "240"
        },
        new()
        {
            Name = "ShowPreview",
            Description = Localizer["ShowPreview"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "AutoStart",
            Description = Localizer["AutoStart"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DeviceLabel",
            Description = Localizer["DeviceLabel"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnInit",
            Description = Localizer["OnInit"],
            Type = "Func<IEnumerable<DeviceItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnStart",
            Description = Localizer["OnStart"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClose",
            Description = Localizer["OnClose"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BootstrapBlazor.Components.Camera.CaptureJpeg),
            Description = Localizer["CaptureJpeg"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(BootstrapBlazor.Components.Camera.Quality),
            Description = Localizer["Quality"],
            Type = "double",
            ValueList = " — ",
            DefaultValue = " 0.9d"
        }
    ];
}
