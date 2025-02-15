// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TreeViewTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Items_Ok()
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
        var items = cut.FindAll(".tree-content");
        Assert.Equal(9, items.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, null);
            pb.Add(a => a.ShowSkeleton, false);
        });
        Assert.Equal("", cut.Markup);

        // SetItems
        await cut.InvokeAsync(() => cut.Instance.SetItems(
        [
            new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "Test1" }) { Text = "Test1" },
            new TreeViewItem<TreeFoo>(new TreeFoo() { Text = "Test2" }) { Text = "Test2" }
        ]));

        items = cut.FindAll(".tree-content");
        Assert.Equal(2, items.Count);
    }

    //[Fact]
    //public void FlatItems_Ok()
    //{
    //    var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
    //    {
    //        pb.Add(a => a.FlatItems, TreeFoo.GetFlatItems());
    //    });
    //    cut.WaitForElement(".tree-view");

    //    // 验证树形结构正确生成
    //    var nodes = cut.FindAll(".tree-content");
    //    Assert.Equal(3, nodes.Count);

    //    // 验证父子关系
    //    var parentNode = cut.Find("[data-item-id='1']");
    //    Assert.NotNull(parentNode);
    //    var childNode = cut.Find("[data-item-id='2']");
    //    Assert.NotNull(childNode);
    //    Assert.Contains("tree-children", childNode.ParentElement?.ClassName);

    //    cut.SetParametersAndRender(pb =>
    //    {
    //        pb.Add(a => a.FlatItems, null);
    //    });
    //    Assert.Equal("", cut.Markup);
    //}

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

        var content = cut.FindAll(".tree-content");
        Assert.Contains("form-check disabled", content[0].InnerHtml);
        Assert.Contains("tree-node disabled", content[0].InnerHtml);
        Assert.Contains("form-check-input disabled", content[0].InnerHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        content = cut.FindAll(".tree-content");
        Assert.Contains("tree-node disabled", content[1].InnerHtml);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.CanExpandWhenDisabled, true);
        });
        content = cut.FindAll(".tree-content");
        Assert.Contains("node-icon fa-solid fa-caret-right", content[0].InnerHtml);
        Assert.Contains("form-check-input disabled", content[0].InnerHtml);
        Assert.Contains("tree-node disabled", content[0].InnerHtml);
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

        var nodes = cut.FindAll(".tree-content");
        Assert.Equal(3, nodes.Count);
        Assert.Equal("tree-content active", nodes[0].ClassName);
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

        var node = cut.Find(".active .tree-node-text");
        Assert.Equal("Navigation one", node.TextContent);

        var activeItem = items[1].Items[0].Value;
        await cut.InvokeAsync(() => cut.Instance.SetActiveItem(activeItem));

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ModelEqualityComparer, (x, y) => x.Id == y.Id);
        });
        await cut.InvokeAsync(() => cut.Instance.SetActiveItem(activeItem));
        node = cut.Find(".active .tree-node-text");
        Assert.Equal("Sub menu 1", node.TextContent);

        activeItem = new TreeFoo();
        await cut.InvokeAsync(() => cut.Instance.SetActiveItem(activeItem));
    }

    [Fact]
    public void AppendNode_Ok()
    {
        var items = TreeFoo.GetAccordionItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        var contents = cut.FindAll(".tree-content");
        Assert.Equal(2, contents.Count);

        items.Add(new TreeViewItem<TreeFoo>(new TreeFoo()) { Text = "append-text" });
        cut.SetParametersAndRender();
        contents = cut.FindAll(".tree-content");
        Assert.Equal(3, contents.Count);
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
        var nodes = TreeFoo.CascadingTree(items);
        nodes[0].IsExpand = true;
        Assert.Equal("Node1", nodes[0].Text);
        nodes[1].IsExpand = true;
        Assert.Equal("Node2", nodes[1].Text);

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.AutoCheckChildren, true);
            pb.Add(a => a.AutoCheckParent, true);
            pb.Add(a => a.Items, nodes);
            pb.Add(a => a.ShowCheckbox, true);
        });
        var checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        await cut.InvokeAsync(() => checkboxes[1].Instance.SetState(CheckboxState.Checked));
        await cut.InvokeAsync(() => checkboxes[2].Instance.SetState(CheckboxState.Checked));

        // Indeterminate
        await cut.InvokeAsync(() => checkboxes[4].Instance.SetState(CheckboxState.Checked));

        checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.Value.CheckedState);
        Assert.Equal(CheckboxState.Indeterminate, checkboxes[3].Instance.Value.CheckedState);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[5].Instance.Value.CheckedState);
    }

    [Fact]
    public async Task OnStateChanged_Ok()
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

        var checkbox = cut.FindComponent<Checkbox<TreeViewItem<TreeFoo>>>();
        await cut.InvokeAsync(checkbox.Instance.OnToggleClick);
        cut.DoesNotContain("fa-solid fa-font-awesome");
        cut.Contains("Test-Class");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowIcon, true);
        });
        cut.Contains("fa-solid fa-font-awesome");
    }

    [Fact]
    public async Task OnMaxSelectedCountExceed_Ok()
    {
        bool max = false;
        var items = TreeFoo.CascadingTree(new List<TreeFoo>()
        {
            new() { Text = "navigation one", Id = "1010", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Navigation two", Id = "1020", Icon = "fa-solid fa-font-awesome" },
            new() { Text = "Navigation three", Id = "1030", Icon = "fa-solid fa-font-awesome" }
        });

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.MaxSelectedCount, 2);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.OnMaxSelectedCountExceed, () =>
            {
                max = true;
                return Task.CompletedTask;
            });
        });
        var checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        Assert.Equal(3, checkboxes.Count);

        await cut.InvokeAsync(async () =>
        {
            await checkboxes[0].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.State);

        await cut.InvokeAsync(async () =>
        {
            await checkboxes[1].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        // 选中第三个由于限制无法选中
        await cut.InvokeAsync(async () =>
        {
            await checkboxes[2].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[2].Instance.State);
        Assert.True(max);

        // 取消选择第一个
        max = false;
        await cut.InvokeAsync(async () =>
        {
            await checkboxes[0].Instance.OnToggleClick();
        });
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[2].Instance.State);
        Assert.False(max);
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
    public async Task KeepExpandState_Ok()
    {
        // UI 重新刷新后保持状态节点状态
        var items = TreeFoo.GetTreeItems();
        items.RemoveAt(1);
        items.RemoveAt(1);
        items[0].HasChildren = true;

        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.OnExpandNodeAsync, item =>
            {
                return OnExpandNodeAsync(item.Value);
            });
        });
        var nodes = cut.FindAll(".tree-node");
        Assert.Single(nodes);

        // 展开节点
        var bar = cut.Find(".fa-caret-right.visible");
        await cut.InvokeAsync(() => bar.Click());

        cut.WaitForAssertion(() =>
        {
            nodes = cut.FindAll(".tree-node");
            Assert.Equal(3, nodes.Count);
        });

        // 重新渲染
        items = TreeFoo.GetTreeItems();
        items.RemoveAt(1);
        items.RemoveAt(1);
        items[0].HasChildren = true;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
        });

        cut.WaitForAssertion(() =>
        {
            nodes = cut.FindAll(".tree-node");
            Assert.Equal(3, nodes.Count);
        });

        // 重新渲染
        items = TreeFoo.GetTreeItems();
        items.RemoveAt(1);
        items.RemoveAt(1);
        items[0].HasChildren = false;
        items[0].Items =
        [
            new(new TreeFoo() { Id = "101", ParentId = "1010" })
            {
                Text = "懒加载子节点11",
                HasChildren = true
            },
            new(new TreeFoo(){ Id = "102", ParentId = "1010" })
            {
                Text = "懒加载子节点22"
            }
        ];
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        cut.WaitForAssertion(() =>
        {
            nodes = cut.FindAll(".tree-node");
            Assert.Equal(3, nodes.Count);
        });
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
            pb.Add(a => a.OnExpandNodeAsync, async (item) =>
            {
                expanded = true;

                await Task.Yield();
                return TreeFoo.GetCheckedTreeItems(item.Value.Id);
            });
        });

        var checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();

        // 初始状态 第一节点未选中 第二节点选中
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        // 展开第一个节点
        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        Assert.True(expanded);

        cut.WaitForState(() => cut.Instance.Items[0].Items.Count > 0);
        // 101 unchecked
        //  -> 101-101 unchecked
        //  -> 101-102 unchecked
        // 102 checked

        // 展开状态-级联选中-子级
        checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.Value.CheckedState);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[1].Instance.Value.CheckedState);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[2].Instance.Value.CheckedState);

        // 级联选中-父级
        await cut.InvokeAsync(() => checkboxes[1].Instance.SetState(CheckboxState.Checked));

        // 由于缺少 JS 回调单元测试中 Instance.State 无法获取到最新状态
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.Value.CheckedState);
        Assert.Equal(CheckboxState.Indeterminate, checkboxes[0].Instance.Value.CheckedState);

        await cut.InvokeAsync(() => checkboxes[2].Instance.SetState(CheckboxState.Checked));
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.Value.CheckedState);
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

        var checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();

        // 初始状态 第一节点未选中 第二节点选中
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        // 展开第一个节点
        // 未设置 AutoCheckChildren 属性，子节点不会级联更新状态
        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        Assert.True(expanded);

        cut.WaitForState(() => cut.Instance.Items[0].Items.Count > 0);
        // 101 unchecked
        //  -> 101-101 unchecked
        //  -> 101-102 checked
        // 102 checked

        // 展开状态
        checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        Assert.Equal(CheckboxState.Indeterminate, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[2].Instance.State);

        // 级联选中-父级
        await cut.InvokeAsync(() => checkboxes[1].Instance.SetState(CheckboxState.Checked));

        // 由于缺少 JS 回调单元测试中 Instance.State 无法获取到最新状态
        Assert.Equal(CheckboxState.Checked, checkboxes[0].Instance.Value.CheckedState);
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

        var checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();

        // 初始状态
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        await cut.InvokeAsync(() => cut.Find(".node-icon.visible").Click());
        Assert.True(expanded);

        cut.WaitForState(() => cut.Instance.Items[0].Items.Count > 0);

        // 展开状态
        checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[1].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[2].Instance.State);
    }

    [Fact]
    public async Task IsVirtualize_Ok()
    {
        var items = TreeFoo.GetVirtualizeTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.IsVirtualize, false);
            pb.Add(a => a.Items, items);
        });
        cut.Contains("tree-root scroll");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsVirtualize, true);
            pb.Add(a => a.RowHeight, 30f);
            pb.Add(a => a.OnExpandNodeAsync, async item =>
            {
                await Task.Delay(10);
                var node1 = new TreeViewItem<TreeFoo>(new TreeFoo() { Id = "1011", ParentId = item.Value.Id })
                {
                    Text = "Sub menu 1",
                    HasChildren = true
                };
                var node2 = new TreeViewItem<TreeFoo>(new TreeFoo() { Id = "1021", ParentId = item.Value.Id })
                {
                    Text = "Sub menu 2",
                };
                return [node1, node2];
            });
        });
        cut.Contains("tree-root scroll is-virtual");

        // 触发第一个节点展开
        await cut.InvokeAsync(() => cut.Find(".node-icon.visible").Click());
        cut.WaitForState(() => cut.Instance.Items[0].Items.Count > 0);

        cut.Contains("--bb-tree-view-level: 0;");
        cut.Contains("--bb-tree-view-level: 1;");
    }

    [Fact]
    public async Task GetParentsState_Ok()
    {
        var items = TreeFoo.GetCheckedTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowCheckbox, true);
            pb.Add(a => a.AutoCheckParent, true);
            pb.Add(a => a.OnExpandNodeAsync, async (item) =>
            {
                await Task.Yield();
                return TreeFoo.GetCheckedTreeItems(item.Value.Id);
            });
        });

        var checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        // 初始状态
        Assert.Equal(CheckboxState.UnChecked, checkboxes[0].Instance.State);
        Assert.Equal(CheckboxState.Checked, checkboxes[1].Instance.State);

        await cut.InvokeAsync(() => cut.Find(".fa-caret-right.visible").Click());
        cut.WaitForState(() => cut.Instance.Items[0].Items.Count > 0);
        // 101 unchecked
        //  -> 101-101 unchecked
        //  -> 101-102 checked
        // 102 checked

        checkboxes = cut.FindComponents<Checkbox<TreeViewItem<TreeFoo>>>();
        var parents = new List<int>() { 0 };
        List<CheckboxState> results = await cut.Instance.GetParentsState(parents);
        Assert.NotNull(results);
        Assert.Equal(CheckboxState.Indeterminate, checkboxes[0].Instance.Value.CheckedState);
        Assert.Equal(CheckboxState.UnChecked, checkboxes[1].Instance.Value.CheckedState);
        Assert.Equal(CheckboxState.Checked, checkboxes[2].Instance.Value.CheckedState);

        Assert.Single(results);
        Assert.Equal(CheckboxState.Indeterminate, results[0]);
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
        var cut = Context.RenderComponent<TreeView<TreeFoo>>();
        var comparer = cut.Instance;

        var items = new List<TreeFoo>()
        {
            new() { Text = "Test1", Id = "01" },
            new() { Text = "Test2", Id = "02", ParentId = "01" },
            new() { Text = "Test3", Id = "03", ParentId = "02" }
        };

        var node = TreeFoo.CascadingTree(items).First();

        // 设置当前几点所有子项选中状态
        var cache = new TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo>(comparer);
        node.CheckedState = CheckboxState.Checked;
        node.SetChildrenCheck(cache);
        Assert.True(node.GetAllTreeSubItems().All(i => i.CheckedState == CheckboxState.Checked));
    }

    [Fact]
    public void SetParentCheck_Ok()
    {
        var cut = Context.RenderComponent<TreeView<TreeFoo>>();
        var comparer = cut.Instance;

        var items = new List<TreeFoo>()
        {
            new() { Text = "Test1", Id = "01" },
            new() { Text = "Test2", Id = "02", ParentId = "01" },
            new() { Text = "Test3", Id = "03", ParentId = "02" }
        };
        var node = TreeFoo.CascadingTree(items).First().Items.First().Items.First();
        Assert.Equal("Test3", node.Value.Text);

        // 设置当前节点所有父项选中状态
        var cache = new TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo>(comparer);
        node.CheckedState = CheckboxState.Checked;
        node.SetParentCheck(cache);

        Assert.Equal(CheckboxState.Checked, node.Parent!.CheckedState);
        Assert.Equal(CheckboxState.Checked, node.Parent!.Parent!.CheckedState);

        // 设置当前节点所有父项为选中状态
        node.CheckedState = CheckboxState.UnChecked;
        node.SetParentCheck(cache);

        Assert.Equal(CheckboxState.UnChecked, node.Parent!.CheckedState);
        Assert.Equal(CheckboxState.UnChecked, node.Parent!.Parent!.CheckedState);
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
    public void CanExpandWhenDisabled_Ok()
    {
        var items = TreeFoo.GetTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.Items, items);
        });

        // 未设置禁用
        var node = cut.Find(".node-icon");
        Assert.DoesNotContain("disabled", node.ClassList);

        // 设置 节点禁用
        items[0].IsDisabled = true;
        cut.SetParametersAndRender();
        node = cut.Find(".node-icon");
        Assert.Contains("disabled", node.ClassList);

        // 设置 CanExpandWhenDisabled 参数
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.CanExpandWhenDisabled, true);
        });
        node = cut.Find(".node-icon");
        Assert.DoesNotContain("disabled", node.ClassList);

        // 设置 Disabled 参数
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        node = cut.Find(".node-icon");
        Assert.Contains("disabled", node.ClassList);
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
    public async Task IsAccordion_Ok()
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
        });

        var bars = cut.FindAll(".fa-caret-right.visible");
        await cut.InvokeAsync(() => bars[0].Click());
        cut.WaitForAssertion(() => Assert.Contains("fa-rotate-90", cut.Markup));

        // 点击第二个节点箭头开展
        bars = cut.FindAll(".fa-caret-right.visible");
        await cut.InvokeAsync(() => bars[1].Click());

        bars = cut.FindAll(".fa-caret-right.visible");
        Assert.DoesNotContain("fa-rotate-90", bars[0].ClassName);
        Assert.Contains("fa-rotate-90", bars[1].ClassName);

        items =
        [
            new() { Text = "Root", Id = "1010" },

            new() { Text = "SubItem1", Id = "1011", ParentId = "1010" },
            new() { Text = "SubItem2", Id = "1012", ParentId = "1010" },

            new() { Text = "SubItem11", Id = "10111", ParentId = "1011" },
            new() { Text = "SubItem21", Id = "10121", ParentId = "1012" }
        ];
        nodes = TreeFoo.CascadingTree(items);

        await cut.InvokeAsync(() => cut.Instance.SetItems(nodes));
        // 子节点
        bars = cut.FindAll(".fa-caret-right.visible");
        await cut.InvokeAsync(() => bars[0].Click());

        bars = cut.FindAll(".fa-caret-right.visible");
        Assert.Contains("fa-rotate-90", bars[0].ClassName);

        // 点击第三个节点箭头开展
        bars = cut.FindAll(".fa-caret-right.visible");
        await cut.InvokeAsync(() => bars[2].Click());

        bars = cut.FindAll(".fa-caret-right.visible");
        Assert.Contains("fa-rotate-90", bars[0].ClassName);
        Assert.DoesNotContain("fa-rotate-90", bars[1].ClassName);
        Assert.Contains("fa-rotate-90", bars[2].ClassName);
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

        var checkbox = cut.FindComponent<Checkbox<TreeViewItem<TreeFoo>>>();
        await cut.InvokeAsync(checkbox.Instance.OnToggleClick);

        Assert.Contains("checked=\"checked\"", cut.Markup);
        var checkedItems = cut.Instance.GetCheckedItems().Count();
        Assert.Equal(1, checkedItems);

        await cut.InvokeAsync(() => cut.Instance.ClearCheckedItems());
        Assert.DoesNotContain("checked=\"checked\"", cut.Markup);
        checkedItems = cut.Instance.GetCheckedItems().Count();
        Assert.Equal(0, checkedItems);
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

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsFixedSearch, true);
        });
        cut.Contains("is-fixed-search");
    }

    [Fact]
    public async Task Enter_Ok()
    {
        var key = "";
        var items = TreeFoo.GetTreeItems();
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.OnSearchAsync, new Func<string?, Task<List<TreeViewItem<TreeFoo>>?>>(v =>
            {
                key = v;
                return Task.FromResult<List<TreeViewItem<TreeFoo>>?>([new TreeViewItem<TreeFoo>(new TreeFoo()) { Text = v }]);
            }));
            pb.Add(a => a.Items, items);
        });

        var input = cut.FindComponent<BootstrapInput<string?>>();
        await cut.InvokeAsync(() => input.Instance.OnEnterAsync!("enter"));
        Assert.Equal("enter", key);

        var nodes = cut.FindAll(".tree-content");
        Assert.Single(nodes);

        // trigger esc key
        await cut.InvokeAsync(() => input.Instance.OnEscAsync!(""));
        nodes = cut.FindAll(".tree-content");
        Assert.Equal(9, nodes.Count);
    }

    [Fact]
    public async Task KeyBoard_Ok()
    {
        List<TreeFoo> data =
        [
            new() { Text = "1010", Id = "1010" },
            new() { Text = "1020", Id = "1020" },
            new() { Text = "1030", Id = "1030" },

            new() { Text = "1020-01", Id = "1020-01", ParentId = "1020" },
            new() { Text = "1020-02", Id = "1020-02", ParentId = "1020" },

            new() { Text = "1020-02-01", Id = "1020-02-01", ParentId = "1020-02" },
            new() { Text = "1020-02-02", Id = "1020-02-02", ParentId = "1020-02" },

            new() { Text = "1020-02-02-01", Id = "1020-02-02-01", ParentId = "1020-02-02" },
            new() { Text = "1020-02-02-02", Id = "1020-02-02-02", ParentId = "1020-02-02" }
        ];

        var items = TreeFoo.CascadingTree(data);
        items[0].IsActive = true;
        items[1].IsExpand = true;
        items[1].Items[1].IsExpand = true;
        items[1].Items[1].Items[1].IsExpand = true;

        var activeItemText = "1010";
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.EnableKeyboard, true);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.OnTreeItemClick, new Func<TreeViewItem<TreeFoo>, Task>(treeViewItem =>
            {
                activeItemText = treeViewItem.Text;
                return Task.CompletedTask;
            }));
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowDown"));
        Assert.Equal("1030", activeItemText);

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowUp"));
        Assert.Equal("1010", activeItemText);
    }

    [Fact]
    public async Task ToggleExpand_Ok()
    {
        List<TreeFoo> data =
        [
            new() { Text = "1010", Id = "1010" },
            new() { Text = "1010-01", Id = "1010-01", ParentId = "1010" },
        ];

        var items = TreeFoo.CascadingTree(data);
        items[0].IsActive = true;
        var cut = Context.RenderComponent<TreeView<TreeFoo>>(pb =>
        {
            pb.Add(a => a.EnableKeyboard, true);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ScrollIntoViewOptions, new ScrollIntoViewOptions() { Behavior = ScrollIntoViewBehavior.Auto });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowRight"));
        cut.Contains("node-icon visible fa-solid fa-caret-right fa-rotate-90");

        await cut.InvokeAsync(() => cut.Instance.TriggerKeyDown("ArrowLeft"));
        cut.Contains("node-icon visible fa-solid fa-caret-right");
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
        public IEnumerable<IExpandableNode<TreeFoo>>? Items { get; set; } = [];

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
