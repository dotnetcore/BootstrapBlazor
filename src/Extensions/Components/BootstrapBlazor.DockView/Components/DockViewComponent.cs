// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContentItem 配置项子项对标 content 配置项内部 content 配置
/// </summary>
public class DockViewComponent : DockViewComponentBase
{
    /// <summary>
    /// 获得/设置 组件是否显示 Header 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件 Title
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 组件 Title 宽度 默认 null 未设置
    /// </summary>
    [Parameter]
    public int? TitleWidth { get; set; }

    /// <summary>
    /// 获得/设置 组件 Title 样式 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? TitleClass { get; set; }

    /// <summary>
    /// 获得/设置 Title 模板 默认 null 未设置
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public RenderFragment? TitleTemplate { get; set; }

    /// <summary>
    /// 获得/设置 组件 Class 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 组件是否可见 默认 true 可见
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件是否允许关闭 默认 null 使用 DockView 的配置
    /// </summary>
    [Parameter]
    public bool? ShowClose { get; set; }

    /// <summary>
    /// 获得/设置 组件唯一标识值 默认 null 未设置时取 Title 作为唯一标识
    /// </summary>
    [Parameter]
    public string? Key { get; set; }

    /// <summary>
    /// 获得/设置 是否锁定 默认 null 未设置时取 DockView 的配置
    /// </summary>
    /// <remarks>锁定后无法拖动</remarks>
    [Parameter]
    public bool? IsLock { get; set; }

    /// <summary>
    /// 获得/设置 是否显示锁定按钮 默认 null 未设置时取 DockView 的配置
    /// </summary>
    [Parameter]
    public bool? ShowLock { get; set; }

    /// <summary>
    /// 获得/设置 是否显示标题前置图标 默认 false 不显示
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public bool ShowTitleBar { get; set; }

    /// <summary>
    /// 获得/设置 标题前置图标 默认 null 未设置使用默认图标
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public string? TitleBarIcon { get; set; }

    /// <summary>
    /// 获得/设置 标题前置图标 Url 默认 null 未设置使用默认图标
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public string? TitleBarIconUrl { get; set; }

    /// <summary>
    /// 获得/设置 标题前置图标点击回调方法 默认 null
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public Func<Task>? OnClickTitleBarCallback { get; set; }

    /// <summary>
    /// 获得/设置 DockViewComponent 集合
    /// </summary>
    [CascadingParameter]
    private List<DockViewComponent>? Components { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Components?.Add(this);
        Type = DockViewContentType.Component;
    }

    /// <summary>
    /// 设置 Visible 参数方法
    /// </summary>
    /// <param name="visible"></param>
    public void SetVisible(bool visible)
    {
        Visible = visible;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            Components?.Clear();
        }
    }
}
