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
    public void OnDisabledCallback_Ok()
    {
        // 回调根据 item 决定禁用状态：Header(null) 与 Value 为 "1" 的项被禁用
        var cut = Context.Render<TransferPanel>(pb =>
        {
            pb.Add(a => a.Items,
            [
                new("1", "Test1"),
                new("2", "Test2")
            ]);
            pb.Add(a => a.OnDisabledCallback, item => item == null || item.Value == "1");
        });

        // 索引 0 为 Header，回调返回 true 被禁用
        var checkboxes = cut.FindComponents<Checkbox<SelectedItem>>();
        Assert.True(checkboxes[0].Instance.IsDisabled);

        // 索引 1 为 Test1，回调返回 true 被禁用
        Assert.True(checkboxes[1].Instance.IsDisabled);

        // 索引 2 为 Test2，回调返回 false 未禁用
        Assert.False(checkboxes[2].Instance.IsDisabled);

        // 设置了回调时优先使用回调结果：IsDisabled=true 但回调返回 false 时仍未禁用
        cut.Render(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
            pb.Add(a => a.OnDisabledCallback, item => false);
        });
        checkboxes = cut.FindComponents<Checkbox<SelectedItem>>();
        Assert.All(checkboxes, c => Assert.False(c.Instance.IsDisabled));
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
