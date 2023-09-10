// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Extensions;

public class QueryPageOptionsExtensionsTest
{
    private Foo[] _foos;

    public QueryPageOptionsExtensionsTest()
    {
        _foos = new Foo[]
        {
            new Foo() { Name = "test 1", Count = 1, Complete = true, Education = EnumEducation.Primary, DateTime = new DateTime(2023, 1, 1) },
            new Foo() { Name = "test 2", Count = 2, Complete = true, Education = EnumEducation.Middle, DateTime = new DateTime(2023, 1, 2) },
            new Foo() { Name = "Test 3", Count = 3, Complete = false, Education = EnumEducation.Primary, DateTime = new DateTime(2023, 1, 3) },
            new Foo() { Name = "Test 4", Count = 4, Complete = false, Education = EnumEducation.Middle, DateTime = new DateTime(2023, 1, 4) },
            new Foo() { Name = "Mock 1", Count = 5, Complete = false, Education = EnumEducation.Primary, DateTime = new DateTime(2023, 1, 5) }
        };
    }

    [Fact]
    public void ToFilter_Searches()
    {
        var option = new QueryPageOptions();
        option.Searches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));

        var predicate = option.ToFilter().GetFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Equal(2, expected.Count());

        option.Searches.Add(new SearchFilterAction("Name", "Test"));
        predicate = option.ToFilter().GetFilterFunc<Foo>();
        expected = _foos.Where(predicate);
        Assert.Equal(4, expected.Count());
    }

    [Fact]
    public void ToFilter_CustomSearches()
    {
        var option = new QueryPageOptions();
        option.CustomerSearches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.CustomerSearches.Add(new SearchFilterAction("Count", 1, FilterAction.Equal));

        var predicate = option.ToFilter().GetFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Single(expected);
    }

    [Fact]
    public void ToFilter_AdvanceSearches()
    {
        var option = new QueryPageOptions();
        option.AdvanceSearches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.AdvanceSearches.Add(new SearchFilterAction("Count", 1, FilterAction.Equal));

        var predicate = option.ToFilter().GetFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Single(expected);
    }

    [Fact]
    public void ToFilter_Filter()
    {
        var option = new QueryPageOptions();
        option.Filters.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.Filters.Add(new SearchFilterAction("Count", 1, FilterAction.Equal));

        var predicate = option.ToFilter().GetFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Single(expected);
    }

    [Fact]
    public void ToFilter_Ok()
    {
        var option = new QueryPageOptions();
        option.Searches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.Searches.Add(new SearchFilterAction("Name", "Test", FilterAction.Contains));

        option.CustomerSearches.Add(new SearchFilterAction("Education", "Primary", FilterAction.Equal));
        option.CustomerSearches.Add(new SearchFilterAction("Complete", true, FilterAction.Equal));

        option.AdvanceSearches.Add(new SearchFilterAction("DateTime", new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Local), FilterAction.GreaterThanOrEqual));
        option.AdvanceSearches.Add(new SearchFilterAction("Count", 1, FilterAction.GreaterThanOrEqual));

        option.Filters.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.Filters.Add(new SearchFilterAction("DateTime", new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Local), FilterAction.LessThanOrEqual));

        var predicate = option.ToFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Single(expected);
    }

    [Fact]
    public void ToFilter_ToLambda()
    {
        var option = new QueryPageOptions();
        option.Searches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.Searches.Add(new SearchFilterAction("Name", "Test", FilterAction.Contains));

        option.CustomerSearches.Add(new SearchFilterAction("Education", "Primary", FilterAction.Equal));
        option.CustomerSearches.Add(new SearchFilterAction("Complete", true, FilterAction.Equal));

        option.AdvanceSearches.Add(new SearchFilterAction("DateTime", new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Local), FilterAction.GreaterThanOrEqual));
        option.AdvanceSearches.Add(new SearchFilterAction("Count", 1, FilterAction.GreaterThanOrEqual));

        option.Filters.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.Filters.Add(new SearchFilterAction("DateTime", new DateTime(2023, 1, 5, 0, 0, 0, DateTimeKind.Local), FilterAction.LessThanOrEqual));

        var expression = option.ToFilterLambda<Foo>();
        var expected = _foos.AsQueryable().Where(expression);
        Assert.Single(expected);
    }

    [Fact]
    public void HasFilters_Ok()
    {
        var filter = new FilterKeyValueAction();
        Assert.False(filter.HasFilters());

        filter.Filters = new();
        Assert.False(filter.HasFilters());

        filter.Filters.Add(new FilterKeyValueAction());
        Assert.True(filter.HasFilters());
    }
}
