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
    /// 获得/设置 On 状态显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OnText { get; set; }

    /// <summary>
    /// 获得/设置 Off 状态显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OffText { get; set; }

    /// <summary>
    /// 获得/设置 当前状态
    /// </summary>
    [Parameter]
    public bool ToggleState { get; set; }

    /// <summary>
    /// 状态切换回调方法
    /// </summary>
    [Parameter]
    public EventCallback<bool> ToggleStateChanged { get; set; }

    /// <summary>
    /// 点击回调方法
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SwitchButton>? Localizer { get; set; }

    /// <summary>
    /// OnParametersSet 方法
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
