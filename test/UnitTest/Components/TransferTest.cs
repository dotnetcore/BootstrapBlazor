// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class TransferTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        // 未设置 Itms 为空
        var cut = Context.RenderComponent<Transfer<string>>();
        cut.Contains("class=\"transfer\"");

        // 设置 Items
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        cut.Contains("transfer-panel");
    }

    [Fact]
    public async Task EnumerableString_Value()
    {
        var cut = Context.RenderComponent<Transfer<IEnumerable<string>>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });

        // 选中左侧第一项
        var checkbox = cut.FindComponents<Checkbox<SelectedItem>>().First(i => i.Instance.DisplayText == "Test1");
        await cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        Assert.Equal("1", cut.Instance.Value.First());
    }

    [Fact]
    public async Task SelectedItem_Value()
    {
        var cut = Context.RenderComponent<Transfer<IEnumerable<SelectedItem>>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });

        // 选中右侧第一项
        var checkbox = cut.FindComponents<Checkbox<SelectedItem>>().First(i => i.Instance.DisplayText == "Test1");
        await cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        Assert.Equal("1", cut.Instance.Value.First().Value);
    }

    [Fact]
    public void Int_Value()
    {
        var cut = Context.RenderComponent<Transfer<int>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });

        // 选中右侧第一项
        var checkbox = cut.FindComponents<Checkbox<SelectedItem>>().First(i => i.Instance.DisplayText == "Test1");
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        Assert.Equal(0, cut.Instance.Value);
    }

    [Fact]
    public async Task TransferItem_Ok()
    {
        IEnumerable<SelectedItem> rightItems = new List<SelectedItem>();
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.OnSelectedItemsChanged, items =>
            {
                rightItems = items;
                return Task.CompletedTask;
            });
        });

        // 选中移动到右侧按钮并且点击
        var checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        await cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        // 右侧共两项
        Assert.Equal(2, rightItems.Count());

        // 选中右侧第一项
        checkbox = cut.FindComponents<Checkbox<SelectedItem>>().First(i => i.Instance.DisplayText == "Test1");
        await cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));

        button = cut.FindComponents<Button>()[0];
        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        // 右侧共一项
        Assert.Single(rightItems);
    }

    [Fact]
    public async Task ValidateForm_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<Transfer<string>>(pb =>
            {
                pb.Add(a => a.Value, foo.Name);
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(Context, v => foo.Name = v));
                pb.Add(a => a.ValueExpression, foo.GenerateValueExpression());
                pb.Add(a => a.Items, new List<SelectedItem>()
                {
                    new("1", "Test1"),
                    new("2", "Test2")
                });
            });
        });
        var checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        await cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        await cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        Assert.Equal("1,2", foo.Name);
    }

    [Fact]
    public void ShowSearch_Ok()
    {
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.LeftButtonText, "LeftButtonText");
            pb.Add(a => a.RightButtonText, "RightButtonText");
            pb.Add(a => a.LeftPannelSearchPlaceHolderString, "LeftPannelSearchPlaceHolderString");
            pb.Add(a => a.RightPannelSearchPlaceHolderString, "RightPannelSearchPlaceHolderString");
        });

        // ShowSearch
        cut.Contains("transfer-panel-filter");

        cut.Contains("LeftButtonText");
        cut.Contains("RightButtonText");
        cut.Contains("LeftPannelSearchPlaceHolderString");
        cut.Contains("RightPannelSearchPlaceHolderString");
    }

    [Fact]
    public void MaxMin_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Value, foo.Name);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v => foo.Name = v));
            pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, "Name", typeof(string)));
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2"),
                new("3", "Test3"),
                new("4", "Test4")
            });
            pb.Add(a => a.Min, 1);
            pb.Add(a => a.Max, 2);
        });

        // 选中移动到右侧按钮并且点击
        var checkbox = cut.FindComponent<Checkbox<SelectedItem>>();
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Max, 3);
        });
    }

    [Fact]
    public void Template_Ok()
    {
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Value, "2,4");
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2") { Active = true },
                new("3", "Test3"),
                new("4", "Test4") { Active = true }
            });
            pb.Add(a => a.LeftHeaderTemplate, items => builder =>
            {
                builder.AddContent(0, "Left-HeaderTemplate");
            });
            pb.Add(a => a.LeftItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"Left-ItemTemplate-{item.Text}");
            });
            pb.Add(a => a.RightHeaderTemplate, items => builder =>
            {
                builder.AddContent(0, "Right-HeaderTemplate");
            });
            pb.Add(a => a.RightItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"Right-ItemTemplate-{item.Text}");
            });
        });
        cut.Contains("Left-HeaderTemplate");
        cut.Contains("Left-ItemTemplate-Test1");
        cut.Contains("Left-ItemTemplate-Test3");
        cut.Contains("Right-HeaderTemplate");
        cut.Contains("Right-ItemTemplate-Test2");
        cut.Contains("Right-ItemTemplate-Test4");
    }
}
