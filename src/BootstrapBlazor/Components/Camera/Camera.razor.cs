// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Camera 组件
///</para>
/// <para lang="en">Camera component
///</para>
/// </summary>
public partial class Camera
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前设备 Id 默认 null
    ///</para>
    /// <para lang="en">Gets or sets 当前设备 Id Default is null
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DeviceId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动开启摄像头 默认为 false
    ///</para>
    /// <para lang="en">Gets or sets whether自动开启摄像头 Default is为 false
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoStart { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 摄像头视频宽度 默认 320
    ///</para>
    /// <para lang="en">Gets or sets 摄像头视频width Default is 320
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? VideoWidth { get; set; } = 320;

    /// <summary>
    /// <para lang="zh">获得/设置 摄像头视频高度 默认 240
    ///</para>
    /// <para lang="en">Gets or sets 摄像头视频height Default is 240
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? VideoHeight { get; set; } = 240;

    /// <summary>
    /// <para lang="zh">获得/设置 拍照格式为 Jpeg 默认为 false 使用 png 格式
    ///</para>
    /// <para lang="en">Gets or sets 拍照格式为 Jpeg Default is为 false 使用 png 格式
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool CaptureJpeg { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图像质量 默认为 0.9
    ///</para>
    /// <para lang="en">Gets or sets 图像质量 Default is为 0.9
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public float Quality { get; set; } = 0.9f;

    /// <summary>
    /// <para lang="zh">获得/设置 初始化摄像头回调方法
    ///</para>
    /// <para lang="en">Gets or sets 初始化摄像头callback method
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<List<DeviceItem>, Task>? OnInit { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 拍照出错回调方法
    ///</para>
    /// <para lang="en">Gets or sets 拍照出错callback method
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 打开摄像头回调方法
    ///</para>
    /// <para lang="en">Gets or sets 打开摄像头callback method
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnOpen { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭摄像头回调方法
    ///</para>
    /// <para lang="en">Gets or sets 关闭摄像头callback method
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    private string VideoWidthString => $"{VideoWidth}";

    private string VideoHeightString => $"{VideoHeight}";

    private string? AutoStartString => AutoStart ? "true" : null;

    private string? CaptureJpegString => CaptureJpeg ? "true" : null;

    private string? QualityString => Quality == 0.9f ? null : Quality.ToString(CultureInfo.InvariantCulture);

    private string? ClassString => CssBuilder.Default("bb-camera")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

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
    /// <para lang="zh">打开摄像头
    ///</para>
    /// <para lang="en">打开摄像头
    ///</para>
    /// </summary>
    /// <returns></returns>
    public Task Open() => InvokeVoidAsync("open", Id);

    /// <summary>
    /// <para lang="zh">关闭摄像头
    ///</para>
    /// <para lang="en">关闭摄像头
    ///</para>
    /// </summary>
    /// <returns></returns>
    public Task Close() => InvokeVoidAsync("close", Id);

    /// <summary>
    /// <para lang="zh">拍照方法
    ///</para>
    /// <para lang="en">拍照方法
    ///</para>
    /// </summary>
    /// <returns></returns>
    public async Task<Stream?> Capture()
    {
        Stream? ret = null;
#if NET5_0
        await Task.Delay(10);
#elif NET6_0_OR_GREATER
        var streamRef = await InvokeAsync<IJSStreamReference?>("capture", Id);
        if (streamRef != null)
        {
            ret = await streamRef.OpenReadStreamAsync(streamRef.Length);
        }
#endif
        return ret;
    }

    /// <summary>
    /// <para lang="zh">保存并下载图片
    ///</para>
    /// <para lang="en">保存并下载图片
    ///</para>
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public Task SaveAndDownload(string? fileName = null) => InvokeVoidAsync("download", Id, fileName);

    /// <summary>
    /// <para lang="zh">重置宽高方法
    ///</para>
    /// <para lang="en">重置宽高方法
    ///</para>
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public Task Resize(int width, int height) => InvokeVoidAsync("resize", Id, width, height);

    /// <summary>
    /// <para lang="zh">初始化设备方法
    ///</para>
    /// <para lang="en">初始化设备方法
    ///</para>
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
    /// <para lang="zh">扫描发生错误回调方法
    ///</para>
    /// <para lang="en">扫描发生错误callback method
    ///</para>
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
    /// <para lang="zh">开启摄像头回调方法
    ///</para>
    /// <para lang="en">开启摄像头callback method
    ///</para>
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
    /// <para lang="zh">停止摄像头回调方法
    ///</para>
    /// <para lang="en">停止摄像头callback method
    ///</para>
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
