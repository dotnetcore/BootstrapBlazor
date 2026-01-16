// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class BarcodeReaders
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnInit(IEnumerable<DeviceItem> devices)
    {
        var cams = string.Join("", devices.Select(i => i.Label));
        Logger.Log($"{Localizer["InitLog"]} {cams}");
        return Task.CompletedTask;
    }
    private Task OnResult(string barcode)
    {
        Logger.Log($"{Localizer["ScanCodeLog"]} {barcode}");
        return Task.CompletedTask;
    }

    private Task OnError(string error)
    {
        Logger.Log($"{Localizer["ErrorLog"]} {error}");
        return Task.CompletedTask;
    }

    private Task OnStart()
    {
        Logger.Log(Localizer["OpenCameraLog"]);
        return Task.CompletedTask;
    }

    private Task OnClose()
    {
        Logger.Log(Localizer["CloseCameraLog"]);
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? ImageLogger { get; set; }

    private Task OnImageResult(string barcode)
    {
        ImageLogger.Log($"{Localizer["ScanCodeLog"]} {barcode}");
        return Task.CompletedTask;
    }

    private Task OnImageError(string err)
    {
        ImageLogger.Log($"{Localizer["ErrorLog"]} {err}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性
    /// </summary>
    /// <returns></returns>
}
