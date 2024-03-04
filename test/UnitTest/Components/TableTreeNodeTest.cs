// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TableTreeNodeTest
{
    [Fact]
    public void Parent_Ok()
    {
        var node = new TableTreeNode<Foo>(new Foo() { Name = "Test" });
        var target = (IExpandableNode<Foo>)node;
        Assert.Null(target.Parent);
    }
}
