// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">AutoGenerateColumn 属性类，用于在 <see cref="Table{TItem}"/> 中标记自动生成的列</para>
/// <para lang="en">AutoGenerateColumn attribute class, used to mark auto-generated columns in <see cref="Table{TItem}"/></para>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class AutoGenerateColumnAttribute : AutoGenerateBaseAttribute, ITableColumn
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示顺序。规则如下：</para>
    /// <para lang="en">Gets or sets the display order. The rules are as follows:</para>
    /// <para lang="zh">&gt;0 正序排列，1,2,3...</para>
    /// <para lang="en">&gt;0 for the front, 1,2,3...</para>
    /// <para lang="zh">=0 保持默认</para>
    /// <para lang="en">=0 for the middle (default)</para>
    /// <para lang="zh">&lt;0 倒序排列，...-3,-2,-1</para>
    /// <para lang="en">&lt;0 for the back, ...-3,-2,-1</para>
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool DefaultSort { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 新建时此列是否只读。默认值为 null，使用 <see cref="IEditorItem.Readonly"/> 值。</para>
    /// <para lang="en">Gets or sets whether the column is read-only when adding a new item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.</para>
    /// </summary>
    public bool IsReadonlyWhenAdd { get; set; }

    bool? ITableColumn.IsReadonlyWhenAdd
    {
        get => IsReadonlyWhenAdd;
        set => IsReadonlyWhenAdd = value ?? false;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑时此列是否只读。默认值为 null，使用 <see cref="IEditorItem.Readonly"/> 值。</para>
    /// <para lang="en">Gets or sets whether the column is read-only when editing an item. Default is null, using the <see cref="IEditorItem.Readonly"/> value.</para>
    /// </summary>
    public bool IsReadonlyWhenEdit { get; set; }

    bool? ITableColumn.IsReadonlyWhenEdit
    {
        get => IsReadonlyWhenEdit;
        set => IsReadonlyWhenEdit = value ?? false;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 新建时此列是否可见。默认值为 null，使用 <see cref="AutoGenerateBaseAttribute.Visible"/> 值。</para>
    /// <para lang="en">Gets or sets whether the column is visible when adding a new item. Default is null, using the <see cref="AutoGenerateBaseAttribute.Visible"/> value.</para>
    /// </summary>
    public bool IsVisibleWhenAdd { get; set; } = true;

    bool? ITableColumn.IsVisibleWhenAdd
    {
        get => IsVisibleWhenAdd;
        set => IsVisibleWhenAdd = value ?? true;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 编辑时此列是否可见。默认值为 null，使用 <see cref="AutoGenerateBaseAttribute.Visible"/> 值。</para>
    /// <para lang="en">Gets or sets whether the column is visible when editing an item. Default is null, using the <see cref="AutoGenerateBaseAttribute.Visible"/> value.</para>
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
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标签工具提示。通常用于标签文本过长被截断时。默认为 false。</para>
    /// <para lang="en">Gets or sets whether to show label tooltip. Mostly used when the label text is too long and gets truncated. Default is false.</para>
    /// </summary>
    public bool ShowLabelTooltip { get; set; }

    bool? IEditorItem.ShowLabelTooltip
    {
        get => ShowLabelTooltip;
        set => ShowLabelTooltip = value ?? false;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 默认排序顺序。默认为 SortOrder.Unset。</para>
    /// <para lang="en">Gets or sets the default sort order. Default is SortOrder.Unset.</para>
    /// </summary>
    public SortOrder DefaultSortOrder { get; set; }

    IEnumerable<SelectedItem>? IEditorItem.Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列宽。</para>
    /// <para lang="en">Gets or sets the column width.</para>
    /// </summary>
    public int Width { get; set; }

    int? ITableColumn.Width
    {
        get => Width <= 0 ? null : Width;
        set => Width = value ?? 0;
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool Fixed { get; set; }

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

    RenderFragment<object>? IEditorItem.EditTemplate { get; set; }

    RenderFragment<ITableColumn>? ITableColumn.HeaderTemplate { get; set; }

    RenderFragment<ITableColumn>? ITableColumn.ToolboxTemplate { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
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

    IFilter? ITableColumn.Filter { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; internal set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    [NotNull]
    internal string? FieldName { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public bool ShowSearchWhenSelect { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
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
    IEnumerable<SelectedItem>? ILookup.Lookup { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    ILookupService? ILookup.LookupService { get; set; }

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
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    Action<TableCellArgs>? ITableColumn.OnCellRender { get; set; }

    List<IValidator>? IEditorItem.ValidateRules { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public virtual string GetDisplayName() => Text ?? "";

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string GetFieldName() => FieldName;

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
    public bool IgnoreWhenExport { get; set; }

    bool? ITableColumn.IgnoreWhenExport
    {
        get => IgnoreWhenExport;
        set => IgnoreWhenExport = value ?? false;
    }
}
