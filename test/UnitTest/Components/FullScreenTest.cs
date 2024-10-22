﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Components;

public class FullScreenTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ButtonIcon_Ok()
    {
        var cut = Context.RenderComponent<FullScreenButton>(builder =>
        {
            builder.Add(s => s.Icon, "fa-solid fa-maximize");
            builder.Add(s => s.Text, "button-text");
        });
        var elements = cut.FindAll(".fa-maximize");
        Assert.Single(elements);
        cut.Contains("bb-fs-off");
        cut.Contains("bb-fs-on");
    }

    [Fact]
    public void FullScreenIcon_Ok()
    {
        var cut = Context.RenderComponent<FullScreenButton>(builder => builder.Add(s => s.Icon, "fa-test"));
        cut.Contains("fa-test");
    }

    [Fact]
    public async Task ToggleFullScreen_Ok()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        ConfigureServices(context.Services);

        var cut = context.RenderComponent<BootstrapBlazorRoot>(builder =>
        {
            builder.Add(s => s.ChildContent, new RenderFragment(builder =>
            {
                builder.OpenComponent<FullScreenButton>(0);
                builder.CloseComponent();
            }));
        });
        var component = cut.FindComponent<FullScreen>();
        var id = component.Instance.Id;
        context.JSInterop.SetupVoid("execute", id);

        var button = cut.Find(".btn-fs");
        await cut.InvokeAsync(() => button.Click());

        var invocation = context.JSInterop.VerifyInvoke("execute");
        Assert.Equal(id, invocation.Arguments[0]);

        var options = invocation.Arguments[1] as FullScreenOption;
        Assert.NotNull(options);
        Assert.Null(options.Id);
        Assert.Null(options.Element.Id);
        Assert.Null(options.Element.Context);

        void ConfigureServices(IServiceCollection services)
        {
            services.AddBootstrapBlazor();
            services.ConfigureJsonLocalizationOptions(op =>
            {
                op.IgnoreLocalizerMissing = false;
            });
        }
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

    [Fact]
    public async Task Toggle_Ok()
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
        await cut.InvokeAsync(fs.Instance.Toggle);
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

        public Task Test(ElementReference ele) => FullScreenService.ToggleByElement(ele);

        public Task TestById(string id) => FullScreenService.ToggleById(id);

        public Task Toggle() => FullScreenService.Toggle();
    }
}
