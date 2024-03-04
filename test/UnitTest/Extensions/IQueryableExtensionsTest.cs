// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Extensions;

public class IQueryableExtensionsTest
{
    [Fact]
    public void Where_Ok()
    {
        var foos = new Foo[]
        {
            new() { Name = "Test1" },
            new() { Name = "Test2" }
        }.AsQueryable();

        Assert.Equal(1, foos.Where(f => f.Name == "Test1", true).Count());
        Assert.Equal(2, foos.Where(f => f.Name == "Test1", false).Count());
    }

    [Fact]
    public void Sort_Ok()
    {
        var foos = new Foo[]
        {
            new() { Name = "Test1" },
            new() { Name = "Test2" }
        }.AsQueryable();

        Assert.Equal("Test2", foos.Sort<Foo>("Name", SortOrder.Desc, true).First().Name);
        Assert.Equal("Test1", foos.Sort<Foo>("Name", SortOrder.Desc, false).First().Name);
    }

    [Fact]
    public void Page_Ok()
    {
        var foos = new Foo[]
        {
            new() { Name = "Test1" },
            new() { Name = "Test2" }
        }.AsQueryable();

        Assert.Equal(1, foos.Page(1, 1).Count());
    }

    [Fact]
    public void Count_Ok()
    {
        var foos = new Foo[]
        {
            new() { Name = "Test1" },
            new() { Name = "Test2" }
        }.AsQueryable();
        foos.Count(out var v);
        Assert.Equal(2, v);
    }
}
