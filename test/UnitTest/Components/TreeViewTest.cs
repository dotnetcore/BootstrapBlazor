// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class TreeViewTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<TreeView<TreeFoo>>();
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
    public void Items_Disabled()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].IsDisabled = true;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.Items, items);
        });

        cut.Contains("tree-item disabled");
        cut.Contains("form-check disabled");
        cut.Contains("tree-node disabled");
        cut.Contains("form-check-input disabled");
    }

    [Fact]
    public void Items_IsActive()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].IsActive = true;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });

        var nodes = cut.FindAll(".tree-view > .tree-root > .tree-item");
        Assert.Equal(3, nodes.Count);
        Assert.Equal("tree-item active", nodes.First().ClassName);
    }

    [Fact]
    public async Task OnClick_Checkbox_Ok()
    {
        var tcs = new TaskCompletionSource<bool>();
        CheckboxState itemChecked = CheckboxState.UnChecked;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.IsAccordion, true);
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.ClickToggleCheck, true);
            pb.Add(a => a.OnTreeItemChecked, items =>
            {
                itemChecked = items.FirstOrDefault()?.CheckedState ?? CheckboxState.UnChecked;
                tcs.SetResult(true);
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, TreeFoo.GetTreeItems());
        });

        // 测试点击选中
        await cut.InvokeAsync(() => cut.Find(".tree-node").Click());
        await tcs.Task;
        Assert.Equal(CheckboxState.Checked, itemChecked);

        // 测试取消选中
        tcs = new TaskCompletionSource<bool>();
        await cut.InvokeAsync(() => cut.Find(".tree-node").Click());
        await tcs.Task;
        Assert.Equal(CheckboxState.UnChecked, itemChecked);
    }

    [Fact]
    public async Task CheckCascadeState_Ok()
    {
        // 级联选中
        var items = new List<TreeFoo>
        {
            new() { Text = "Node1", Id = "1010" },
            new() { Text = "Node2", Id = "1020" },

            new() { Text = "Node11", Id = "1011", ParentId = "1010" },
            new() { Text = "Node12", Id = "1011", ParentId = "1010" },

            new() { Text = "Node21", Id = "1021", ParentId = "1020" },
            new() { Text = "Node22", Id = "1021", ParentId = "1020" }
        };

        // 根节点
        var nodes = TreeFoo.CascadingTree(items).ToList();
        nodes[0].IsExpand = true;
        nodes[1].IsExpand = true;

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.AutoCheckChildren, true);
            pb.Add(a => a.AutoCheckParent, true);
            pb.Add(a => a.Items, nodes);
            pb.Add(a => a.ShowCheckbox, true);
        });
        var checkboxs = cut.FindComponents<Checkbox<CheckboxState>>();
        await cut.InvokeAsync(() => checkboxs[1].Instance.SetState(CheckboxState.Checked));
        await cut.InvokeAsync(() => checkboxs[2].Instance.SetState(CheckboxState.Checked));

        // Indeterminate
        await cut.InvokeAsync(() => checkboxs[4].Instance.SetState(CheckboxState.Checked));

        checkboxs = cut.FindComponents<Checkbox<CheckboxState>>();
        Assert.Equal(CheckboxState.Checked, checkboxs[0].Instance.State);
        Assert.Equal(CheckboxState.Indeterminate, checkboxs[3].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxs[5].Instance.State);
    }

    [Fact]
    public void OnStateChanged_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].CssClass = "Test-Class";

        List<TreeViewItem<TreeFoo>>? checkedLists = null;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
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
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
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
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.OnExpandNodeAsync, item =>
            {
                expanded = true;
                return OnExpandNodeAsync(item.Value);
            });
            pb.Add(a => a.Items, items);
        });

        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        Assert.True(expanded);
    }

    [Fact]
    public void OnExpandRowAsync_Exception()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });

        Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click()));
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

        var subs = data.First().GetAllTreeSubItems();
        Assert.Equal(2, subs.Count());
    }

    [Fact]
    public async Task ShowRadio_Ok()
    {
        List<TreeViewItem<TreeFoo>>? checkedLists = null;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
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
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        Assert.Contains("test-tree-css-class", cut.Markup);
    }

    [Fact]
    public void Items_OfType()
    {
        IExpandableNode<TreeFoo> item = new TreeViewItem<TreeFoo>(new TreeFoo());
        item.Items = new MockTreeItem[]
        {
            new MockTreeItem(new TreeFoo())
        };

        // MockTreeItem 无法转化成 TreeItem
        // 显式转换无对象，集合数量为 0
        Assert.Empty(item.Items);

        item.Items = new TreeViewItem<TreeFoo>[]
        {
            new TreeViewItem<TreeFoo>(new MockTreeFoo())
        };
        // MockTreeFoo 转化成 TreeFoo
        // 显式转换，集合数量为 1
        Assert.Single(item.Items);
    }

    [Fact]
    public void CascadeSetCheck_Ok()
    {
        var items = new List<TreeFoo>()
        {
            new TreeFoo() { Text = "Test1", Id = "01" },
            new TreeFoo() { Text = "Test2", Id = "02", ParentId = "01" },
            new TreeFoo() { Text = "Test3", Id = "03", ParentId = "02" }
        };

        var node = TreeFoo.CascadingTree(items).First();

        // 设置当前几点所有子项选中状态
        node.SetChildrenCheck<TreeViewItem<TreeFoo>, TreeFoo>(CheckboxState.Checked);
        Assert.True(node.GetAllTreeSubItems().All(i => i.CheckedState == CheckboxState.Checked));
    }

    [Fact]
    public async Task ClickToggleNode_Ok()
    {
        var clicked = false;
        // 点击节点 Text 展开/收缩
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ClickToggleNode, true);
            pb.Add(a => a.Items, TreeFoo.GetTreeItems());
            pb.Add(a => a.OnTreeItemClick, node =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        var node = cut.FindAll(".tree-node").Skip(1).First();
        await cut.InvokeAsync(() => node.Click());
        Assert.True(clicked);

        // 显示 Radio 组件
        List<TreeViewItem<TreeFoo>>? selectedItems = null;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowRadio, true);
            pb.Add(a => a.OnTreeItemChecked, items =>
            {
                selectedItems = items;
                return Task.CompletedTask;
            });
        });
        node = cut.FindAll(".tree-node").Skip(1).First();
        await cut.InvokeAsync(() => node.Click());
        Assert.Single(selectedItems);
    }

    [Fact]
    public void KeyAttribute_Ok()
    {
        var cut = Context.RenderComponent<MockTree<Cat>>(pb =>
        {
            pb.Add(a => a.CustomKeyAttribute, typeof(CatKeyAttribute));
        });

        var ret = cut.Instance.TestComparerItem(new Cat() { Id = 1 }, new Cat() { Id = 1 });
        Assert.True(ret);
    }

    [Fact]
    public async Task IsReset_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;
        items.RemoveAt(1);

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.IsReset, false);
            pb.Add(a => a.OnExpandNodeAsync, item =>
            {
                var ret = new List<TreeViewItem<TreeFoo>>
                {
                    new TreeViewItem<TreeFoo>(new TreeFoo() { Id = item.Value.Id + "10", ParentId = item.Value.Id })
                };
                return Task.FromResult(ret.AsEnumerable());
            });
        });
        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());

        // 展开第一个节点生成一行子节点
        var nodes = cut.FindAll(".tree-item");
        Assert.Equal(3, nodes.Count);

        // 重新设置数据源更新组件，保持状态
        items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;
        items.RemoveAt(1);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        nodes = cut.FindAll(".tree-item");
        Assert.Equal(3, nodes.Count);

        // 设置 IsReset=true 更新数据源后不保持状态
        items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;
        items.RemoveAt(1);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.IsReset, true);
        });
        nodes = cut.FindAll(".tree-item");
        Assert.Equal(2, nodes.Count);
    }

    [Fact]
    public void ModelEqualityComparer_Ok()
    {
        var cut = Context.RenderComponent<MockTree<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ModelEqualityComparer, (x, y) => x.Id == y.Id);
        });

        var ret = cut.Instance.TestComparerItem(new TreeFoo() { Id = "1" }, new TreeFoo() { Id = "1" });
        Assert.True(ret);
    }

    [Fact]
    public void EqualityComparer_Ok()
    {
        var cut = Context.RenderComponent<MockTree<Dummy>>();

        var ret = cut.Instance.TestComparerItem(new Dummy() { Id = 1 }, new Dummy() { Id = 1 });
        Assert.True(ret);
    }

    [Fact]
    public void Equality_Ok()
    {
        var cut = Context.RenderComponent<MockTree<CatKeyAttribute>>();

        var ret = cut.Instance.TestComparerItem(new CatKeyAttribute(), new CatKeyAttribute());
        Assert.True(ret);
    }

    [Fact]
    public void Equality_False()
    {
        var cut = Context.RenderComponent<MockTree<Dog>>();
        var ret = cut.Instance.TestComparerItem(new Dog(), new Dog());
        Assert.False(ret);
    }

    [Fact]
    public async Task IsAccordion_Ok()
    {
        var items = new List<TreeFoo>
        {
            new TreeFoo() { Text = "导航一", Id = "1010" },
            new TreeFoo() { Text = "导航二", Id = "1020" },

            new TreeFoo() { Text = "子菜单一", Id = "1011", ParentId = "1010" },
            new TreeFoo() { Text = "子菜单二", Id = "1021", ParentId = "1020" }
        };

        // 根节点
        var nodes = TreeFoo.CascadingTree(items).ToList();

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, nodes);
            pb.Add(a => a.IsAccordion, true);
            pb.Add(a => a.IsReset, true);
        });

        var bars = cut.FindAll(".tree-root > .tree-item > .tree-content > .fa-caret-right.visible");
        await cut.InvokeAsync(() => bars.First().Click());
        Assert.Contains("fa-rotate-90", cut.Markup);

        // 点击第二个节点箭头开展
        await cut.InvokeAsync(() => bars.Last().Click());
        bars = cut.FindAll(".tree-root > .tree-item > .tree-content > .fa-caret-right.visible");
        Assert.DoesNotContain("fa-rotate-90", bars[0].ClassName);
        Assert.Contains("fa-rotate-90", bars[1].ClassName);

        items = new List<TreeFoo>
        {
            new TreeFoo() { Text = "Root", Id = "1010" },

            new TreeFoo() { Text = "SubItem1", Id = "1011", ParentId = "1010" },
            new TreeFoo() { Text = "SubItem2", Id = "1012", ParentId = "1010" },

            new TreeFoo() { Text = "SubItem11", Id = "10111", ParentId = "1011" },
            new TreeFoo() { Text = "SubItem21", Id = "10121", ParentId = "1012" }
        };
        nodes = TreeFoo.CascadingTree(items).ToList();

        cut.SetParametersAndRender(pb => pb.Add(a => a.Items, nodes));

        // 子节点
        bars = cut.FindAll(".tree-root > .tree-item > .tree-content + .tree-ul > .tree-item > .tree-content > .fa-caret-right.visible");
        await cut.InvokeAsync(() => bars[0].Click());
        Assert.Contains("fa-rotate-90", cut.Markup);

        // 点击第二个节点箭头开展
        await cut.InvokeAsync(() => bars.Last().Click());
        bars = cut.FindAll(".tree-root > .tree-item > .tree-content + .tree-ul > .tree-item > .tree-content > .fa-caret-right.visible");
        Assert.DoesNotContain("fa-rotate-90", bars[0].ClassName);
        Assert.Contains("fa-rotate-90", bars[1].ClassName);
    }

    [Fact]
    public void TreeItem_Parent()
    {
        IExpandableNode<TreeFoo> item = new TreeViewItem<TreeFoo>(new TreeFoo());
        item.Parent = new TreeViewItem<TreeFoo>(new TreeFoo());
        Assert.NotNull(item.Parent);
    }

    class MockTree<TItem> : TreeView<TItem> where TItem : class
    {
        public bool TestComparerItem(TItem a, TItem b)
        {
            return ComparerItem(a, b);
        }
    }

    private class Cat
    {
        [CatKey]
        public int Id { get; set; }

        public string? Name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    private class CatKeyAttribute : Attribute
    {

    }

    class MockTreeItem : IExpandableNode<TreeFoo>
    {
        public bool IsExpand { get; set; }

        /// <summary>
        /// 获得/设置 父级节点
        /// </summary>
        public IExpandableNode<TreeFoo>? Parent { get; set; }

        [NotNull]
        public IEnumerable<IExpandableNode<TreeFoo>>? Items { get; set; }

        [NotNull]
        public TreeFoo? Value { get; set; }

        public MockTreeItem(TreeFoo foo)
        {
            Value = foo;
            Items = Enumerable.Empty<IExpandableNode<TreeFoo>>();
        }
    }

    class MockTreeFoo : TreeFoo { }

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandNodeAsync(TreeFoo item)
    {
        await Task.Yield();
        return new TreeViewItem<TreeFoo>[]
        {
            new TreeViewItem<TreeFoo>(new TreeFoo() { Id = $"{item.Id}-101", ParentId = item.Id })
            {
                Text = "懒加载子节点1",
                HasChildren = true
            },
            new TreeViewItem<TreeFoo>(new TreeFoo(){ Id = $"{item.Id}-102", ParentId = item.Id })
            {
                Text = "懒加载子节点2"
            }
        };
    }

    private class Dummy : IEqualityComparer<Dummy>
    {
        public int Id { get; set; }

        public bool Equals(Dummy? x, Dummy? y)
        {
            var ret = false;
            if (x != null && y != null)
            {
                ret = x.Id == y.Id;
            }
            return ret;
        }

        public int GetHashCode([DisallowNull] Dummy obj) => obj.GetHashCode();
    }

    private class Dog
    {
        public int Id { get; set; }
    }
}
