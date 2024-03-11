// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Extensions;

public class TreeItemExtensionsTest
{
    /// <summary>
    /// 单一顶级树节点
    /// </summary>
    [Fact]
    public void GetAllItems_Single()
    {
        var sourceItems = new List<TreeViewItem<string>>()
        {
            new("01")
            {
                Text="01",Items =
                [
                    new TreeViewItem<string>("0101") { Text="0101" },
                    new TreeViewItem<string>("0102") { Text="0102" }
                ]
            }
        };
        var items = sourceItems.GetAllItems();
        var list = items.Select(x => x.Text ?? "").ToList();
        Assert.Equal(3, items.Count());
    }

    /// <summary>
    /// 多顶级树节点
    /// </summary>
    [Fact]
    public void GetAllItems_Multiple()
    {
        var sourceItems = new List<TreeViewItem<string>>()
        {
            new("01")
            {
                Text="01",Items =
                [
                    new TreeViewItem<string>("0101") { Text="0101" },
                    new TreeViewItem<string>("0102") { Text="0102" },
                ]
            },
            new("02")
            {
                Text="02",Items =
                [
                    new TreeViewItem<string>("0201") { Text="0201" },
                    new TreeViewItem<string>("0202") { Text="0202" },
                ]
            },
        };

        var items = sourceItems.GetAllItems();
        var list = items.Select(x => x.Text ?? "").ToList();

        Assert.Equal(6, items.Count());
    }
}
