// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DialButtonItem 项组件</para>
/// <para lang="en">DialButtonItem component</para>
/// </summary>
public class DialButtonItem : ComponentBase, IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选项值</para>
    /// <para lang="en">Gets or sets the value</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件内容</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// <para><version>10.2.2</version></para>
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
    /// <para lang="zh">资源销毁</para>
    /// <para lang="en">Dispose usage</para>
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
    /// <para lang="zh">资源销毁</para>
    /// <para lang="en">Dispose usage</para>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
