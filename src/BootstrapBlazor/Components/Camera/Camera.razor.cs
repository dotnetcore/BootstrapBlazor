// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Text;

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
    /// 获得/设置 初始化设备列表文字 默认为 正在识别摄像头
    /// </summary>
    [Parameter]
    [NotNull]
    public string? InitDevicesString { get; set; }

    /// <summary>
    /// 获得/设置 摄像头视频宽度
    /// </summary>
    [Parameter]
    public int VideoWidth { get; set; } = 320;

    /// <summary>
    /// 获得/设置 摄像头视频高度
    /// </summary>
    [Parameter]
    public int VideoHeight { get; set; } = 240;

    /// <summary>
    /// 获得/设置 拍照格式为 Jpeg 默认为 false 使用 png 格式
    /// </summary>
    [Parameter]
    public bool CaptureJpeg { get; set; }

    /// <summary>
    /// 获得/设置 图像质量 默认为 0.9
    /// </summary>
    [Parameter]
    public double Quality { get; set; } = 0.9d;

    /// <summary>
    /// 获得/设置 初始化摄像头回调方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<DeviceItem>, Task>? OnInit { get; set; }

    /// <summary>
    /// 获得/设置 拍照出错回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnError { get; set; }

    /// <summary>
    /// 获得/设置 开始拍照回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnStart { get; set; }

    /// <summary>
    /// 获得/设置 关闭拍照回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// 获得/设置 拍照成功回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnCapture { get; set; }

    /// <summary>
    /// 获得/设置 开始按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PlayIcon { get; set; }

    /// <summary>
    /// 获得/设置 停止按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? StopIcon { get; set; }

    /// <summary>
    /// 获得/设置 拍照按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PhotoIcon { get; set; }

    /// <summary>
    /// 获得/设置 开启按钮显示文本 默认为开启
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PlayText { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮显示文本 默认为关闭
    /// </summary>
    [Parameter]
    [NotNull]
    public string? StopText { get; set; }

    /// <summary>
    /// 获得/设置 拍照按钮显示文本 默认为拍照
    /// </summary>
    [Parameter]
    [NotNull]
    public string? PhotoText { get; set; }

    /// <summary>
    /// 获得/设置 未找到视频相关设备文字 默认为 未找到视频相关设备
    /// </summary>
    [Parameter]
    [NotNull]
    public string? NotFoundDevicesString { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Camera>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool _captureDisabled = true;

    private List<SelectedItem> _devices = new();

    private string _videoWidthString => $"{VideoWidth}px;";

    private string _videoHeightString => $"{VideoHeight}px;";

    private string? _autoStartString => AutoStart ? "true" : null;

    private bool _update;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PlayText ??= Localizer[nameof(PlayText)];
        StopText ??= Localizer[nameof(StopText)];
        PhotoText ??= Localizer[nameof(PhotoText)];
        InitDevicesString ??= Localizer[nameof(InitDevicesString)];
        NotFoundDevicesString ??= Localizer[nameof(NotFoundDevicesString)];
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlayIcon ??= IconTheme.GetIconByKey(ComponentIcons.CameraPlayIcon);
        StopIcon ??= IconTheme.GetIconByKey(ComponentIcons.CameraStopIcon);
        PhotoIcon ??= IconTheme.GetIconByKey(ComponentIcons.CameraPhotoIcon);

        if (VideoWidth < 40)
        {
            VideoWidth = 40;
        }

        if (VideoHeight < 30)
        {
            VideoHeight = 30;
        }

        _update = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _update = false;
        }

        if (_update)
        {
            _update = false;
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    /// <summary>
    /// 初始化设备方法
    /// </summary>
    /// <param name="devices"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task InitDevices(List<DeviceItem> devices)
    {
        if (devices.Count > 0)
        {
            for (var index = 0; index < devices.Count; index++)
            {
                var d = devices[index];
                if (string.IsNullOrEmpty(d.Label))
                {
                    d.Label = $"Video device {index + 1}";
                }
            }
        }
        else
        {
            InitDevicesString = NotFoundDevicesString;
        }

        _devices.AddRange(devices.Select(i => new SelectedItem { Value = i.DeviceId, Text = i.Label }));

        if (OnInit != null)
        {
            await OnInit(devices);
        }
        StateHasChanged();
    }

    /// <summary>
    /// 扫描发生错误回调方法
    /// </summary>
    /// <param name="err"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task GetError(string err)
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
    public async Task Start()
    {
        _captureDisabled = false;
        if (OnStart != null)
        {
            await OnStart();
        }
        StateHasChanged();
    }

    /// <summary>
    /// 停止摄像头回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task Stop()
    {
        _captureDisabled = true;
        if (OnClose != null)
        {
            await OnClose();
        }
        StateHasChanged();
    }

    private readonly StringBuilder _sb = new();
    private string? PreviewData { get; set; }

    /// <summary>
    /// 拍照回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task Capture(string payload)
    {
        if (payload == "__BB__%END%__BB__")
        {
            var data = _sb.ToString();
            _sb.Clear();
            if (OnCapture != null)
            {
                await OnCapture(data);
            }

            if (ShowPreview)
            {
                PreviewData = data;
                StateHasChanged();
            }
        }
        else
        {
            _sb.Append(payload);
        }
    }
}
