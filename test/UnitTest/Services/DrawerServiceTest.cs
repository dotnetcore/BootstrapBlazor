// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Services;

public class DrawerServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Show_Ok()
    {
        var option = new DrawerOption()
        {
            AllowResize = true,
            ChildContent = RenderContent(),
            Height = "100px",
            Width = "100px",
            IsBackdrop = true,
            OnClickBackdrop = () => Task.CompletedTask,
            OnCloseAsync = () => Task.CompletedTask,
            Placement = Placement.Bottom,
            ShowBackdrop = true,
            BodyContext = "test-body-context",
            IsKeyboard = true,
            BodyScroll = true,
            ZIndex = 1066
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>();
        await service.Show(option);
        cut.Contains("data-bb-keyboard=\"true\"");
        cut.Contains("--bb-drawer-zindex: 1066;");
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        option.ChildContent = null;
        option.Component = BootstrapDynamicComponent.CreateComponent<DialogCloseButton>();
        await service.Show(option);
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        option.Component = null;
        Assert.Null(option.GetContent());

        await service.Show<DrawerDemo>();
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        var type = typeof(DrawerDemo);
        await service.Show(type);
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    private static RenderFragment RenderContent() => builder =>
    {
        builder.AddContent(0, "drawer-content");
        builder.OpenComponent<DialogCloseButton>(0);
        builder.CloseComponent();
    };

    class DrawerDemo : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, RenderContent());
        }
    }
}
