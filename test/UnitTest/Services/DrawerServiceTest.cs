// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class DrawerServiceTest : DrawerTestBase
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
            ShowBackdrop = true
        };
        var service = Context.Services.GetRequiredService<DrawerService>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>();
        await service.Show(option);
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        await service.Show<DrawerDemo>();
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        await service.Show(typeof(DrawerDemo));
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
