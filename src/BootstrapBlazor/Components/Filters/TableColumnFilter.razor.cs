// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TableFilter component
/// </summary>
public partial class TableColumnFilter : IFilter
{
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
    /// 获得 相关联 ITableColumn 实例
    /// </summary>
    [Parameter]
    [NotNull]
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
    [NotNull]
    public ITable? Table { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableColumnFilter>? Localizer { get; set; }

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
    /// 获得/设置 过滤条件 IFilterAction 接口
    /// </summary>
    [NotNull]
    public IFilterAction? FilterAction { get; set; }

    private string _fieldKey = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Column.Filter = this;
        _fieldKey = Column.GetFieldName();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        NotSupportedMessage ??= Localizer[nameof(NotSupportedMessage)];
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
    /// Reset filter method
    /// </summary>
    public async Task Reset()
    {
        FilterAction.Reset();
        await OnFilterAsync();
    }

    /// <summary>
    /// Filter method
    /// </summary>
    /// <returns></returns>
    public async Task OnFilterAsync()
    {
        if (Table.OnFilterAsync == null)
        {
            return;
        }

        var action = FilterAction.GetFilterConditions();
        if (action.Filters != null && action.Filters.Count > 0)
        {
            Table.Filters[_fieldKey] = FilterAction;
        }
        else
        {
            Table.Filters.Remove(_fieldKey);
        }

        await Table.OnFilterAsync();
    }
}
