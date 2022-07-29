// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class TreeTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<Tree<TreeFoo>>();
        cut.DoesNotContain("tree-root");

        // 由于 Items 为空不生成 TreeItem 显示 loading
        cut.Contains("table-loading");
        cut.DoesNotContain("li");

        cut.SetParametersAndRender(pb => pb.Add(a => a.ShowSkeleton, true));
        cut.Contains("skeleton tree");

        // 设置 Items
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, TreeFoo.GetTreeItems());
        });
        cut.Contains("li");
    }

    [Fact]
    public async Task OnClick_Checkbox_Ok()
    {
        var tcs = new TaskCompletionSource<bool>();
        bool itemChecked = false;
        var cut = Context.RenderComponent<Tree<TreeFoo>>(pb =>
        {
            pb.Add(a => a.IsAccordion, true);
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.OnTreeItemChecked, items =>
            {
                itemChecked = items.FirstOrDefault()?.Checked ?? false;
                tcs.SetResult(true);
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, TreeFoo.GetTreeItems());
        });

        // 测试点击选中
        await cut.InvokeAsync(() => cut.Find(".tree-node").Click());
        await tcs.Task;
        Assert.True(itemChecked);

        // 测试取消选中
        tcs = new TaskCompletionSource<bool>();
        await cut.InvokeAsync(() => cut.Find(".tree-node").Click());
        await tcs.Task;
        Assert.False(itemChecked);
    }

    [Fact]
    public void OnStateChanged_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].CssClass = "Test-Class";

        List<TreeItem<TreeFoo>>? checkedLists = null;
        var cut = Context.RenderComponent<Tree<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.OnTreeItemChecked, items =>
            {
                checkedLists = items;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, items);
        });

        cut.InvokeAsync(() => cut.Find("[type=\"checkbox\"]").Click());
        cut.DoesNotContain("fa fa-fa");
        cut.Contains("Test-Class");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowIcon, true);
        });
        cut.Contains("fa fa-fa");
    }

    [Fact]
    public void Template_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].Template = foo => builder => builder.AddContent(0, "Test-Template");
        var cut = Context.RenderComponent<Tree<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        cut.Contains("Test-Template");
    }

    [Fact]
    public async Task OnExpandRowAsync_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;

        var expanded = false;
        var cut = Context.RenderComponent<Tree<TreeFoo>>(pb =>
        {
            pb.Add(a => a.OnExpandNodeAsync, item =>
            {
                expanded = true;
                return OnExpandNodeAsync(item);
            });
            pb.Add(a => a.Items, items);
        });

        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        Assert.True(expanded);
    }

    [Fact]
    public void GetAllSubItems_Ok()
    {
        var items = new List<TreeFoo>()
        {
            new TreeFoo() { Text = "Test1", Id = "01" },
            new TreeFoo() { Text = "Test2", Id = "02", ParentId = "01" },
            new TreeFoo() { Text = "Test3", Id = "03", ParentId = "02" }
        };

        var data = TreeFoo.CascadingTree(items);
        Assert.Single(data);

        var subs = data.First().GetAllSubItems();
        Assert.Equal(2, subs.Count());
    }

    [Fact]
    public async Task ShowRadio_Ok()
    {
        List<TreeItem<TreeFoo>>? checkedLists = null;
        var cut = Context.RenderComponent<Tree<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowRadio, true);
            pb.Add(a => a.OnTreeItemChecked, items =>
            {
                checkedLists = items;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, TreeFoo.GetTreeItems());
        });
        cut.Find("[type=\"radio\"]").Click();
        Assert.Single(checkedLists);
        Assert.Equal("导航一", checkedLists![0].Text);

        var radio = cut.FindAll("[type=\"radio\"]")[1];
        await cut.InvokeAsync(() => radio.Click());
        Assert.Equal("导航二", checkedLists![0].Text);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowSkeleton, false);
        });
    }

    [Fact]
    public void Tree_CssClass()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].CssClass = "test-tree-css-class";
        var cut = Context.RenderComponent<Tree<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        Assert.Contains("test-tree-css-class", cut.Markup);
    }

    private static async Task<IEnumerable<TreeItem<TreeFoo>>> OnExpandNodeAsync(TreeFoo item)
    {
        await Task.Yield();
        return new TreeItem<TreeFoo>[]
        {
            new TreeItem<TreeFoo>(new TreeFoo() { Id = $"{item.Id}-101", ParentId = item.Id })
            {
                Text = "懒加载子节点1",
                HasChildren = true
            },
            new TreeItem<TreeFoo>(new TreeFoo(){ Id = $"{item.Id}-102", ParentId = item.Id })
            {
                Text = "懒加载子节点2"
            }
        };
    }
}
