// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.Rendering;

namespace UnitTest.Components;

public class PopConfirmButtonTest : PopoverTestBase
{
    [Fact]
    public void Show_Ok()
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
            });
        });

        // Show
        cut.InvokeAsync(() =>
        {
            var button = cut.Find("div");
            button.Click();
        });

        // Close
        cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".popover-confirm-buttons div");
            buttons[0].Click();
        });

        // Confirm
        cut.InvokeAsync(() =>
        {
            var button = cut.Find("div");
            button.Click();
        });
        cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".popover-confirm-buttons div");
            buttons[1].Click();
        });

        // 重置增加 按钮回调方法
        var confirm = false;
        var close = false;
        var beforeClose = false;

        cut.InvokeAsync(() =>
        {
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
        });
        // 默认设置增加 shadow 样式
        Assert.Contains("data-bs-custom-class=\"test-custom-class shadow\"", cut.Markup);

        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            // 移除 shadow 样式
            popButton.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.ShowShadow, false);
            });
        });
        Assert.Contains("data-bs-custom-class=\"test-custom-class\"", cut.Markup);

        // 弹窗
        cut.InvokeAsync(() =>
        {
            var button = cut.Find("div");
            button.Click();
        });

        // Close
        cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".popover-confirm-buttons div");
            buttons[0].Click();
        });
        Assert.True(beforeClose);
        Assert.True(close);

        // Confirm
        cut.InvokeAsync(() =>
        {
            var button = cut.Find("div");
            button.Click();
        });
        cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".popover-confirm-buttons div");
            buttons[1].Click();
        });
        Assert.True(confirm);

        // Submit
        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            popButton.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.ButtonType, ButtonType.Submit);
            });
        });

        // Show
        cut.InvokeAsync(() =>
        {
            var button = cut.Find("div");
            button.Click();
        });

        // Confirm
        cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".popover-confirm-buttons div");
            buttons[1].Click();
        });

        // IsAsync
        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            popButton.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.IsAsync, true);
            });
        });

        // Show
        cut.InvokeAsync(() =>
        {
            var button = cut.Find("div");
            button.Click();
        });

        // async confirm
        cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".popover-confirm-buttons div");
            buttons[1].Click();
        });

        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            popButton.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.ButtonType, ButtonType.Button);
            });
        });

        // Show
        cut.InvokeAsync(() =>
        {
            var button = cut.Find("div");
            button.Click();
        });
        // async confirm
        cut.InvokeAsync(() =>
        {
            var buttons = cut.FindAll(".popover-confirm-buttons div");
            buttons[1].Click();
        });

        // IsLink
        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            popButton.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.IsLink, true);
            });
        });

        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            popButton.Contains("<a id=");
        });

        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            popButton.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.BodyTemplate, builder =>
                {
                    builder.OpenComponent<Button>(0);
                    builder.CloseComponent();
                });
            });
        });

        cut.InvokeAsync(() =>
        {
            var popButton = cut.FindComponent<PopConfirmButton>();
            Assert.NotNull(popButton.FindComponent<Button>());
        });
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
    public void ConfirmIcon_Ok()
    {
        var cut = Context.RenderComponent<PopConfirmButtonContent>();
        cut.Contains("text-info fa-solid fa-circle-exclamation");
    }

    [Fact]
    public void TooltipText_Null()
    {
        var cut = Context.RenderComponent<PopConfirmButton>(pb =>
        {
            pb.Add(a => a.TooltipText, "");
        });
        cut.InvokeAsync(() =>
        {
            Assert.Throws<ComponentNotFoundException>(() => cut.FindComponent<Tooltip>());
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.TooltipText, "test");
        });
        cut.InvokeAsync(() =>
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
}
