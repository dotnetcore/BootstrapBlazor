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

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? Readonly { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? Visible { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsVisibleWhenAdd { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsVisibleWhenEdit { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? Required { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public RenderFragment<object>? Template { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public RenderFragment<object>? SearchTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IFilter? Filter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? FormatString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Alignment? Align { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? ShowTips { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Type PropertyType { get; } = fieldType;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Step { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Cols { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public string? Text { get; set; } = fieldText;

    public RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int GroupOrder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool HeaderTextWrap { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string GetDisplayName() => Text;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string GetFieldName() => FieldName;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }
}
