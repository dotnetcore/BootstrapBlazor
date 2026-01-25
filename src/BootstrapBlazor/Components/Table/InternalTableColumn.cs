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
    /// <para lang="zh">获得/设置 是否不进行验证 默认为 false</para>
    /// <para lang="en">Gets or sets whether不进行验证 Default is为 false</para>
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? Ignore { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? Readonly { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? Visible { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? IsVisibleWhenAdd { get; set; } = true;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? IsVisibleWhenEdit { get; set; } = true;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? Required { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public RenderFragment<object>? Template { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public RenderFragment<object>? SearchTemplate { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public IFilter? Filter { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? FormatString { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Alignment? Align { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? ShowTips { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Type PropertyType { get; } = fieldType;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? Step { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public int Cols { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [NotNull]
    public string? Text { get; set; } = fieldText;

    public RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool IsPopover { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public int GroupOrder { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool HeaderTextWrap { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string GetDisplayName() => Text;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string GetFieldName() => FieldName;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool? IgnoreWhenExport { get; set; }
}
