// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockContentItem 配置项子项对标 content 配置项内部 content 配置
/// </summary>
public class DockViewComponent : DockViewComponentBase
{
    /// <summary>
    /// 获得/设置 组件名称 默认 component golden-layout 渲染使用
    /// </summary>
    [Parameter]
    public string ComponentName { get; set; } = "component";

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
    /// 获得/设置 组件 Class 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 组件是否可见 默认 true 可见
    /// </summary>
    [Parameter]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件是否允许关闭 默认 true
    /// </summary>
    [Parameter]
    public bool ShowClose { get; set; } = true;

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
    /// 获得/设置 组件唯一标识值 默认 null 未设置时取 Title 作为唯一标识
    /// </summary>
    [Parameter]
    public string? Key { get; set; }

    /// <summary>
    /// 获得/设置 组件状态
    /// </summary>
    [Parameter]
    public object? ComponentState { get; set; }

    /// <summary>
    /// 获得/设置 是否锁定 默认 false
    /// </summary>
    /// <remarks>锁定后无法拖动</remarks>
    [Parameter]
    public bool IsLock { get; set; }

    /// <summary>
    /// 获得/设置 Title 模板 默认 null 未设置
    /// </summary>
    [Parameter]
    public RenderFragment? TitleTemplate { get; set; }

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

        ComponentState = new { Id, ShowClose, Class, Key = Key ?? Title, Lock = IsLock, TitleWidth, TitleClass, HasTitleTemplate = TitleTemplate != null };
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
