// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class IQueryableExtensionsTest : BootstrapBlazorTestBase
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

        Assert.Equal("Test2", foos.Sort("Name", SortOrder.Desc, true).First().Name);
        Assert.Equal("Test1", foos.Sort("Name", SortOrder.Desc, false).First().Name);
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
