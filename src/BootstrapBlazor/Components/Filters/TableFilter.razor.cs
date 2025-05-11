// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// 获得/设置 不支持过滤类型提示信息 默认 null 读取资源文件内容
    /// </summary>
    [Parameter]
    public string? NotSupportedMessage { get; set; }

    /// <summary>
    /// 获得/设置 相关 Field 字段名称
    /// </summary>
    [NotNull]
    internal string? FieldKey { get; set; }

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
    [EditorRequired]
    public ITableColumn? Column { get; set; }

    /// <summary>
    /// 获得/设置 是否为 HeaderRow 模式 默认 false
    /// </summary>
    [Parameter]
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// 获得/设置 ITable 实例
    /// </summary>
    [Parameter]
    public ITable? Table { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    protected IStringLocalizer<TableFilter>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Column.Filter = this;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NotSupportedMessage ??= Localizer[nameof(NotSupportedMessage)];
        FieldKey = Column.GetFieldName();
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
}
