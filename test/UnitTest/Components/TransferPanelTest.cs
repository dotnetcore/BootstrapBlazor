// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TransferPanelTest : BootstrapBlazorTestBase
{
    [Fact]
    public void HeaderCheckState_Ok()
    {
        var cut = Context.RenderComponent<TransferPanel>();

        // Items 为空时全选 Checkbox
        var checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.UnChecked));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.UnChecked));

        // 显示 Search
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
        });
        var input = cut.Find(".input-inner");
        input.Input(new ChangeEventArgs()
        {
            Value = "1"
        });

        // Items 为空时全选 Checkbox
        checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));

        input.KeyUp("Escape");
    }

    [Fact]
    public void GetShownItems_Ok()
    {
        var cut = Context.RenderComponent<TransferPanel>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
        });

        var input = cut.Find(".input-inner");
        input.Input(new ChangeEventArgs()
        {
            Value = "No"
        });

        // Items 为空时全选 Checkbox
        var checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));

        // 未设置 Items 显示 Search
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<TransferPanel>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("disabled=\"disabled\"");
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.RenderComponent<TransferPanel>(pb =>
        {
            pb.Add(a => a.HeaderTemplate, items => builder =>
            {
                builder.AddContent(0, "HeaderTemplate-Test");
            });
        });
        cut.Contains("HeaderTemplate-Test");
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<TransferPanel>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"ItemTemplate-Test-{item.Text}");
            });
        });
        cut.Contains("ItemTemplate-Test-Test1");
        cut.Contains("ItemTemplate-Test-Test2");
    }
}
