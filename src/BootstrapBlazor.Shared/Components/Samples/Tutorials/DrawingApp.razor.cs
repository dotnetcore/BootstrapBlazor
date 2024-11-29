﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Tutorials;

/// <summary>
/// DrawingApp Tutorial
/// </summary>
public partial class DrawingApp
{
    /// <summary>
    /// LineThickness
    /// </summary>
    private int LineThickness { get; set; } = 2;

    /// <summary>
    /// DrawingColor
    /// </summary>
    private string DrawingColor { get; set; } = "#6610f2";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, LineThickness, DrawingColor);

    /// <summary>
    /// ChangeSize
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private async Task ChangeSize(int val)
    {
        LineThickness = val;
        await InvokeVoidAsync("changeSize", Id, val);
    }

    /// <summary>
    /// ChangeColor
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private async Task ChangeColor(string val)
    {
        DrawingColor = val;
        await InvokeVoidAsync("changeColor", Id, val);
    }

    /// <summary>
    /// ClearRect
    /// </summary>
    /// <returns></returns>
    private async Task ClearCanvas()
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
    /// Undo Action
    /// </summary>
    /// <returns></returns>
    private Task Undo() => InvokeVoidAsync("undo", Id);

    /// <summary>
    /// Redo Action
    /// </summary>
    /// <returns></returns>
    private Task Redo() => InvokeVoidAsync("redo", Id);

    /// <summary>
    /// DownloadImage
    /// </summary>
    /// <returns></returns>
    private async Task DownloadImage()
    {
        var base64String = await InvokeAsync<string>("exportImage", Id);
        if (!string.IsNullOrEmpty(base64String))
        {
            byte[] byteArray = Convert.FromBase64String(base64String.Replace("data:image/jpeg;base64,", ""));
            await DownloadService.DownloadFromByteArrayAsync("drawing-app.jpeg", byteArray);
            await MessageService.Show(new MessageOption()
            {
                Content = "已下载图片"
            });
        }
    }
}
