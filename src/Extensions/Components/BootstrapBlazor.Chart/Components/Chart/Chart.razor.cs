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
public partial class Chart
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("chart d-flex justify-content-center align-items-center position-relative is-loading")
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
    /// 获得/设置 图表所在 canvas 是否随其容器大小变化而变化 默认为 true
    /// </summary>
    [Parameter]
    public bool Responsive { get; set; } = true;

    /// <summary>
    /// 获取/设置 是否 约束图表比例 默认为 true
    /// </summary>
    [Parameter]
    public bool MaintainAspectRatio { get; set; } = true;

    /// <summary>
    /// 获得/设置 设置 canvas 的宽高比（值为1表示 canvas 是正方形），如果显示定义了 canvas 的高度，则此属性无效 默认为 2
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
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public Func<Task<ChartDataSource>>? OnInitAsync { get; set; }

    /// <summary>
    /// 获得/设置 点击图标数据回调方法
    /// </summary>
    [Parameter]
    public Func<(int DatasetIndex, int Index), Task>? OnClickDataAsync { get; set; }

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
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (OnInitAsync == null)
            {
                throw new InvalidOperationException("OnInit parameter must be set");
            }

            var ds = await OnInitAsync();
            UpdateOptions(ds);
            ds.Options.Title = ds.Options.Title ?? Title;
            ds.Options.Responsive = ds.Options.Responsive ?? Responsive;
            ds.Options.MaintainAspectRatio = ds.Options.MaintainAspectRatio ?? MaintainAspectRatio;
            ds.Options.AspectRatio = ds.Options.AspectRatio ?? AspectRatio;
            ds.Options.ResizeDelay = ds.Options.ResizeDelay ?? ResizeDelay;

            if (Height != null && Width != null)
            {
                //设置了高度和宽度,会自动禁用约束图表比例,图表充满容器
                ds.Options.MaintainAspectRatio = false;
            }

            await InvokeVoidAsync("init", Id, Interop, nameof(Completed), ds);
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
    /// 点击数据回调方法
    /// </summary>
    /// <param name="datasetIndex"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnClickCallback(int datasetIndex, int index)
    {
        if (OnClickDataAsync != null)
        {
            await OnClickDataAsync((datasetIndex, index));
        }
    }

    /// <summary>
    /// 更新图表方法
    /// </summary>
    public async Task Update(ChartAction action)
    {
        if (OnInitAsync != null)
        {
            var ds = await OnInitAsync();
            UpdateOptions(ds);

            await InvokeVoidAsync("update", Id, ds, action.ToDescriptionString(), Angle);

            if (OnAfterUpdateAsync != null)
            {
                await OnAfterUpdateAsync(action);
            }
        }
    }

    /// <summary>
    /// 重新加载方法, 强制重新渲染图表
    /// </summary>
    public Task Reload() => Update(ChartAction.Reload);

    private void UpdateOptions(ChartDataSource ds)
    {
        ds.Options.OnClickDataMethod = OnClickDataAsync == null ? null : nameof(OnClickCallback);
        ds.Type ??= ChartType.ToDescriptionString();
    }

    /// <summary>
    /// 生成图片方法
    /// </summary>
    /// <returns></returns>
    public Task<byte[]?> ToImageAsync(string mimeType = "image/png") => InvokeAsync<byte[]?>("toImage", Id, mimeType);
}
