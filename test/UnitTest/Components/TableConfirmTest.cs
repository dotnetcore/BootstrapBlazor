// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TableConfirmTest : TableConfirmTestBase
{
    [Fact]
    public async Task TableCellPopconfirmButton_Ok()
    {
        var clicked = false;
        var trigger = false;
        var cut1 = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<TableExtensionButton>(pb =>
            {
                pb.Add(a => a.ChildContent, builder =>
                {
                    builder.OpenComponent<TableCellPopconfirmButton>(0);
                    builder.AddAttribute(1, nameof(TableCellPopconfirmButton.OnConfirm), () =>
                    {
                        clicked = true;
                        return Task.CompletedTask;
                    });
                    builder.AddAttribute(2, "AutoSelectedRowWhenClick", true);
                    builder.AddAttribute(3, "AutoRenderTableWhenClick", false);
                    builder.AddAttribute(3, "IsShow", true);
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnClickButton, args =>
                {
                    trigger = true;
                    return Task.CompletedTask;
                });
            });
        });

        var button = cut1.Find("button");
        await cut1.InvokeAsync(() => button.Click());
        var buttonConfirm = cut1.Find(".popover-confirm-buttons .btn-primary");
        await cut1.InvokeAsync(() => buttonConfirm.Click());
        Assert.True(trigger);
        Assert.True(clicked);
    }
}
