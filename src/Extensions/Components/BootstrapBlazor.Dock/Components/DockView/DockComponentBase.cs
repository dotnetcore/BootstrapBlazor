// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockComponent 基类
/// </summary>
public abstract class DockComponentBase : IdComponentBase, IDockComponent
{
    /// <summary>
    /// 获得/设置 渲染类型 默认 Component
    /// </summary>
    [Parameter]
    [JsonConverter(typeof(DockTypeConverter))]
    public DockContentType Type { get; set; }

    /// <summary>
    /// 获得/设置 DockContent 实例
    /// </summary>
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

    private bool disposedValue;

    /// <summary>
    /// 资源销毁方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;

            if (disposing)
            {
                Content?.Items.Remove(this);
            }
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
