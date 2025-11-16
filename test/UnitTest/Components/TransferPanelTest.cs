// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TransferPanelTest : BootstrapBlazorTestBase
{
    [Fact]
    public void HeaderCheckState_Ok()
    {
        var cut = Context.Render<TransferPanel>();

        // Items 为空时全选 Checkbox
        var checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.UnChecked));

        cut.Render(pb =>
        {
            pb.Add(a => a.Items,
            [
                new("1", "Test1"),
                new("2", "Test2")
            ]);
        });
        checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.UnChecked));

        // 显示 Search
        cut.Render(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
        });
        cut.WaitForAssertion(() => cut.Contains("input-inner"));

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
        var cut = Context.Render<TransferPanel>(pb =>
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
        var cut = Context.Render<TransferPanel>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.IsDisabled, true);
        });
        cut.Contains("disabled=\"disabled\"");
    }

    [Fact]
    public void HeaderTemplate_Ok()
    {
        var cut = Context.Render<TransferPanel>(pb =>
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
        var cut = Context.Render<TransferPanel>(pb =>
        {
            pb.Add(a => a.Items,
            [
                new("1", "Test1"),
                new("2", "Test2")
            ]);
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"ItemTemplate-Test-{item.Text}");
            });
        });
        cut.Contains("ItemTemplate-Test-Test1");
        cut.Contains("ItemTemplate-Test-Test2");
    }
}
