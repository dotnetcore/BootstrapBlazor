// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
