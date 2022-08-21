// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Components;

public class FullScreenTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ButtonIcon_Ok()
    {
        var cut = Context.RenderComponent<FullScreenButton>(builder => builder.Add(s => s.ButtonIcon, "fa-solid fa-maximize"));
        var ele = cut.Find(".fa-maximize");
        Assert.NotNull(ele);
    }

    [Fact]
    public void FullScreenIcon_Ok()
    {
        var cut = Context.RenderComponent<FullScreenButton>(builder => builder.Add(s => s.FullScreenIcon, "fa"));
        var ele = cut.Find(".fa");
        Assert.NotNull(ele);
    }

    [Fact]
    public void Title_Ok()
    {
        var cut = Context.RenderComponent<FullScreenButton>(builder => builder.Add(s => s.Title, "FullScreen Title"));
    }

    [Fact]
    public void ToggleFullScreen_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<FullScreenButton>(0);
                builder.CloseComponent();
            }));
        });
        cut.Find(".bb-fs").Click();
    }

    [Fact]
    public async Task ToggleByElement_Ok()
    {
        ElementReference element = default;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddElementReferenceCapture(1, e =>
                {
                    element = e;
                });
                builder.CloseElement();
                builder.OpenComponent<FullScreenServiceTest>(0);
                builder.CloseComponent();
            }));
        });
        var fs = cut.FindComponent<FullScreenServiceTest>();
        await cut.InvokeAsync(() => fs.Instance.Test(element));
    }

    [Fact]
    public async Task ToggleById_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "id", "test-id");
                builder.CloseElement();
                builder.OpenComponent<FullScreenServiceTest>(0);
                builder.CloseComponent();
            }));
        });
        var fs = cut.FindComponent<FullScreenServiceTest>();
        await cut.InvokeAsync(() => fs.Instance.TestById("test-id"));
    }

    private class FullScreenServiceTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public FullScreenService? FullScreenService { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
        }

        public async Task Test(ElementReference ele) => await FullScreenService.ToggleByElement(ele);

        public async Task TestById(string id) => await FullScreenService.ToggleById(id);
    }
}
