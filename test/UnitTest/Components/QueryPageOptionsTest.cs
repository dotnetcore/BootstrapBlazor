// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class QueryPageOptionsTest
{
    [Fact]
    public void SearchText_Ok()
    {
        var option = new QueryPageOptions()
        {
            SearchText = "Test"
        };
        Assert.Equal("Test", option.SearchText);
    }

    [Fact]
    public void SearchModel_Ok()
    {
        var option = new QueryPageOptions()
        {
            SearchModel = new Foo() { Name = "Test" }
        };
        Assert.Equal("Test", (option.SearchModel as Foo)!.Name);
    }

    [Fact]
    public void PageIndex_Ok()
    {
        var option = new QueryPageOptions()
        {
            PageIndex = 2
        };
        Assert.Equal(2, option.PageIndex);
    }

    [Fact]
    public void StartIndex_Ok()
    {
        var option = new QueryPageOptions()
        {
            StartIndex = 2
        };
        Assert.Equal(2, option.StartIndex);
    }

    [Fact]
    public void PageItems_Ok()
    {
        var option = new QueryPageOptions()
        {
            PageItems = 20
        };
        Assert.Equal(20, option.PageItems);
    }

    [Fact]
    public void IsPage_Ok()
    {
        var option = new QueryPageOptions()
        {
            IsPage = true
        };
        Assert.True(option.IsPage);
    }
}
