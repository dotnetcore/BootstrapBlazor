// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class Split
{
    private const string FirstPane = "FirstPane";
    private const string SecondPane = "SecondPane";
    private const string FirstPaneCollapsed = "first-pane-collapsed";
    private const string SecondPaneCollapsed = "second-pane-collapsed";
    private const string BothPaneExpand = "both-pane-expand";

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("split")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 组件 Wrapper 样式
    /// </summary>
    private string? WrapperClassString => CssBuilder.Default("split-wrapper")
        .AddClass("is-horizontal", !IsVertical)
        .Build();

    /// <summary>
    /// 获得 第一个窗格 Style
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddClass($"flex-basis: {Basis.ConvertToPercentString()};")
        .Build();

    /// <summary>
    /// 获取/设置 当前折叠的窗格（FirstPane/SecondPane）
    /// </summary>
    private string? CollapsedPane { get; set; }

    /// <summary>
    /// 获取 是否显示第一个窗格的折叠/展开按钮
    /// </summary>
    private bool IsShowFirstPaneCollapseSwitch => FirstPaneCollapsible && CollapsedPane != SecondPane;

    /// <summary>
    /// 获取 是否显示第二个窗格的折叠/展开按钮
    /// </summary>
    private bool IsShowSecondPaneCollapseSwitch => SecondPaneCollapsible && CollapsedPane != FirstPane;

    /// <summary>
    /// 获取 是否处于折叠状态
    /// </summary>
    private bool IsCollapsed => !string.IsNullOrEmpty(CollapsedPane);

    /// <summary>
    /// 获得/设置 是否垂直分割
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 第一个窗格初始化位置占比 默认为 50%
    /// </summary>
    [Parameter]
    public string Basis { get; set; } = "50%";

    /// <summary>
    /// 获得/设置 第一个窗格模板
    /// </summary>
    [Parameter]
    public RenderFragment? FirstPaneTemplate { get; set; }

    /// <summary>
    /// 获得/设置 第二个窗格模板
    /// </summary>
    [Parameter]
    public RenderFragment? SecondPaneTemplate { get; set; }

    /// <summary>
    /// 获取/设置 是否可以拖动改变大小
    /// </summary>
    [Parameter]
    public bool IsResizable { get; set; } = true;

    /// <summary>
    /// 获取/设置 第一个窗格是否可折叠
    /// </summary>
    [Parameter]
    public bool FirstPaneCollapsible { get; set; }

    /// <summary>
    /// 获取/设置 第二个窗格是否可折叠
    /// </summary>
    [Parameter]
    public bool SecondPaneCollapsible { get; set; }

    /// <summary>
    /// 折叠/展开窗格
    /// </summary>
    /// <param name="pane"></param>
    private void PaneCollapseSwitch(string pane)
    {
        if (CollapsedPane == pane)
        {
            CollapsedPane = null;
        }
        else
        {
            CollapsedPane = pane;
        }
    }

    /// <summary>
    /// 获取窗格状态
    /// </summary>
    /// <returns></returns>
    private string GetPaneClassStatus()
    {
        if (CollapsedPane == FirstPane)
        {
            return FirstPaneCollapsed;
        }
        else if (CollapsedPane == SecondPane)
        {
            return SecondPaneCollapsed;
        }
        else
        {
            return BothPaneExpand;
        }
    }
}
