// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TransferTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        // 未设置 Items 为空
        var cut = Context.RenderComponent<Transfer<string>>();
        cut.Contains("class=\"bb-transfer\"");

        // 设置 Items
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.OnSetItemClass, new Func<SelectedItem, string>(i => $"class-{i.Value}"));
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
        });
        cut.Contains("transfer-panel");
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Height, "200px");
        });
        cut.Contains("--bb-transfer-height: 200px;");
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
                pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(Context, v => foo.Name = v));
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
            pb.Add(a => a.LeftPanelSearchPlaceHolderString, "LeftPanelSearchPlaceHolderString");
            pb.Add(a => a.RightPanelSearchPlaceHolderString, "RightPanelSearchPlaceHolderString");
        });

        // ShowSearch
        cut.Contains("transfer-panel-filter");

        cut.Contains("LeftButtonText");
        cut.Contains("RightButtonText");
        cut.Contains("LeftPanelSearchPlaceHolderString");
        cut.Contains("RightPanelSearchPlaceHolderString");
    }

    [Fact]
    public void MaxMin_Ok()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Value, foo.Name);
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string?>(this, v => foo.Name = v));
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

    [Fact]
    public void IsWrapItem_Ok()
    {
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1 with a very long text"),
                new("2", "Test2 with a very long text")
            });
            pb.Add(a => a.IsWrapItem, false);
        });
        cut.DoesNotContain("wrap-item");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsWrapItem, true);
        });
        cut.Contains("wrap-item");
    }

    [Fact]
    public void ItemWidth_Ok()
    {
        var cut = Context.RenderComponent<Transfer<string>>(pb =>
        {
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.Items, new List<SelectedItem>()
            {
                new("1", "Test1 with a very long text"),
                new("2", "Test2 with a very long text")
            });
        });
        cut.DoesNotContain("--bb-transfer-panel-item-width");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ItemWidth, "160px");
        });
        cut.Contains("--bb-transfer-panel-item-width: 160px;");
    }
}
