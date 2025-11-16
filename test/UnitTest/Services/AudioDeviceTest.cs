// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;
using UnitTest.Mock;

namespace UnitTest.Services;

public class AudioDeviceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetDevices_Ok()
    {
        Context.JSInterop.Setup<List<MediaDeviceInfo>>("enumerateDevices").SetResult([
            new() { DeviceId = "test-device-video-id", GroupId = "test-groupd-id", Kind = "videoinput", Label="test-video" },
            new() { DeviceId = "test-device-audio-id", GroupId = "test-groupd-id", Kind = "audioinput", Label="test-audio" }
        ]);
        var service = Context.Services.GetRequiredService<IAudioDevice>();
        var devices = await service.GetDevices();
        Assert.NotNull(devices);
        Assert.Equal("test-device-audio-id", devices[0].DeviceId);
        Assert.Equal("test-groupd-id", devices[0].GroupId);
        Assert.Equal("audioinput", devices[0].Kind);
        Assert.Equal("test-audio", devices[0].Label);
    }

    [Fact]
    public async Task Open_Ok()
    {
        Context.JSInterop.Setup<bool>("open", _ => true).SetResult(true);
        Context.JSInterop.Setup<bool>("close", _ => true).SetResult(true);
        Context.JSInterop.Setup<IJSStreamReference?>("getAudioData").SetResult(new MockJSStreamReference());

        var service = Context.Services.GetRequiredService<IAudioDevice>();
        var options = new MediaTrackConstraints()
        {
            DeviceId = "test-device-id",
            Selector = ".bb-audio"
        };
        var open = await service.Open(options);
        Assert.True(open);

        var close = await service.Close(".bb-audio");
        Assert.True(close);

        var data = await service.GetData();
        Assert.NotNull(data);
        Assert.Equal(4, data.Length);
    }
}
