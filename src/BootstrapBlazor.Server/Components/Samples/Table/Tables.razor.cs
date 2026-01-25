// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    private bool _isCompact = true;

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
}
