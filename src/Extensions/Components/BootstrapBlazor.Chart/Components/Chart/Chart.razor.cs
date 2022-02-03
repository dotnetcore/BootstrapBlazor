// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Chart 组件基类
/// </summary>
public partial class Chart : BootstrapComponentBase, IDisposable
{
    [NotNull]
    private JSInterop<Chart>? Interop { get; set; }

    /// <summary>
    /// 获得/设置 EChart DOM 元素实例
    /// </summary>
    private ElementReference ChartElement { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("chart is-loading")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 组件 Style 字符串
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width))
        .Build();

    /// <summary>
    /// 获得/设置 组件宽度支持单位
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// 获得/设置 组件数据初始化委托方法
    /// </summary>
    [Parameter]
    public Func<Task<ChartDataSource>>? OnInitAsync { get; set; }

    /// <summary>
    /// 获得/设置 客户端绘制图表完毕后回调此委托方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnAfterInitAsync { get; set; }

    /// <summary>
    /// 获得/设置 客户端更新图表完毕后回调此委托方法
    /// </summary>
    [Parameter]
    public Func<ChartAction, Task>? OnAfterUpdateAsync { get; set; }

    /// <summary>
    /// 获得/设置 Bubble 模式下显示角度 180 为 半圆 360 为正圆
    /// </summary>
    [Parameter]
    public int Angle { get; set; }

    /// <summary>
    /// 获得/设置 图表组件渲染类型 默认为 line 图
    /// </summary>
    [Parameter]
    public ChartType ChartType { get; set; }

    /// <summary>
    /// 获得/设置 图表组件渲染类型 默认为 Update
    /// </summary>
    [Parameter]
    public ChartAction ChartAction { get; set; }

    /// <summary>
    /// 获得/设置 正在加载文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LoadingText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Chart>? Localizer { get; set; }

    private bool UpdateDataSource { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadingText ??= Localizer[nameof(LoadingText)];
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            if (OnInitAsync == null)
            {
                throw new InvalidOperationException("OnInit paramenter must be set");
            }

            Interop ??= new JSInterop<Chart>(JSRuntime);
            var ds = await OnInitAsync.Invoke();
            await Interop.InvokeVoidAsync(this, ChartElement, "bb_chart", nameof(Completed), ds, "", ChartType.ToDescriptionString(), Angle);
        }
    }

    /// <summary>
    /// 图表绘制完成后回调此方法
    /// </summary>
    [JSInvokable]
    public void Completed()
    {
        OnAfterInitAsync?.Invoke();
    }

    /// <summary>
    /// 更新图表方法
    /// </summary>
    public async Task Update(ChartAction action)
    {
        if (OnInitAsync != null)
        {
            var ds = await OnInitAsync();
            await Interop.InvokeVoidAsync(this, ChartElement, "bb_chart", nameof(Completed), ds, action.ToDescriptionString(), ChartType.ToDescriptionString(), Angle);

            if (OnAfterUpdateAsync != null)
            {
                await OnAfterUpdateAsync(action);
            }
        }
    }

    #region Dispose
    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Interop?.Dispose();
            Interop = null;
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
