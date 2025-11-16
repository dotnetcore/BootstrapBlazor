// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
