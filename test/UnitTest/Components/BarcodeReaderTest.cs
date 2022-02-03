// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class BarcodeReaderTest : BootstrapBlazorTestBase
{
    [Fact]
    public void InitDevices_Ok()
    {
        var init = false;
        var cut = Context.RenderComponent<BarcodeReader>(pb =>
        {
            pb.Add(a => a.ScanType, ScanType.Camera);
            pb.Add(a => a.OnInit, items =>
            {
                init = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.InitDevices(new DeviceItem[]
        {
            new DeviceItem { DeviceId = "TestId", Label = "Test" }
        }));
        Assert.True(init);
        cut.InvokeAsync(() => cut.Instance.InitDevices(Array.Empty<DeviceItem>()));
    }

    [Fact]
    public void GetResult_Ok()
    {
        var init = false;
        var cut = Context.RenderComponent<BarcodeReader>(pb =>
        {
            pb.Add(a => a.ScanType, ScanType.Camera);
            pb.Add(a => a.OnResult, v =>
            {
                init = true;
                return Task.CompletedTask;
            });
        });

        cut.InvokeAsync(() => cut.Instance.GetResult("Test"));
        Assert.True(init);
    }

    [Fact]
    public void GetError_Ok()
    {
        var init = false;
        var cut = Context.RenderComponent<BarcodeReader>(pb =>
        {
            pb.Add(a => a.ScanType, ScanType.Camera);
            pb.Add(a => a.OnError, v =>
            {
                init = true;
                return Task.CompletedTask;
            });
        });

        cut.InvokeAsync(() => cut.Instance.GetError("Test"));
        Assert.True(init);
    }

    [Fact]
    public void Start_Ok()
    {
        var init = false;
        var cut = Context.RenderComponent<BarcodeReader>(pb =>
        {
            pb.Add(a => a.ScanType, ScanType.Camera);
            pb.Add(a => a.OnStart, () =>
            {
                init = true;
                return Task.CompletedTask;
            });
        });

        cut.InvokeAsync(() => cut.Instance.Start());
        Assert.True(init);
    }

    [Fact]
    public void Close_Ok()
    {
        var init = false;
        var cut = Context.RenderComponent<BarcodeReader>(pb =>
        {
            pb.Add(a => a.ScanType, ScanType.Camera);
            pb.Add(a => a.OnClose, () =>
            {
                init = true;
                return Task.CompletedTask;
            });
        });

        cut.InvokeAsync(() => cut.Instance.Stop());
        Assert.True(init);
    }

    [Fact]
    public void AutoStop_Ok()
    {
        var cut = Context.RenderComponent<BarcodeReader>(pb =>
        {
            pb.Add(a => a.ScanType, ScanType.Image);
            pb.Add(a => a.AutoStop, true);
            pb.Add(a => a.AutoStart, true);
        });
        Assert.Contains("data-autostop=\"true\"", cut.Markup);
        Assert.Contains("scanner-image", cut.Markup);
    }

    [Fact]
    public void OnDeviceChanged_Ok()
    {
        var changed = false;
        var cut = Context.RenderComponent<BarcodeReader>(pb =>
        {
            pb.Add(a => a.OnDeviceChanged, item =>
            {
                changed = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.InitDevices(new DeviceItem[]
        {
            new DeviceItem { DeviceId = "TestId", Label = "Test" }
        }));
        cut.Find(".dropdown-item").Click();
        Assert.True(changed);
    }
}
