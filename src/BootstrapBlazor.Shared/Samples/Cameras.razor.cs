// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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
    }

    private Task OnInit(IEnumerable<DeviceItem> devices)
    {
        var cams = string.Join("", devices.Select(i => i.Label));
        Logger.Log($"{TraceOnInit} {cams}");
        return Task.CompletedTask;
    }

    private Task OnError(string err)
    {
        Logger.Log($"{TraceOnError} {err}");
        return Task.CompletedTask;
    }

    private Task OnStart()
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

    private Task OnCapture(string url)
    {
        ImageUrl = url;
        Logger.Log(TraceOnCapture);
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new() {
            Name = nameof(Camera.VideoWidth),
            Description = Localizer["VideoWidth"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "320"
        },
        new() {
            Name = nameof(Camera.VideoHeight),
            Description = Localizer["VideoHeight"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "240"
        },
        new() {
            Name = "ShowPreview",
            Description = Localizer["ShowPreview"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "AutoStart",
            Description = Localizer["AutoStart"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "DeviceLabel",
            Description = Localizer["DeviceLabel"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "FrontText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "BackText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "PlayText",
            Description = "",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "StopText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "PhotoText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "InitDevicesString",
            Description = Localizer["InitDevicesString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["InitDevicesStringDefaultValue"]
        },
        new()
        {
            Name = "NotFoundDevicesString",
            Description = Localizer["NotFoundDevicesString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["NotFoundDevicesStringDefaultValue"]
        },
        new() {
            Name = "OnInit",
            Description = Localizer["OnInit"],
            Type = "Func<IEnumerable<DeviceItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnStart",
            Description = Localizer["OnStart"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnClose",
            Description = Localizer["OnClose"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnCapture",
            Description = Localizer["OnCapture"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(Camera.CaptureJpeg),
            Description = Localizer["CaptureJpeg"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new() {
            Name = nameof(Camera.Quality),
            Description = Localizer["Quality"],
            Type = "double",
            ValueList = " — ",
            DefaultValue = " 0.9d"
        },

    };
}
