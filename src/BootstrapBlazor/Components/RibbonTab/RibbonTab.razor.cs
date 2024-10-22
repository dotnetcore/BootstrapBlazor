﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// RibbonTab 组件
/// </summary>
public partial class RibbonTab
{
    /// <summary>
    /// 获得/设置 是否显示悬浮小箭头 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFloatButton { get; set; }

    /// <summary>
    /// 获得/设置 组件是否悬浮状态改变时回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnFloatChanged { get; set; }

    /// <summary>
    /// 获得/设置 选项卡向上箭头图标
    /// </summary>
    [Parameter]
    public string? RibbonArrowUpIcon { get; set; }

    /// <summary>
    /// 获得/设置 选项卡向下箭头图标
    /// </summary>
    [Parameter]
    public string? RibbonArrowDownIcon { get; set; }

    /// <summary>
    /// 获得/设置 选项卡可固定图标
    /// </summary>
    [Parameter]
    public string? RibbonArrowPinIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否开启 Url 锚点
    /// </summary>
    [Parameter]
    public bool IsSupportAnchor { get; set; }

    /// <summary>
    /// 编码锚点回调方法 第一参数是当前地址 Url 第二个参数是当前选项 Text 属性 返回值为地址全路径
    /// </summary>
    [Parameter]
    public Func<string, string?, string?>? EncodeAnchorCallback { get; set; }

    /// <summary>
    /// 解码锚点回调方法
    /// </summary>
    [Parameter]
    public Func<string, string?>? DecodeAnchorCallback { get; set; }

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public IEnumerable<RibbonTabItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 点击命令按钮回调方法
    /// </summary>
    [Parameter]
    public Func<RibbonTabItem, Task>? OnItemClickAsync { get; set; }

    /// <summary>
    /// 获得/设置 点击标签 Menu 回调方法
    /// </summary>
    [Parameter]
    public Func<RibbonTabItem, Task>? OnMenuClickAsync { get; set; }

    /// <summary>
    /// 获得/设置 右侧按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? RightButtonsTemplate { get; set; }

    /// <summary>
    /// 获得/设置 内容模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否为带边框卡片样式 默认 true
    /// </summary>
    [Parameter]
    public bool IsBorder { get; set; } = true;

    private RibbonTabHeader _header = default!;

    private string? HeaderClassString => CssBuilder.Default("ribbon-tab")
        .AddClass("border", IsBorder)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(SetExpand));

    /// <summary>
    /// SetExpand 方法
    /// </summary>
    [JSInvokable]
    public void SetExpand() => _header.SetExpand();

    /// <summary>
    /// 重新渲染组件
    /// </summary>
    public void Render() => _header.Render();
}
