// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 构造函数
/// </summary>
/// <param name="fieldName">字段名称</param>
/// <param name="fieldType">字段类型</param>
/// <param name="fieldText">显示文字</param>
class InternalTableColumn(string fieldName, Type fieldType, string? fieldText = null) : ITableColumn
{
    private string FieldName { get; } = fieldName;

    public bool Sortable { get; set; }

    public bool DefaultSort { get; set; }

    public SortOrder DefaultSortOrder { get; set; }

    public bool Filterable { get; set; }

    public bool Searchable { get; set; }

    public int? Width { get; set; }

    public bool Fixed { get; set; }

    public bool TextWrap { get; set; }

    public bool TextEllipsis { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    public bool SkipValidate { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Ignore { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Readonly { get; set; }

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
    public bool Visible { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsVisibleWhenAdd { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool? IsVisibleWhenEdit { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示标签 Tooltip 多用于标签文字过长导致裁减时使用 默认 null
    /// </summary>
    public bool? ShowLabelTooltip { get; set; }

    public string? CssClass { get; set; }

    public BreakPoint ShownWithBreakPoint { get; set; }

    public RenderFragment<object>? Template { get; set; }

    public RenderFragment<object>? SearchTemplate { get; set; }

    public RenderFragment? FilterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 表头模板
    /// </summary>
    public RenderFragment<ITableColumn>? HeaderTemplate { get; set; }

    public IFilter? Filter { get; set; }

    public string? FormatString { get; set; }

    /// <summary>
    /// 获得/设置 placeholder 文本 默认为 null
    /// </summary>
    public string? PlaceHolder { get; set; }

    public Func<object?, Task<string?>>? Formatter { get; set; }

    public Alignment Align { get; set; }

    public bool ShowTips { get; set; }

    public Type PropertyType { get; } = fieldType;

    [ExcludeFromCodeCoverage]
    public bool Editable { get; set; } = true;

    public string? Step { get; set; }

    public int Rows { get; set; }

    [NotNull]
    public string? Text { get; set; } = fieldText;

    public RenderFragment<object>? EditTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件类型 默认为 null
    /// </summary>
    public Type? ComponentType { get; set; }

    /// <summary>
    /// 获得/设置 组件自定义类型参数集合 默认为 null
    /// </summary>
    public IEnumerable<KeyValuePair<string, object>>? ComponentParameters { get; set; }

    /// <summary>
    /// 获得/设置 额外数据源一般用于下拉框或者 CheckboxList 这种需要额外配置数据源组件使用
    /// </summary>
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 显示顺序
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源 常用于外键自动转换为名称操作
    /// </summary>
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 字段数据源下拉框是否显示搜索栏 默认 false 不显示
    /// </summary>
    public bool ShowSearchWhenSelect { get; set; }

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
    /// 获得/设置 单元格回调方法
    /// </summary>
    public Action<TableCellArgs>? OnCellRender { get; set; }

    /// <summary>
    /// 获得/设置 自定义验证集合
    /// </summary>
    public List<IValidator>? ValidateRules { get; set; }

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
    public bool ShowCopyColumn { get; set; }

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

    public string GetDisplayName() => Text;

    public string GetFieldName() => FieldName;
}
