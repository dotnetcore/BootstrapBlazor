// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TableSortItemTest
{
    [Theory]
    [InlineData("Name", SortOrder.Unset, "Name ")]
    [InlineData("Name", SortOrder.Asc, "Name Asc")]
    [InlineData("Name", SortOrder.Desc, "Name Desc")]
    [InlineData("", SortOrder.Unset, " ")]
    [InlineData("", SortOrder.Asc, " Asc")]
    [InlineData("", SortOrder.Desc, " Desc")]
    public void ToString_Ok(string sortName, SortOrder order, string expected)
    {
        var item = new TableSortItem() { SortName = sortName, SortOrder = order };
        var actual = item.ToString();
        Assert.Equal(expected, actual);
    }
}
