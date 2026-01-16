// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Switch Button
/// </summary>
public partial class SwitchButton
{
    /// <summary>
    /// <para lang="zh">获得/设置 On 状态显示文字</para>
    /// <para lang="en">Gets or sets the On state display text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OnText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Off 状态显示文字</para>
    /// <para lang="en">Gets or sets the Off state display text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OffText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前状态</para>
    /// <para lang="en">Gets or sets the current state</para>
    /// </summary>
    [Parameter]
    public bool ToggleState { get; set; }

    /// <summary>
    /// <para lang="zh">状态切换回调方法</para>
    /// <para lang="en">State toggle callback method</para>
    /// </summary>
    [Parameter]
    public EventCallback<bool> ToggleStateChanged { get; set; }

    /// <summary>
    /// <para lang="zh">点击回调方法</para>
    /// <para lang="en">Click callback method</para>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SwitchButton>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        OnText ??= Localizer[nameof(OnText)];
        OffText ??= Localizer[nameof(OffText)];
    }

    private async Task OnToggle()
    {
        ToggleState = !ToggleState;
        if (ToggleStateChanged.HasDelegate)
        {
            await ToggleStateChanged.InvokeAsync(ToggleState);
        }
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }

    private string? GetText() => ToggleState ? OnText : OffText;
}
