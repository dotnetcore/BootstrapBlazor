// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Misc;

public class NodeItemTest
{
    [Fact]
    public void NodeItem_Ok()
    {
        var item = new MockNodeItem()
        {
            Id = "00",
            ParentId = "",
            IsCollapsed = true
        };
        Assert.Equal("00", item.Id);
        Assert.Equal("", item.ParentId);
        Assert.True(item.IsCollapsed);
    }

    class MockNodeItem : NodeItem
    {

    }
}
