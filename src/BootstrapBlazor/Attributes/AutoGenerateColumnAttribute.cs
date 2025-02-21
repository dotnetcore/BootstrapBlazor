// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// AutoGenerateColumn 标签类，用于 <see cref="Table{TItem}"/> 标识自动生成列
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class AutoGenerateColumnAttribute : AutoGenerateBaseAttribute, ITableColumn
{
    /// <summary>
    /// 获得/设置 显示顺序 ，规则如下：
    /// <para></para>
    /// &gt;0时排前面，1,2,3...
    /// <para></para>
    /// =0时排中间(默认)
    /// <para></para>
    /// &lt;0时排后面，...-3,-2,-1
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 获得/设置 是否为默认排序列 默认为 false
    /// </summary>
    public bool DefaultSort { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsReadonlyWhenAdd { get; set; }

    bool? ITableColumn.IsReadonlyWhenAdd
    {
        get => IsReadonlyWhenAdd;
        set => IsReadonlyWhenAdd = value ?? false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsReadonlyWhenEdit { get; set; }

    bool? ITableColumn.IsReadonlyWhenEdit
    {
        get => IsReadonlyWhenEdit;
        set => IsReadonlyWhenEdit = value ?? false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsVisibleWhenAdd { get; set; } = true;

    bool? ITableColumn.IsVisibleWhenAdd
    {
        get => IsVisibleWhenAdd;
        set => IsVisibleWhenAdd = value ?? true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsVisibleWhenEdit { get; set; } = true;

    /// <summary>
    /// 自定义搜索方法
    /// </summary>
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
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 false
    /// </summary>
    public bool ShowLabelTooltip { get; set; }

    bool? IEditorItem.ShowLabelTooltip
    {
        get => ShowLabelTooltip;
        set => ShowLabelTooltip = value ?? false;
    }

    /// <summary>
    /// 获得/设置 是否为默认排序规则 默认为 SortOrder.Unset
    /// </summary>
    public SortOrder DefaultSortOrder { get; set; }

    IEnumerable<SelectedItem>? IEditorItem.Items { get; set; }

    /// <summary>
    /// 获得/设置 列宽
    /// </summary>
    public int Width { get; set; }

    int? ITableColumn.Width
    {
        get => Width <= 0 ? null : Width;
        set => Width = value ?? 0;
    }

    /// <summary>
    /// 获得/设置 是否固定本列 默认 false 不固定
    /// </summary>
    public bool Fixed { get; set; }

    /// <summary>
    /// 获得/设置 列 td 自定义样式 默认为 null 未设置
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置
    /// </summary>
    public BreakPoint ShownWithBreakPoint { get; set; }

    /// <summary>
    /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
    /// </summary>
    public string? FormatString { get; set; }

    /// <summary>
    /// 获得/设置 placeholder 文本 默认为 null
    /// </summary>
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// 获得/设置 编辑模板
    /// </summary>
    RenderFragment<object>? IEditorItem.EditTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表头模板
    /// </summary>
    RenderFragment<ITableColumn>? ITableColumn.HeaderTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    RenderFragment<ITableColumn>? ITableColumn.ToolboxTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件类型 默认为 null
    /// </summary>
    public Type? ComponentType { get; set; }

    /// <summary>
    /// 获得/设置 组件自定义类型参数集合 默认为 null
    /// </summary>
    IEnumerable<KeyValuePair<string, object>>? IEditorItem.ComponentParameters { get; set; }

    /// <summary>
    /// 获得/设置 显示模板
    /// </summary>
    RenderFragment<object>? ITableColumn.Template { get; set; }

    /// <summary>
    /// 获得/设置 搜索模板
    /// </summary>
    RenderFragment<object>? ITableColumn.SearchTemplate { get; set; }

    /// <summary>
    /// 获得/设置 过滤模板
    /// </summary>
    RenderFragment? ITableColumn.FilterTemplate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    Func<object?, Task<string?>>? ITableColumn.GetTooltipTextCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.Searchable { get => Searchable; set => Searchable = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.Filterable { get => Filterable; set => Filterable = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.Sortable { get => Sortable; set => Sortable = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.TextWrap { get => TextWrap; set => TextWrap = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.TextEllipsis { get => TextEllipsis; set => TextEllipsis = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? IEditorItem.Ignore { get => Ignore; set => Ignore = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? IEditorItem.Readonly { get => Readonly; set => Readonly = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.Visible { get => Visible; set => Visible = value ?? true; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.ShowTips { get => ShowTips; set => ShowTips = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    bool? ITableColumn.ShowCopyColumn { get => ShowCopyColumn; set => ShowCopyColumn = value ?? false; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    Alignment? ITableColumn.Align { get => Align; set => Align = value ?? Alignment.None; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Step { get; set; }

    /// <summary>
    /// 获得/设置 行数
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    /// 获得/设置 控件的占列数值范围 1-12
    /// </summary>
    public int Cols { get; set; }

    /// <summary>
    /// 获得/设置 列过滤器
    /// </summary>
    IFilter? ITableColumn.Filter { get; set; }

    /// <summary>
    /// 获得 属性类型
    /// </summary>
    [NotNull]
    public Type? PropertyType { get; internal set; }

    /// <summary>
    /// 获得/设置 当前属性显示文字 列头或者标签名称
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    internal string? FieldName { get; set; }

    /// <summary>
    /// 获得/设置 字段数据源下拉框是否显示搜索栏 默认 false 不显示
    /// </summary>
    public bool ShowSearchWhenSelect { get; set; }

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

    /// <summary>
    /// 获得/设置 单元格回调方法
    /// </summary>
    Action<TableCellArgs>? ITableColumn.OnCellRender { get; set; }

    /// <summary>
    /// 获得/设置 自定义验证集合
    /// </summary>
    List<IValidator>? IEditorItem.ValidateRules { get; set; }

    /// <summary>
    /// 获取绑定字段显示名称方法
    /// </summary>
    /// <returns></returns>
    public virtual string GetDisplayName() => Text ?? "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public string GetFieldName() => FieldName;

    /// <summary>
    /// 获得/设置 当前属性分组
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 当前属性分组排序 默认 0
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
