// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class VideoDeviceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task GetDevices_Ok()
    {
        Context.JSInterop.Setup<List<MediaDeviceInfo>>("enumerateDevices").SetResult([
            new() { DeviceId = "test-device-id", GroupId = "test-groupd-id", Kind = "videoinput", Label="test-video" }
        ]);
        var service = Context.Services.GetRequiredService<IVideoDevice>();
        var devices = await service.GetDevices();
        Assert.NotNull(devices);
        Assert.Equal("test-device-id", devices[0].DeviceId);
        Assert.Equal("test-groupd-id", devices[0].GroupId);
        Assert.Equal("videoinput", devices[0].Kind);
        Assert.Equal("test-video", devices[0].Label);
    }

    [Fact]
    public async Task Open_Ok()
    {
        Context.JSInterop.Setup<string?>("getPreviewUrl").SetResult("blob:https://test-preview");
        Context.JSInterop.Setup<bool>("open", _ => true).SetResult(true);

        var service = Context.Services.GetRequiredService<IVideoDevice>();
        var options = new MediaTrackConstraints()
        {
            DeviceId = "test-device-id",
            FacingMode = "user",
            Height = 640,
            Width = 480,
            VideoSelector = ".bb-video"
        };
        var open = await service.Open(options);
        Assert.True(open);

        await service.Close(".bb-video");

        Assert.Equal("test-device-id", options.DeviceId);
        Assert.Equal("user", options.FacingMode);
        Assert.Equal(640, options.Height);
        Assert.Equal(480, options.Width);
        Assert.Equal(".bb-video", options.VideoSelector);

        await service.Capture();
        var url = await service.GetPreviewUrl();
        Assert.Equal("blob:https://test-preview", url);
    }
}
