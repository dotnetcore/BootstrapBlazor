// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">RibbonTab 组件</para>
/// <para lang="en">RibbonTab Component</para>
/// </summary>
public partial class RibbonTab
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示悬浮小箭头，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show float button. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowFloatButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件悬浮状态改变时的回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when float state changes. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnFloatChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项卡向上箭头图标</para>
    /// <para lang="en">Gets or sets the tab arrow up icon</para>
    /// </summary>
    [Parameter]
    public string? RibbonArrowUpIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项卡向下箭头图标</para>
    /// <para lang="en">Gets or sets the tab arrow down icon</para>
    /// </summary>
    [Parameter]
    public string? RibbonArrowDownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项卡可固定图标</para>
    /// <para lang="en">Gets or sets the tab pin icon</para>
    /// </summary>
    [Parameter]
    public string? RibbonArrowPinIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启 Url 锚点</para>
    /// <para lang="en">Gets or sets whether to enable URL anchor</para>
    /// </summary>
    [Parameter]
    public bool IsSupportAnchor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 编码锚点回调方法。第一参数是当前地址 Url，第二个参数是当前选项 Text 属性，返回值为地址全路径</para>
    /// <para lang="en">Gets or sets the encode anchor callback method. First parameter is current URL, second parameter is current item Text property. Return value is full path</para>
    /// </summary>
    [Parameter]
    public Func<string, string?, string?>? EncodeAnchorCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 解码锚点回调方法</para>
    /// <para lang="en">Gets or sets the decode anchor callback method</para>
    /// </summary>
    [Parameter]
    public Func<string, string?>? DecodeAnchorCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据源</para>
    /// <para lang="en">Gets or sets the items</para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public IEnumerable<RibbonTabItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击命令按钮回调方法</para>
    /// <para lang="en">Gets or sets the click command button callback method</para>
    /// </summary>
    [Parameter]
    public Func<RibbonTabItem, Task>? OnItemClickAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击标签 Menu 回调方法</para>
    /// <para lang="en">Gets or sets the click tab menu callback method</para>
    /// </summary>
    [Parameter]
    public Func<RibbonTabItem, Task>? OnMenuClickAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 右侧按钮模板</para>
    /// <para lang="en">Gets or sets the right buttons template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? RightButtonsTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容模板</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为带边框卡片样式，默认为 true</para>
    /// <para lang="en">Gets or sets whether to have border. Default is true</para>
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
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(SetExpand));

    /// <summary>
    /// <para lang="zh">SetExpand 方法</para>
    /// <para lang="en">SetExpand method</para>
    /// </summary>
    [JSInvokable]
    public void SetExpand() => _header.SetExpand();

    /// <summary>
    /// <para lang="zh">重新渲染组件</para>
    /// <para lang="en">Renders the component</para>
    /// </summary>
    public void Render() => _header.Render();
}
