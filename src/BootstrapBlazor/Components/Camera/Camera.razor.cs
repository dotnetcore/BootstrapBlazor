// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;
using System.Text;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Camera 组件
/// </summary>
public partial class Camera
{
    /// <summary>
    /// 获得/设置 当前设备 Id 默认 null
    /// </summary>
    [Parameter]
    public string? DeviceId { get; set; }

    /// <summary>
    /// 获得/设置 是否自动开启摄像头 默认为 false
    /// </summary>
    [Parameter]
    public bool AutoStart { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 照片预览 默认为 false 不预览
    /// </summary>
    [Parameter]
    public bool ShowPreview { get; set; }

    /// <summary>
    /// 获得/设置 摄像头视频宽度 默认 320
    /// </summary>
    [Parameter]
    public int? VideoWidth { get; set; } = 320;

    /// <summary>
    /// 获得/设置 摄像头视频高度 默认 240
    /// </summary>
    [Parameter]
    public int? VideoHeight { get; set; } = 240;

    /// <summary>
    /// 获得/设置 拍照格式为 Jpeg 默认为 false 使用 png 格式
    /// </summary>
    [Parameter]
    public bool CaptureJpeg { get; set; }

    /// <summary>
    /// 获得/设置 图像质量 默认为 0.9
    /// </summary>
    [Parameter]
    public float Quality { get; set; } = 0.9f;

    /// <summary>
    /// 获得/设置 初始化摄像头回调方法
    /// </summary>
    [Parameter]
    public Func<List<DeviceItem>, Task>? OnInit { get; set; }

    /// <summary>
    /// 获得/设置 拍照出错回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

    /// <summary>
    /// 获得/设置 打开摄像头回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnOpen { get; set; }

    /// <summary>
    /// 获得/设置 关闭摄像头回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// 获得/设置 拍照成功回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnCapture { get; set; }

    private string VideoWidthString => $"{VideoWidth}px;";

    private string VideoHeightString => $"{VideoHeight}px;";

    private string? AutoStartString => AutoStart ? "true" : null;

    private string VideoStyleString => $"width: {VideoWidth}px; height: {VideoHeight}px;";

    private string? CaptureJpegString => CaptureJpeg ? "true" : null;

    private string? QualityString => Quality == 0.9f ? null : Quality.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    /// <summary>
    /// 打开摄像头
    /// </summary>
    /// <returns></returns>
    public Task Open() => InvokeVoidAsync("open", Id);

    /// <summary>
    /// 关闭摄像头
    /// </summary>
    /// <returns></returns>
    public Task Close() => InvokeVoidAsync("close", Id);

    /// <summary>
    /// 拍照方法
    /// </summary>
    /// <returns></returns>
    public async Task<Stream?> Capture()
    {
        Stream? ret = null;
#if NET5_0
        await Task.Delay(10);
#elif NET6_0_OR_GREATER
        var streamRef = await InvokeAsync<IJSStreamReference>("capture", Id);
        if (streamRef != null)
        {
            ret = await streamRef.OpenReadStreamAsync(streamRef.Length);
        }
#endif
        return ret;
    }

    /// <summary>
    /// 保存并下载图片
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public Task SaveAndDownload(string? fileName = null) => InvokeVoidAsync("download", Id, fileName);

    /// <summary>
    /// 初始化设备方法
    /// </summary>
    /// <param name="devices"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerInit(List<DeviceItem> devices)
    {
        if (devices.Count > 0)
        {
            if (OnInit != null)
            {
                await OnInit(devices);
            }

            StateHasChanged();
        }
    }

    /// <summary>
    /// 扫描发生错误回调方法
    /// </summary>
    /// <param name="err"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerError(string err)
    {
        if (OnError != null)
        {
            await OnError.Invoke(err);
        }
    }

    /// <summary>
    /// 开启摄像头回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerOpen()
    {
        if (OnOpen != null)
        {
            await OnOpen();
        }
    }

    /// <summary>
    /// 停止摄像头回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerClose()
    {
        if (OnClose != null)
        {
            await OnClose();
        }
    }
}
