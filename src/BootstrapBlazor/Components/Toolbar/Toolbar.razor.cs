// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Toolbar 组件用于显示工具栏内容
/// </summary>
public partial class Toolbar
{
    /// <summary>
    /// 获得/设置 是否允许换行显示工具栏内容 默认 false
    /// </summary>
    [Parameter]
    public bool IsWrap { get; set; }

    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-toolbar")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddStyle("flex-wrap", "wrap", IsWrap)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private readonly List<ComponentBase> _components = [];

    /// <summary>
    /// 添加按钮到工具栏方法
    /// </summary>
    public void Add(ComponentBase component) => _components.Add(component);

    /// <summary>
    /// 移除按钮到工具栏方法
    /// </summary>
    public void Remove(ComponentBase component) => _components.Remove(component);
}
