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
        .AddClass($"height: {Height};", !string.IsNullOrEmpty(Height))
        .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width))
        .Build();

    /// <summary>
    /// 获得/设置 图表标题
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 组件高度支持单位<para>如: 30% , 30px , 30em , calc(30%)</para>
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// 获得/设置 组件宽度支持单位<para>如: 30% , 30px , 30em , calc(30%)</para>
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// 获得/设置 图表所在canvas是否随其容器大小变化而变化 默认为 true
    /// </summary>
    [Parameter]
    public bool Responsive { get; set; } = true;

    /// <summary>
    /// 获取/设置 是否 约束图表比例 默认为 true
    /// </summary>
    [Parameter]
    public bool MaintainAspectRatio { get; set; } = true;

    /// <summary>
    /// 获得/设置 设置canvas的宽高比（值为1表示canvas是正方形），如果显示定义了canvas的高度，则此属性无效 默认为 2
    /// </summary>
    [Parameter]
    public int AspectRatio { get; set; } = 2;

    /// <summary>
    /// 获得/设置 图表尺寸延迟变化时间 默认为 0
    /// </summary>
    [Parameter]
    public int ResizeDelay { get; set; } = 0;

    /// <summary>
    /// 获得/设置 Bubble 模式下显示角度 180 为 半圆 360 为正圆
    /// </summary>
    [Parameter]
    public int Angle { get; set; }

    /// <summary>
    /// 获得/设置 Line 折线图线的宽度
    /// </summary>
    [Parameter]
    public double BorderWidth { get; set; }

    /// <summary>
    /// 获得/设置 正在加载文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LoadingText { get; set; }

    /// <summary>
    /// 获得/设置 图表组件渲染类型 默认为 line 图
    /// </summary>
    [Parameter]
    public ChartType ChartType { get; set; }

    /// <summary>
    /// 获得/设置 图表组件组件方法 默认为 Update
    /// </summary>
    [Parameter]
    public ChartAction ChartAction { get; set; }

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
            ds.Options.Title = ds.Options.Title ?? Title;
            ds.Options.Responsive = ds.Options.Responsive ?? Responsive;
            ds.Options.MaintainAspectRatio = ds.Options.MaintainAspectRatio ?? MaintainAspectRatio;
            ds.Options.AspectRatio = ds.Options.AspectRatio ?? AspectRatio;
            ds.Options.ResizeDelay = ds.Options.ResizeDelay ?? ResizeDelay;
            ds.Options.BorderWidth ??= BorderWidth;
            if (Height != null && Width != null)
            {
                //设置了高度和宽度,会自动禁用约束图表比例,图表充满容器
                ds.Options.MaintainAspectRatio = false;
            }
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

    /// <summary>
    /// 重新加载方法, 强制重新渲染图表
    /// </summary>
    public async Task Reload()
    {
        if (OnInitAsync != null)
        {
            var ds = await OnInitAsync();
            await Interop.InvokeVoidAsync(this, ChartElement, "bb_chart", nameof(Completed), ds, ChartAction.Reload.ToDescriptionString(), ChartType.ToDescriptionString(), Angle);

            if (OnAfterUpdateAsync != null)
            {
                await OnAfterUpdateAsync(ChartAction.Reload);
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
