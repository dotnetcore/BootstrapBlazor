// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TableFilter 基类
/// </summary>
public partial class TableFilter : IFilter
{
    /// <summary>
    /// 获得 过滤小图标样式
    /// </summary>
    private string? FilterClassString => CssBuilder.Default(Icon)
        .AddClass("active", IsActive)
        .Build();

    /// <summary>
    /// 获得 样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("filter-icon")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 是否 active
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 过滤图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 增加过滤条件图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// 获得/设置 减少过滤条件图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// 获得/设置 不支持过滤类型提示信息 默认 null 读取资源文件内容
    /// </summary>
    [Parameter]
    public string? NotSupportedMessage { get; set; }

    /// <summary>
    /// 获得/设置 Header 显示文字
    /// </summary>
    private string? _title;

    /// <summary>
    /// 获得/设置 相关 Field 字段名称
    /// </summary>
    [NotNull]
    internal string? FieldKey { get; set; }

    /// <summary>
    /// 获得/设置 条件数量
    /// </summary>
    private int _count;

    /// <summary>
    /// 获得/设置 是否显示增加减少条件按钮
    /// </summary>
    public bool ShowMoreButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 过滤条件 IFilterAction 接口
    /// </summary>
    [NotNull]
    public IFilterAction? FilterAction { get; set; }

    /// <summary>
    /// 获得 当前过滤条件是否激活
    /// </summary>
    internal bool HasFilter => (Table != null) && Table.Filters.ContainsKey(Column.GetFieldName());

    /// <summary>
    /// 获得 相关联 ITableColumn 实例
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public ITableColumn? Column { get; set; }

    /// <summary>
    /// 重置按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ClearButtonText { get; set; }

    /// <summary>
    /// 获得/设置 是否为 HeaderRow 模式 默认 false
    /// </summary>
    [Parameter]
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// 过滤按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FilterButtonText { get; set; }

    /// <summary>
    /// 获得/设置 Table Header 实例
    /// </summary>
    [Parameter]
    public ITable? Table { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private ILookupService? LookupService { get; set; }

    /// <summary>
    /// 组件步长
    /// </summary>
    private string? _step;

    /// <summary>
    /// 外键数据源集合
    /// </summary>
    private Lazy<IEnumerable<SelectedItem>?> _lookup = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _title = Column.GetDisplayName();
        FieldKey = Column.GetFieldName();
        Column.Filter = this;

        _lookup = new(() => Column.Lookup ?? LookupService.GetItemsByKey(Column.LookupServiceKey, Column.LookupServiceData));
        _step = Column.Step;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        FilterButtonText ??= Localizer[nameof(FilterButtonText)];
        ClearButtonText ??= Localizer[nameof(ClearButtonText)];
        NotSupportedMessage ??= Localizer[nameof(NotSupportedMessage)];

        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterPlusIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.TableFilterMinusIcon);

        if (Table != null && Table.Filters.TryGetValue(Column.GetFieldName(), out var action))
        {
            var filter = action.GetFilterConditions();
            if (filter.Filters?.Count > 1)
            {
                _count = 1;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        if (!IsHeaderRow)
        {
            await base.InvokeInitAsync();
        }
    }

    /// <summary>
    /// 点击重置按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClickReset()
    {
        _count = 0;

        if (Table != null)
        {
            Table.Filters.Remove(FieldKey);
            FilterAction.Reset();
            if (Table.OnFilterAsync != null)
            {
                await Table.OnFilterAsync();
            }
        }
    }

    /// <summary>
    /// 点击确认时回调此方法
    /// </summary>
    /// <returns></returns>
    private Task OnClickConfirm() => OnFilterAsync();

    /// <summary>
    /// 过滤数据方法
    /// </summary>
    /// <returns></returns>
    internal async Task OnFilterAsync()
    {
        if (Table != null)
        {
            var f = FilterAction.GetFilterConditions();
            if (f.Filters != null && f.Filters.Count > 0)
            {
                Table.Filters[FieldKey] = FilterAction;
            }
            else
            {
                Table.Filters.Remove(FieldKey);
            }
            if (Table.OnFilterAsync != null)
            {
                await Table.OnFilterAsync();
            }
        }
    }

    /// <summary>
    /// 点击增加按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    private void OnClickPlus()
    {
        if (_count == 0)
        {
            _count++;
        }
    }

    /// <summary>
    /// 点击减少按钮时回调此方法
    /// </summary>
    /// <returns></returns>
    private void OnClickMinus()
    {
        if (_count == 1)
        {
            _count--;
        }
    }

    private bool IsLookup => Column.Lookup != null || !string.IsNullOrEmpty(Column.LookupServiceKey);
}
