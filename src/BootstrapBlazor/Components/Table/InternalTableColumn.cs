// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class InternalTableColumn(string fieldName, Type fieldType, string? fieldText = null) : ITableColumn
{
    private string FieldName { get; } = fieldName;

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Sortable"/>
    /// </summary>
    public bool? Sortable { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.DefaultSort"/>
    /// </summary>
    public bool DefaultSort { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.DefaultSortOrder"/>
    /// </summary>
    public SortOrder DefaultSortOrder { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Filterable"/>
    /// </summary>
    public bool? Filterable { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Searchable"/>
    /// </summary>
    public bool? Searchable { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Width"/>
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Fixed"/>
    /// </summary>
    public bool Fixed { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.TextWrap"/>
    /// </summary>
    public bool? TextWrap { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.TextEllipsis"/>
    /// </summary>
    public bool? TextEllipsis { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.SkipValidate"/>
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Ignore"/>
    /// </summary>
    public bool? Ignore { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Readonly"/>
    /// </summary>
    public bool? Readonly { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IsReadonlyWhenAdd"/>
    /// </summary>
    public bool? IsReadonlyWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IsReadonlyWhenEdit"/>
    /// </summary>
    public bool? IsReadonlyWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Visible"/>
    /// </summary>
    public bool? Visible { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IsVisibleWhenAdd"/>
    /// </summary>
    public bool? IsVisibleWhenAdd { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IsVisibleWhenEdit"/>
    /// </summary>
    public bool? IsVisibleWhenEdit { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Required"/>
    /// </summary>
    public bool? Required { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IsRequiredWhenAdd"/>
    /// </summary>
    public bool? IsRequiredWhenAdd { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IsRequiredWhenEdit"/>
    /// </summary>
    public bool? IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.RequiredErrorMessage"/>
    /// </summary>
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ShowLabelTooltip"/>
    /// </summary>
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.CssClass"/>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.ShownWithBreakPoint"/>
    /// </summary>
    public BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Template"/>
    /// </summary>
    public RenderFragment<object>? Template { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.SearchTemplate"/>
    /// </summary>
    public RenderFragment<object>? SearchTemplate { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.FilterTemplate"/>
    /// </summary>
    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.HeaderTemplate"/>
    /// </summary>
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.ToolboxTemplate"/>
    /// </summary>
    public RenderFragment<ITableColumn>? ToolboxTemplate { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Filter"/>
    /// </summary>
    public IFilter? Filter { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.FormatString"/>
    /// </summary>
    public string? FormatString { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.PlaceHolder"/>
    /// </summary>
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Formatter"/>
    /// </summary>
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.Align"/>
    /// </summary>
    public Alignment? Align { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.ShowTips"/>
    /// </summary>
    public bool? ShowTips { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.GetTooltipTextCallback"/>
    /// </summary>
    public Func<object?, Task<string?>>? GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.PropertyType"/>
    /// </summary>
    public Type PropertyType { get; } = fieldType;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Editable"/>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Step"/>
    /// </summary>
    public string? Step { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Rows"/>
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Cols"/>
    /// </summary>
    public int Cols { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Text"/>
    /// </summary>
    [NotNull]
    public string? Text { get; set; } = fieldText;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.EditTemplate"/>
    /// </summary>
    public RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ComponentType"/>
    /// </summary>
    public Type? ComponentType { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ComponentParameters"/>
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Items"/>
    /// </summary>
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.Order"/>
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.Lookup"/>
    /// </summary>
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ShowSearchWhenSelect"/>
    /// </summary>
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.IsFixedSearchWhenSelect"/>
    /// </summary>
    [Obsolete("已弃用，请删除；Deprecated, please delete")]
    [ExcludeFromCodeCoverage]
    public bool IsFixedSearchWhenSelect { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.IsPopover"/>
    /// </summary>
    public bool IsPopover { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupStringComparison"/>
    /// </summary>
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceKey"/>
    /// </summary>
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupServiceData"/>
    /// </summary>
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc cref="ILookup.LookupService"/>
    /// </summary>
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.OnCellRender"/>
    /// </summary>
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.ValidateRules"/>
    /// </summary>
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GroupName"/>
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GroupOrder"/>
    /// </summary>
    public int GroupOrder { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.ShowCopyColumn"/>
    /// </summary>
    public bool? ShowCopyColumn { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.HeaderTextWrap"/>
    /// </summary>
    public bool HeaderTextWrap { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.ShowHeaderTooltip"/>
    /// </summary>
    public bool ShowHeaderTooltip { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.HeaderTextTooltip"/>
    /// </summary>
    public string? HeaderTextTooltip { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.HeaderTextEllipsis"/>
    /// </summary>
    public bool HeaderTextEllipsis { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IsMarkupString"/>
    /// </summary>
    public bool IsMarkupString { get; set; }

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GetDisplayName"/>
    /// </summary>
    public string GetDisplayName() => Text;

    /// <summary>
    /// <inheritdoc cref="IEditorItem.GetFieldName"/>
    /// </summary>
    public string GetFieldName() => FieldName;

    /// <summary>
    /// <inheritdoc cref="ITableColumn.CustomSearch"/>
    /// </summary>
    public Func<ITableColumn, string?, SearchFilterAction>? CustomSearch { get; set; }

    /// <summary>
    /// <inheritdoc cref="ITableColumn.IgnoreWhenExport"/>
    /// </summary>
    public bool? IgnoreWhenExport { get; set; }
}
