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
public class DockContent : ComponentBase, IDockComponent
{
    /// <summary>
    /// 获得/设置 排列方式 默认 Row 水平排列
    /// </summary>
    [Parameter]
    [JsonConverter(typeof(DockContentTypeConverter))]
    public DockContentType Type { get; set; }

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
    public List<IDockComponent> Items { get; } = new();

    [CascadingParameter]
    private DockContent? Content { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Content?.Items.Add(this);
    }

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
