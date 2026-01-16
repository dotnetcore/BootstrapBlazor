// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">确认弹窗按钮组件</para>
/// <para lang="en">PopConfirm Button component</para>
/// </summary>
[BootstrapModuleAutoLoader("Button/PopConfirmButton.razor.js", JSObjectReference = true)]
public abstract class PopConfirmButtonBase : ButtonBase
{
    /// <summary>
    /// <para lang="zh">弹窗位置字符串</para>
    /// <para lang="en">Placement string</para>
    /// </summary>
    protected override string? PlacementString => Placement != Placement.Auto ? Placement.ToDescriptionString() : null;

    /// <summary>
    /// <para lang="zh">Trigger 字符串</para>
    /// <para lang="en">Trigger string</para>
    /// </summary>
    protected override string? TriggerString => Trigger == "click" ? null : Trigger;

    /// <summary>
    /// <para lang="zh">获得/设置 是否为 A 标签 默认 false 使用 button 渲染</para>
    /// <para lang="en">Gets or sets whether it is an anchor tag. Default is false (renders as button)</para>
    /// </summary>
    [Parameter]
    public bool IsLink { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗显示位置 默认 <see cref="Placement.Auto"/></para>
    /// <para lang="en">Gets or sets the popup placement. Default is <see cref="Placement.Auto"/></para>
    /// </summary>
    /// <remarks><para lang="zh">仅支持 <see cref="Placement.Auto"/> <see cref="Placement.Top"/> <see cref="Placement.Right"/> <see cref="Placement.Bottom"/> <see cref="Placement.Left"/></para><para lang="en">Only supports <see cref="Placement.Auto"/> <see cref="Placement.Top"/> <see cref="Placement.Right"/> <see cref="Placement.Bottom"/> <see cref="Placement.Left"/></para></remarks>
    [Parameter]
    public Placement Placement { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗触发方式 默认 click 可设置 hover focus</para>
    /// <para lang="en">Gets or sets the popup trigger method. Default is click (can be hover, focus)</para>
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Gets or sets the display text</para>
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义内容</para>
    /// <para lang="en">Gets or sets the custom content</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击确认时回调方法</para>
    /// <para lang="en">Gets or sets the callback method when confirm is clicked</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示确认按钮</para>
    /// <para lang="en">Gets or sets whether to show the confirm button</para>
    /// </summary>
    [Parameter]
    public bool ShowConfirmButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 点击关闭时回调方法</para>
    /// <para lang="en">Gets or sets the callback method when close is clicked</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮</para>
    /// <para lang="en">Gets or sets whether to show the close button</para>
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 点击确认弹窗前回调方法 返回真时弹出弹窗 返回假时不弹出 默认 null</para>
    /// <para lang="en">Gets or sets the callback method before showing the confirm popup. Returns true to show, false to prevent. Default is null</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task<bool>>? OnBeforeClick { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示标题</para>
    /// <para lang="en">Gets or sets the title</para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮颜色</para>
    /// <para lang="en">Gets or sets the close button color</para>
    /// </summary>
    [Parameter]
    public Color CloseButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮显示文字 默认为 关闭</para>
    /// <para lang="en">Gets or sets the close button text. Default is Close</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮显示图标</para>
    /// <para lang="en">Gets or sets the close button icon</para>
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮显示文字 默认为 确定</para>
    /// <para lang="en">Gets or sets the confirm button text. Default is OK</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮颜色</para>
    /// <para lang="en">Gets or sets the confirm button color</para>
    /// </summary>
    [Parameter]
    public Color ConfirmButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮显示图标</para>
    /// <para lang="en">Gets or sets the confirm button icon</para>
    /// </summary>
    [Parameter]
    public string? ConfirmButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认框图标</para>
    /// <para lang="en">Gets or sets the confirm icon</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式 默认 null</para>
    /// <para lang="en">Gets or sets the custom class. Default is null</para>
    /// </summary>
    /// <remarks><para lang="zh">由 data-bs-custom-class 实现</para><para lang="en">Implemented by data-bs-custom-class</para></remarks>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示阴影 默认 true</para>
    /// <para lang="en">Gets or sets whether to show shadow. Default is true</para>
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ConfirmIcon ??= IconTheme.GetIconByKey(ComponentIcons.PopConfirmButtonConfirmIcon);
        Trigger ??= "click";

        if (Placement != Placement.Top && Placement != Placement.Right && Placement != Placement.Bottom && Placement != Placement.Left)
        {
            Placement = Placement.Auto;
        }
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(TriggerCloseCallback));

    /// <summary>
    /// Trigger OnClose event callback.
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerCloseCallback()
    {
        if (OnClose != null)
        {
            await OnClose();
        }
    }
}
