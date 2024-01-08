// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.IO;

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// DrawingApp Tutorial
/// </summary>
public partial class DrawingApp
{
    private int LineSize { get; set; } = 2;

    private string DrawColor { get; set; } = "#4dff00";

    /// <inheritdoc />
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, LineSize, DrawColor);

    /// <summary>
    /// ChangeSize
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private async Task ChangeSize(int val) => await InvokeVoidAsync("changeSize", val);

    /// <summary>
    /// ChangeColor
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private async Task ChangeColor(string val) => await InvokeVoidAsync("changeColor", val);

    /// <summary>
    /// ClearRect
    /// </summary>
    /// <returns></returns>
    private async Task ClearRect()
    {
        await InvokeVoidAsync("clearRect", Id);
        await MessageService.Show(new MessageOption()
        {
            Content = "已清空画板",
            ForceDelay = true,
            Delay = 500
        });
    }

    /// <summary>
    /// DownloadImage
    /// </summary>
    /// <returns></returns>
    private async Task DownloadImage()
    {
        var base64String = await InvokeAsync<string>("exportImage", Id);
        if (!string.IsNullOrEmpty(base64String))
        {
            byte[] byteArray = Convert.FromBase64String(base64String);
            await DownloadService.DownloadFromByteArrayAsync("drawing-app.jpeg", byteArray);
        }
    }
}
