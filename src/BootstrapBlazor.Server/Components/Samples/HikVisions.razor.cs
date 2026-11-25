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
    private string _password = "";
    private string _userName = "admin";
    private bool _inited;
    private bool _loginInProgress;
    private bool _showWebRtc;

    private bool IsDisabledLoginButton => _loginStatus || _loginInProgress;

    private bool _loginStatus => _hikVision.IsMultipleWindowType ? false : _hikVision.IsLogin;
    private bool _logoutStatus => _hikVision.IsMultipleWindowType ? false : !_hikVision.IsLogin;
    private bool _startRealPlayStatus => _hikVision.IsMultipleWindowType ? false : _hikVision is not { IsLogin: true, IsRealPlaying: false };
    private bool _stopRealPlayStatus => _hikVision.IsMultipleWindowType ? false : _hikVision is not { IsLogin: true, IsRealPlaying: true };
    private bool _openSoundStatus => _hikVision.IsMultipleWindowType ? false : _hikVision is not { IsLogin: true, IsRealPlaying: true, IsOpenSound: false };
    private bool _closeSoundStatus => _hikVision.IsMultipleWindowType ? false : _hikVision is not { IsLogin: true, IsRealPlaying: true, IsOpenSound: true };
    private bool _startRecordStatus => _hikVision.IsMultipleWindowType ? false : _hikVision is not { IsLogin: true, IsRealPlaying: true, IsStartRecord: false };
    private bool _stopRecordStatus => _hikVision.IsMultipleWindowType ? false : _hikVision is not { IsLogin: true, IsRealPlaying: true, IsStartRecord: true };

    private List<SelectedItem> _analogChannels = [];
    private int _channelId = 1;
    private int _streamType = 1;
    private readonly List<SelectedItem> _streamTypes = [];
    private readonly List<SelectedItem> _iWndTypes =
    [
        new SelectedItem("1", "1*1"),
        new SelectedItem("2", "2*2"),
        new SelectedItem("3", "3*3"),
        new SelectedItem("4", "4*4"),
        new SelectedItem("1*2", "1*2"),
        new SelectedItem("2*1", "2*1")
    ];

    private string _iWndType = "1";

    private void ToggleWebRtc() => _showWebRtc = !_showWebRtc;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _streamTypes.AddRange(
        [
            new SelectedItem("1", Localizer["MainStream"]),
            new SelectedItem("2", Localizer["SubStream"]),
            new SelectedItem("3", Localizer["ThirdStream"]),
            new SelectedItem("4", Localizer["TranscodedStream"])
        ]);
    }

    private async Task OnLoginClickAsync()
    {
        if (_loginInProgress)
        {
            return;
        }

        _loginInProgress = true;
        try
        {
            if (!await _hikVision.Login(_ip, _port, _userName, _password, HikVisionLoginType.Http))
            {
                await ToastService.Error(Localizer["OperationFailed", Localizer["Login"].Value], Localizer["LoginFailed"]);
            }
        }
        finally
        {
            _loginInProgress = false;
        }
    }

    private Task OnLogoutClickAsync() => _hikVision.Logout();

    private Task OnStartRealPlayAsync() => _hikVision.StartRealPlay(_streamType, _channelId);

    private Task OnStopRealPlayAsync() => _hikVision.StopRealPlay();

    private async Task OnOpenSoundAsync()
    {
        if (await _hikVision.OpenSound())
        {
            await ToastService.Success(Localizer["NotificationTitle"], Localizer["OperationSucceeded", Localizer["OpenSound"].Value]);
        }
        else
        {
            await ToastService.Error(Localizer["OperationFailed", Localizer["OpenSound"].Value], Localizer["OperationFailedMessage"]);
        }
    }

    private async Task OnCloseSoundAsync()
    {
        if (await _hikVision.CloseSound())
        {
            await ToastService.Success(Localizer["NotificationTitle"], Localizer["OperationSucceeded", Localizer["CloseSound"].Value]);
        }
        else
        {
            await ToastService.Error(Localizer["OperationFailed", Localizer["CloseSound"].Value], Localizer["OperationFailedMessage"]);
        }
    }

    private async Task OnCaptureAsync()
    {
        if (await _hikVision.CapturePicture())
        {
            await ToastService.Success(Localizer["NotificationTitle"], Localizer["OperationSucceeded", Localizer["CapturePicture"].Value]);
        }
        else
        {
            await ToastService.Error(Localizer["OperationFailed", Localizer["CapturePicture"].Value], Localizer["OperationFailedMessage"]);
        }
    }

    private async Task OnCaptureAndDownloadAsync()
    {
        if (await _hikVision.CapturePictureAndDownload())
        {
            await ToastService.Success(Localizer["NotificationTitle"], Localizer["OperationSucceeded", Localizer["CapturePictureAndDownload"].Value]);
        }
        else
        {
            await ToastService.Error(Localizer["OperationFailed", Localizer["CapturePictureAndDownload"].Value], Localizer["OperationFailedMessage"]);
        }
    }

    private async Task OnStartRecordAsync()
    {
        if (await _hikVision.StartRecord())
        {
            await ToastService.Success(Localizer["NotificationTitle"], Localizer["OperationSucceeded", Localizer["StartRecord"].Value]);
        }
        else
        {
            await ToastService.Error(Localizer["OperationFailed", Localizer["StartRecord"].Value], Localizer["OperationFailedMessage"]);
        }
    }

    private async Task OnStopRecordAsync()
    {
        if (await _hikVision.StopRecord())
        {
            await ToastService.Success(Localizer["NotificationTitle"], Localizer["OperationSucceeded", Localizer["StopRecord"].Value]);
        }
        else
        {
            await ToastService.Error(Localizer["OperationFailed", Localizer["StopRecord"].Value], Localizer["OperationFailedMessage"]);
        }
    }

    private async Task OnInitedAsync(bool initialized)
    {
        _inited = initialized;
        StateHasChanged();
        if (!initialized)
        {
            await SwalService.Show(new SwalOption()
            {
                Category = SwalCategory.Error,
                Title = Localizer["OperationFailed", Localizer["TriggerInited"].Value],
                Content = Localizer["InitializationFailed"],
                ShowFooter = true,
                FooterTemplate = RenderPluginDownloadFooter
            });
        }
    }

    private Task OnLoginAsync()
    {
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
        _analogChannels.Clear();
        _channelId = 1;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnStartRealPlayedAsync()
    {
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnStopRealPlayedAsync()
    {
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task OnWndTypeChangedAsync(SelectedItem item)
    {
        if (await _hikVision.ChangeWindowNum(item.Value))
        {
            _iWndType = item.Value;
        }
        else
        {
            await ToastService.Error(Localizer["OperationFailed", Localizer["ChangeWindowNum"].Value], Localizer["OperationFailedMessage"]);
        }
    }
}
