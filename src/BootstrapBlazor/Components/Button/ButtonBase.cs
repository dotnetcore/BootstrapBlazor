// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// Button 按钮组件
/// </summary>
public abstract class ButtonBase : TooltipComponentBase
{
    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    protected string? ClassName => CssBuilder.Default("btn")
        .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
        .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass("btn-block", IsBlock)
        .AddClass("is-round", ButtonStyle == ButtonStyle.Round)
        .AddClass("is-circle", ButtonStyle == ButtonStyle.Circle)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 按钮 disabled 属性
    /// </summary>
    protected string? Disabled => IsDisabled ? "disabled" : null;

    /// <summary>
    /// 获得 按钮 tabindex 属性
    /// </summary>
    protected string? Tab => IsDisabled ? "-1" : null;

    /// <summary>
    /// 按钮点击回调方法，内置支持 IsAsync 开关
    /// </summary>
    protected EventCallback<MouseEventArgs> OnClickButton { get; set; }

    /// <summary>
    /// 获得/设置 实际按钮渲染图标
    /// </summary>
    protected string? ButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 按钮风格枚举
    /// </summary>
    [Parameter]
    public ButtonStyle ButtonStyle { get; set; }

    /// <summary>
    /// 获得/设置 按钮类型 Submit 为表单提交按钮 Reset 为表单重置按钮 默认为 Button
    /// </summary>
    [Parameter]
    public ButtonType ButtonType { get; set; } = ButtonType.Button;

    /// <summary>
    /// 获得/设置 OnClick 事件
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// 获得/设置 OnClick 事件不刷新父组件
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickWithoutRender { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 正在加载动画图标 默认为 fa fa-spin fa-spinner
    /// </summary>
    [Parameter]
    public string LoadingIcon { get; set; } = "fa fa-fw fa-spin fa-spinner";

    /// <summary>
    /// 获得/设置 是否为异步按钮，默认为 false 如果为 true 表示是异步按钮，点击按钮后禁用自身并且等待异步完成，过程中显示 loading 动画
    /// </summary>
    [Parameter]
    public bool IsAsync { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 Outline 样式 默认 false
    /// </summary>
    [Parameter]
    public bool IsOutline { get; set; }

    /// <summary>
    /// 获得/设置 Size 大小
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.None;

    /// <summary>
    /// 获得/设置 Block 模式
    /// </summary>
    [Parameter]
    public bool IsBlock { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 点击时间是否向上传播 默认 false
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; }

    /// <summary>
    /// 获得/设置 RenderFragment 实例
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否当前正在异步执行操作
    /// </summary>
    protected bool IsAsyncLoading { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ButtonIcon = Icon;

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

        async Task HandlerClick()
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
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!IsAsyncLoading)
        {
            ButtonIcon = Icon;
        }
    }

    private bool _prevDisable;
    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender && Tooltip != null)
        {
            var id = RetrieveId();
            if (_prevDisable != IsDisabled)
            {
                _prevDisable = IsDisabled;
                if (IsDisabled)
                {
                    // TODO: id 不可为空整理 主要是 AvatarUpload 的 Id 处理
                    if (Tooltip.PopoverType == PopoverType.Tooltip)
                    {
                        await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", id!, "dispose");
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync(null, "bb_popover", id!, "dispose");
                    }
                }
                else
                {
                    if (Tooltip.PopoverType == PopoverType.Tooltip)
                    {
                        await ShowTooltip();
                    }
                    else
                    {
                        await ShowPopover();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 设置按钮是否可用状态
    /// </summary>
    /// <param name="disable"></param>
    public void SetDisable(bool disable)
    {
        IsDisabled = disable;
        StateHasChanged();
    }
}
