// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContent 类对标 content 配置项
/// </summary>
public class DockContent : DockComponentBase
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 子项集合
    /// </summary>
    [JsonConverter(typeof(DockComponentConverter))]
    [JsonPropertyName("content")]
    public List<IDockComponent> Items { get; } = [];

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
    /// 获得/设置 组件是否显示 Header 默认 true
    /// </summary>
    [Parameter]
    [JsonPropertyName("hasHeaders")]
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<DockContent>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<DockContent>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<DockContent>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<DockContent>.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}
