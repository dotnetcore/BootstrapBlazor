// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json;

namespace UnitTest.Extensions;

public class QueryPageOptionsExtensionsTest : BootstrapBlazorTestBase
{
    private readonly Foo[] _foos;

    public QueryPageOptionsExtensionsTest()
    {
        _foos =
        [
            new Foo() { Name = "test 1", Count = 1, Complete = true, Education = EnumEducation.Primary, DateTime = new DateTime(2023, 1, 1) },
            new Foo() { Name = "test 2", Count = 2, Complete = true, Education = EnumEducation.Middle, DateTime = new DateTime(2023, 1, 2) },
            new Foo() { Name = "Test 3", Count = 3, Complete = false, Education = EnumEducation.Primary, DateTime = new DateTime(2023, 1, 3) },
            new Foo() { Name = "Test 4", Count = 4, Complete = false, Education = EnumEducation.Middle, DateTime = new DateTime(2023, 1, 4) },
            new Foo() { Name = "Mock 1", Count = 5, Complete = false, Education = EnumEducation.Primary, DateTime = new DateTime(2023, 1, 5) }
        ];
    }

    [Fact]
    public void ToFilter_Searches()
    {
        var option = new QueryPageOptions();
        option.Searches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));

        var predicate = option.ToFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Equal(2, expected.Count());

        option.Searches.Clear();
        option.Searches.Add(new SearchFilterAction("Name", "Test"));
        predicate = option.ToFilterFunc<Foo>();
        expected = _foos.Where(predicate);
        Assert.Equal(2, expected.Count());

        option.Searches.Clear();
        option.Searches.Add(new SearchFilterAction("Name", "Mock"));
        predicate = option.ToFilterFunc<Foo>();
        expected = _foos.Where(predicate);
        Assert.Single(expected);
    }

    [Fact]
    public void ToFilter_CustomSearches()
    {
        var option = new QueryPageOptions();
        option.CustomerSearches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.CustomerSearches.Add(new SearchFilterAction("Count", 1, FilterAction.Equal));

        var predicate = option.ToFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Single(expected);
    }

    [Fact]
    public void ToFilter_AdvanceSearches()
    {
        var option = new QueryPageOptions();
        option.AdvanceSearches.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.AdvanceSearches.Add(new SearchFilterAction("Count", 1, FilterAction.Equal));

        var predicate = option.ToFilterFunc<Foo>();
        var expected = _foos.Where(predicate);
        Assert.Single(expected);
    }

    [Fact]
    public void ToFilter_Filter()
    {
        var option = new QueryPageOptions();
        option.Filters.Add(new SearchFilterAction("Name", "test", FilterAction.Contains));
        option.Filters.Add(new SearchFilterAction("Count", 1, FilterAction.Equal));

        var predicate = option.ToFilterFunc<Foo>();
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
    public void Set_Ok()
    {
        var filter = new SearchFilterAction("", "", FilterAction.Equal);
        filter.Name = "test";
        filter.Value = "test";
        filter.Action = FilterAction.GreaterThan;
        Assert.Equal("test", filter.Name);
        Assert.Equal("test", filter.Value);
        Assert.Equal(FilterAction.GreaterThan, filter.Action);
    }

    [Fact]
    public void HasFilters_Ok()
    {
        var filter = new FilterKeyValueAction();
        Assert.False(filter.HasFilters());

        filter.Filters = [];
        Assert.False(filter.HasFilters());

        filter.Filters.Add(new FilterKeyValueAction());
        Assert.True(filter.HasFilters());
    }

    [Fact]
    public void Serialize_Ok()
    {
        var model = new QueryPageOptions
        {
            SearchText = "SearchText",
            SortName = "Name1",
            StartIndex = 3,
            PageIndex = 4,
            PageItems = 5,
            SortOrder = SortOrder.Asc,
            IsFirstQuery = true,
            IsTriggerByPagination = true,
            IsPage = true,
            IsVirtualScroll = true,
            SearchModel = new { Name = "Test1", Count = 2 }
        };
        model.Searches.Add(new SearchFilterAction("Name2", "Argo2"));
        model.AdvanceSearches.Add(new SearchFilterAction("Name3", "Argo3"));
        model.CustomerSearches.Add(new SearchFilterAction("Name4", "Argo4"));
        model.Filters.Add(new SearchFilterAction("Name5", "Argo5"));
        model.SortList.AddRange(["Name6", "Count6"]);
        model.AdvancedSortList.AddRange(["Name7", "Count7"]);

        var payload = JsonSerializer.Serialize(model);
        var expacted = JsonSerializer.Deserialize<QueryPageOptions>(payload);
        Assert.NotNull(expacted);
        Assert.Equal("SearchText", expacted.SearchText);
        Assert.Equal("Name1", expacted.SortName);
        Assert.Equal(3, expacted.StartIndex);
        Assert.Equal(4, expacted.PageIndex);
        Assert.Equal(5, expacted.PageItems);
        Assert.Equal(SortOrder.Asc, expacted.SortOrder);
        Assert.True(expacted.IsFirstQuery);
        Assert.True(expacted.IsTriggerByPagination);
        Assert.True(expacted.IsPage);
        Assert.True(expacted.IsVirtualScroll);
        Assert.NotNull(expacted.SearchModel);

        Assert.Single(expacted.Searches);
        Assert.Single(expacted.AdvanceSearches);
        Assert.Single(expacted.CustomerSearches);
        Assert.Single(expacted.Filters);
        Assert.Equal(2, expacted.SortList.Count);
        Assert.Equal(2, expacted.AdvancedSortList.Count);
    }
}
