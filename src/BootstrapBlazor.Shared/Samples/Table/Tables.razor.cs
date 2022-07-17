// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 表格示例代码
/// </summary>
public partial class Tables
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Tables>? TablesLocalizer { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private string? RefreshText { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);

        RefreshText ??= TablesLocalizer[nameof(RefreshText)];
    }

    private void OnClick()
    {
        Items = Foo.GenerateFoo(Localizer);
    }

    private IEnumerable<AttributeItem> GetTableColumnAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "TextWrap",
            Description = TablesLocalizer["TextWrapAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "AutoGenerateColumns",
            Description = TablesLocalizer["AutoGenerateColumnsAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "CssClass",
            Description = TablesLocalizer["CssClassAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Editable",
            Description = TablesLocalizer["EditableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "EditTemplate",
            Description = TablesLocalizer["EditTemplateColumnAttr"],
            Type = "RenderFragment<object>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Filterable",
            Description = TablesLocalizer["FilterableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "FilterTemplate",
            Description = TablesLocalizer["FilterTemplateAttr"],
            Type = "RenderFragment?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Filter",
            Description = TablesLocalizer["FilterAttr"],
            Type = "IFilter?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = TablesLocalizer["HeaderTemplateAttr"],
            Type = "RenderFragment<ITableColumn>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(IEditorItem.IsReadonlyWhenAdd),
            Description = TablesLocalizer["IsReadonlyWhenAddAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(IEditorItem.IsReadonlyWhenEdit),
            Description = TablesLocalizer["IsReadonlyWhenEditAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(IEditorItem.Lookup),
            Description = TablesLocalizer["LookupAttr"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(IEditorItem.LookupStringComparison),
            Description = TablesLocalizer["LookupStringComparisonAttr"],
            Type = "StringComparison",
            ValueList = " — ",
            DefaultValue = "OrdinalIgnoreCase"
        },
        new()
        {
            Name = nameof(IEditorItem.LookupServiceKey),
            Description = TablesLocalizer["LookupServiceKeyAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Readonly",
            Description = TablesLocalizer["ReadonlyAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "SearchTemplate",
            Description = TablesLocalizer["SearchTemplateColumnAttr"],
            Type = "RenderFragment<object>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowTips",
            Description = TablesLocalizer["ShowTipsAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Searchable",
            Description = TablesLocalizer["SearchableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Sortable",
            Description = TablesLocalizer["SortableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DefaultSort",
            Description = TablesLocalizer["DefaultSortAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DefaultSortOrder",
            Description = TablesLocalizer["DefaultSortOrderAttr"],
            Type = "SortOrder",
            ValueList = "Unset|Asc|Desc",
            DefaultValue = "Unset"
        },
        new()
        {
            Name = "Text",
            Description = TablesLocalizer["TextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TextEllipsis",
            Description = TablesLocalizer["TextEllipsisAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Template",
            Description = TablesLocalizer["TemplateAttr"],
            Type = "RenderFragment<TableColumnContext<object, TItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Visible",
            Description = TablesLocalizer["VisibleAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Width",
            Description = TablesLocalizer["WidthAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Fixed",
            Description = TablesLocalizer["FixedAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TableColumn<Foo, string>.GroupName),
            Description = TablesLocalizer["GroupNameAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TableColumn<Foo, string>.GroupOrder),
            Description = TablesLocalizer["GroupOrderAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShownWithBreakPoint",
            Description = TablesLocalizer["ShownWithBreakPointAttr"],
            Type = "BreakPoint",
            ValueList = "None|ExtraSmall|...",
            DefaultValue = "None"
        },
        new()
        {
            Name = "FormatString",
            Description = TablesLocalizer["FormatStringAttr"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Formatter",
            Description = TablesLocalizer["FormatterAttr"],
            Type = "Func<object?, Task<string>>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Align",
            Description = TablesLocalizer["AlignAttr"],
            Type = "Alignment",
            ValueList = "None|Left|Center|Right",
            DefaultValue = "None"
        },
        new()
        {
            Name = "Order",
            Description = TablesLocalizer["OrderAttr"],
            Type = "int",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = "OnCellRender",
            Description = TablesLocalizer["OnCellRenderAttr"],
            Type = "Action<TableCellArgs>?",
            ValueList = " - ",
            DefaultValue = " - "
        },
    };

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new()
        {
            Name = "TableSize",
            Description = TablesLocalizer["TableSizeAttr"],
            Type = "TableSize",
            ValueList = "Normal|Compact",
            DefaultValue = "Normal"
        },
        new()
        {
            Name = "HeaderStyle",
            Description = TablesLocalizer["HeaderStyleAttr"],
            Type = "TableHeaderStyle",
            ValueList = "None|Light|Dark",
            DefaultValue = "None"
        },
        new()
        {
            Name = "HeaderTextWrap",
            Description = TablesLocalizer["HeaderTextWrap"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Height",
            Description = TablesLocalizer["HeightAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PageItems",
            Description = TablesLocalizer["PageItemsAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "AutoRefreshInterval",
            Description = TablesLocalizer["AutoRefreshIntervalAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2000"
        },
        new()
        {
            Name = "ExtendButtonColumnWidth",
            Description = TablesLocalizer["ExtendButtonColumnWidthAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "130"
        },
        new()
        {
            Name = "RenderModeResponsiveWidth",
            Description = TablesLocalizer["RenderModeResponsiveWidthAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "768"
        },
        new()
        {
            Name = "IndentSize",
            Description = TablesLocalizer["IndentSizeAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "16"
        },
        new()
        {
            Name = "Items",
            Description = TablesLocalizer["ItemsAttr"],
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PageItemsSource",
            Description = TablesLocalizer["PageItemsSourceAttr"],
            Type = "IEnumerable<int>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditMode",
            Description = TablesLocalizer["EditModeAttr"],
            Type = "EditMode",
            ValueList = "Popup|Inline|InCell",
            DefaultValue = "Popup"
        },
        new()
        {
            Name = "MultiHeaderTemplate",
            Description = TablesLocalizer["MultiHeaderTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TableFooter",
            Description = TablesLocalizer["TableFooterAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TableToolbarTemplate",
            Description = TablesLocalizer["TableToolbarTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditTemplate",
            Description = TablesLocalizer["EditTemplateAttr"],
            Type = "RenderFragment<TItem?>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowAdvancedSearch",
            Description = TablesLocalizer["ShowAdvancedSearchAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "SearchTemplate",
            Description = TablesLocalizer["SearchTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BeforeRowButtonTemplate",
            Description = TablesLocalizer["BeforeRowButtonTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "RowButtonTemplate",
            Description = TablesLocalizer["RowButtonTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "DetailRowTemplate",
            Description = TablesLocalizer["DetailRowTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsAutoCollapsedToolbarButton",
            Description = TablesLocalizer["IsAutoCollapsedToolbarButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsBordered",
            Description = TablesLocalizer["IsBorderedAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsPagination",
            Description = TablesLocalizer["IsPaginationAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsStriped",
            Description = TablesLocalizer["IsStripedAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsRendered",
            Description = TablesLocalizer["IsRenderedAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsMultipleSelect",
            Description = TablesLocalizer["IsMultipleSelectAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsAutoRefresh",
            Description = TablesLocalizer["IsAutoRefreshAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsTree",
            Description = TablesLocalizer["IsTreeAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsDetails",
            Description = TablesLocalizer["IsDetailsAttr"],
            Type = "boolean",
            ValueList = "true / false / null",
            DefaultValue = "null"
        },
        new()
        {
            Name = nameof(Table<Foo>.IsHideFooterWhenNoData),
            Description = TablesLocalizer["IsHideFooterWhenNoDataAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ClickToSelect",
            Description = TablesLocalizer["ClickToSelectAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowCheckboxText",
            Description = TablesLocalizer["ShowCheckboxTextAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowFooter",
            Description = TablesLocalizer["ShowFooterAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowFilterHeader),
            Description = TablesLocalizer["ShowFilterHeaderAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowMultiFilterHeader),
            Description = TablesLocalizer["ShowMultiFilterHeaderAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSearch",
            Description = TablesLocalizer["ShowSearchAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSearchText",
            Description = TablesLocalizer["ShowSearchTextAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowSearchTextTooltip",
            Description = TablesLocalizer["ShowSearchTextTooltipAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowResetButton",
            Description = TablesLocalizer["ShowResetButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowSearchButton",
            Description = TablesLocalizer["ShowSearchButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "SearchMode",
            Description = TablesLocalizer["SearchModeAttr"],
            Type = "SearchMode",
            ValueList = "Popup / Top",
            DefaultValue = "Popup"
        },
        new()
        {
            Name = nameof(Table<Foo>.CollapsedTopSearch),
            Description = TablesLocalizer["CollapsedTopSearchAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowToolbar",
            Description = TablesLocalizer["ShowToolbarAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowLineNo",
            Description = TablesLocalizer["ShowLineNoAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowDefaultButtons",
            Description = TablesLocalizer["ShowDefaultButtonsAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowAddButton",
            Description = TablesLocalizer["ShowAddButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowEditButton",
            Description = TablesLocalizer["ShowEditButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowEditButtonCallback",
            Description = TablesLocalizer["ShowEditButtonCallbackAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowDeleteButton",
            Description = TablesLocalizer["ShowDeleteButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowDeleteButtonCallback",
            Description = TablesLocalizer["ShowDeleteButtonCallbackAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowExtendButtons",
            Description = TablesLocalizer["ShowExtendButtonsAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowExtendEditButton),
            Description = TablesLocalizer["ShowExtendEditButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowExtendDeleteButton),
            Description = TablesLocalizer["ShowExtendDeleteButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowUnsetGroupItemsOnTop),
            Description = TablesLocalizer["ShowUnsetGroupItemsOnTopAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSkeleton",
            Description = TablesLocalizer["ShowSkeletonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowLoadingInFirstRender",
            Description = TablesLocalizer["ShowLoadingInFirstRenderAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowColumnList",
            Description = TablesLocalizer["ShowColumnListAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnColumnVisibleChanged",
            Description = TablesLocalizer["OnColumnVisibleChangedAttr"],
            Type = "Func<string, bool>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowEmpty",
            Description = TablesLocalizer["ShowEmptyAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowToastAfterSaveOrDeleteModel",
            Description = TablesLocalizer["ShowToastAfterSaveOrDeleteModelAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "TreeIcon",
            Description = TablesLocalizer["TreeIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-caret-right"
        },
        new()
        {
            Name = "UseComponentWidth",
            Description = TablesLocalizer["UseComponentWidthAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ScrollingDialogContent",
            Description = TablesLocalizer["ScrollingDialogContentAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "FixedExtendButtonsColumn",
            Description = TablesLocalizer["FixedExtendButtonsColumnAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnQueryAsync",
            Description = TablesLocalizer["OnQueryAsyncAttr"],
            Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnAddAsync",
            Description = TablesLocalizer["OnAddAsyncAttr"],
            Type = "Func<Task<TItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.OnColumnCreating),
            Description = TablesLocalizer["OnColumnCreatingAttr"],
            Type = "Func<List<ITableColumn>,Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.OnDoubleClickCellCallback),
            Description = TablesLocalizer["OnDoubleClickCellCallbackAttr"],
            Type = "Func<string, object, object?, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnDeleteAsync",
            Description = TablesLocalizer["OnDeleteAsyncAttr"],
            Type = "Func<IEnumerable<TItem>, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnEditAsync",
            Description = TablesLocalizer["OnEditAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnSaveAsync",
            Description = TablesLocalizer["OnSaveAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnResetSearchAsync",
            Description = TablesLocalizer["OnResetSearchAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClickRowCallback",
            Description = TablesLocalizer["OnClickRowCallbackAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnAfterSaveAsync",
            Description = TablesLocalizer["OnAfterSaveAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.OnAfterRenderCallback),
            Description = TablesLocalizer["OnAfterRenderCallbackAttr"],
            Type = "Func<Table<TItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnTreeExpand",
            Description = TablesLocalizer["OnTreeExpandAttr"],
            Type = "Func<TItem, Task<IEnumerable<TItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnDoubleClickRowCallback",
            Description = TablesLocalizer["OnDoubleClickRowCallbackAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SortIcon",
            Description = TablesLocalizer["SortIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa fa-sort"
        },
        new()
        {
            Name = "SortIconAsc",
            Description = TablesLocalizer["SortIconAscAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa fa-sort-asc"
        },
        new()
        {
            Name = "SortIconDesc",
            Description = TablesLocalizer["SortIconDescAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa fa-sort-desc"
        },
        new()
        {
            Name = "EditDialogSaveButtonText",
            Description = TablesLocalizer["EditDialogSaveButtonTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.EditDialogIsDraggable),
            Description = TablesLocalizer["EditDialogIsDraggableAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.EditDialogShowMaximizeButton),
            Description = TablesLocalizer["EditDialogShowMaximizeButtonAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "EditDialogSize",
            Description = TablesLocalizer["EditDialogSizeAttr"],
            Type = "Size",
            ValueList = " — ",
            DefaultValue = "Large"
        },
        new()
        {
            Name = "ExportButtonDropdownTemplate",
            Description = TablesLocalizer["ExportButtonDropdownTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.SearchDialogIsDraggable),
            Description = TablesLocalizer["SearchDialogIsDraggableAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.SearchDialogShowMaximizeButton),
            Description = TablesLocalizer["SearchDialogShowMaximizeButtonAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "SearchDialogSize",
            Description = TablesLocalizer["SearchDialogSizeAttr"],
            Type = "Size",
            ValueList = " — ",
            DefaultValue = "Large"
        },
        new()
        {
            Name = "AddModalTitle",
            Description = TablesLocalizer["AddModalTitleAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditModalTitle",
            Description = TablesLocalizer["EditModalTitleAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "UnsetText",
            Description = TablesLocalizer["UnsetTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = TablesLocalizer["UnsetTextValue"]
        },
        new()
        {
            Name = "SortAscText",
            Description = TablesLocalizer["SortAscTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = TablesLocalizer["SortAscTextValue"]
        },
        new()
        {
            Name = "SortDescText",
            Description = TablesLocalizer["SortDescTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = TablesLocalizer["SortDescTextValue"]
        },
        new()
        {
            Name = "RenderMode",
            Description = TablesLocalizer["RenderModeAttr"],
            Type = "TableRenderMode",
            ValueList = "Auto|Table|CardView",
            DefaultValue = "Auto"
        },
        new()
        {
            Name = "EmptyText",
            Description = TablesLocalizer["EmptyTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<MethodItem>.EmptyImage),
            Description = TablesLocalizer["EmptyImageAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EmptyTemplate",
            Description = TablesLocalizer["EmptyTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditDialogItemsPerRow",
            Description = TablesLocalizer["EditDialogItemsPerRowAttr"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditDialogRowType",
            Description = TablesLocalizer["EditDialogRowTypeAttr"],
            Type = "RowType",
            ValueList = "Row|Inline",
            DefaultValue = "Row"
        },
        new()
        {
            Name = "EditDialogLabelAlign",
            Description = TablesLocalizer["EditDialogLabelAlignAttr"],
            Type = "Alignment",
            ValueList = "None|Left|Center|Right",
            DefaultValue = "None"
        }
    };

    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new()
        {
            Name = nameof(Table<MethodItem>.AddAsync),
            Description = TablesLocalizer["AddAsyncMethod"],
            Parameters = " — ",
            ReturnValue = "Task"
        },
        new()
        {
            Name = nameof(Table<MethodItem>.EditAsync),
            Description = TablesLocalizer["EditAsyncMethod"],
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Table<MethodItem>.QueryAsync),
            Description = TablesLocalizer["QueryAsyncMethod"],
            Parameters = " — ",
            ReturnValue = "Task"
        }
    };
}
