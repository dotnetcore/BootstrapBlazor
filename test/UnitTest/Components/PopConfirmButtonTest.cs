// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Bunit.Rendering;

namespace UnitTest.Components;

public class PopConfirmButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Show_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<PopConfirmButton>(pb =>
            {
                pb.Add(a => a.Placement, Placement.Left);
                pb.Add(a => a.Title, "Test_Title");
                pb.Add(a => a.CloseButtonColor, Color.Info);
                pb.Add(a => a.ConfirmButtonColor, Color.Danger);
                pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
                pb.Add(a => a.Text, "Test_Text");
                pb.Add(a => a.CloseButtonIcon, "fa-solid fa-xmark");
                pb.Add(a => a.ConfirmButtonIcon, "fa-solid fa-check");
            });
        });

        cut.Contains("fa-solid fa-xmark");
        cut.Contains("fa-solid fa-check");

        // Show
        var button = cut.Find("div");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });

        // Close
        var buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() =>
        {
            buttons[0].Click();
        });

        // Confirm
        button = cut.Find("div");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });
        buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() =>
        {
            buttons[1].Click();
        });

        // 重置增加 按钮回调方法
        var confirm = false;
        var close = false;
        var beforeClose = false;

        var popButton = cut.FindComponent<PopConfirmButton>();
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Content, "Test_Content");
            pb.Add(a => a.ConfirmButtonText, "Test_Confirm_Text");
            pb.Add(a => a.CloseButtonText, "Test_Close_Text");
            pb.Add(a => a.CustomClass, "test-custom-class");
            pb.Add(a => a.OnConfirm, () =>
            {
                confirm = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnClose, () =>
            {
                close = true;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnBeforeClick, () =>
            {
                beforeClose = true;
                return Task.FromResult(true);
            });
        });
        // 默认设置增加 shadow 样式
        Assert.Contains("data-bs-custom-class=\"test-custom-class shadow\"", cut.Markup);

        // 移除 shadow 样式
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowShadow, false);
        });
        Assert.Contains("data-bs-custom-class=\"test-custom-class\"", cut.Markup);

        // 弹窗
        button = cut.Find("div");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });

        // Close
        buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() =>
        {
            buttons[0].Click();
        });
        Assert.True(beforeClose);
        Assert.True(close);

        // Confirm
        button = cut.Find("div");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });
        buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() =>
        {
            buttons[1].Click();
        });
        Assert.True(confirm);

        // Submit
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ButtonType, ButtonType.Submit);
        });

        // Show
        button = cut.Find("div");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });

        // Confirm
        buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() =>
        {
            buttons[1].Click();
        });

        // IsAsync
        var tcs = new TaskCompletionSource();
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsAsync, true);
            pb.Add(a => a.OnConfirm, () =>
            {
                tcs.TrySetResult();
                return Task.CompletedTask;
            });
        });

        // Show
        button = cut.Find("div");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });

        // async confirm
        buttons = cut.FindAll(".popover-confirm-buttons button");
        _ = cut.InvokeAsync(() =>
        {
            buttons[1].Click();
        });
        await tcs.Task;

        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ButtonType, ButtonType.Button);
        });

        // Show
        button = cut.Find("div");
        await cut.InvokeAsync(() =>
        {
            button.Click();
        });

        // async confirm
        buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() =>
        {
            buttons[1].Click();
        });

        // IsLink
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsLink, true);
        });

        await cut.InvokeAsync(() =>
        {
            popButton.Contains("<a id=");
        });

        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.BodyTemplate, builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.CloseComponent();
            });
        });

        await cut.InvokeAsync(() =>
        {
            Assert.NotNull(popButton.FindComponent<Button>());
        });

        // ShowButton
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowCloseButton, false);
            pb.Add(a => a.ShowConfirmButton, false);
        });
        cut.DoesNotContain("<div class=\"popover-confirm-buttons\"></div>");

        // 级联参数
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowCloseButton, true);
            pb.Add(a => a.ShowConfirmButton, true);
            pb.Add(a => a.BodyTemplate, builder =>
            {
                builder.OpenComponent<MockContent>(0);
                builder.CloseComponent();
            });
        });

        var mockContent = cut.FindComponent<MockContent>();
        await cut.InvokeAsync(mockContent.Instance.Close);
        await cut.InvokeAsync(mockContent.Instance.Confirm);
    }

    [Fact]
    public void Trigger_Ok()
    {
        var cut = Context.RenderComponent<PopConfirmButton>();
        Assert.DoesNotContain("data-bs-trigger", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Trigger, "test");
        });
        Assert.Contains("data-bs-trigger=\"test\"", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<PopConfirmButtonContent>();
        cut.Contains("text-info fa-solid fa-circle-exclamation");
        cut.Contains("fa-solid fa-xmark");
        cut.Contains("fa-solid fa-check");
    }

    [Fact]
    public async Task TooltipText_Null()
    {
        var cut = Context.RenderComponent<PopConfirmButton>(pb =>
        {
            pb.Add(a => a.TooltipText, "");
        });
        await cut.InvokeAsync(() =>
        {
            Assert.Throws<ComponentNotFoundException>(() => cut.FindComponent<Tooltip>());
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.TooltipText, "test");
        });
        await cut.InvokeAsync(() =>
        {
            Assert.NotNull(cut.FindComponent<Tooltip>());
        });
    }

    [Fact]
    public void TooltipText_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "Test");
            pb.AddChildContent<PopConfirmButton>(pb =>
            {
                pb.Add(a => a.TooltipText, "pop-tooltip");
            });
        });
        cut.Contains("data-bs-original-title=\"pop-tooltip\"");
    }

    [Fact]
    public void Placement_Ok()
    {
        var cut = Context.RenderComponent<PopConfirmButton>(pb =>
        {
            pb.Add(a => a.Placement, Placement.LeftStart);
        });
        cut.DoesNotContain("data-bs-placement");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Auto);
        });
        cut.DoesNotContain("data-bs-placement");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Top);
        });
        cut.Contains("data-bs-placement=\"top\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Right);
        });
        cut.Contains("data-bs-placement=\"right\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Bottom);
        });
        cut.Contains("data-bs-placement=\"bottom\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Left);
        });
        cut.Contains("data-bs-placement=\"left\"");
    }

    public class MockContent : ComponentBase
    {
        [CascadingParameter(Name = "PopoverConfirmButtonCloseAsync"), NotNull]
        private Func<Task>? OnCloseAsync { get; set; }

        [CascadingParameter(Name = "PopoverConfirmButtonConfirmAsync"), NotNull]
        private Func<Task>? OnConfirmAsync { get; set; }

        public Task Close() => OnCloseAsync();

        public Task Confirm() => OnConfirmAsync();
    }
}
