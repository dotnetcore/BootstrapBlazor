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
public class DockViewContent : DockViewComponentBase
{
    /// <summary>
    /// 获得/设置 子项集合
    /// </summary>
    [JsonConverter(typeof(DockViewComponentConverter))]
    [JsonPropertyName("content")]
    public List<DockViewComponentBase> Items { get; set; } = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<List<DockViewComponentBase>>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<List<DockViewComponentBase>>.Value), Items);
        builder.AddAttribute(2, nameof(CascadingValue<List<DockViewComponentBase>>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<List<DockViewComponentBase>>.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}
