// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class PopConfirmButtonTest : PopoverTestBase
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
            });
        });

        // Show
        var button = cut.Find("div");
        await cut.InvokeAsync(() => button.Click());

        // Close
        var buttons = cut.FindAll(".popover-confirm-buttons div");
        await cut.InvokeAsync(() => buttons[0].Click());

        // Confirm
        await cut.InvokeAsync(() => button.Click());
        await cut.InvokeAsync(() => buttons[1].Click());

        // 重置增加 按钮回调方法
        var confirm = false;
        var close = false;
        var beforeClose = false;
        var popButton = cut.FindComponent<PopConfirmButton>();
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Content, "Test_Cotent");
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
        await cut.InvokeAsync(() => button.Click());

        // Close
        buttons = cut.FindAll(".popover-confirm-buttons div");
        await cut.InvokeAsync(() => buttons[0].Click());
        Assert.True(beforeClose);
        Assert.True(close);

        // Confirm
        await cut.InvokeAsync(() => button.Click());
        await cut.InvokeAsync(() => buttons[1].Click());
        Assert.True(confirm);

        // Submit
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ButtonType, ButtonType.Submit);
        });

        // Show
        button = cut.Find("div");
        await cut.InvokeAsync(() => button.Click());

        // Confirm
        buttons = cut.FindAll(".popover-confirm-buttons div");
        await cut.InvokeAsync(() => buttons[1].Click());

        // IsAsync
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsAsync, true);
        });

        // Show
        button = cut.Find("div");
        await cut.InvokeAsync(() => button.Click());

        // async confirm
        buttons = cut.FindAll(".popover-confirm-buttons div");
        await cut.InvokeAsync(() => buttons[1].Click());

        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ButtonType, ButtonType.Button);
        });

        // Show
        button = cut.Find("div");
        await cut.InvokeAsync(() => button.Click());

        // async confirm
        buttons = cut.FindAll(".popover-confirm-buttons div");
        await cut.InvokeAsync(() => buttons[1].Click());

        // IsLink
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsLink, true);
        });
        popButton.Contains("<a id=");

        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.BodyTemplate, bulider =>
            {
                bulider.OpenComponent<Button>(0);
                bulider.CloseComponent();
            });
        });
        Assert.NotNull(popButton.FindComponent<Button>());
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
}
