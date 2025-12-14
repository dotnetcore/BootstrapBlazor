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
        var filter = new SearchFilterAction("", "", FilterAction.Equal)
        {
            Name = "test",
            Value = "test",
            Action = FilterAction.GreaterThan
        };
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
    public async Task Serialize_Ok()
    {
        var cut = Context.Render<DateTimeFilter>(pb =>
        {
            pb.Add(a => a.FieldKey, "DateTime");
        });
        var filter = cut.Instance;

        var conditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = DateTime.Now, FilterAction = FilterAction.GreaterThanOrEqual },
                new FilterKeyValueAction() { FieldValue = DateTime.Now, FilterAction = FilterAction.LessThanOrEqual }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(conditions));

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
            SearchModel = new Foo { Name = "Test1", Count = 2 }
        };

        model.Filters.Add(cut.Instance);
        model.Searches.Add(new SerializeFilterAction() { Filter = new FilterKeyValueAction() { FieldKey = "Name2", FieldValue = "Argo2" } });
        model.AdvanceSearches.Add(new SerializeFilterAction() { Filter = new FilterKeyValueAction() { FieldKey = "Name3", FieldValue = "Argo3" } });
        model.CustomerSearches.Add(new SerializeFilterAction() { Filter = new FilterKeyValueAction() { FieldKey = "Name4", FieldValue = "Argo4" } });
        model.SortList.AddRange(["Name6", "Count6"]);
        model.AdvancedSortList.AddRange(["Name7", "Count7"]);

        var payload = JsonSerializer.Serialize(model);
        var expected = JsonSerializer.Deserialize<QueryPageOptions>(payload);
        Assert.NotNull(expected);
        Assert.Equal("SearchText", expected.SearchText);

        var foo = expected.SearchModel as Foo;
        Assert.NotNull(foo);
        Assert.Equal("Test1", foo.Name);

        Assert.Equal("Name1", expected.SortName);
        Assert.Equal(3, expected.StartIndex);
        Assert.Equal(4, expected.PageIndex);
        Assert.Equal(5, expected.PageItems);
        Assert.Equal(SortOrder.Asc, expected.SortOrder);
        Assert.True(expected.IsFirstQuery);
        Assert.True(expected.IsTriggerByPagination);
        Assert.True(expected.IsPage);
        Assert.True(expected.IsVirtualScroll);
        Assert.NotNull(expected.SearchModel);

        Assert.Single(expected.Searches);
        Assert.Single(expected.AdvanceSearches);
        Assert.Single(expected.CustomerSearches);
        Assert.Single(expected.Filters);

        Assert.Equal(2, expected.SortList.Count);
        Assert.Equal(2, expected.AdvancedSortList.Count);
    }

    [Fact]
    public void SearchModel_Serialize()
    {
        var model = new QueryPageOptions
        {
            SearchModel = new { Name = "Test1", Count = 2 }
        };
        var payload = JsonSerializer.Serialize(model);
        var expected = JsonSerializer.Deserialize<QueryPageOptions>(payload);

        Assert.NotNull(expected);
        Assert.NotNull(expected.SearchModel);
    }

    [Fact]
    public void SearchModel_TableSearchModel_Serialize()
    {
        var model = new QueryPageOptions
        {
            SearchModel = new FooSearchModel { Name = "Test1" }
        };
        var payload = JsonSerializer.Serialize(model);
        var expected = JsonSerializer.Deserialize<QueryPageOptions>(payload);

        Assert.NotNull(expected);
        Assert.NotNull(expected.SearchModel);
    }

    private class FooSearchModel : ITableSearchModel
    {
        public string? Name { get; set; }

        public IEnumerable<IFilterAction> GetSearches()
        {
            var ret = new List<IFilterAction>();
            if (!string.IsNullOrEmpty(Name))
            {
                ret.Add(new SearchFilterAction(nameof(Foo.Name), Name));
            }
            return ret;
        }

        public void Reset()
        {
            Name = null;
        }
    }

    [Fact]
    public void SerializeFilterAction_Ok()
    {
        var filter = new SerializeFilterAction();
        var action = filter.GetFilterConditions();
        Assert.Empty(action.Filters);

        filter.SetFilterConditionsAsync(new FilterKeyValueAction()
        {
            FieldKey = "DateTime",
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = DateTime.Now, FilterAction = FilterAction.GreaterThanOrEqual },
                new FilterKeyValueAction() { FieldValue = DateTime.Now, FilterAction = FilterAction.LessThanOrEqual }
            ]
        });
        action = filter.GetFilterConditions();
        Assert.Equal(2, action.Filters.Count);

        filter.Reset();
        action = filter.GetFilterConditions();
        Assert.Empty(action.Filters);
    }
}
