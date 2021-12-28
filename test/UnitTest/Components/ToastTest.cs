// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class ToastTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Placement_Ok()
    {
        var options = Context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ToastPlacement = Placement.TopStart;

        Context.RenderComponent<BootstrapBlazorRoot>();

        var service = Context.Services.GetRequiredService<ToastService>();
        service.Success("Test", "test content");

        // 恢复设置
        options.Value.ToastPlacement = Placement.Auto;
    }

    [Fact]
    public void Clear_Ok()
    {
        var cut = Context.RenderComponent<Toast>();
        cut.InvokeAsync(async () => await cut.Instance.Clear());

        var service = Context.Services.GetRequiredService<ToastService>();
        service.Success("Test", "test content");
    }

    [Fact]
    public void SetPlacement_Ok()
    {
        var cut = Context.RenderComponent<Toast>();
        cut.InvokeAsync(() => cut.Instance.SetPlacement(Placement.BottomStart));
        Assert.Contains("left", cut.Markup);

        var service = Context.Services.GetRequiredService<ToastService>();
        service.Success("Test", "test content");
    }
}
