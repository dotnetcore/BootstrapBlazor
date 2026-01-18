// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableFilter component</para>
/// <para lang="en">TableFilter component</para>
/// </summary>
public partial class TableColumnFilter : IFilter
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否 active</para>
    /// <para lang="en">Gets or sets Whether is active</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤图标</para>
    /// <para lang="en">Gets or sets Filter Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 不支持过滤类型提示信息 默认 null 读取资源文件内容</para>
    /// <para lang="en">Gets or sets Not Supported Filter Type Message Default null Read Resource File Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 NotSupportedColumnFilterMessage 参数; Deprecated, please use NotSupportedColumnFilterMessage parameter")]
    public string? NotSupportedMessage { get => NotSupportedColumnFilterMessage; set => NotSupportedColumnFilterMessage = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 不支持过滤类型提示信息 默认 null 读取资源文件内容</para>
    /// <para lang="en">Gets or sets Not Supported Filter Type Message Default null Read Resource File Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? NotSupportedColumnFilterMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得 相关联 ITableColumn 实例</para>
    /// <para lang="en">Get Related ITableColumn Instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public ITableColumn? Column { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为 HeaderRow 模式 默认 false</para>
    /// <para lang="en">Gets or sets Whether is HeaderRow Mode Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ITable 实例</para>
    /// <para lang="en">Gets or sets ITable Instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public ITable? Table { get; set; }

    /// <summary>
    /// <para lang="zh">获得 过滤小图标样式</para>
    /// <para lang="en">Get Filter Small Icon Style</para>
    /// </summary>
    private string? FilterClassString => CssBuilder.Default(Icon)
        .AddClass("active", IsActive)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 样式</para>
    /// <para lang="en">Get Style</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("filter-icon")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件 IFilterAction 接口</para>
    /// <para lang="en">Gets or sets Filter Condition IFilterAction Interface</para>
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
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        if (!IsHeaderRow)
        {
            await base.InvokeInitAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">Reset filter method</para>
    /// <para lang="en">Reset filter method</para>
    /// </summary>
    public async Task Reset()
    {
        FilterAction.Reset();
        await OnFilterAsync();
    }

    /// <summary>
    /// <para lang="zh">Filter method</para>
    /// <para lang="en">Filter method</para>
    /// </summary>
    /// <returns></returns>
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
