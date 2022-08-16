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
                pb.Add(a => a.ConfirmIcon, "fa fa-exclamation-circle text-info");
                pb.Add(a => a.Icon, "fa fa-fa");
                pb.Add(a => a.Text, "Test_Text");
            });
        });

        // Show
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        // Close
        var buttons = cut.FindAll(".popover-confirm-buttons button");
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
            pb.Add(a => a.CssClass, "test-custom-class");
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

        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        Assert.Contains("data-bs-custom-class=\"test-custom-class\"", cut.Markup);

        // Close
        buttons = cut.FindAll(".popover-confirm-buttons button");
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
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        // Confirm
        buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() => buttons[1].Click());

        // IsAsync
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsAsync, true);
        });

        // Show
        button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());

        // Confirm
        buttons = cut.FindAll(".popover-confirm-buttons button");
        await cut.InvokeAsync(() => buttons[1].Click());

        // IsLink
        popButton.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsLink, true);
        });
        Assert.Contains("data-bs-toggle=\"confirm\"", popButton.Markup);
    }
}
