// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class InternalTableColumn(string fieldName, Type fieldType, string? fieldText = null) : ITableColumn
{
    private string FieldName { get; } = fieldName;

    public bool? Sortable { get; set; }

    public bool DefaultSort { get; set; }

    public SortOrder DefaultSortOrder { get; set; }

    public bool? Filterable { get; set; }

    public bool? Searchable { get; set; }

    public int? Width { get; set; }

    public bool Fixed { get; set; }

    public bool? TextWrap { get; set; }

    public bool? TextEllipsis { get; set; }

    public bool SkipValidate { get; set; }

    public bool? Ignore { get; set; }

    public bool? Readonly { get; set; }

    public bool? IsReadonlyWhenAdd { get; set; }

    public bool? IsReadonlyWhenEdit { get; set; }

    public bool? Visible { get; set; }

    public bool? IsVisibleWhenAdd { get; set; } = true;

    public bool? IsVisibleWhenEdit { get; set; } = true;

    public bool? Required { get; set; }

    public bool? IsRequiredWhenAdd { get; set; }

    public bool? IsRequiredWhenEdit { get; set; }

    public string? RequiredErrorMessage { get; set; }

    public bool? ShowLabelTooltip { get; set; }

    public string? CssClass { get; set; }

    public BreakPoint ShownWithBreakPoint { get; set; }

    public RenderFragment<object>? Template { get; set; }

    public RenderFragment<object>? SearchTemplate { get; set; }

    public RenderFragment? FilterTemplate { get; set; }

    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    public RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    public IFilter? Filter { get; set; }

    public string? FormatString { get; set; }

    public string? PlaceHolder { get; set; }

    public Func<object?, Task<string?>>? Formatter { get; set; }

    public Alignment? Align { get; set; }

    public bool? ShowTips { get; set; }

    public Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    public Type PropertyType { get; } = fieldType;

    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    public string? Step { get; set; }

    public int Rows { get; set; }

    public int Cols { get; set; }

    [NotNull]
    public string? Text { get; set; } = fieldText;

    public RenderFragment<object>? EditTemplate { get; set; }

    public Type? ComponentType { get; set; }

    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    public IEnumerable<SelectedItem>? Items { get; set; }

    public int Order { get; set; }

    public IEnumerable<SelectedItem>? Lookup { get; set; }

    public bool ShowSearchWhenSelect { get; set; }

    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    public bool IsPopover { get; set; }

    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    public string? LookupServiceKey { get; set; }

    public object? LookupServiceData { get; set; }

    public ILookupService? LookupService { get; set; }

    public Action<TableCellArgs>? OnCellRender { get; set; }

    public List<IValidator>? ValidateRules { get; set; }

    public string? GroupName { get; set; }

    public int GroupOrder { get; set; }

    public bool? ShowCopyColumn { get; set; }

    public bool HeaderTextWrap { get; set; }

    public bool ShowHeaderTooltip { get; set; }

    public string? HeaderTextTooltip { get; set; }

    public bool HeaderTextEllipsis { get; set; }

    public bool IsMarkupString { get; set; }

    public string GetDisplayName() => Text;

    public string GetFieldName() => FieldName;

    public Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }

    public bool? IgnoreWhenExport { get; set; }
}
