// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Button 按钮组件</para>
/// <para lang="en">Button component</para>
/// </summary>
[BootstrapModuleAutoLoader("Button/Button.razor.js")]
public abstract class ButtonBase : TooltipWrapperBase
{
    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Gets the button style collection</para>
    /// </summary>
    protected string? ClassName => CssBuilder.Default("btn")
        .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
        .AddClass($"btn-{Color.ToDescriptionString()}", !IsOutline && Color != Color.None)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass("btn-block", IsBlock)
        .AddClass("btn-round", ButtonStyle == ButtonStyle.Round)
        .AddClass("btn-circle", ButtonStyle == ButtonStyle.Circle)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 按钮 disabled 属性</para>
    /// <para lang="en">Gets the button disabled attribute</para>
    /// </summary>
    protected string? Disabled => IsDisabled ? "disabled" : null;

    /// <summary>
    /// <para lang="zh">获得 按钮 aria-disabled 属性</para>
    /// <para lang="en">Gets the button aria-disabled attribute</para>
    /// </summary>
    protected string DisabledString => IsDisabled ? "true" : "false";

    /// <summary>
    /// <para lang="zh">获得 按钮 tab index 属性</para>
    /// <para lang="en">Gets the button tab index attribute</para>
    /// </summary>
    protected string? Tab => IsDisabled ? "-1" : null;

    /// <summary>
    /// <para lang="zh">获得/设置 按钮风格枚举</para>
    /// <para lang="en">Gets or sets the button style enum</para>
    /// </summary>
    [Parameter]
    public ButtonStyle ButtonStyle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮类型 Submit 为表单提交按钮 Reset 为表单重置按钮 默认为 Button</para>
    /// <para lang="en">Gets or sets the button type. Submit for form submission, Reset for form reset. Default is Button</para>
    /// </summary>
    [Parameter]
    public ButtonType ButtonType { get; set; } = ButtonType.Button;

    /// <summary>
    /// <para lang="zh">获得/设置 OnClick 事件</para>
    /// <para lang="en">Gets or sets the OnClick event</para>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OnClick 事件不刷新父组件</para>
    /// <para lang="en">Gets or sets the OnClickWithoutRender event</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickWithoutRender { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮颜色 默认 <see cref="Color.Primary"/></para>
    /// <para lang="en">Gets or sets the button color. Default is <see cref="Color.Primary"/></para>
    /// </summary>
    [Parameter]
    public virtual Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 显示图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 正在加载动画图标 默认为 fa-solid fa-spin fa-spinner</para>
    /// <para lang="en">Gets or sets the loading icon. Default is fa-solid fa-spin fa-spinner</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为异步按钮，默认为 false 如果为 true 表示是异步按钮，点击按钮后禁用自身并且等待异步完成，过程中显示 loading 动画</para>
    /// <para lang="en">Gets or sets whether it is an async button. Default is false. If true, it disables itself after click and shows loading animation until async operation completes</para>
    /// </summary>
    [Parameter]
    public bool IsAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否异步结束后是否保持禁用状态，默认为 false</para>
    /// <para lang="en">Gets or sets whether to keep disabled state after async operation. Default is false</para>
    /// </summary>
    /// <remarks><para lang="zh"><see cref="IsAsync"/> 开启时有效</para><para lang="en">Effective when <see cref="IsAsync"/> is true</para></remarks>
    [Parameter]
    public bool IsKeepDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Gets or sets the display text</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Outline 样式 默认 false</para>
    /// <para lang="en">Gets or sets the Outline style. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsOutline { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Size 大小</para>
    /// <para lang="en">Gets or sets the Size</para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Block 模式</para>
    /// <para lang="en">Gets or sets the Block mode</para>
    /// </summary>
    [Parameter]
    public bool IsBlock { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认为 false</para>
    /// <para lang="en">Gets or sets whether it is disabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击事件是否向上传播 默认 false</para>
    /// <para lang="en">Gets or sets whether to propagate click event. Default is false</para>
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 RenderFragment 实例</para>
    /// <para lang="en">Gets or sets the RenderFragment instance</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得 ValidateForm 实例</para>
    /// <para lang="en">Gets the ValidateForm instance</para>
    /// </summary>
    [CascadingParameter]
    protected ValidateForm? ValidateForm { get; set; }

    /// <summary>
    /// <para lang="zh">获得 IconTheme 实例</para>
    /// <para lang="en">Gets the IconTheme instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否当前正在异步执行操作</para>
    /// <para lang="en">Gets or sets whether an async operation is currently executing</para>
    /// </summary>
    protected bool IsAsyncLoading { get; set; }

    private string? _lastTooltipText;

    /// <summary>
    /// <para lang="zh">获得 异步属性字符串</para>
    /// <para lang="en">Gets the async attribute string</para>
    /// </summary>
    protected string? IsAsyncString => IsAsync ? "true" : null;

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (IsAsync && ValidateForm != null)
        {
            // 开启异步操作时与 ValidateForm 联动
            ValidateForm.RegisterAsyncSubmitButton(this);
        }
    }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonLoadingIcon);

        if (Tooltip != null && !string.IsNullOrEmpty(TooltipText))
        {
            Tooltip.SetParameters(TooltipText, TooltipPlacement, TooltipTrigger);
        }
    }

    private bool _prevDisable;
    /// <summary>
    /// <para lang="zh">OnAfterRenderAsync 方法</para>
    /// <para lang="en">OnAfterRenderAsync method</para>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _prevDisable = IsDisabled;
            _lastTooltipText = TooltipText;
            if (!IsDisabled)
            {
                await ShowTooltip();
            }
        }
        else if (_prevDisable != IsDisabled)
        {
            _prevDisable = IsDisabled;
            if (IsDisabled)
            {
                await RemoveTooltip();
            }
            else
            {
                await ShowTooltip();
            }
        }
        else if (Tooltip == null && _lastTooltipText != TooltipText)
        {
            _lastTooltipText = TooltipText;
            if (!IsDisabled)
            {
                await ShowTooltip();
            }
        }
    }

    /// <summary>
    /// <para lang="zh">处理点击方法</para>
    /// <para lang="en">Handle click method</para>
    /// </summary>
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

    /// <summary>
    /// <para lang="zh">设置按钮是否可用状态</para>
    /// <para lang="en">Set button disabled state</para>
    /// </summary>
    /// <param name="disable"></param>
    public void SetDisable(bool disable)
    {
        IsDisabled = disable;
        StateHasChanged();
    }

    /// <summary>
    /// <para lang="zh">触发按钮异步操作方法</para>
    /// <para lang="en">Trigger button async operation</para>
    /// </summary>
    /// <param name="loading"><para lang="zh">true 时显示正在操作 false 时表示结束</para><para lang="en">true to show loading, false to indicate completion</para></param>
    internal void TriggerAsync(bool loading)
    {
        IsAsyncLoading = loading;
        SetDisable(loading);
    }

    /// <summary>
    /// <para lang="zh">显示 Tooltip 方法</para>
    /// <para lang="en">Show Tooltip method</para>
    /// </summary>
    public virtual async Task ShowTooltip()
    {
        if (Tooltip == null && !string.IsNullOrEmpty(TooltipText))
        {
            await InvokeVoidAsync("showTooltip", Id, TooltipText);
        }
    }

    /// <summary>
    /// <para lang="zh">销毁 Tooltip 方法</para>
    /// <para lang="en">Remove Tooltip method</para>
    /// </summary>
    public virtual async Task RemoveTooltip()
    {
        if (Tooltip == null)
        {
            await InvokeVoidAsync("removeTooltip", Id);
        }
    }

    /// <summary>
    /// <para lang="zh">DisposeAsyncCore 方法</para>
    /// <para lang="en">DisposeAsyncCore method</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            await RemoveTooltip();
        }
        await base.DisposeAsync(disposing);
    }
}
