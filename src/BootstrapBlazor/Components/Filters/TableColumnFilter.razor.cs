// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableColumnFilter 组件</para>
/// <para lang="en">TableColumnFilter component</para>
/// </summary>
public partial class TableColumnFilter : IFilter
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否激活</para>
    /// <para lang="en">Gets or sets whether active</para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤图标</para>
    /// <para lang="en">Gets or sets filter icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 不支持过滤类型提示信息 默认 null 从资源读取</para>
    /// <para lang="en">Gets or sets not supported filter type message. Default is null and read from resource</para>
    /// </summary>
    [Parameter]
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 NotSupportedColumnFilterMessage 参数; Deprecated, please use NotSupportedColumnFilterMessage parameter")]
    public string? NotSupportedMessage { get => NotSupportedColumnFilterMessage; set => NotSupportedColumnFilterMessage = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 不支持过滤类型提示信息 默认 null 从资源读取</para>
    /// <para lang="en">Gets or sets not supported filter type message. Default is null and read from resource</para>
    /// </summary>
    [Parameter]
    public string? NotSupportedColumnFilterMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关联的 <see cref="ITableColumn"/> 实例</para>
    /// <para lang="en">Gets or sets the related <see cref="ITableColumn"/> instance</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public ITableColumn? Column { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为表头行模式 默认 false</para>
    /// <para lang="en">Gets or sets whether header row mode is enabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ITable"/> 实例</para>
    /// <para lang="en">Gets or sets the <see cref="ITable"/> instance</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public ITable? Table { get; set; }

    private string? FilterClassString => CssBuilder.Default(Icon)
        .AddClass("active", IsActive)
        .Build();

    private string? ClassString => CssBuilder.Default("filter-icon")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc cref="IFilter.FilterAction"/>
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
    protected override async Task InvokeInitAsync()
    {
        if (!IsHeaderRow)
        {
            await base.InvokeInitAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">重置过滤方法</para>
    /// <para lang="en">Resets filter</para>
    /// </summary>
    public async Task Reset()
    {
        FilterAction.Reset();
        await OnFilterAsync();
    }

    /// <summary>
    /// <para lang="zh">执行过滤方法</para>
    /// <para lang="en">Executes filter</para>
    /// </summary>
    public async Task OnFilterAsync()
    {
        if (Table.OnFilterAsync == null)
        {
            return;
        }

        var action = FilterAction.GetFilterConditions();
        if (action.Filters.Count > 0)
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
