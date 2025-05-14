﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Attributes;

public class AutoGenerateClassTest
{
    [Fact]
    public void AutoGenerateClass_Ok()
    {
        var attr = new AutoGenerateClassAttribute()
        {
            Ignore = false,
            Readonly = true,
            Sortable = true,
            Filterable = true,
            Searchable = true,
            TextWrap = true,
            TextEllipsis = true,
            ShowTips = true,
            Align = Alignment.Center
        };
        Assert.False(attr.Ignore);
        Assert.True(attr.Readonly);
        Assert.True(attr.Sortable);
        Assert.True(attr.Filterable);
        Assert.True(attr.Searchable);
        Assert.True(attr.TextWrap);
        Assert.True(attr.TextEllipsis);
        Assert.True(attr.ShowTips);
        Assert.Equal(Alignment.Center, attr.Align);
    }

    [Fact]
    public void AutoGenerateColumn_Ok()
    {
        var attr = new AutoGenerateColumnAttribute()
        {
            Order = 1,
            Ignore = true,
            DefaultSort = true,
            SkipValidate = true,
            IsReadonlyWhenAdd = true,
            IsReadonlyWhenEdit = true,
            IsVisibleWhenAdd = false,
            IsVisibleWhenEdit = false,
            ShowLabelTooltip = true,
            DefaultSortOrder = SortOrder.Asc,
            Width = 10,
            Fixed = true,
            Visible = true,
            CssClass = "test_class",
            ShownWithBreakPoint = BreakPoint.Large,
            FormatString = "yyyy-MM-dd",
            PlaceHolder = "test_holder",
            Formatter = null,
            ComponentType = typeof(Select<string>),
            Step = "1",
            Rows = 1,
            Cols = 6,
            LookupStringComparison = StringComparison.Ordinal,
            LookupServiceKey = "test-lookup",
            LookupServiceData = true,
            GroupName = "Test",
            GroupOrder = 1,
            ShowHeaderTooltip = true,
            HeaderTextTooltip = "test header tooltip",
            HeaderTextEllipsis = true,
            HeaderTextWrap = true,
            IsMarkupString = true,

            RequiredErrorMessage = "test",
            IgnoreWhenExport = true
        };
        Assert.Equal(1, attr.Order);
        Assert.True(attr.Ignore);
        Assert.True(attr.DefaultSort);
        Assert.True(attr.SkipValidate);
        Assert.True(attr.IsReadonlyWhenAdd);
        Assert.True(attr.IsReadonlyWhenEdit);
        Assert.False(attr.IsVisibleWhenAdd);
        Assert.False(attr.IsVisibleWhenEdit);
        Assert.True(attr.ShowLabelTooltip);
        Assert.Equal(SortOrder.Asc, attr.DefaultSortOrder);
        Assert.Equal(10, attr.Width);
        Assert.True(attr.Fixed);
        Assert.True(attr.Visible);
        Assert.Equal("test_class", attr.CssClass);
        Assert.Equal(BreakPoint.Large, attr.ShownWithBreakPoint);
        Assert.Equal("yyyy-MM-dd", attr.FormatString);
        Assert.Equal("test_holder", attr.PlaceHolder);
        Assert.Null(attr.Formatter);
        Assert.Equal(typeof(Select<string>), attr.ComponentType);
        Assert.Equal("1", attr.Step);
        Assert.Equal(1, attr.Rows);
        Assert.Equal(6, attr.Cols);
        Assert.Equal(StringComparison.Ordinal, attr.LookupStringComparison);
        Assert.Equal("Test", attr.GroupName);
        Assert.Equal(1, attr.GroupOrder);
        Assert.Equal("test-lookup", attr.LookupServiceKey);
        Assert.Equal(true, attr.LookupServiceData);
        Assert.True(attr.ShowHeaderTooltip);
        Assert.True(attr.HeaderTextWrap);
        Assert.True(attr.HeaderTextEllipsis);
        Assert.Equal("test header tooltip", attr.HeaderTextTooltip);
        Assert.True(attr.IsMarkupString);
        Assert.True(attr.IgnoreWhenExport);

        var attrInterface = (ITableColumn)attr;
        attrInterface.ShowLabelTooltip = true;
        Assert.True(attrInterface.ShowLabelTooltip);

        attrInterface.ShowLabelTooltip = null;
        Assert.False(attrInterface.ShowLabelTooltip);

        attrInterface.HeaderTemplate = null;
        Assert.Null(attrInterface.HeaderTemplate);

        attrInterface.Template = null;
        Assert.Null(attrInterface.Template);

        attrInterface.SearchTemplate = null;
        Assert.Null(attrInterface.SearchTemplate);

        attrInterface.FilterTemplate = null;
        Assert.Null(attrInterface.FilterTemplate);

        attrInterface.Filter = null;
        Assert.Null(attrInterface.Filter);

        attrInterface.OnCellRender = null;
        Assert.Null(attrInterface.OnCellRender);

        attrInterface.Width = null;
        Assert.Equal(0, attr.Width);

        attrInterface.IgnoreWhenExport = null;
        Assert.False(attrInterface.IgnoreWhenExport);

        attrInterface.Width = -10;
        Assert.Equal(-10, attr.Width);

        attr.Width = -10;
        Assert.Null(attrInterface.Width);

        attr.Width = 10;
        Assert.Equal(10, attrInterface.Width);

        attrInterface.IsVisibleWhenAdd = false;
        Assert.False(attrInterface.IsVisibleWhenAdd);

        attrInterface.IsVisibleWhenEdit = false;
        Assert.False(attrInterface.IsVisibleWhenEdit);

        attrInterface.IsReadonlyWhenAdd = true;
        Assert.True(attrInterface.IsReadonlyWhenAdd);

        attrInterface.IsReadonlyWhenEdit = true;
        Assert.True(attrInterface.IsReadonlyWhenEdit);

        attrInterface.GetTooltipTextCallback = _ => Task.FromResult((string?)"Test");
        Assert.NotNull(attrInterface.GetTooltipTextCallback);

        attrInterface.CustomSearch = (_, _) => new SearchFilterAction("test", "test");
        Assert.NotNull(attrInterface.CustomSearch);

        attrInterface.Searchable = null;
        Assert.False(attrInterface.Searchable);

        attrInterface.Filterable = null;
        Assert.False(attrInterface.Filterable);

        attrInterface.Searchable = null;
        Assert.False(attrInterface.Searchable);

        attrInterface.Sortable = null;
        Assert.False(attrInterface.Sortable);

        attrInterface.TextWrap = null;
        Assert.False(attrInterface.TextWrap);

        attrInterface.TextEllipsis = null;
        Assert.False(attrInterface.TextEllipsis);

        attrInterface.Ignore = null;
        Assert.False(attrInterface.Ignore);

        attrInterface.Readonly = null;
        Assert.False(attrInterface.Readonly);

        attrInterface.ShowTips = null;
        Assert.False(attrInterface.ShowTips);

        attrInterface.ShowCopyColumn = null;
        Assert.False(attrInterface.ShowCopyColumn);

        attrInterface.Visible = null;
        Assert.True(attrInterface.Visible);

        attrInterface.Align = null;
        Assert.Equal(Alignment.None, attrInterface.Align);

        attrInterface.ToolboxTemplate = col => builder => builder.AddContent(0, "test");
        Assert.NotNull(attrInterface.ToolboxTemplate);

        attrInterface.IsRequiredWhenAdd = true;
        Assert.True(attrInterface.IsRequiredWhenAdd);
        attrInterface.IsRequiredWhenEdit = true;
        Assert.True(attrInterface.IsRequiredWhenEdit);

        var attrEditor = (IEditorItem)attr;
        attrEditor.Items = null;
        Assert.Null(attrEditor.Items);

        attrEditor.EditTemplate = null;
        Assert.Null(attrEditor.EditTemplate);

        attrEditor.ComponentParameters = null;
        Assert.Null(attrEditor.ComponentParameters);

        attrEditor.Lookup = null;
        Assert.Null(attrEditor.Lookup);

        attrEditor.ValidateRules = null;
        Assert.Null(attrEditor.ValidateRules);

        attrEditor.ShowSearchWhenSelect = true;
        Assert.True(attrEditor.ShowSearchWhenSelect);

        attrEditor.IsPopover = true;
        Assert.True(attrEditor.IsPopover);

        attrEditor.LookupService = new LookupService();
        Assert.NotNull(attrEditor.LookupService);

        attrEditor.Required = true;
        Assert.True(attrEditor.Required);

        // 增加 GetDisplay 单元覆盖率
        attr.Text = null;
        Assert.Equal(string.Empty, attr.GetDisplayName());

        Assert.Equal("test", attr.RequiredErrorMessage);
    }

    class LookupService : LookupServiceBase
    {
        public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data) => null;
    }
}
