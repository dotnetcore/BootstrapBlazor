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
    public void EnumerableString_Value()
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
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        Assert.Equal("1", cut.Instance.Value.First());
    }

    [Fact]
    public void SelectedItem_Value()
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
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
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
    public void TransferItem_Ok()
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
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        // 右侧共两项
        Assert.Equal(2, rightItems.Count());

        // 选中右侧第一项
        checkbox = cut.FindComponents<Checkbox<SelectedItem>>().First(i => i.Instance.DisplayText == "Test1");
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        button = cut.FindComponents<Button>()[0];
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

        // 右侧共一项
        Assert.Single(rightItems);
    }

    [Fact]
    public void ValidateForm_Ok()
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
        cut.InvokeAsync(() => checkbox.Instance.SetState(CheckboxState.Checked));
        var button = cut.FindComponents<Button>()[1];
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());

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

    //[Fact]
    //public void Items_Ok()
    //{
    //    var cut = Context.RenderComponent<Transfer<string>>(builder =>
    //    {
    //        builder.Add(a => a.Items, Items);
    //    });
    //    var items = cut.FindAll(".transfer-panel-list div");

    //    Assert.True(items.Count == Items.Count());
    //}

    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; } = Enumerable.Range(1, 15).Select(i => new SelectedItem()
    {
        Text = $"DataLeft {i:d2}",
        Value = i.ToString()
    });

    [NotNull]
    private IEnumerable<SelectedItem>? ItemsRight { get; set; } = Enumerable.Range(1, 15).Select(i => new SelectedItem()
    {
        Text = $"DataRight {i:d2}",
        Value = i.ToString()
    });

    private static string? SetItemClass(SelectedItem item) => item.Value switch
    {
        "2" => "bg-success text-white",
        "4" => "bg-info text-white",
        "6" => "bg-primary text-white",
        "8" => "bg-warning text-white",
        _ => null
    };

    //[Fact]
    //public void TransferPanel_Ok()
    //{
    //    var select = false;
    //    var cut = Context.RenderComponent<TransferPanel>();
    //    cut.SetParametersAndRender(builder => builder.Add(a => a.Text, "Test"));
    //    Assert.Contains("Test", cut.Markup);

    //    //左侧头部复选框
    //    var btns = cut.Find(".transfer-panel-header input");
    //    btns.Click();
    //    cut.SetParametersAndRender(builder => builder.Add(a => a.Items, Items.ToList()));
    //    btns.Click();
    //    Assert.False(select);

    //    cut.SetParametersAndRender(builder => builder.Add(a => a.OnSetItemClass, SetItemClass));
    //    var items = cut.FindAll(".transfer-panel-list div");
    //    Assert.Contains("bg-success text-white", items[1].ClassName);

    //    //搜索框,有item,无item
    //    Assert.DoesNotContain("transfer-panel-filter", cut.Markup);
    //    cut.SetParametersAndRender(builder => builder.Add(s => s.ShowSearch, true));
    //    cut.SetParametersAndRender(builder => builder.Add(s => s.SearchPlaceHolderString, "SearchPlaceHolderStringOK"));
    //    Assert.Contains("SearchPlaceHolderStringOK", cut.Markup);
    //    Assert.Contains("transfer-panel-filter", cut.Markup);
    //    var searchbar = cut.Find(".transfer-panel-filter input");
    //    searchbar.Input("好");
    //    Assert.Contains("is-on", cut.Markup);
    //    var searchbaritem = cut.FindAll(".transfer-panel-list input");
    //    Assert.True(searchbaritem.Count() == 0);

    //    searchbar.KeyUp(new KeyboardEventArgs() { Key = "Escape" });
    //    cut.SetParametersAndRender(builder => builder.Add(a => a.Items, null));
    //    searchbar.Input("3");
    //    Assert.True(searchbaritem.Count() == 0);
    //    cut.SetParametersAndRender(builder => builder.Add(a => a.Items, Items.ToList()));
    //    searchbar.Input("3");
    //    searchbaritem = cut.FindAll(".transfer-panel-list input");
    //    Assert.True(searchbaritem.Count() == 2);


    //    // 选中事件,先测空的OnSelectedItemsChanged分支
    //    var item1 = cut.FindAll(".transfer-panel-list input");
    //    item1[0].Click();

    //    cut.SetParametersAndRender(builder => builder.Add(a => a.OnSelectedItemsChanged, () =>
    //        {
    //            select = true;
    //            return Task.CompletedTask;
    //        }));
    //    item1[0].Click();
    //    Assert.True(select);

    //    btns.Click();
    //    btns.Click();

    //    cut.SetParametersAndRender(builder => builder.Add(s => s.IsDisabled, true));
    //    cut.SetParametersAndRender(builder => builder.Add(s => s.IsDisabled, false));
    //    Assert.DoesNotContain(" disabled", cut.Markup);

    //}

    //[Fact]
    //public void TextAndIsDisabled_Ok()
    //{
    //    var cut = Context.RenderComponent<Transfer<string>>(builder =>
    //    {
    //        builder.Add(a => a.Items, Items);
    //    });
    //    cut.SetParametersAndRender(builder => builder.Add(s => s.LeftPanelText, "LeftPanelTextOK"));
    //    Assert.Contains("LeftPanelTextOK", cut.Markup);

    //    cut.SetParametersAndRender(builder => builder.Add(s => s.RightPanelText, "RightPanelTextOK"));
    //    Assert.Contains("RightPanelTextOK", cut.Markup);

    //    cut.SetParametersAndRender(builder => builder.Add(s => s.LeftButtonText, "LeftButtonTextOK"));
    //    Assert.Contains("LeftButtonTextOK", cut.Markup);

    //    cut.SetParametersAndRender(builder => builder.Add(s => s.RightButtonText, "RightButtonTextOK"));
    //    Assert.Contains("RightButtonTextOK", cut.Markup);

    //    Assert.DoesNotContain("transfer-panel-filter", cut.Markup);
    //    cut.SetParametersAndRender(builder => builder.Add(s => s.ShowSearch, true));

    //    cut.SetParametersAndRender(builder => builder.Add(s => s.LeftPannelSearchPlaceHolderString, "LeftPannelSearchPlaceHolderStringOK"));
    //    Assert.Contains("LeftPannelSearchPlaceHolderStringOK", cut.Markup);

    //    cut.SetParametersAndRender(builder => builder.Add(s => s.RightPannelSearchPlaceHolderString, "RightPannelSearchPlaceHolderStringOK"));
    //    Assert.Contains("RightPannelSearchPlaceHolderStringOK", cut.Markup);

    //    cut.SetParametersAndRender(builder => builder.Add(s => s.IsDisabled, true));
    //    Assert.Contains("transfer-panel-filter", cut.Markup);

    //    //要配合ValidateForm
    //    cut.SetParametersAndRender(builder => builder.Add(s => s.DisplayText, "DisplayTextTest"));
    //    //Assert.Contains("DisplayTextTest", cut.Markup);
    //}

    //[Fact]
    //public void ValidateForm_Ok()
    //{
    //    Foo Model = new();
    //    var cut = Context.RenderComponent<ValidateForm>(pb =>
    //    {
    //        pb.Add(a => a.Model, Items);
    //        pb.AddChildContent<Transfer<string>>(builder =>
    //        {
    //            builder.Add(a => a.Items, Items);
    //            builder.Add(a => a.DisplayText, "DisplayTextTest");
    //            //@bind - Value = "@Model.Hobby"
    //        });
    //    });
    //    Assert.Contains("DisplayTextTest", cut.Markup);
    //}

    //[Fact]
    //public void OnSetItemClass_Ok()
    //{
    //    var cut = Context.RenderComponent<Transfer<string>>(builder =>
    //    {
    //        builder.Add(a => a.Items, Items);
    //        builder.Add(a => a.OnSetItemClass, SetItemClass);
    //    });
    //    var items = cut.FindAll(".transfer-panel-list div");

    //    Assert.Contains("bg-success text-white", items[1].ClassName);
    //}


    //[Fact]
    //public void SelectedItemsChanged_Ok()
    //{
    //    IEnumerable<SelectedItem> selecteditems = new List<SelectedItem>();
    //    var cut = Context.RenderComponent<Transfer<string>>(builder =>
    //    {
    //        builder.Add(a => a.Items, Items);
    //        builder.Add(a => a.OnSelectedItemsChanged, (v1) =>
    //        {
    //            selecteditems = v1;
    //            return Task.CompletedTask;
    //        });
    //    });

    //    Assert.DoesNotContain("form-check is-checked", cut.Markup);
    //    var btns = cut.FindAll(".transfer-buttons button");

    //    // 选中事件
    //    var item1 = cut.FindAll(".transfer-panel-list input");
    //    item1[0].Click();
    //    Assert.Contains("form-check is-checked", cut.Markup);
    //    //DataLeft 03
    //    item1[2].Click();
    //    //走两个到右边
    //    btns[1].Click();
    //    Assert.True(selecteditems.Any() && selecteditems.Count() == 2);
    //    Assert.True(selecteditems.ToList()[0].Text == "DataLeft 01");
    //    Assert.True(selecteditems.ToList()[1].Text == "DataLeft 03");

    //    //回来一个到左边
    //    var item2 = cut.FindAll(".transfer-panel-list input");
    //    item2.Last().Click();
    //    btns[0].Click();
    //    Assert.True(selecteditems.Any() && selecteditems.Count() == 1);
    //    Assert.True(selecteditems.ToList()[0].Text == "DataLeft 01");

    //    // [全部]选中事件
    //    var item = cut.Find(".form-check-input");
    //    item.Click();
    //    Assert.Contains("form-check is-checked", cut.Markup);

    //    btns[1].Click();
    //    Assert.True(selecteditems.Any() && selecteditems.Count() == Items.Count());

    //}
}
