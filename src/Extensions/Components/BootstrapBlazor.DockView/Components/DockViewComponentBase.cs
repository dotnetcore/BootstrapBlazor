// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockComponent 基类
/// </summary>
public abstract class DockViewComponentBase : IdComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 渲染类型 默认 Component
    /// </summary>
    [Parameter]
    public DockViewContentType Type { get; set; }

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
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 DockContent 实例
    /// </summary>
    [CascadingParameter]
    private List<DockViewComponentBase>? Parent { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Parent?.Add(this);
    }

    /// <summary>
    /// 资源销毁方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Parent?.Remove(this);
        }
    }

    /// <summary>
    /// 资源销毁方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
