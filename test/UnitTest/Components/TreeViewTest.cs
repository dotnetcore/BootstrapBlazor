// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        var nodes = cut.FindAll(".tree-item");
        Assert.Contains("disabled", nodes[1].InnerHtml);
        Assert.Contains("tree-node disabled", nodes[1].InnerHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.CanExpandWhenDisabled, true);
        });
        nodes = cut.FindAll(".tree-content");
        Assert.Contains("node-icon fa-solid fa-caret-right", nodes[0].InnerHtml);
        Assert.Contains("form-check-input disabled", nodes[0].InnerHtml);
        Assert.Contains("tree-node disabled", nodes[0].InnerHtml);
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
        Assert.Equal("tree-item active", nodes[0].ClassName);
    }

    [Fact]
    public async Task Items_SetActive()
    {
        var items = TreeFoo.GetTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });

        await cut.InvokeAsync(() => cut.Instance.SetActiveItem(items[0]));

        var node = cut.Find(".active");
        Assert.Equal("navigation one", node.TextContent);

        var activeItem = items[1].Items[0].Value;
        await cut.InvokeAsync(() => cut.Instance.SetActiveItem(activeItem));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ModelEqualityComparer, (x, y) => x.Id == y.Id);
        });
        await cut.InvokeAsync(() => cut.Instance.SetActiveItem(activeItem));
        node = cut.Find(".active");
        Assert.Equal("Sub menu 1", node.TextContent);

        activeItem = new TreeFoo();
        await cut.InvokeAsync(() => cut.Instance.SetActiveItem(activeItem));
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
        var checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();
        await cut.InvokeAsync(() => checkboxes[1].Instance.SetState(CheckboxState.Checked));
        await cut.InvokeAsync(() => checkboxes[2].Instance.SetState(CheckboxState.Checked));

        // Indeterminate
        await cut.InvokeAsync(() => checkboxes[4].Instance.SetState(CheckboxState.Checked));

        checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Indeterminate, checkboxes[3].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[5].Instance.State);
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

        cut.Find("[type=\"checkbox\"]").Click();
        cut.WaitForAssertion(() => cut.DoesNotContain("fa-solid fa-font-awesome"));
        cut.WaitForAssertion(() => cut.Contains("Test-Class"));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowIcon, true);
        });
        cut.WaitForAssertion(() => cut.Contains("fa-solid fa-font-awesome"));
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
    public async Task OnExpandRowAsync_CheckCascadeState_Ok()
    {
        var items = TreeFoo.GetCheckedTreeItems();
        items[0].HasChildren = true;

        var expanded = false;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.AutoCheckChildren, true);
            pb.Add(a => a.AutoCheckParent, true);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.OnExpandNodeAsync,async (item) =>
            {
                expanded = true;

                await Task.Yield();
                return TreeFoo.GetCheckedTreeItems(item.Value.Id);
            });
        });

        var checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();

        // 初始状态
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        Assert.True(expanded);

        cut.WaitForAssertion(() => cut.Instance.Items[0].Items.Any());

        // 展开状态-级联选中-子级
        checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();
        Assert.Equal(CheckboxState.UnChecked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[2].Instance.State);

        // 级联选中-父级
        await cut.InvokeAsync(() => checkboxes[1].Instance.SetState(CheckboxState.Checked));
        Assert.Equal(CheckboxState.Indeterminate, checkboxes[0].Instance.State);

        await cut.InvokeAsync(() => checkboxes[2].Instance.SetState(CheckboxState.Checked));
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.State);
    }

    [Fact]
    public async Task OnExpandRowAsync_CheckCascadeStateWithoutChild_Ok()
    {
        var items = TreeFoo.GetCheckedTreeItems();
        items[0].HasChildren = true;

        var expanded = false;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.AutoCheckParent, true);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.OnExpandNodeAsync, async (item) =>
            {
                expanded = true;

                await Task.Yield();
                return TreeFoo.GetCheckedTreeItems(item.Value.Id);
            });
        });

        var checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();

        // 初始状态
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        Assert.True(expanded);

        cut.WaitForAssertion(() => cut.Instance.Items[0].Items.Any());

        // 展开状态
        checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();
        Assert.Equal(CheckboxState.Indeterminate, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[2].Instance.State);

        // 级联选中-父级
        await cut.InvokeAsync(() => checkboxes[1].Instance.SetState(CheckboxState.Checked));
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.State);
    }

    [Fact]
    public async Task OnExpandRowAsync_ManualCheckState_Ok()
    {
        var items = TreeFoo.GetCheckedTreeItems();
        items[0].HasChildren = true;

        var expanded = false;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.OnExpandNodeAsync, async (item) =>
            {
                expanded = true;

                await Task.Yield();
                return TreeFoo.GetCheckedTreeItems(item.Value.Id);
            });
        });

        var checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();

        // 初始状态
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        Assert.True(expanded);

        cut.WaitForAssertion(() => cut.Instance.Items[0].Items.Any());

        // 展开状态
        checkboxes = cut.FindComponents<Checkbox<CheckboxState>>();
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[2].Instance.State);
    }

    [Fact]
    public async Task OnExpandRowAsync_Exception()
    {
        var items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });

        await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click()));
    }

    [Fact]
    public void GetAllSubItems_Ok()
    {
        var items = new List<TreeFoo>()
        {
            new() { Text = "Test1", Id = "01" },
            new() { Text = "Test2", Id = "02", ParentId = "01" },
            new() { Text = "Test3", Id = "03", ParentId = "02" }
        };

        var data = TreeFoo.CascadingTree(items);
        Assert.Single(data);

        var subs = data.First().GetAllTreeSubItems();
        Assert.Equal(2, subs.Count());
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
            new(new TreeFoo())
        };

        // MockTreeItem 无法转化成 TreeItem
        // 显式转换无对象，集合数量为 0
        Assert.Empty(item.Items);

        item.Items = new TreeViewItem<TreeFoo>[]
        {
            new(new MockTreeFoo())
        };
        // MockTreeFoo 转化成 TreeFoo
        // 显式转换，集合数量为 1
        Assert.Single(item.Items);
    }

    class MockTreeFoo : TreeFoo { }

    [Fact]
    public void CascadeSetCheck_Ok()
    {
        var items = new List<TreeFoo>()
        {
            new() { Text = "Test1", Id = "01" },
            new() { Text = "Test2", Id = "02", ParentId = "01" },
            new() { Text = "Test3", Id = "03", ParentId = "02" }
        };

        var node = TreeFoo.CascadingTree(items).First();

        // 设置当前几点所有子项选中状态
        node.SetChildrenCheck<TreeViewItem<TreeFoo>, TreeFoo>(CheckboxState.Checked);
        Assert.True(node.GetAllTreeSubItems().All(i => i.CheckedState == CheckboxState.Checked));
    }

    [Fact]
    public void SetParentCheck_Ok()
    {
        var items = new List<TreeFoo>()
        {
            new() { Text = "Test1", Id = "01" },
            new() { Text = "Test2", Id = "02", ParentId = "01" },
            new() { Text = "Test3", Id = "03", ParentId = "02" }
        };
        var node = TreeFoo.CascadingTree(items).First().Items.First().Items.First();

        // 设置当前几点所有父项选中状态
        node.SetParentCheck<TreeViewItem<TreeFoo>, TreeFoo>(CheckboxState.Checked);
        Assert.True(node.GetAllTreeSubItems().All(i => i.CheckedState == CheckboxState.Checked));
    }

    [Fact]
    public void SetParentExpand_Ok()
    {
        var items = new List<TreeFoo>()
        {
            new() { Text = "Test1", Id = "01" },
            new() { Text = "Test2", Id = "02", ParentId = "01" },
            new() { Text = "Test3", Id = "03", ParentId = "02" }
        };
        var node = TreeFoo.CascadingTree(items).First().Items.First().Items.First();

        // 设置当前几点所有父项选中状态
        node.SetParentExpand<TreeViewItem<TreeFoo>, TreeFoo>(true);
        Assert.True(node.GetAllTreeSubItems().All(i => i.IsExpand));

        node.SetParentExpand<TreeViewItem<TreeFoo>, TreeFoo>(false);
        Assert.True(node.GetAllTreeSubItems().All(i => !i.IsExpand));
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
    public void IsReset_Ok()
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
                    new(new TreeFoo() { Id = item.Value.Id + "10", ParentId = item.Value.Id })
                };
                return Task.FromResult(ret.AsEnumerable());
            });
        });
        cut.Find(".fa-caret-right.visible").Click();

        // 展开第一个节点生成一行子节点
        cut.WaitForAssertion(() =>
        {
            var nodes = cut.FindAll(".tree-item");
            Assert.Equal(3, nodes.Count);
        });

        // 重新设置数据源更新组件，保持状态
        items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;
        items.RemoveAt(1);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        cut.WaitForAssertion(() =>
        {
            var nodes = cut.FindAll(".tree-item");
            Assert.Equal(3, nodes.Count);
        });

        // 设置 IsReset=true 更新数据源后不保持状态
        items = TreeFoo.GetTreeItems();
        items[0].HasChildren = true;
        items.RemoveAt(1);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.IsReset, true);
        });
        cut.WaitForAssertion(() =>
        {
            var nodes = cut.FindAll(".tree-item");
            Assert.Equal(2, nodes.Count);
        });
    }

    [Fact]
    public void ExpandIcon_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        items[1].ExpandIcon = "test-expand-icon";
        items[1].IsExpand = true;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowIcon, true);
            pb.Add(a => a.Items, items);
        });
        cut.Contains("test-expand-icon");
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
    public void IsAccordion_Ok()
    {
        var items = new List<TreeFoo>
        {
            new() { Text = "导航一", Id = "1010" },
            new() { Text = "导航二", Id = "1020" },

            new() { Text = "子菜单一", Id = "1011", ParentId = "1010" },
            new() { Text = "子菜单二", Id = "1021", ParentId = "1020" }
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
        bars[0].Click();
        cut.WaitForAssertion(() => Assert.Contains("fa-rotate-90", cut.Markup));

        // 点击第二个节点箭头开展
        bars = cut.FindAll(".tree-root > .tree-item > .tree-content > .fa-caret-right.visible");
        bars[bars.Count - 1].Click();
        cut.WaitForAssertion(() =>
        {
            bars = cut.FindAll(".tree-root > .tree-item > .tree-content > .fa-caret-right.visible");
            Assert.DoesNotContain("fa-rotate-90", bars[0].ClassName);
            Assert.Contains("fa-rotate-90", bars[1].ClassName);
        });

        items =
        [
            new() { Text = "Root", Id = "1010" },

            new() { Text = "SubItem1", Id = "1011", ParentId = "1010" },
            new() { Text = "SubItem2", Id = "1012", ParentId = "1010" },

            new() { Text = "SubItem11", Id = "10111", ParentId = "1011" },
            new() { Text = "SubItem21", Id = "10121", ParentId = "1012" }
        ];
        nodes = TreeFoo.CascadingTree(items).ToList();

        cut.SetParametersAndRender(pb => pb.Add(a => a.Items, nodes));
        // 子节点
        bars = cut.FindAll(".tree-root > .tree-item > .tree-content + .tree-ul > .tree-item > .tree-content > .fa-caret-right.visible");
        bars[0].Click();
        cut.WaitForAssertion(() => Assert.Contains("fa-rotate-90", cut.Markup));

        // 点击第二个节点箭头开展
        bars = cut.FindAll(".tree-root > .tree-item > .tree-content + .tree-ul > .tree-item > .tree-content > .fa-caret-right.visible");
        bars[bars.Count - 1].Click();

        cut.WaitForAssertion(() =>
        {
            bars = cut.FindAll(".tree-root > .tree-item > .tree-content + .tree-ul > .tree-item > .tree-content > .fa-caret-right.visible");
            Assert.DoesNotContain("fa-rotate-90", bars[0].ClassName);
            Assert.Contains("fa-rotate-90", bars[1].ClassName);
        });
    }

    [Fact]
    public void TreeItem_Parent()
    {
        IExpandableNode<TreeFoo> item = new TreeViewItem<TreeFoo>(new TreeFoo());
        item.Parent = new TreeViewItem<TreeFoo>(new TreeFoo());
        Assert.NotNull(item.Parent);
    }

    [Fact]
    public async Task ClearCheckedItems_Ok()
    {
        var items = new List<TreeFoo>
        {
            new() { Text = "导航一", Id = "1010" },

            new() { Text = "子菜单一", Id = "1011", ParentId = "1010" },
        };

        // 根节点
        var nodes = TreeFoo.CascadingTree(items).ToList();

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, nodes);
            pb.Add(a => a.ShowCheckbox, true);
        });

        var checkbox = cut.FindAll(".tree-root > .tree-item > .tree-content > .form-check > .form-check-input");

        await cut.InvokeAsync(() => checkbox[0].Click());

        Assert.Contains("is-checked", cut.Markup);
        var isChecked = cut.Instance.GetCheckedItems().Any();
        Assert.True(isChecked);

        await cut.InvokeAsync(() => cut.Instance.ClearCheckedItems());
        Assert.DoesNotContain("is-checked", cut.Markup);
        var noChecked = !cut.Instance.GetCheckedItems().Any();
        Assert.True(noChecked);
    }

    [Fact]
    public void ShowSearch_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.Items, items);
        });
        cut.Contains("tree-search");
        cut.Contains("tree-search-reset");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowResetSearchButton, false);
        });
        cut.DoesNotContain("tree-search-reset");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SearchTemplate, builder =>
            {
                builder.AddContent(0, "search-template");
            });
        });
        cut.Contains("search-template");
    }

    [Fact]
    public async void Enter_Ok()
    {
        var key = "";
        var items = TreeFoo.GetTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.OnSearchAsync, v =>
            {
                key = v;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, items);
        });

        var input = cut.FindComponent<BootstrapInput<string?>>();
        await cut.InvokeAsync(() => input.Instance.OnEnterAsync!("enter"));
        Assert.Equal("enter", key);
    }

    [Fact]
    public async void Esc_Ok()
    {
        var key = "123";
        var items = TreeFoo.GetTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.OnSearchAsync, v =>
            {
                key = v;
                return Task.CompletedTask;
            });
            pb.Add(a => a.Items, items);
        });

        var input = cut.FindComponent<BootstrapInput<string?>>();
        await cut.InvokeAsync(() => input.Instance.OnEscAsync!(null));
        Assert.Null(key);
    }

    class MockTree<TItem> : TreeView<TItem> where TItem : class
    {
        public bool TestComparerItem(TItem? a, TItem? b) => base.Equals(a, b);
    }

    private class Cat
    {
        [CatKey]
        public int Id { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    private class CatKeyAttribute : Attribute
    {

    }

    class MockTreeItem(TreeFoo foo) : IExpandableNode<TreeFoo>
    {
        public bool IsExpand { get; set; }

        /// <summary>
        /// 获得/设置 父级节点
        /// </summary>
        public IExpandableNode<TreeFoo>? Parent { get; set; }

        [NotNull]
        public IEnumerable<IExpandableNode<TreeFoo>>? Items { get; set; } = Enumerable.Empty<IExpandableNode<TreeFoo>>();

        [NotNull]
        public TreeFoo? Value { get; set; } = foo;

        public bool HasChildren { get; set; }
    }

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandNodeAsync(TreeFoo item)
    {
        await Task.Yield();
        return new TreeViewItem<TreeFoo>[]
        {
            new(new TreeFoo() { Id = $"{item.Id}-101", ParentId = item.Id })
            {
                Text = "懒加载子节点1",
                HasChildren = true
            },
            new(new TreeFoo(){ Id = $"{item.Id}-102", ParentId = item.Id })
            {
                Text = "懒加载子节点2"
            }
        };
    }

    private static async Task<IEnumerable<TreeViewItem<TreeFoo>>> OnExpandNodeWithCheckedStateAsync(TreeFoo item)
    {
        await Task.Yield();
        return new TreeViewItem<TreeFoo>[]
        {
            new(new TreeFoo() { Id = $"{item.Id}-101", ParentId = item.Id })
            {
                Text = "懒加载子节点1",
                HasChildren = true
            },
            new(new TreeFoo(){ Id = $"{item.Id}-102", ParentId = item.Id })
            {
                Text = "懒加载子节点2",
                CheckedState=CheckboxState.Checked
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

    private class Dog { }
}
