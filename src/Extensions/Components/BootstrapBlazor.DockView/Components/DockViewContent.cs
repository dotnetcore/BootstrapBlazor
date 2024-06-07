// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContent 类对标 content 配置项
/// </summary>
public class DockViewContent : DockViewComponentBase, IDockViewContent
{
    /// <summary>
    /// 获得/设置 子项集合
    /// </summary>
    List<IDockViewComponentBase> IDockViewContent.Items => _items;

    private List<IDockViewComponentBase> _items = [];

    /// <summary>
    /// 获得/设置 组件宽度百分比 默认 null 未设置
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// 获得/设置 组件高度百分比 默认 null 未设置
    /// </summary>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<List<IDockViewComponentBase>>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<List<IDockViewComponentBase>>.Value), _items);
        builder.AddAttribute(2, nameof(CascadingValue<List<IDockViewComponentBase>>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<List<IDockViewComponentBase>>.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}
