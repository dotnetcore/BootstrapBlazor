// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 确认弹窗按钮组件
/// </summary>
[BootstrapModuleAutoLoader("Button/PopConfirmButton.razor.js", JSObjectReference = true)]
public abstract class PopConfirmButtonBase : ButtonBase
{
    /// <summary>
    /// 弹窗位置字符串
    /// </summary>
    protected override string? PlacementString => Placement != Placement.Auto ? Placement.ToDescriptionString() : null;

    /// <summary>
    /// Trigger 字符串
    /// </summary>
    protected override string? TriggerString => Trigger == "click" ? null : Trigger;

    /// <summary>
    /// 获得/设置 是否为 A 标签 默认 false 使用 button 渲染
    /// </summary>
    [Parameter]
    public bool IsLink { get; set; }

    /// <summary>
    /// 获得/设置 弹窗显示位置 默认 <see cref="Placement.Auto"/>
    /// </summary>
    /// <remarks>仅支持 <see cref="Placement.Auto"/> <see cref="Placement.Top"/> <see cref="Placement.Right"/> <see cref="Placement.Bottom"/> <see cref="Placement.Left"/></remarks>
    [Parameter]
    public Placement Placement { get; set; }

    /// <summary>
    /// 获得/设置 弹窗触发方式 默认 click 可设置 hover focus
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 自定义内容
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 点击确认时回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 是否显示确认按钮
    /// </summary>
    [Parameter]
    public bool ShowConfirmButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 点击关闭时回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 点击确认弹窗前回调方法 返回真时弹出弹窗 返回假时不弹出 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<Task<bool>>? OnBeforeClick { get; set; }

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Parameter]
    public Color CloseButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 关闭按钮显示文字 默认为 关闭
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮显示图标
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮显示文字 默认为 确定
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Parameter]
    public Color ConfirmButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 确认按钮显示图标
    /// </summary>
    [Parameter]
    public string? ConfirmButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 确认框图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmIcon { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 true
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// OnParametersSet 方法
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
    /// <inheritdoc/>
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
