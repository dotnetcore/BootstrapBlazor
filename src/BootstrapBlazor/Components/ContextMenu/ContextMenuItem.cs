// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ContextMenuItem 类</para>
/// <para lang="en">A type that represents a menu item in a <see cref="ContextMenu"/>.</para>
/// </summary>
public class ContextMenuItem : ComponentBase, IContextMenuItem, IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示文本</para>
    /// <para lang="en">The text to display.</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    ///  <para lang="en">The CSS class name that represents an icon (if any)</para>
    /// </summary>
    /// <example>
    /// <code>
    /// Icon="fa-solid fa-bookmark"
    /// </code> 
    /// </example>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用 默认 false 优先级低于 <see cref="OnDisabledCallback"/></para>
    /// <para lang="en">Flags whether the item is disabled. Default is <see langword="false"/>. It has a lower priority than <see cref="OnDisabledCallback"/>.</para>
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用回调方法 默认 null 优先级高于 <see cref="Disabled"/></para>
    /// <para lang="en">Defines the callback to determine if the item is disabled. Default is <see langword="null" />. It has a higher priority than <see cref="Disabled"/>.</para>
    /// </summary>
    [Parameter]
    public Func<ContextMenuItem, object?, bool>? OnDisabledCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击回调方法 默认 null</para>
    /// <para lang="en">Defines the click callback. Default is <see langword="null" />.</para>
    /// </summary>
    [Parameter]
    public Func<ContextMenuItem, object?, Task>? OnClick { get; set; }

    [CascadingParameter]
    [NotNull]
    private ContextMenu? ContextMenu { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ContextMenu.AddItem(this);
    }

    private bool disposedValue;

    /// <summary>
    /// <para lang="zh">释放资源方法</para>
    /// <para lang="en">Method to release resources.</para>
    /// </summary>
    /// <param name="disposing"><para lang="zh">是否释放托管资源</para><para lang="en">Whether to release managed resources</para></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                ContextMenu.RemoveItem(this);
            }
            disposedValue = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
