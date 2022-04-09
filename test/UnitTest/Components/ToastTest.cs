// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UnitTest.Components;

public class ToastTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Placement_Ok()
    {
        var options = Context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ToastPlacement = Placement.TopStart;

        var cut = Context.RenderComponent<BootstrapBlazorRoot>();

        var service = Context.Services.GetRequiredService<ToastService>();
        service.Success("Test", "test content");

        // 恢复设置
        options.Value.ToastPlacement = Placement.Auto;

        Assert.NotNull(cut.Instance.ToastContainer);
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
        service.Error("Error", "test content");
        service.Information("Information", "test content");
        service.Warning("Warning", "test content");
    }

    [Fact]
    public async Task Options_Ok()
    {
        Context.RenderComponent<Toast>();

        var service = Context.Services.GetRequiredService<ToastService>();
        var option = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        await service.Show(new ToastOption()
        {
            ForceDelay = true
        });

        await service.Success(null, "test content");
        await service.Success("Test", null);
        await service.Success("Test", "test content");

        await service.Error(null, "test content");
        await service.Error("Test", null);
        await service.Error("Test", "test content");

        await service.Information(null, "test content");
        await service.Information("Test", null);
        await service.Information("Test", "test content");

        option.CurrentValue.ToastDelay = 2000;
        await service.Warning(null, "test content");
        await service.Warning("Test", null);
        await service.Warning("Test", "test content");
    }

    [Fact]
    public async Task AutoHide_Ok()
    {
        Context.RenderComponent<Toast>();
        var service = Context.Services.GetRequiredService<ToastService>();
        await service.Show(new ToastOption()
        {
            IsAutoHide = false
        });
    }

    [Fact]
    public async Task ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Toast>();

        var service = Context.Services.GetRequiredService<ToastService>();
        await service.Show(new ToastOption()
        {
            ChildContent = new RenderFragment(builder =>
            {
                builder.AddContent(0, "Toast ChildContent");
            })
        });
    }

    [Fact]
    public async Task Close_Ok()
    {
        var cut = Context.RenderComponent<Toast>();

        var service = Context.Services.GetRequiredService<ToastService>();
        var option = new ToastOption()
        {
            ChildContent = new RenderFragment(builder =>
            {
                builder.AddContent(0, "Toast ChildContent");
            })
        };
        await service.Show(option);

        await option.Close();
    }
}
