// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Misc;

public class TreeNodeCacheTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ToggleCheck_Ok()
    {
        var cut = Context.Render<TreeView<TreeFoo>>();
        var comparer = cut.Instance;

        var items = TreeFoo.GetTreeItems();
        var nodeCache = new TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo>(comparer);
        nodeCache.IsChecked(items);

        // 设置 1010 节点为选中状态
        var node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1010",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1020 节点为未确定状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1020",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Indeterminate
        };
        nodeCache.ToggleCheck(node);

        // 设置 1040 节点为未选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1040",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.UnChecked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1100 子节点全部被选中
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1130",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1100 子节点全部被选中
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1140",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1100 子节点全部被选中
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1150",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1100 子节点选中
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1100",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1080 节点为选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1080",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Indeterminate
        };
        nodeCache.ToggleCheck(node);

        // 设置 1050 节点为选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1050",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Indeterminate
        };
        nodeCache.ToggleCheck(node);

        // 设置 1020 节点为选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1020",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Indeterminate
        };
        nodeCache.ToggleCheck(node);

        var count = GetCheckItemCount(nodeCache);
        Assert.Equal(5, count);

        count = GetUncheckItemCount(nodeCache);
        Assert.Equal(1, count);

        count = GetIndeterminateItemCount(nodeCache);
        Assert.Equal(3, count);

        // 开始测试
        // 设置 1030 节点为选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1030",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1030 节点为未选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1030",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.UnChecked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1030 节点为选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1030",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 重给 Items
        items = TreeFoo.GetTreeItems();
        nodeCache.IsChecked(items);

        // 检查结果
        var nodes = items.GetAllItems();
        var state = nodes.First(i => i.Value?.Id == "1010").CheckedState;
        Assert.Equal(CheckboxState.Checked, state);

        state = nodes.First(i => i.Value?.Id == "1030").CheckedState;
        Assert.Equal(CheckboxState.Checked, state);

        state = nodes.First(i => i.Value?.Id == "1020").CheckedState;
        Assert.Equal(CheckboxState.Indeterminate, state);

        state = nodes.First(i => i.Value?.Id == "1050").CheckedState;
        Assert.Equal(CheckboxState.Indeterminate, state);

        state = nodes.First(i => i.Value?.Id == "1080").CheckedState;
        Assert.Equal(CheckboxState.Indeterminate, state);

        state = nodes.First(i => i.Value?.Id == "1100").CheckedState;
        Assert.Equal(CheckboxState.Checked, state);

        state = nodes.First(i => i.Value?.Id == "1130").CheckedState;
        Assert.Equal(CheckboxState.Checked, state);

        state = nodes.First(i => i.Value?.Id == "1130").CheckedState;
        Assert.Equal(CheckboxState.Checked, state);

        state = nodes.First(i => i.Value?.Id == "1130").CheckedState;
        Assert.Equal(CheckboxState.Checked, state);
    }

    [Fact]
    public void FindParentNode_Ok()
    {
        var cut = Context.Render<TreeView<TreeFoo>>();
        var comparer = cut.Instance;

        var items = TreeFoo.GetTreeItems();
        var nodeCache = new TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo>(comparer);
        var targetId = nodeCache.FindParentNode(items, new TreeViewItem<TreeFoo>(new TreeFoo() { Id = "1110" }))?.Value?.Id;
        Assert.Equal("1080", targetId);
    }

    [Fact]
    public void SetChildrenCheck_Ok()
    {
        var cut = Context.Render<TreeView<TreeFoo>>();
        var comparer = cut.Instance;

        var items = TreeFoo.GetTreeItems();
        var nodeCache = new TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo>(comparer);
        var count = GetUncheckItemCount(nodeCache);
        Assert.Equal(0, count);

        var node = nodeCache.FindParentNode(items, new TreeViewItem<TreeFoo>(new TreeFoo() { Id = "1110" }));
        Assert.NotNull(node);
        Assert.Equal("Sub Menu Two", node.Value.Text);

        node.CheckedState = CheckboxState.UnChecked;
        node.SetChildrenCheck(nodeCache);
        count = GetUncheckItemCount(nodeCache);
        Assert.Equal(6, count);

        // Indeterminate 状态不更改子节点状态
        node.CheckedState = CheckboxState.Indeterminate;
        node.SetChildrenCheck(nodeCache);
        count = GetUncheckItemCount(nodeCache);
        Assert.Equal(6, count);
    }

    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.Render<TreeView<TreeFoo>>();
        var comparer = cut.Instance;

        var nodeCache = new TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo>(comparer);

        // 设置 1070 节点为选中状态
        var node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1070",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Checked
        };
        nodeCache.ToggleCheck(node);

        // 设置 1080 节点为选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1080",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.Indeterminate
        };
        nodeCache.ToggleCheck(node);

        // 设置 1090 节点为选中状态
        node = new TreeViewItem<TreeFoo>(new TreeFoo()
        {
            Id = "1090",
            Text = "Test"
        })
        {
            CheckedState = CheckboxState.UnChecked
        };
        nodeCache.ToggleCheck(node);

        var count = GetCheckItemCount(nodeCache);
        Assert.Equal(1, count);

        count = GetUncheckItemCount(nodeCache);
        Assert.Equal(1, count);

        count = GetIndeterminateItemCount(nodeCache);
        Assert.Equal(1, count);

        nodeCache.Reset();
        count = GetCheckItemCount(nodeCache);
        Assert.Equal(0, count);

        count = GetUncheckItemCount(nodeCache);
        Assert.Equal(0, count);

        count = GetIndeterminateItemCount(nodeCache);
        Assert.Equal(0, count);
    }

    private bool Comparer(TreeFoo x, TreeFoo y) => x.Id == y.Id;

    private static int GetCheckItemCount(TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo> treeNodeCache)
    {
        var count = 0;
        var type = treeNodeCache.GetType();
        var pi = type.GetField("_checkedNodeCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (pi != null && pi.GetValue(treeNodeCache) is HashSet<TreeFoo> data)
        {
            count = data.Count;
        }
        return count;
    }

    private static int GetUncheckItemCount(TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo> treeNodeCache)
    {
        var count = 0;
        var type = treeNodeCache.GetType();
        var pi = type.GetField("_uncheckedNodeCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (pi != null && pi.GetValue(treeNodeCache) is HashSet<TreeFoo> data)
        {
            count = data.Count;
        }
        return count;
    }

    private static int GetIndeterminateItemCount(TreeNodeCache<TreeViewItem<TreeFoo>, TreeFoo> treeNodeCache)
    {
        var count = 0;
        var type = treeNodeCache.GetType();
        var pi = type.GetField("_indeterminateNodeCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (pi != null && pi.GetValue(treeNodeCache) is HashSet<TreeFoo> data)
        {
            count = data.Count;
        }
        return count;
    }
}
