// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Toggle Button 按钮组件
/// </summary>
public partial class ToggleButton
{
    /// <summary>
    /// 获得/设置 状态切换回调方法
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnToggleAsync { get; set; }

    /// <summary>
    /// 获得/设置 当前状态是否为激活状态 默认 false
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// 获得/设置 激活状态回调方法
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsActiveChanged { get; set; }

    private string? ToggleClassName => CssBuilder.Default(ClassName)
        .AddClass("active", IsActive)
        .Build();

    private async Task OnClickButton()
    {
        if (IsAsync)
        {
            IsAsyncLoading = true;
            IsDisabled = true;
        }

        await HandlerClick();

        // 恢复按钮
        if (IsAsync)
        {
            IsDisabled = IsKeepDisabled;
            IsAsyncLoading = false;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task HandlerClick()
    {
        IsActive = !IsActive;
        if (OnClickWithoutRender != null)
        {
            if (!IsAsync)
            {
                IsNotRender = true;
            }

            await OnClickWithoutRender();
        }

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }

        if (IsActiveChanged.HasDelegate)
        {
            await IsActiveChanged.InvokeAsync(IsActive);
        }

        if (OnToggleAsync != null)
        {
            await OnToggleAsync(IsActive);
        }
    }
}
