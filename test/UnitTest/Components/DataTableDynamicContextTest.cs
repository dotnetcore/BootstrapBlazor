// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Data;

namespace UnitTest.Components;

public class DataTableDynamicContextTest : BootstrapBlazorTestBase
{
    [Fact]
    public void UseCache_Ok()
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Rows.Add(1);
        table.AcceptChanges();

        var context = new DataTableDynamicContext(table);
        Assert.True(context.UseCache);

        var data = context.GetItems();
        Assert.Single(data);
        Assert.Equal(1, data.First().GetValue("Id"));

        // 增加数据
        table.Rows.Add(2);
        var data2 = context.GetItems();
        Assert.Equal(data, data2);

        // 关闭缓存
        context.UseCache = false;
        table.Rows.Add(3);
        data2 = context.GetItems();
        Assert.Equal(3, data2.Count());
    }
}
