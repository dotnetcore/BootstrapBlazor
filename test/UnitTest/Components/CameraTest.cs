// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;
using UnitTest.Mock;

namespace UnitTest.Components;

public class CameraTest : BootstrapBlazorTestBase
{
    [Fact]
    public void InitDevices_Ok()
    {
        List<DeviceItem> items = [];
        var cut = Context.Render<Camera>(pb =>
        {
            pb.Add(a => a.OnInit, devices =>
            {
                items = devices;
                return Task.CompletedTask;
            });
            pb.Add(a => a.AutoStart, true);
        });
        cut.InvokeAsync(() => cut.Instance.TriggerInit([]));
        Assert.Empty(items);

        cut.InvokeAsync(() => cut.Instance.TriggerInit(
        [
            new DeviceItem()
            {
                DeviceId = "1",
                Label = "Device 1"
            },
            new DeviceItem()
            {
                DeviceId = "2"
            }
        ]));
        Assert.Equal("1", items[0].DeviceId);
        Assert.Equal("Device 1", items[0].Label);
        Assert.Equal(string.Empty, items[1].Label);
    }

    [Fact]
    public void GetError_Ok()
    {
        var msg = "";
        var cut = Context.Render<Camera>(pb =>
        {
            pb.Add(a => a.OnError, error =>
            {
                msg = error;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.TriggerError("Error"));
        Assert.Equal("Error", msg);
    }

    [Fact]
    public void TriggerOpen_Ok()
    {
        var start = false;
        var cut = Context.Render<Camera>(pb =>
        {
            pb.Add(a => a.OnOpen, () =>
            {
                start = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.TriggerOpen());
        Assert.True(start);
    }

    [Fact]
    public void TriggerClose_Ok()
    {
        var stop = false;
        var cut = Context.Render<Camera>(pb =>
        {
            pb.Add(a => a.OnClose, () =>
            {
                stop = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.TriggerClose());
        Assert.True(stop);
    }

    [Fact]
    public void Open_Ok()
    {
        var cut = Context.Render<Camera>();
        var handler = Context.JSInterop.SetupVoid("open", cut.Instance.Id);
        cut.InvokeAsync(() => cut.Instance.Open());
        handler.VerifyInvoke("open");
    }


    [Fact]
    public void Close_Ok()
    {
        var cut = Context.Render<Camera>();
        var handler = Context.JSInterop.SetupVoid("close", cut.Instance.Id);
        cut.InvokeAsync(() => cut.Instance.Close());
        handler.VerifyInvoke("close");
    }

    [Fact]
    public void Capture_Ok()
    {
        Stream? stream = null;
        var cut = Context.Render<Camera>();
        Context.JSInterop.Setup<IJSStreamReference>("capture", cut.Instance.Id).SetResult(new MockJSStreamReference());
        cut.InvokeAsync(async () =>
        {
            stream = await cut.Instance.Capture();
        });
        Assert.NotNull(stream);
        Assert.Equal(4, stream.Length);
    }

    [Fact]
    public void Save_Ok()
    {
        var cut = Context.Render<Camera>();
        var handler = Context.JSInterop.SetupVoid("download", cut.Instance.Id, "test.png");
        cut.InvokeAsync(() => cut.Instance.SaveAndDownload("test.png"));
        handler.VerifyInvoke("download");
    }

    [Fact]
    public void Resize_Ok()
    {
        var cut = Context.Render<Camera>();
        var handler = Context.JSInterop.SetupVoid("resize", cut.Instance.Id, 320, 240);
        cut.InvokeAsync(() => cut.Instance.Resize(320, 240));
        handler.VerifyInvoke("resize");
    }

    [Fact]
    public void DeviceId_Ok()
    {
        var cut = Context.Render<Camera>(pb =>
        {
            pb.Add(a => a.DeviceId, "test_id");
        });
        cut.Contains("data-device-id=\"test_id\"");
    }

    [Fact]
    public void Width_Height_Ok()
    {
        var cut = Context.Render<Camera>(pb =>
        {
            pb.Add(a => a.VideoWidth, 30);
            pb.Add(a => a.VideoHeight, 20);
            pb.Add(a => a.CaptureJpeg, true);
            pb.Add(a => a.Quality, 0.8f);
        });

        cut.Contains("data-video-width=\"30\" data-video-height=\"20\" data-capture-jpeg=\"true\" data-capture-quality=\"0.8\"");
    }
}
