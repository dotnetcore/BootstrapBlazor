// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Services;

public class FullScreenServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ButtonIcon_Ok()
    {
        var cut = Context.Render<FullScreenButton>(builder =>
        {
            builder.Add(s => s.Icon, "fa-solid fa-maximize");
            builder.Add(s => s.Text, "button-text");
            builder.Add(s => s.TargetId, "fsId");
        });
        cut.Contains("bb-fs-off");
        cut.Contains("bb-fs-on");

        var element = cut.Find(".btn-fs");
        await cut.InvokeAsync(() => element.Click());
    }

    [Fact]
    public void FullScreenIcon_Ok()
    {
        var cut = Context.Render<FullScreenButton>(builder => builder.Add(s => s.Icon, "fa-test"));
        cut.Contains("fa-test");
    }

    [Fact]
    public async Task ToggleByElement_Ok()
    {
        ElementReference element = default;
        var cut = Context.Render(builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddElementReferenceCapture(1, e =>
            {
                element = e;
            });
            builder.CloseElement();

            builder.OpenComponent<MockFullScreen>(0);
            builder.CloseComponent();
        });
        var fs = cut.FindComponent<MockFullScreen>();
        await cut.InvokeAsync(() => fs.Instance.Test(element));
    }

    [Fact]
    public async Task ToggleById_Ok()
    {
        var cut = Context.Render(builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", "test-id");
            builder.CloseElement();

            builder.OpenComponent<MockFullScreen>(0);
            builder.CloseComponent();
        });
        var fs = cut.FindComponent<MockFullScreen>();
        await cut.InvokeAsync(() => fs.Instance.TestById("test-id"));
    }

    [Fact]
    public async Task Toggle_Ok()
    {
        var cut = Context.Render(builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", "test-id");
            builder.CloseElement();

            builder.OpenComponent<MockFullScreen>(0);
            builder.CloseComponent();
        });
        var fs = cut.FindComponent<MockFullScreen>();
        await cut.InvokeAsync(fs.Instance.Toggle);
    }

    [Fact]
    public void FullScreenOption_Ok()
    {
        var option = new FullScreenOption() { Element = new("test01", null), Id = "test", Selector = "test-selector" };
        Assert.NotNull(option.Id);
        Assert.Null(option.Element.Context);
        Assert.NotNull(option.Selector);
    }

    private class MockFullScreen : ComponentBase
    {
        [Inject]
        [NotNull]
        public FullScreenService? FullScreenService { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
        }

        public Task Test(ElementReference ele) => FullScreenService.ToggleByElement(ele);

        public Task TestById(string id) => FullScreenService.ToggleById(id);

        public Task Toggle() => FullScreenService.Toggle();
    }
}
