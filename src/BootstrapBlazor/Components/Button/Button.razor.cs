// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Button
{
    /// <summary>
    /// 获得/设置 是否自动获取焦点 默认 false 不自动获取焦点
    /// </summary>
    [Parameter]
    public bool IsAutoFocus { get; set; }

    /// <summary>
    /// 按钮点击回调方法，内置支持 IsAsync 开关
    /// </summary>
    protected EventCallback<MouseEventArgs> OnClickButton { get; set; }

    /// <summary>
    /// 获得 ValidateForm 实例
    /// </summary>
    [CascadingParameter]
    protected ValidateForm? ValidateForm { get; set; }

    /// <summary>
    /// 获得/设置 html button 实例
    /// </summary>
    protected ElementReference ButtonElement { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnClickButton = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
        {
            if (IsAsync && ButtonType == ButtonType.Button)
            {
                IsAsyncLoading = true;
                ButtonIcon = LoadingIcon;
                IsDisabled = true;
            }

            Exception? exception = null;
            try
            {
                if (IsAsync)
                {

                    await Task.Run(async () => await InvokeAsync(HandlerClick));
                }
                else
                {
                    await HandlerClick();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // 恢复按钮
            if (IsAsync && ButtonType == ButtonType.Button)
            {
                ButtonIcon = Icon;
                IsDisabled = false;
                IsAsyncLoading = false;
            }

            if (exception != null)
            {
                // 如果有异常发生强制按钮恢复
                StateHasChanged();
                throw exception;
            }
        });

        if (IsAsync && ValidateForm != null)
        {
            // 开启异步操作时与 ValidateForm 联动
            ValidateForm.RegisterAsyncSubmitButton(this);
        }
    }

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
            await OnClickWithoutRender.Invoke();
        }
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }

    /// <summary>
    /// 触发按钮异步操作方法
    /// </summary>
    /// <param name="loading">true 时显示正在操作 false 时表示结束</param>
    internal void TriggerAsync(bool loading)
    {
        IsAsyncLoading = loading;
        ButtonIcon = loading ? LoadingIcon : Icon;
        SetDisable(loading);
    }
}
