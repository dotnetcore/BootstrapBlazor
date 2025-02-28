// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// DialButtonItem 项组件
/// </summary>
public class DialButtonItem : ComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 选项值
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    private List<DialButtonItem>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Items?.Add(this);
    }

    /// <summary>
    /// 资源销毁
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Items?.Remove(this);
        }
    }

    /// <summary>
    /// 资源销毁
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
