// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DropdownItem 组件</para>
/// <para lang="en">DropdownItem Component</para>
/// </summary>
public partial class DropdownItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示文本</para>
    /// <para lang="en">Gets or sets Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用 默认 false 优先级低于 <see cref="OnDisabledCallback"/></para>
    /// <para lang="en">Gets or sets Disabled. Default is false. Priority lower than <see cref="OnDisabledCallback"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用回调方法 默认 null 优先级高于 <see cref="Disabled"/></para>
    /// <para lang="en">Gets or sets Disabled Callback. Default is null. Priority higher than <see cref="Disabled"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<bool>? OnDisabledCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击回调方法 默认 null</para>
    /// <para lang="en">Gets or sets Click Callback. Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件内容</para>
    /// <para lang="en">Gets or sets Child Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ItemIconString => CssBuilder.Default("dropdown-item-icon")
        .AddClass(Icon, !string.IsNullOrEmpty(Icon))
        .Build();

    private string? ItemClassString => CssBuilder.Default("dropdown-item")
        .AddClass("disabled", IsDisabled)
        .Build();

    private bool IsDisabled => OnDisabledCallback?.Invoke() ?? Disabled;

    private async Task OnClickItem()
    {
        if (OnClick != null)
        {
            await OnClick();
        }
    }
}
