// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

/// <summary>
/// MaskService 单元测试
/// </summary>
public class MaskServiceTest : TestBase
{
    [Fact]
    public async Task Mask_Ok()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var maskService = context.Services.GetRequiredService<MaskService>();
        var cut = context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClickWithoutRender, async () =>
                {
                    await maskService.Show(new MaskOption()
                    {
                        BackgroundColor = "#000",
                        Opacity = 0.5f,
                        ZIndex = 1050,
                        ChildContent = builder => builder.AddContent(0, "test-mask-content")
                    });
                });
            });
        });
        cut.Contains("<div class=\"bb-mask-backdrop\"></div>");

        // 点击按钮显示
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        cut.Contains("bb-mask-content");

        // 关闭遮罩
        await cut.InvokeAsync(() => maskService.Close());
        cut.DoesNotContain("bb-mask-content");
    }

    [Fact]
    public async Task Container_Ok()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var maskService = context.Services.GetRequiredService<MaskService>();
        var cut = context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClickWithoutRender, async () =>
                {
                    await maskService.Show(new MaskOption()
                    {
                        BackgroundColor = "#000",
                        Opacity = 0.5f,
                        ZIndex = 1050,
                        ChildContent = builder => builder.AddContent(0, "test-mask-content"),
                        ContainerId = "test-9527",
                        Selector = "test-mask-selector"
                    });
                });
            });
        });
        cut.Contains("<div class=\"bb-mask-backdrop\"></div>");

        // 点击按钮显示
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        cut.Contains("bb-mask-content");

        // 关闭遮罩
        await cut.InvokeAsync(() => maskService.Close(all: true));
        cut.DoesNotContain("bb-mask-content");
    }

    [Fact]
    public async Task Show_Component()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var maskService = context.Services.GetRequiredService<MaskService>();
        var cut = context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClickWithoutRender, async () =>
                {
                    await maskService.Show<MockComponent>();
                });
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public async Task Show_Type()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddBootstrapBlazor();

        var maskService = context.Services.GetRequiredService<MaskService>();
        var cut = context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(a => a.OnClickWithoutRender, async () =>
                {
                    await maskService.Show(typeof(MockComponent));
                });
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    class MockComponent : ComponentBase
    {

    }
}
