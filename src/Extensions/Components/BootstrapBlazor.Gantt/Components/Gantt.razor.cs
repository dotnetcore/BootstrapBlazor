// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 甘特图组件
/// </summary>
public partial class Gantt
{
    /// <summary>
    /// 获得或设置 甘特图数据源
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public IEnumerable<GanttItem>? Items { get; set; }

    /// <summary>
    /// 获得或设置 甘特图配置项
    /// </summary>
    [Parameter]
    [NotNull]
    public GanttOption? Option { get; set; }

    /// <summary>
    /// 获得或设置 点击事件
    /// </summary>
    [Parameter]
    public Func<GanttItem, Task>? OnClick { get; set; }

    /// <summary>
    /// 获得或设置 任务时间改变事件
    /// </summary>
    [Parameter]
    public Func<GanttItem, string, string, Task>? OnDataChanged { get; set; }

    /// <summary>
    /// 获得或设置 任务进度改变事件
    /// </summary>
    [Parameter]
    public Func<GanttItem, int, Task>? OnProgressChanged { get; set; }

    private string? ClassString => CssBuilder.Default("bb-gantt")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Option ??= new GanttOption();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Items, Option, Interop);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnGanttClick(GanttItem item)
    {
        if (OnClick != null)
        {
            await OnClick(item);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="progress"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnGanttProgressChange(GanttItem item, int progress)
    {
        if (OnProgressChanged != null)
        {
            await OnProgressChanged(item, progress);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnGanttDataChange(GanttItem item, string start, string end)
    {
        if (OnDataChanged != null)
        {
            await OnDataChanged(item, start, end);
        }
    }

    /// <summary>
    /// 改变甘特图视图
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public Task ChangeVieMode(string mode) => InvokeVoidAsync("changeViewMode", Id, mode);
}
