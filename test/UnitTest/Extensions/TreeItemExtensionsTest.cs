// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics;

namespace UnitTest.Extensions;

public class TreeItemExtensionsTest
{

    /// <summary>
    /// 单一顶级树节点
    /// </summary>
    [Fact]
    public async Task 单一顶级树节点()
    {
        //GetAllItems存在错误。
        var sourceItems = new List<TreeViewItem<string>>()
        {
            new TreeViewItem<string>("01")
            {
                Text="01",Items = new List<TreeViewItem<string>>()
                {
                    new TreeViewItem<string>("0101") { Text="0101" },
                    new TreeViewItem<string>("0102") { Text="0102" },
                }
            }
        };

        var items = sourceItems.GetAllItems();
        var list = items.Select(x => x.Text ?? "").ToList();

        Assert.True(items.Count() == 3);

        await Task.CompletedTask;
    }


    /// <summary>
    /// 多顶级树节点
    /// </summary>
    [Fact]
    public async Task 多顶级树节点()
    {
        //GetAllItems存在错误。
        var sourceItems = new List<TreeViewItem<string>>()
        {
            new TreeViewItem<string>("01")
            {
                Text="01",Items = new List<TreeViewItem<string>>()
                {
                    new TreeViewItem<string>("0101") { Text="0101" },
                    new TreeViewItem<string>("0102") { Text="0102" },
                }
            },
            new TreeViewItem<string>("02")
            {
                Text="02",Items = new List<TreeViewItem<string>>()
                {
                    new TreeViewItem<string>("0201") { Text="0201" },
                    new TreeViewItem<string>("0202") { Text="0202" },
                }
            },
        };

        var items = sourceItems.GetAllItems();
        var list = items.Select(x => x.Text ?? "").ToList();

        Assert.True(items.Count() == 6);

        await Task.CompletedTask;
    }
}
