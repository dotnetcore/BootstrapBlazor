// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TabToolbarButton 组件</para>
/// <para lang="en">TabToolbarButton Component</para>
/// </summary>
public partial class TabToolbarButton
{
    /// <summary>
    /// <para lang="zh">获得/设置 按钮图标，默认为 null</para>
    /// <para lang="en">Gets or sets the button icon string. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮单击事件处理程序，默认为 null</para>
    /// <para lang="en">Gets or sets the button click event handler. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 提示文本，默认为 null</para>
    /// <para lang="en">Gets or sets the tooltip text. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    private string? ClassString => CssBuilder.Default("tabs-nav-toolbar-button")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnClick()
    {
        if (OnClickAsync != null)
        {
            await OnClickAsync();
        }
    }
}
