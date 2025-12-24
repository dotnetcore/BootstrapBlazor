// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 海康威视网络摄像机组件
/// </summary>
public partial class HikVisions
{
    [Inject, NotNull]
    private SwalService? SwalService { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private HikVisionWebPlugin _hikVision = default!;

    private string _ip = "47.121.113.151";
    private int _port = 9980;
    private string _password = "vhbn8888";
    private string _userName = "admin";
    private bool _inited;

    private bool _loginStatus = true;
    private bool _logoutStatus = true;
    private bool _startRealPlayStatus = true;
    private bool _stopRealPlayStatus = true;
    private bool _openSoundStatus = true;
    private bool _closeSoundStatus = true;

    private List<SelectedItem> _analogChannels = [];
    private int _channelId = 1;
    private int _streamType = 1;
    private readonly List<SelectedItem> _streamTypes =
    [
        new SelectedItem("1", "主码流"),
        new SelectedItem("2", "子码流"),
        new SelectedItem("3", "第三码流"),
        new SelectedItem("4", "转码码流")
    ];

    private async Task OnLogin()
    {
        _loginStatus = true;
        _logoutStatus = true;
        _loginStatus = await _hikVision.Login(_ip, _port, _userName, _password, HikVisionLoginType.Http);
    }

    private async Task OnLogout()
    {
        _analogChannels.Clear();
        _loginStatus = true;
        _logoutStatus = true;
        _startRealPlayStatus = true;
        _stopRealPlayStatus = true;
        _openSoundStatus = true;
        _closeSoundStatus = true;
        await _hikVision.Logout();
    }

    private async Task OnStartRealPlay()
    {
        _startRealPlayStatus = true;
        _stopRealPlayStatus = true;
        _openSoundStatus = false;
        _closeSoundStatus = true;
        await _hikVision.StartRealPlay(_streamType, _channelId);
    }

    private async Task OnStopRealPlay()
    {
        _startRealPlayStatus = true;
        _stopRealPlayStatus = true;
        _openSoundStatus = true;
        _closeSoundStatus = true;
        await _hikVision.StopRealPlay();
    }

    private async Task OnOpenSound()
    {
        var result = await _hikVision.OpenSound();
        if (result)
        {
            _openSoundStatus = true;
            _closeSoundStatus = false;
            await ToastService.Success("消息通知", "打开声音成功");
        }
        else
        {
            await ToastService.Error("消息通知", "打开声音失败");
        }
    }

    private async Task OnCloseSound()
    {
        var result = await _hikVision.CloseSound();
        if (result)
        {
            _openSoundStatus = false;
            _closeSoundStatus = true;
            await ToastService.Success("消息通知", "关闭声音成功");
        }
        else
        {
            await ToastService.Error("消息通知", "关闭声音失败");
        }
    }

    private async Task OnInitedAsync(bool initialized)
    {
        _inited = initialized;
        if (_inited)
        {
            _loginStatus = false;
            StateHasChanged();
        }
        else
        {
            await SwalService.Show(new SwalOption()
            {
                Category = SwalCategory.Error,
                Title = "组件初始化错误",
                Content = "组件初始化失败，请检查浏览器是否安装海康威视插件或插件是否启用",
                ShowFooter = true,
                FooterTemplate = new RenderFragment(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "请访问");
                    builder.OpenElement(10, "a");
                    builder.AddAttribute(11, "href", "https://open.hikvision.com/download/5cda567cf47ae80dd41a54b3?type=20&id=77c7f9ab64da4dbe8b2df7efe3365ec2");
                    builder.AddAttribute(12, "target", "_blank");
                    builder.AddAttribute(13, "style", "margin:0 8px;");
                    builder.AddContent(14, "海康威视官网下载插件");
                    builder.CloseElement();
                    builder.AddContent(2, "进行安装。");
                    builder.CloseElement();
                })
            });
        }
    }

    private Task OnLoginAsync()
    {
        _loginStatus = true;
        _logoutStatus = !_loginStatus;
        _startRealPlayStatus = _logoutStatus;
        _stopRealPlayStatus = !_startRealPlayStatus;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnGetChannelsAsync(HikVisionChannel channel)
    {
        _analogChannels = channel.AnalogChannels.Select(i => new SelectedItem(i.Id.ToString(CultureInfo.InvariantCulture), i.Name!)).ToList();
        _channelId = channel.AnalogChannels.FirstOrDefault()?.Id ?? 1;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnLogoutAsync()
    {
        _loginStatus = _hikVision.IsLogin;
        _logoutStatus = !_loginStatus;
        _startRealPlayStatus = true;
        _stopRealPlayStatus = true;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnStartRealPlayedAsync()
    {
        _startRealPlayStatus = _hikVision.IsRealPlaying;
        _stopRealPlayStatus = !_startRealPlayStatus;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnStopRealPlayedAsync()
    {
        _startRealPlayStatus = _hikVision.IsRealPlaying;
        _stopRealPlayStatus = !_startRealPlayStatus;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
