// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Extensions;

public class ITableColumnExtensionsTest
{
    [Fact]
    public void InheritValue_Ok()
    {
        var col = new MockTableColumn("Name", typeof(string));
        var attr = new AutoGenerateClassAttribute()
        {
            Align = Alignment.Center,
            TextWrap = true,
            Editable = false,
            Filterable = true,
            Readonly = true,
            Searchable = true,
            ShowTips = true,
            Sortable = true,
            TextEllipsis = true,
            ShowCopyColumn = true
        };
        col.InheritValue(attr);
        Assert.Equal(Alignment.Center, col.Align);
        Assert.True(attr.TextWrap);
        Assert.False(attr.Editable);
        Assert.True(attr.Filterable);
        Assert.True(attr.Readonly);
        Assert.True(attr.Searchable);
        Assert.True(attr.ShowTips);
        Assert.True(attr.Sortable);
        Assert.True(attr.TextEllipsis);
        Assert.True(attr.ShowCopyColumn);
    }

    [Fact]
    public void CopyValue_Ok()
    {
        var col = new MockTableColumn("Name", typeof(string));
        var attr = new MockTableColumn("Name", typeof(string))
        {
            ComponentType = typeof(NullSwitch),
            ComponentParameters = Enumerable.Empty<KeyValuePair<string, object>>(),
            Editable = false,
            EditTemplate = new RenderFragment<object>(obj => builder => builder.AddContent(0, "test")),
            Items = new List<SelectedItem>(),
            Lookup = new List<SelectedItem>(),
            LookupStringComparison = StringComparison.Ordinal,
            LookupServiceKey = "test-key",
            IsReadonlyWhenAdd = true,
            IsReadonlyWhenEdit = true,
            Readonly = true,
            Rows = 3,
            SkipValidate = true,
            Text = "Test",
            ValidateRules = new List<IValidator>() { new RequiredValidator() },
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
            FilterTemplate = new RenderFragment(builder => builder.AddContent(0, "test-filter")),
            Fixed = true,
            FormatString = "test-format",
            Formatter = obj =>
            {
                return Task.FromResult("test-formatter");
            },
            HeaderTemplate = new RenderFragment<ITableColumn>(col => builder => builder.AddContent(0, "test-header")),
            OnCellRender = args => { },
            Searchable = true,
            SearchTemplate = new RenderFragment<object>(obj => builder => builder.AddContent(0, "test-search")),
            ShownWithBreakPoint = BreakPoint.Large,
            ShowTips = true,
            Sortable = true,
            Template = new RenderFragment<object>(obj => builder => builder.AddContent(0, "test-template")),
            TextEllipsis = true,
            Visible = false,
            Width = 100,
            ShowHeaderTooltip = true,
            HeaderTextEllipsis = true,
            HeaderTextWrap = true,
            HeaderTextTooltip = "test tooltip",
            ShowSearchWhenSelect = true,
            IsPopover = true,
            ShowCopyColumn = true,
        };
        col.CopyValue(attr);
        Assert.NotNull(attr.ComponentType);
        Assert.NotNull(attr.ComponentParameters);
        Assert.False(attr.Editable);
        Assert.NotNull(attr.EditTemplate);
        Assert.NotNull(attr.Items);
        Assert.NotNull(attr.Lookup);
        Assert.Equal(StringComparison.Ordinal, attr.LookupStringComparison);
        Assert.Equal("test-key", attr.LookupServiceKey);
        Assert.True(attr.IsReadonlyWhenAdd);
        Assert.True(attr.IsReadonlyWhenEdit);
        Assert.True(attr.Readonly);
        Assert.Equal(3, attr.Rows);
        Assert.True(attr.SkipValidate);
        Assert.Equal("Test", attr.Text);
        Assert.NotNull(attr.ValidateRules);
        Assert.True(attr.ShowLabelTooltip);
        Assert.Equal("test-group", attr.GroupName);
        Assert.Equal(1, attr.GroupOrder);

        Assert.Equal(Alignment.Center, col.Align);
        Assert.True(attr.TextWrap);
        Assert.Equal("test-css", attr.CssClass);
        Assert.True(attr.DefaultSort);
        Assert.Equal(SortOrder.Desc, attr.DefaultSortOrder);
        Assert.NotNull(attr.Filter);
        Assert.True(attr.Filterable);
        Assert.NotNull(attr.FilterTemplate);
        Assert.True(attr.Fixed);
        Assert.Equal("test-format", attr.FormatString);
        Assert.NotNull(attr.Formatter);
        Assert.NotNull(attr.HeaderTemplate);
        Assert.NotNull(attr.OnCellRender);
        Assert.True(attr.Searchable);
        Assert.NotNull(attr.SearchTemplate);
        Assert.Equal(BreakPoint.Large, attr.ShownWithBreakPoint);
        Assert.True(attr.ShowTips);
        Assert.True(attr.Sortable);
        Assert.NotNull(attr.Template);
        Assert.True(attr.TextEllipsis);
        Assert.False(attr.Visible);
        Assert.Equal(100, attr.Width);
        Assert.True(attr.ShowHeaderTooltip);
        Assert.True(attr.HeaderTextEllipsis);
        Assert.True(attr.HeaderTextWrap);
        Assert.Equal("test tooltip", attr.HeaderTextTooltip);
        Assert.True(attr.ShowSearchWhenSelect);
        Assert.True(attr.IsPopover);
        Assert.True(attr.ShowCopyColumn);
    }

    [Fact]
    public void ToSearches_Ok()
    {
        var cols = new MockTableColumn[]
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
}
