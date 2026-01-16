// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Drawer 弹出窗参数配置类</para>
///  <para lang="en">Drawer Option Class</para>
/// </summary>
public class DrawerOption
{
    /// <summary>
    ///  <para lang="zh">获得/设置 Drawer 组件样式</para>
    ///  <para lang="en">Get/Set Drawer Component CSS Class</para>
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 抽屉宽度 左右布局时生效</para>
    ///  <para lang="en">Get/Set Drawer Width. Effective when layout is Left/Right</para>
    /// </summary>
    public string? Width { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 抽屉高度 上下布局时生效</para>
    ///  <para lang="en">Get/Set Drawer Height. Effective when layout is Top/Bottom</para>
    /// </summary>
    public string? Height { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否支持键盘 ESC 关闭当前弹窗 默认 false</para>
    ///  <para lang="en">Get/Set Whether to support ESC key to close. Default is false</para>
    /// </summary>
    public bool IsKeyboard { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 点击遮罩是否关闭抽屉 默认为 false</para>
    ///  <para lang="en">Get/Set Whether to Close Drawer on Backdrop Click. Default is false</para>
    /// </summary>
    public bool IsBackdrop { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示遮罩 默认为 true 显示遮罩</para>
    ///  <para lang="en">Get/Set Whether to Show Backdrop. Default is true</para>
    /// </summary>
    public bool ShowBackdrop { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 抽屉显示时是否允许滚动 body 默认为 false 不滚动</para>
    ///  <para lang="en">Get/Set Whether to allow body scrolling when drawer is shown. Default is false</para>
    /// </summary>
    public bool BodyScroll { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件出现位置 默认显示在 Left 位置</para>
    ///  <para lang="en">Get/Set Component Placement. Default is Left</para>
    /// </summary>
    public Placement Placement { get; set; } = Placement.Left;

    /// <summary>
    ///  <para lang="zh">获得/设置 子组件</para>
    ///  <para lang="en">Get/Set Child Content</para>
    /// </summary>
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 自定义组件</para>
    ///  <para lang="en">Get/Set Custom Component</para>
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否允许调整大小 默认 false</para>
    ///  <para lang="en">Get/Set Whether to Allow Resize. Default is false</para>
    /// </summary>
    public bool AllowResize { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 点击背景遮罩时回调委托方法 默认为 null</para>
    ///  <para lang="en">Get/Set Callback for Backdrop Click. Default is null</para>
    /// </summary>
    public Func<Task>? OnClickBackdrop { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 关闭当前 Drawer 回调委托 默认 null</para>
    ///  <para lang="en">Get/Set Close Drawer Callback Delegate. Default is null</para>
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 相关连数据，多用于传值使用</para>
    ///  <para lang="en">Get/Set Context Data. Used for passing values</para>
    /// </summary>
    public object? BodyContext { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 z-index 参数值 默认 null 未设置</para>
    ///  <para lang="en">Get/Set z-index parameter. Default is null</para>
    /// </summary>
    public int? ZIndex { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 <see cref="Drawer"/> 实例</para>
    ///  <para lang="en">Get/Set <see cref="Drawer"/> Instance</para>
    /// </summary>
    internal Drawer? Drawer { get; set; }

    /// <summary>
    ///  <para lang="zh">关闭抽屉弹窗方法</para>
    ///  <para lang="en">Close Drawer Method</para>
    /// </summary>
    public async Task CloseAsync()
    {
        if (Drawer != null)
        {
            await Drawer.Close();
        }
    }
}
