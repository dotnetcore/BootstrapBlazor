// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CameraTest : BootstrapBlazorTestBase
{
    [Fact]
    public void InitDevices_Ok()
    {
        var count = 0;
        var cut = Context.RenderComponent<Camera>(pb =>
        {
            pb.Add(a => a.OnInit, devices =>
            {
                count = devices.Count;
                return Task.CompletedTask;
            });
            pb.Add(a => a.AutoStart, true);
        });
        cut.InvokeAsync(() => cut.Instance.TriggerInit(new List<DeviceItem>()));
        Assert.Equal(0, count);

        cut.InvokeAsync(() => cut.Instance.TriggerInit(new List<DeviceItem>
        {
            new DeviceItem()
            {
                DeviceId = "1",
                Label = "Device 1"
            },
            new DeviceItem()
            {
                DeviceId = "1"
            }
        }));
        Assert.Equal(2, count);
    }

    [Fact]
    public void GetError_Ok()
    {
        var msg = "";
        var cut = Context.RenderComponent<Camera>(pb =>
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
    public void Start_Ok()
    {
        var start = false;
        var cut = Context.RenderComponent<Camera>(pb =>
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
    public void Stop_Ok()
    {
        var stop = false;
        var cut = Context.RenderComponent<Camera>(pb =>
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
    public void Capture_Ok()
    {
        Stream? stream = null;
        var cut = Context.RenderComponent<Camera>();
        cut.InvokeAsync(async () =>
        {
            stream = await cut.Instance.Capture();
        });
        Assert.Null(stream);
    }

    [Fact]
    public void Width_Height_Ok()
    {
        var cut = Context.RenderComponent<Camera>(pb =>
        {
            pb.Add(a => a.VideoWidth, 30);
            pb.Add(a => a.VideoHeight, 20);
            pb.Add(a => a.CaptureJpeg, true);
            pb.Add(a => a.Quality, 0.8f);
        });

        cut.Contains("data-video-width=\"30\" data-video-height=\"20\" data-capture-jpeg=\"true\" data-capture-quality=\"0.8\"");
    }
}
