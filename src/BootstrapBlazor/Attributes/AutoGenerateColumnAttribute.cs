// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoGenerateColumn attribute class, used to mark auto-generated columns in <see cref="Table{TItem}"/>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class AutoGenerateColumnAttribute : AutoGenerateBaseAttribute, ITableColumn
{
    /// <summary>
    /// Gets or sets the display order. The rules are as follows:
    /// <para></para>
    /// &gt;0 for the front, 1,2,3...
    /// <para></para>
    /// =0 for the middle (default)
    /// <para></para>
    /// &lt;0 for the back, ...-3,-2,-1
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool DefaultSort { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// Gets or sets whether the column is read-only when adding a new item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.
    /// </summary>
    public bool IsReadonlyWhenAdd { get; set; }

    bool? ITableColumn.IsReadonlyWhenAdd
    {
        get => IsReadonlyWhenAdd;
        set => IsReadonlyWhenAdd = value ?? false;
    }

    /// <summary>
    /// Gets or sets whether the column is read-only when editing an item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.
    /// </summary>
    public bool IsReadonlyWhenEdit { get; set; }

    bool? ITableColumn.IsReadonlyWhenEdit
    {
        get => IsReadonlyWhenEdit;
        set => IsReadonlyWhenEdit = value ?? false;
    }

    /// <summary>
    /// Gets or sets whether the column is visible when adding a new item. Default is null, using the <see cref="AutoGenerateBaseAttribute.Visible"/> value.
    /// </summary>
    public bool IsVisibleWhenAdd { get; set; } = true;

    bool? ITableColumn.IsVisibleWhenAdd
    {
        get => IsVisibleWhenAdd;
        set => IsVisibleWhenAdd = value ?? true;
    }

    /// <summary>
    /// Gets or sets whether the column is visible when editing an item. Default is null, using the <see cref="AutoGenerateBaseAttribute.Visible"/> value.
    /// </summary>
    public bool IsVisibleWhenEdit { get; set; } = true;

    Func<ITableColumn, string?, SearchFilterAction>? ITableColumn.CustomSearch { get; set; }

    bool? ITableColumn.IsVisibleWhenEdit
    {
        get => IsVisibleWhenEdit;
        set => IsVisibleWhenEdit = value ?? true;
    }

    bool? IEditorItem.Required { get; set; }

    bool? ITableColumn.IsRequiredWhenAdd { get; set; }

    bool? ITableColumn.IsRequiredWhenEdit { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets whether to show label tooltip. Mostly used when the label text is too long and gets truncated. Default is false.
    /// </summary>
    public bool ShowLabelTooltip { get; set; }

    bool? IEditorItem.ShowLabelTooltip
    {
        get => ShowLabelTooltip;
        set => ShowLabelTooltip = value ?? false;
    }

    /// <summary>
    /// Gets or sets the default sort order. Default is SortOrder.Unset.
    /// </summary>
    public SortOrder DefaultSortOrder { get; set; }

    IEnumerable<SelectedItem>? IEditorItem.Items { get; set; }

    /// <summary>
    /// Gets or sets the column width.
    /// </summary>
    public int Width { get; set; }

    int? ITableColumn.Width
    {
        get => Width <= 0 ? null : Width;
        set => Width = value ?? 0;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Fixed { get; set; }

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
    public string? FormatString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<object?, Task<string?>>? Formatter { get; set; }

    RenderFragment<object>? IEditorItem.EditTemplate { get; set; }

    RenderFragment<ITableColumn>? ITableColumn.HeaderTemplate { get; set; }

    RenderFragment<ITableColumn>? ITableColumn.ToolboxTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Type? ComponentType { get; set; }

    IEnumerable<KeyValuePair<string, object>>? IEditorItem.ComponentParameters { get; set; }

    RenderFragment<object>? ITableColumn.Template { get; set; }

    RenderFragment<object>? ITableColumn.SearchTemplate { get; set; }

    RenderFragment? ITableColumn.FilterTemplate { get; set; }

    Func<object?, Task<string?>>? ITableColumn.GetTooltipTextCallback { get; set; }

    bool? ITableColumn.Searchable { get => Searchable; set => Searchable = value ?? false; }

    bool? ITableColumn.Filterable { get => Filterable; set => Filterable = value ?? false; }

    bool? ITableColumn.Sortable { get => Sortable; set => Sortable = value ?? false; }

    bool? ITableColumn.TextWrap { get => TextWrap; set => TextWrap = value ?? false; }

    bool? ITableColumn.TextEllipsis { get => TextEllipsis; set => TextEllipsis = value ?? false; }

    bool? IEditorItem.Ignore { get => Ignore; set => Ignore = value ?? false; }

    bool? IEditorItem.Readonly { get => Readonly; set => Readonly = value ?? false; }

    bool? ITableColumn.Visible { get => Visible; set => Visible = value ?? true; }

    bool? ITableColumn.ShowTips { get => ShowTips; set => ShowTips = value ?? false; }

    bool? ITableColumn.ShowCopyColumn { get => ShowCopyColumn; set => ShowCopyColumn = value ?? false; }

    Alignment? ITableColumn.Align { get => Align; set => Align = value ?? Alignment.None; }

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

    IFilter? ITableColumn.Filter { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; internal set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    internal string? FieldName { get; set; }

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
    /// <inheritdoc/>
    /// </summary>
    IEnumerable<SelectedItem>? ILookup.Lookup { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    ILookupService? ILookup.LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    Action<TableCellArgs>? ITableColumn.OnCellRender { get; set; }

    List<IValidator>? IEditorItem.ValidateRules { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual string GetDisplayName() => Text ?? "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string GetFieldName() => FieldName;

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
}
