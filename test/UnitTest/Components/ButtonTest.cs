// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using System;

namespace UnitTest.Components;

public class ButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ButtonStyle_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.ButtonStyle, ButtonStyle.None);
        });
        Assert.DoesNotContain("btn-round", cut.Markup);
        Assert.DoesNotContain("btn-circle", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.ButtonStyle, ButtonStyle.Circle);
        });
        Assert.Contains("btn-circle", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.ButtonStyle, ButtonStyle.Round);
        });
        Assert.Contains("btn-round", cut.Markup);
    }

    [Fact]
    public void Popover_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.AddChildContent<Popover>(pb =>
            {
                pb.Add(t => t.Title, "popover-title");
            });
        });
        cut.Contains("data-bs-toggle=\"popover\" data-bs-original-title=\"popover-title\" data-bs-placement=\"top\" data-bs-custom-class=\"shadow\" data-bs-trigger=\"focus hover\"");

        // 切换 Disabled 状态移除 Popover
        cut.Render(pb =>
        {
            pb.Add(b => b.IsDisabled, true);
        });
        var button = cut.Find("button");
        var d = button.GetAttribute("disabled");
        Assert.Equal("disabled", d);

        cut.Render(pb =>
        {
            pb.Add(b => b.IsDisabled, false);
        });
        button = cut.Find("button");
        Assert.False(button.HasAttribute("disabled"));
    }

    [Fact]
    public void ButtonType_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.ButtonType, ButtonType.Button);
        });
        Assert.Contains("type=\"button\"", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.ButtonType, ButtonType.Submit);
        });
        Assert.Contains("type=\"submit\"", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.ButtonType, ButtonType.Reset);
        });
        Assert.Contains("type=\"reset\"", cut.Markup);
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.Text, "Test");
        });
        Assert.Contains("<span>Test</span>", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.Text, null);
            pb.AddChildContent("Button-Test");
        });
        Assert.Contains("Button-Test", cut.Markup);
    }

    [Theory]
    [InlineData(Color.Primary, "btn-primary")]
    [InlineData(Color.Secondary, "btn-secondary")]
    [InlineData(Color.Info, "btn-info")]
    [InlineData(Color.Success, "btn-success")]
    [InlineData(Color.Warning, "btn-warning")]
    [InlineData(Color.Danger, "btn-danger")]
    [InlineData(Color.Light, "btn-light")]
    [InlineData(Color.Dark, "btn-dark")]
    [InlineData(Color.Link, "btn-link")]
    [InlineData(Color.None, "btn")]
    public void Color_Ok(Color color, string @class)
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.Color, color);
        });
        Assert.Contains(@class, cut.Markup);
    }

    [Fact]
    public void DialogCloseButton_Color()
    {
        var cut = Context.Render<DialogCloseButton>();
        Assert.Contains("btn-secondary", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
        });
        Assert.Contains("btn-danger", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.Icon, "fa-solid fa-font-awesome");
        });
        Assert.Contains("class=\"fa-solid fa-font-awesome\"", cut.Markup);
        Assert.Contains("fa-solid fa-spin fa-spinner", cut.Instance.LoadingIcon);

        cut.Render(pb =>
        {
            pb.Add(b => b.LoadingIcon, "fa-solid fa-font-awesome");
        });
        Assert.Contains("fa-solid fa-font-awesome", cut.Instance.LoadingIcon);
    }

    [Fact]
    public async Task IsAsync_Ok()
    {
        // 同步点击
        var clicked = false;
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.IsAsync, false);
            pb.Add(b => b.OnClick, e => clicked = true);
        });
        var b = cut.Find("button");
        b.Click();
        Assert.True(clicked);

        // 异步点击
        var tcs = new TaskCompletionSource<bool>();
        clicked = false;
        cut.Render(pb =>
        {
            pb.Add(b => b.IsAsync, true);
            pb.Add(b => b.OnClick, async e =>
            {
                await Task.Delay(10);
                clicked = true;
                tcs.SetResult(true);
            });
        });
        b.Click();
        Assert.False(clicked);
        await tcs.Task;
        Assert.True(clicked);

        // 同步无刷新点击
        clicked = false;
        cut.Render(pb =>
        {
            pb.Add(b => b.IsAsync, false);
            pb.Add(b => b.OnClick, EventCallback<MouseEventArgs>.Empty);
            pb.Add(b => b.OnClickWithoutRender, () =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        b.Click();
        Assert.True(clicked);

        // 异步无刷新点击
        clicked = false;
        tcs = new TaskCompletionSource<bool>();
        cut.Render(pb =>
        {
            pb.Add(b => b.IsAsync, true);
            pb.Add(b => b.OnClick, EventCallback<MouseEventArgs>.Empty);
            pb.Add(b => b.OnClickWithoutRender, async () =>
            {
                await Task.Delay(10);
                clicked = true;
                tcs.SetResult(true);
            });
        });
        b.Click();
        Assert.False(clicked);
        Assert.True(cut.Instance.IsDisabled);
        await tcs.Task;
        Assert.True(clicked);
        cut.WaitForState(() => cut.Instance.IsDisabled == false);

        cut.Render(pb =>
        {
            pb.Add(b => b.IsAsync, true);
            pb.Add(b => b.IsKeepDisabled, true);
        });
        b.Click();
        Assert.True(cut.Instance.IsDisabled);
        await tcs.Task;
        Assert.True(cut.Instance.IsDisabled);
    }

    [Fact]
    public void IsOutline_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.IsOutline, true);
        });
        Assert.Contains("btn-outline-primary", cut.Markup);
    }

    [Theory]
    [InlineData(Size.ExtraSmall, "btn-xs")]
    [InlineData(Size.Small, "btn-sm")]
    [InlineData(Size.Medium, "btn-md")]
    [InlineData(Size.Large, "btn-lg")]
    [InlineData(Size.ExtraLarge, "btn-xl")]
    [InlineData(Size.ExtraExtraLarge, "btn-xxl")]
    public void Size_Ok(Size size, string @class)
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.Size, size);
        });
        Assert.Contains(@class, cut.Markup);
    }

    [Fact]
    public void IsBlock_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.IsBlock, true);
        });
        Assert.Contains("btn-block", cut.Markup);
    }

    [Fact]
    public void StopPropagation_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(b => b.StopPropagation, true);
        });
        cut.Contains("blazor:onclick:stopPropagation");
    }

    [Fact]
    public async Task SetDisable_Ok()
    {
        var cut = Context.Render<Button>();
        Assert.DoesNotContain("disabled=\"disabled\"", cut.Markup);
        Assert.Contains("aria-disabled=\"false\"", cut.Markup);

        await cut.InvokeAsync(() => cut.Instance.SetDisable(true));
        Assert.Contains("disabled=\"disabled\"", cut.Markup);
        Assert.Contains("aria-disabled=\"true\"", cut.Markup);
    }

    [Fact]
    public async Task Tooltip_Ok()
    {
        var cut = Context.Render<Tooltip>(pb =>
        {
            pb.Add(a => a.Placement, Placement.Top);
            pb.Add(a => a.Title, "Tooltip");
            pb.AddChildContent<Button>();
        });

        var button = cut.FindComponent<Button>();
        await cut.InvokeAsync(() => button.Instance.ShowTooltip());

        button.Render(pb =>
        {
            pb.Add(a => a.TooltipText, "Tooltip-Button");
        });
        Assert.Equal("Tooltip-Button", cut.Instance.Title);

        var cut1 = Context.Render<Button>(pb =>
        {
            pb.Add(a => a.TooltipText, "tooltip");
        });
        await cut1.InvokeAsync(() => cut1.Instance.ShowTooltip());
    }

    [Fact]
    public async Task ValidateFormButton_Ok()
    {
        var valid = false;
        var tcs = new TaskCompletionSource<bool>();
        var model = Foo.Generate(Context.Services.GetRequiredService<IStringLocalizer<Foo>>());
        var cut = Context.Render<ValidateForm>(pb =>
        {
            pb.Add(v => v.Model, model);
            pb.Add(v => v.OnValidSubmit, context =>
            {
                valid = true;
                tcs.SetResult(true);
                return Task.CompletedTask;
            });
            pb.AddChildContent<BootstrapInput<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueChanged, v => model.Name = v);
                pb.Add(a => a.ValueExpression, model.GenerateValueExpression());
            });
            pb.AddChildContent<Button>(pb =>
            {
                pb.Add(b => b.IsAsync, true);
                pb.Add(b => b.ButtonType, ButtonType.Submit);
            });
        });
        cut.Find("input").Change("Test1");
        cut.Find("form").Submit();
        await tcs.Task;
        Assert.True(valid);
    }

    [Fact]
    public void ShowTooltip_Ok()
    {
        var cut = Context.Render<Button>();
        var handler = Context.JSInterop.SetupVoid("showTooltip", cut.Instance.Id, "Tooltip");
        // 未调用
        cut.InvokeAsync(() => cut.Instance.ShowTooltip());
        handler.VerifyNotInvoke("showTooltip");

        cut.Render(pb =>
        {
            pb.Add(a => a.TooltipText, "Tooltip");
        });
        // 调用
        Assert.Equal("Tooltip", cut.Instance.TooltipText);
        handler.VerifyInvoke("showTooltip");
    }

    [Fact]
    public void Trigger_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(a => a.TooltipTrigger, "click");
        });
        cut.Contains("data-bs-trigger=\"click\"");
    }

    [Fact]
    public void RemoveTooltip_Ok()
    {
        var cut = Context.Render<Button>();
        cut.InvokeAsync(() => cut.Instance.RemoveTooltip());
        Assert.Null(cut.Instance.TooltipText);
    }

    [Fact]
    public void IsAutoFocus_Ok()
    {
        var cut = Context.Render<Button>(pb =>
        {
            pb.Add(a => a.IsAutoFocus, true);
        });
    }

    [Fact]
    public void DialogCloseButton_Ok()
    {
        var clicked = false;
        var cut = Context.Render<DialogCloseButton>(pb =>
        {
            pb.AddCascadingValue<Func<Task>>(() =>
            {
                clicked = true;
                return Task.FromResult(0);
            });
        });
        var button = cut.Find("button");
        cut.InvokeAsync(() => button.Click());
        Assert.True(clicked);
    }

    [Fact]
    public void DialogSaveButton_Ok()
    {
        var clicked = false;
        var cut = Context.Render<DialogSaveButton>(pb =>
        {
            pb.AddCascadingValue<Func<Task>>(() =>
            {
                clicked = true;
                return Task.FromResult(0);
            });
            pb.Add(a => a.OnSaveAsync, () => Task.FromResult(true));
        });
        cut.Contains("button type=\"submit\"");
        var button = cut.Find("button");
        cut.InvokeAsync(() => button.Click());
        Assert.True(clicked);

        clicked = false;
        cut.Render(pb => pb.Add(a => a.OnSaveAsync, () => Task.FromResult(false)));
        button = cut.Find("button");
        cut.InvokeAsync(() => button.Click());
        Assert.False(clicked);
    }

    [Fact]
    public void ShareButton_Ok()
    {
        var cut = Context.Render<ShareButton>(pb =>
        {
            pb.Add(a => a.ShareContext, new ShareButtonContext() { Text = "test-share-text", Title = "test-share-title", Url = "www.blazor.zone" });
        });

        cut.InvokeAsync(() =>
        {
            var button = cut.Find("button");
            button.Click();
        });

        Assert.Equal("test-share-text", cut.Instance.ShareContext?.Text);
        Assert.Equal("test-share-title", cut.Instance.ShareContext?.Title);
        Assert.Equal("www.blazor.zone", cut.Instance.ShareContext?.Url);
    }

    [Fact]
    public async Task ToogleButton_Ok()
    {
        var active = false;
        var bindActive = false;
        var clickWithoutRender = false;
        var clicked = false;
        var tcs = new TaskCompletionSource();
        var cut = Context.Render<ToggleButton>(pb =>
        {
            pb.Add(a => a.IsActive, false);
            pb.Add(a => a.IsActiveChanged, EventCallback.Factory.Create<bool>(this, b =>
            {
                active = b;
                bindActive = true;
            }));
            pb.Add(a => a.OnClickWithoutRender, () =>
            {
                clickWithoutRender = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnClick, () =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnToggleAsync, async isActive =>
            {
                await Task.Delay(10);
                active = isActive;
                tcs.TrySetResult();
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        await tcs.Task;
        Assert.True(active);
        Assert.True(bindActive);

        Assert.True(clickWithoutRender);
        Assert.True(clicked);
    }

    [Fact]
    public async Task ToggleButton_IsAsync()
    {
        var active = false;
        var tcs = new TaskCompletionSource();
        var cut = Context.Render<ToggleButton>(pb =>
        {
            pb.Add(a => a.IsAsync, true);
            pb.Add(a => a.Icon, "fa-solid fa-test");
            pb.Add(a => a.Text, "toggle-button");
            pb.Add(a => a.OnClick, async () =>
            {
                await Task.Delay(100);
            });
            pb.Add(a => a.OnToggleAsync, isActive =>
            {
                active = isActive;
                tcs.TrySetResult();
                return Task.CompletedTask;
            });
        });
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        await tcs.Task;
    }
}
