// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class ITableColumnExtensionsTest
{
    [Fact]
    public void InheritValue_Ok()
    {
        var col = new InternalTableColumn("Name", typeof(string));
        var attr = new AutoGenerateClassAttribute()
        {
            Align = Alignment.Center,
            TextWrap = true,
            Ignore = true,
            Filterable = true,
            Readonly = true,
            Searchable = true,
            ShowTips = true,
            Sortable = true,
            TextEllipsis = true,
            ShowCopyColumn = true,
            Visible = false,
        };
        col.InheritValue(attr);
        Assert.Equal(Alignment.Center, col.Align);
        Assert.True(col.TextWrap);
        Assert.True(col.Ignore);
        Assert.True(col.Filterable);
        Assert.True(col.Readonly);
        Assert.True(col.Searchable);
        Assert.True(col.ShowTips);
        Assert.True(col.Sortable);
        Assert.True(col.TextEllipsis);
        Assert.True(col.ShowCopyColumn);
        Assert.False(col.Visible);
    }

    [Fact]
    public void CopyValue_Ok()
    {
        var col = new InternalTableColumn("Name", typeof(string));
        var attr = new InternalTableColumn("Name", typeof(string))
        {
            ComponentType = typeof(NullSwitch),
            ComponentParameters = [],
            Ignore = true,
            EditTemplate = obj => builder => builder.AddContent(0, "test"),
            Items = new List<SelectedItem>(),
            Lookup = new List<SelectedItem>(),
            LookupStringComparison = StringComparison.Ordinal,
            LookupServiceKey = "test-key",
            LookupService = new LookupService(),
            LookupServiceData = true,
            IsReadonlyWhenAdd = true,
            IsReadonlyWhenEdit = true,
            Readonly = true,
            Rows = 3,
            Cols = 6,
            SkipValidate = true,
            Text = "Test",
            ValidateRules = [new RequiredValidator()],
            ShowLabelTooltip = true,
            GroupName = "test-group",
            GroupOrder = 1,
            PlaceHolder = "enter placeholder",

            Align = Alignment.Center,
            TextWrap = true,
            CssClass = "test-css",
            DefaultSort = true,
            DefaultSortOrder = SortOrder.Desc,
            Filter = new TableFilter(),
            Filterable = true,
            FilterTemplate = builder => builder.AddContent(0, "test-filter"),
            Fixed = true,
            FormatString = "test-format",
            Formatter = obj =>
            {
                var ret = "test-formatter";
                return Task.FromResult<string?>(ret);
            },
            HeaderTemplate = col => builder => builder.AddContent(0, "test-header"),
            ToolboxTemplate = col => builder => builder.AddContent(0, "test-toolbox"),
            OnCellRender = args => { },
            Searchable = true,
            SearchTemplate = obj => builder => builder.AddContent(0, "test-search"),
            ShownWithBreakPoint = BreakPoint.Large,
            ShowTips = true,
            Sortable = true,
            Template = obj => builder => builder.AddContent(0, "test-template"),
            TextEllipsis = true,
            Visible = false,
            IsVisibleWhenAdd = false,
            IsVisibleWhenEdit = false,
            Width = 100,
            ShowHeaderTooltip = true,
            HeaderTextEllipsis = true,
            HeaderTextWrap = true,
            HeaderTextTooltip = "test tooltip",
            ShowSearchWhenSelect = true,
            IsPopover = true,
            ShowCopyColumn = true,
            Step = "0.01",
            Order = -1,
            IsMarkupString = true,
            GetTooltipTextCallback = _ => Task.FromResult<string?>(null),
            CustomSearch = (_, _) => new SearchFilterAction("test", "test"),

            Required = true,
            RequiredErrorMessage = "test",
            IsRequiredWhenAdd = true,
            IsRequiredWhenEdit = true
        };
        col.CopyValue(attr);
        Assert.NotNull(col.ComponentType);
        Assert.NotNull(col.ComponentParameters);
        Assert.True(col.Ignore);
        Assert.NotNull(col.EditTemplate);
        Assert.NotNull(col.Items);
        Assert.NotNull(col.Lookup);
        Assert.Equal(StringComparison.Ordinal, col.LookupStringComparison);
        Assert.Equal("test-key", col.LookupServiceKey);
        Assert.Equal(true, col.LookupServiceData);
        Assert.True(col.IsReadonlyWhenAdd);
        Assert.True(col.IsReadonlyWhenEdit);
        Assert.False(col.IsVisibleWhenAdd);
        Assert.False(col.IsVisibleWhenEdit);
        Assert.True(col.Readonly);
        Assert.Equal(3, col.Rows);
        Assert.Equal(6, col.Cols);
        Assert.True(col.SkipValidate);
        Assert.Equal("Test", col.Text);
        Assert.NotNull(col.ValidateRules);
        Assert.True(col.ShowLabelTooltip);
        Assert.Equal("test-group", col.GroupName);
        Assert.Equal(1, col.GroupOrder);

        Assert.Equal(Alignment.Center, col.Align);
        Assert.True(col.TextWrap);
        Assert.Equal("test-css", col.CssClass);
        Assert.True(col.DefaultSort);
        Assert.Equal(SortOrder.Desc, col.DefaultSortOrder);
        Assert.NotNull(col.Filter);
        Assert.True(col.Filterable);
        Assert.NotNull(col.FilterTemplate);
        Assert.True(col.Fixed);
        Assert.Equal("test-format", col.FormatString);
        Assert.NotNull(col.Formatter);
        Assert.NotNull(col.HeaderTemplate);
        Assert.NotNull(attr.ToolboxTemplate);
        Assert.NotNull(col.OnCellRender);
        Assert.True(col.Searchable);
        Assert.NotNull(col.SearchTemplate);
        Assert.Equal(BreakPoint.Large, col.ShownWithBreakPoint);
        Assert.True(col.ShowTips);
        Assert.True(col.Sortable);
        Assert.NotNull(col.Template);
        Assert.True(col.TextEllipsis);
        Assert.False(col.Visible);
        Assert.Equal(100, col.Width);
        Assert.True(col.ShowHeaderTooltip);
        Assert.True(col.HeaderTextEllipsis);
        Assert.True(col.HeaderTextWrap);
        Assert.Equal("test tooltip", col.HeaderTextTooltip);
        Assert.True(col.ShowSearchWhenSelect);
        Assert.True(col.IsPopover);
        Assert.True(col.ShowCopyColumn);
        Assert.Equal("0.01", col.Step);
        Assert.Equal(-1, col.Order);
        Assert.NotNull(col.GetTooltipTextCallback);
        Assert.True(col.IsMarkupString);
        Assert.NotNull(col.CustomSearch);

        Assert.True(col.Required);
        Assert.True(col.IsRequiredWhenEdit);
        Assert.True(col.IsRequiredWhenAdd);
        Assert.Equal("test", col.RequiredErrorMessage);

        Assert.NotNull(col.LookupService);
        Assert.Equal("test-key", col.LookupServiceKey);
        Assert.Equal(true, col.LookupServiceData);
    }

    [Fact]
    public void ToSearches_Ok()
    {
        var cols = new InternalTableColumn[]
        {
            new("Test_Name", typeof(string)),
            new("Test_Bool", typeof(bool)),
            new("Test_NullBool", typeof(bool?)),
            new("Test_Enum", typeof(SortOrder)),
            new("Test_NullEnum", typeof(SortOrder?)),
            new("Test_Int", typeof(int)),
            new("Test_NullInt", typeof(int?)),
            new("Test_Long", typeof(long)),
            new("Test_NullLong", typeof(long?)),
            new("Test_Short", typeof(short)),
            new("Test_NullShort", typeof(short?)),
            new("Test_Float", typeof(float)),
            new("Test_NullFloat", typeof(float?)),
            new("Test_Double", typeof(double)),
            new("Test_NullDouble", typeof(double?)),
            new("Test_Decimal", typeof(decimal)),
            new("Test_Decimal", typeof(decimal?)),
        };

        // NullOrEmpty
        var filters = cols.ToSearches(null);
        Assert.Empty(filters);
        filters = cols.ToSearches("");
        Assert.Empty(filters);

        // bool
        filters = cols.ToSearches("true");
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue is bool));

        // Enum
        filters = cols.ToSearches("Asc");
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue is SortOrder));

        // Number
        filters = cols.ToSearches("1");
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue?.GetType() == typeof(int)));
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue?.GetType() == typeof(short)));
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue?.GetType() == typeof(long)));

        filters = cols.ToSearches("2.1");
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue?.GetType() == typeof(float)));
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue?.GetType() == typeof(double)));
        Assert.Equal(2, filters.Count(f => f.GetFilterConditions().FieldValue?.GetType() == typeof(decimal)));
    }

    class LookupService : LookupServiceBase
    {
        public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data) => null;
    }
}
