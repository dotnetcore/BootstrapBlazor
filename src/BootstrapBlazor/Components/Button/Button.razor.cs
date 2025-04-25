// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Button 按钮组件
/// </summary>
public partial class Button : ButtonBase
{
    /// <summary>
    /// 获得/设置 是否自动获取焦点 默认 false 不自动获取焦点
    /// </summary>
    [Parameter]
    public bool IsAutoFocus { get; set; }

    /// <summary>
    /// 获得/设置 html button 实例
    /// </summary>
    protected ElementReference ButtonElement { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (IsAutoFocus)
            {
                await FocusAsync();
            }
        }
    }

    /// <summary>
    /// OnClickButton 方法
    /// </summary>
    protected virtual async Task OnClickButton()
    {
        if (IsAsync && ButtonType == ButtonType.Button)
        {
            IsAsyncLoading = true;
            IsDisabled = true;
        }

        await HandlerClick();

        // 恢复按钮
        if (IsAsync && ButtonType == ButtonType.Button)
        {
            IsDisabled = IsKeepDisabled;
            IsAsyncLoading = false;
        }
    }

    /// <summary>
    /// 自动获得焦点方法
    /// </summary>
    /// <returns></returns>
    public ValueTask FocusAsync() => ButtonElement.FocusAsync();

    /// <summary>
    /// 处理点击方法
    /// </summary>
    /// <returns></returns>
    protected virtual async Task HandlerClick()
    {
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
    }
}
