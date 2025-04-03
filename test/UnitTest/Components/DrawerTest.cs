// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DrawerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder => builder.Add(a => a.Width, "100px"));
        Assert.Contains("width: 100px", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Width, "");
        });
        Assert.DoesNotContain("width:", cut.Markup);
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.Height, "100px");
            builder.Add(a => a.Placement, Placement.Top);
        });
        Assert.Contains("height: 100px", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Height, "");
        });
        Assert.DoesNotContain("height:", cut.Markup);
    }

    [Fact]
    public void IsOpen_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.IsOpen, true);
            builder.Add(a => a.Placement, Placement.Top);
        });
    }

    [Fact]
    public void AllowResize_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.AllowResize, true);
        });
        cut.Contains("<div class=\"drawer-bar\"><div class=\"drawer-bar-body\"></div></div>");
    }

    [Fact]
    public void IsOpenChanged_Ok()
    {
        var isOpen = true;
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.IsBackdrop, true);
            builder.Add(a => a.IsOpen, true);
            builder.Add(a => a.IsOpenChanged, EventCallback.Factory.Create<bool>(this, e =>
            {
                isOpen = e;
            }));
        });

        cut.Find(".drawer-backdrop").Click();
        Assert.False(isOpen);
    }

    [Fact]
    public void OnClickBackdrop_Ok()
    {
        var isOpen = true;
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.IsBackdrop, true);
            builder.Add(a => a.IsOpen, true);
            builder.Add(a => a.OnClickBackdrop, () => { isOpen = false; return Task.CompletedTask; });
        });

        cut.Find(".drawer-backdrop").Click();
        Assert.False(isOpen);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });

        var button = cut.FindComponent<Button>();
        Assert.NotNull(button);
    }

    [Fact]
    public void BodyContext_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.BodyContext, "test-body-context");
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<MockContent>(0);
                s.CloseComponent();
            });
        });

        var component = cut.FindComponent<MockContent>();
        Assert.NotNull(component);

        Assert.Equal("test-body-context", component.Instance.GetBodyContext());
    }

    [Fact]
    public void ShowBackdrop_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.ShowBackdrop, true);
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });
        cut.Contains("drawer-backdrop");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowBackdrop, false);
        });
        cut.DoesNotContain("drawer-backdrop");
    }

    [Fact]
    public void Position_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.Position, "absolute");
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });
        cut.Contains("--bb-drawer-position: absolute;");
    }

    [Fact]
    public void ZIndex_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.ZIndex, 1055);
        });
        cut.Contains("--bb-drawer-zindex: 1055;");
    }

    [Fact]
    public void IsKeyboard_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.IsKeyboard, true);
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });
        cut.Contains("data-bb-keyboard=\"true\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsKeyboard, false);
        });
        cut.DoesNotContain("data-bb-keyboard=\"true\"");
    }

    [Fact]
    public void BodyScroll_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.BodyScroll, true);
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });
        cut.Contains("data-bb-scroll=\"true\"");
    }

    [Fact]
    public async Task Close_Ok()
    {
        Context.JSInterop.Setup<bool>("execute", matcher => true).SetResult(true);
        var cut = Context.RenderComponent<Drawer>();
        await cut.InvokeAsync(() => cut.Instance.Close());
    }

    class MockContent : ComponentBase
    {
        [CascadingParameter(Name = "BodyContext")]
        [NotNull]
        private object? BodyContext { get; set; }

        public string? GetBodyContext() => BodyContext.ToString();
    }
}
