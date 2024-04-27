// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 表格示例代码
/// </summary>
public partial class Tables
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private string? RefreshText { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        //获取随机数据
        //Get random data
        Items = Foo.GenerateFoo(FooLocalizer);

        RefreshText ??= Localizer["TableBaseNormalRefreshText"];
    }

    private void OnClick()
    {
        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private AttributeItem[] GetTableColumnAttributes() =>
    [
        new()
        {
            Name = "TextWrap",
            Description = Localizer["TextWrapAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "AutoGenerateColumns",
            Description = Localizer["AutoGenerateColumnsAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "CssClass",
            Description = Localizer["CssClassAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Editable",
            Description = Localizer["EditableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "EditTemplate",
            Description = Localizer["EditTemplateColumnAttr"],
            Type = "RenderFragment<object>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Filterable",
            Description = Localizer["FilterableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "FilterTemplate",
            Description = Localizer["FilterTemplateAttr"],
            Type = "RenderFragment?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Filter",
            Description = Localizer["FilterAttr"],
            Type = "IFilter?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["HeaderTemplateAttr"],
            Type = "RenderFragment<ITableColumn>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(IEditorItem.Lookup),
            Description = Localizer["LookupAttr"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(IEditorItem.LookupStringComparison),
            Description = Localizer["LookupStringComparisonAttr"],
            Type = "StringComparison",
            ValueList = " — ",
            DefaultValue = "OrdinalIgnoreCase"
        },
        new()
        {
            Name = nameof(IEditorItem.LookupServiceKey),
            Description = Localizer["LookupServiceKeyAttr"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(IEditorItem.LookupServiceData),
            Description = Localizer["LookupServiceDataAttr"],
            Type = "object?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Readonly",
            Description = Localizer["ReadonlyAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "SearchTemplate",
            Description = Localizer["SearchTemplateColumnAttr"],
            Type = "RenderFragment<object>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowTips",
            Description = Localizer["ShowTipsAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Searchable",
            Description = Localizer["SearchableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Sortable",
            Description = Localizer["SortableAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DefaultSort",
            Description = Localizer["DefaultSortAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DefaultSortOrder",
            Description = Localizer["DefaultSortOrderAttr"],
            Type = "SortOrder",
            ValueList = "Unset|Asc|Desc",
            DefaultValue = "Unset"
        },
        new()
        {
            Name = "ShowAdvancedSort",
            Description = Localizer["ShowAdvancedSortAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Text",
            Description = Localizer["TextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TextEllipsis",
            Description = Localizer["TextEllipsisAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Template",
            Description = Localizer["TemplateAttr"],
            Type = "RenderFragment<TableColumnContext<object, TItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Visible",
            Description = Localizer["VisibleAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsVisibleWhenAdd",
            Description = Localizer["IsVisibleWhenAddAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsVisibleWhenEdit",
            Description = Localizer["IsVisibleWhenEditAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Width",
            Description = Localizer["WidthAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Fixed",
            Description = Localizer["FixedAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(TableColumn<Foo, string>.GroupName),
            Description = Localizer["GroupNameAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(TableColumn<Foo, string>.GroupOrder),
            Description = Localizer["GroupOrderAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShownWithBreakPoint",
            Description = Localizer["ShownWithBreakPointAttr"],
            Type = "BreakPoint",
            ValueList = "None|ExtraSmall|...",
            DefaultValue = "None"
        },
        new()
        {
            Name = "FormatString",
            Description = Localizer["FormatStringAttr"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Formatter",
            Description = Localizer["FormatterAttr"],
            Type = "Func<object?, Task<string>>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Align",
            Description = Localizer["AlignAttr"],
            Type = "Alignment",
            ValueList = "None|Left|Center|Right",
            DefaultValue = "None"
        },
        new()
        {
            Name = "Order",
            Description = Localizer["OrderAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnCellRender",
            Description = Localizer["OnCellRenderAttr"],
            Type = "Action<TableCellArgs>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsMarkupString",
            Description = Localizer["IsMarkupStringAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    ];

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "TableSize",
            Description = Localizer["TableSizeAttr"],
            Type = "TableSize",
            ValueList = "Normal|Compact",
            DefaultValue = "Normal"
        },
        new()
        {
            Name = "HeaderStyle",
            Description = Localizer["HeaderStyleAttr"],
            Type = "TableHeaderStyle",
            ValueList = "None|Light|Dark",
            DefaultValue = "None"
        },
        new()
        {
            Name = "HeaderTextWrap",
            Description = Localizer["HeaderTextWrap"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Height",
            Description = Localizer["HeightAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PageItems",
            Description = Localizer["PageItemsAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "AutoRefreshInterval",
            Description = Localizer["AutoRefreshIntervalAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2000"
        },
        new()
        {
            Name = "ExtendButtonColumnWidth",
            Description = Localizer["ExtendButtonColumnWidthAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "130"
        },
        new()
        {
            Name = "RenderModeResponsiveWidth",
            Description = Localizer["RenderModeResponsiveWidthAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "768"
        },
        new()
        {
            Name = "IndentSize",
            Description = Localizer["IndentSizeAttr"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "16"
        },
        new()
        {
            Name = "Items",
            Description = Localizer["ItemsAttr"],
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PageItemsSource",
            Description = Localizer["PageItemsSourceAttr"],
            Type = "IEnumerable<int>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditMode",
            Description = Localizer["EditModeAttr"],
            Type = "EditMode",
            ValueList = "Popup|Inline|InCell",
            DefaultValue = "Popup"
        },
        new()
        {
            Name = "MultiHeaderTemplate",
            Description = Localizer["MultiHeaderTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TableFooter",
            Description = Localizer["TableFooterAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TableToolbarTemplate",
            Description = Localizer["TableToolbarTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditTemplate",
            Description = Localizer["EditTemplateAttr"],
            Type = "RenderFragment<TItem?>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowAdvancedSearch",
            Description = Localizer["ShowAdvancedSearchAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "SearchTemplate",
            Description = Localizer["SearchTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BeforeRowButtonTemplate",
            Description = Localizer["BeforeRowButtonTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "RowButtonTemplate",
            Description = Localizer["RowButtonTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "DetailRowTemplate",
            Description = Localizer["DetailRowTemplateAttr"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsAutoCollapsedToolbarButton",
            Description = Localizer["IsAutoCollapsedToolbarButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsBordered",
            Description = Localizer["IsBorderedAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsPagination",
            Description = Localizer["IsPaginationAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowGotoNavigator",
            Description = Localizer["ShowGotoNavigatorAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "GotoTemplate",
            Description = Localizer["GotoTemplateAttr"],
            Type = "RenderFragment?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "GotoNavigatorLabelText",
            Description = Localizer["GotoNavigatorLabelTextAttr"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowPageInfo",
            Description = Localizer["ShowPageInfoAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "PageInfoTemplate",
            Description = Localizer["PageInfoTemplateAttr"],
            Type = "RenderFragment?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PageInfoText",
            Description = Localizer["PageInfoTextAttr"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsStriped",
            Description = Localizer["IsStripedAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsRendered",
            Description = Localizer["IsRenderedAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsMultipleSelect",
            Description = Localizer["IsMultipleSelectAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsAutoRefresh",
            Description = Localizer["IsAutoRefreshAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsTree",
            Description = Localizer["IsTreeAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsDetails",
            Description = Localizer["IsDetailsAttr"],
            Type = "boolean",
            ValueList = "true / false / null",
            DefaultValue = "null"
        },
        new()
        {
            Name = nameof(Table<Foo>.IsHideFooterWhenNoData),
            Description = Localizer["IsHideFooterWhenNoDataAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ClickToSelect",
            Description = Localizer["ClickToSelectAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowCheckboxText",
            Description = Localizer["ShowCheckboxTextAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowFooter",
            Description = Localizer["ShowFooterAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowFilterHeader),
            Description = Localizer["ShowFilterHeaderAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowMultiFilterHeader),
            Description = Localizer["ShowMultiFilterHeaderAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSearch",
            Description = Localizer["ShowSearchAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSearchText",
            Description = Localizer["ShowSearchTextAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowSearchTextTooltip",
            Description = Localizer["ShowSearchTextTooltipAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowResetButton",
            Description = Localizer["ShowResetButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowSearchButton",
            Description = Localizer["ShowSearchButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "SearchMode",
            Description = Localizer["SearchModeAttr"],
            Type = "SearchMode",
            ValueList = "Popup / Top",
            DefaultValue = "Popup"
        },
        new()
        {
            Name = nameof(Table<Foo>.CollapsedTopSearch),
            Description = Localizer["CollapsedTopSearchAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowToolbar",
            Description = Localizer["ShowToolbarAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowLineNo",
            Description = Localizer["ShowLineNoAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowDefaultButtons",
            Description = Localizer["ShowDefaultButtonsAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowAddButton",
            Description = Localizer["ShowAddButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowEditButton",
            Description = Localizer["ShowEditButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowEditButtonCallback",
            Description = Localizer["ShowEditButtonCallbackAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowDeleteButton",
            Description = Localizer["ShowDeleteButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowDeleteButtonCallback",
            Description = Localizer["ShowDeleteButtonCallbackAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowExtendButtons",
            Description = Localizer["ShowExtendButtonsAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowExtendEditButton),
            Description = Localizer["ShowExtendEditButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowExtendEditButtonCallback),
            Description = Localizer["ShowExtendEditButtonCallbackAttr"],
            Type = "Func<TItem, bool>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = nameof(Table<Foo>.DisableExtendEditButton),
            Description = Localizer["ShowExtendEditButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Table<Foo>.DisableExtendEditButtonCallback),
            Description = Localizer["DisableExtendEditButtonCallbackAttr"],
            Type = "Func<TItem, bool>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowExtendDeleteButton),
            Description = Localizer["ShowExtendDeleteButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowExtendDeleteButtonCallback),
            Description = Localizer["ShowExtendDeleteButtonCallbackAttr"],
            Type = "Func<TItem, bool>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = nameof(Table<Foo>.DisableExtendDeleteButton),
            Description = Localizer["ShowExtendDeleteButtonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Table<Foo>.DisableExtendDeleteButtonCallback),
            Description = Localizer["DisableExtendDeleteButtonCallbackAttr"],
            Type = "Func<TItem, bool>",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = nameof(Table<Foo>.ShowUnsetGroupItemsOnTop),
            Description = Localizer["ShowUnsetGroupItemsOnTopAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSkeleton",
            Description = Localizer["ShowSkeletonAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowLoadingInFirstRender",
            Description = Localizer["ShowLoadingInFirstRenderAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowColumnList",
            Description = Localizer["ShowColumnListAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnColumnVisibleChanged",
            Description = Localizer["OnColumnVisibleChangedAttr"],
            Type = "Func<string, bool>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowEmpty",
            Description = Localizer["ShowEmptyAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowToastAfterSaveOrDeleteModel",
            Description = Localizer["ShowToastAfterSaveOrDeleteModelAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowToastBeforeExport",
            Description = Localizer["ShowToastBeforeExport"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowToastAfterExport",
            Description = Localizer["ShowToastAfterExport"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "BeforeExportCallback",
            Description = Localizer["BeforeExportCallback"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "AfterExportCallback",
            Description = Localizer["AfterExportCallback"],
            Type = "Func<bool, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TreeIcon",
            Description = Localizer["TreeIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-caret-right"
        },
        new()
        {
            Name = "ScrollingDialogContent",
            Description = Localizer["ScrollingDialogContentAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "FixedExtendButtonsColumn",
            Description = Localizer["FixedExtendButtonsColumnAttr"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnQueryAsync",
            Description = Localizer["OnQueryAsyncAttr"],
            Type = "Func<QueryPageOptions, Task<QueryData<TItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnAddAsync",
            Description = Localizer["OnAddAsyncAttr"],
            Type = "Func<Task<TItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.OnColumnCreating),
            Description = Localizer["OnColumnCreatingAttr"],
            Type = "Func<List<ITableColumn>,Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.ColumnOrderCallback),
            Description = Localizer["ColumnOrderCallbackAttr"],
            Type = "Func<List<ITableColumn>, IEnumerable<ITableColumn>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.OnDoubleClickCellCallback),
            Description = Localizer["OnDoubleClickCellCallbackAttr"],
            Type = "Func<string, object, object?, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnDeleteAsync",
            Description = Localizer["OnDeleteAsyncAttr"],
            Type = "Func<IEnumerable<TItem>, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnEditAsync",
            Description = Localizer["OnEditAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnSaveAsync",
            Description = Localizer["OnSaveAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnResetSearchAsync",
            Description = Localizer["OnResetSearchAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClickRowCallback",
            Description = Localizer["OnClickRowCallbackAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnAfterSaveAsync",
            Description = Localizer["OnAfterSaveAsyncAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnAfterDeleteAsync",
            Description = Localizer["OnAfterDeleteAsyncAttr"],
            Type = "Func<List<TItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnAfterModifyAsync",
            Description = Localizer["OnAfterModifyAsyncAttr"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.OnAfterRenderCallback),
            Description = Localizer["OnAfterRenderCallbackAttr"],
            Type = "Func<Table<TItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnTreeExpand",
            Description = Localizer["OnTreeExpandAttr"],
            Type = "Func<TItem, Task<IEnumerable<TItem>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnDoubleClickRowCallback",
            Description = Localizer["OnDoubleClickRowCallbackAttr"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SortIcon",
            Description = Localizer["SortIconAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-sort"
        },
        new()
        {
            Name = "SortIconAsc",
            Description = Localizer["SortIconAscAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-sort-up"
        },
        new()
        {
            Name = "SortIconDesc",
            Description = Localizer["SortIconDescAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-sort-down"
        },
        new()
        {
            Name = "EditDialogSaveButtonText",
            Description = Localizer["EditDialogSaveButtonTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.EditDialogIsDraggable),
            Description = Localizer["EditDialogIsDraggableAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.EditDialogShowMaximizeButton),
            Description = Localizer["EditDialogShowMaximizeButtonAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "EditDialogSize",
            Description = Localizer["EditDialogSizeAttr"],
            Type = "Size",
            ValueList = " — ",
            DefaultValue = "Large"
        },
        new()
        {
            Name = "ExportButtonDropdownTemplate",
            Description = Localizer["ExportButtonDropdownTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<Foo>.SearchDialogIsDraggable),
            Description = Localizer["SearchDialogIsDraggableAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Table<Foo>.SearchDialogShowMaximizeButton),
            Description = Localizer["SearchDialogShowMaximizeButtonAttr"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "SearchDialogSize",
            Description = Localizer["SearchDialogSizeAttr"],
            Type = "Size",
            ValueList = " — ",
            DefaultValue = "Large"
        },
        new()
        {
            Name = "AddModalTitle",
            Description = Localizer["AddModalTitleAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditModalTitle",
            Description = Localizer["EditModalTitleAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "UnsetText",
            Description = Localizer["UnsetTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["UnsetTextValue"]
        },
        new()
        {
            Name = "SortAscText",
            Description = Localizer["SortAscTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SortAscTextValue"]
        },
        new()
        {
            Name = "SortDescText",
            Description = Localizer["SortDescTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SortDescTextValue"]
        },
        new()
        {
            Name = "RenderMode",
            Description = Localizer["RenderModeAttr"],
            Type = "TableRenderMode",
            ValueList = "Auto|Table|CardView",
            DefaultValue = "Auto"
        },
        new()
        {
            Name = "EmptyText",
            Description = Localizer["EmptyTextAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Table<MethodItem>.EmptyImage),
            Description = Localizer["EmptyImageAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EmptyTemplate",
            Description = Localizer["EmptyTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditDialogItemsPerRow",
            Description = Localizer["EditDialogItemsPerRowAttr"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "EditDialogRowType",
            Description = Localizer["EditDialogRowTypeAttr"],
            Type = "RowType",
            ValueList = "Row|Inline",
            DefaultValue = "Row"
        },
        new()
        {
            Name = "EditDialogLabelAlign",
            Description = Localizer["EditDialogLabelAlignAttr"],
            Type = "Alignment",
            ValueList = "None|Left|Center|Right",
            DefaultValue = "None"
        },
        new()
        {
            Name = "AllowDragColumn",
            Description = Localizer["AllowDragOrderAttr"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
    ];

    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = nameof(Table<MethodItem>.AddAsync),
            Description = Localizer["AddAsyncMethod"],
            Parameters = " — ",
            ReturnValue = "Task"
        },
        new()
        {
            Name = nameof(Table<MethodItem>.EditAsync),
            Description = Localizer["EditAsyncMethod"],
            Parameters = " — ",
            ReturnValue = " — "
        },
        new()
        {
            Name = nameof(Table<MethodItem>.QueryAsync),
            Description = Localizer["QueryAsyncMethod"],
            Parameters = " — ",
            ReturnValue = "Task"
        }
    ];
}
