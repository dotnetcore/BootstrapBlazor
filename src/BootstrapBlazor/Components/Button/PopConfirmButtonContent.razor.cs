// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Popover Confirm 组件
/// </summary>
[BootstrapModuleAutoLoader("Button/PopConfirmButtonContent.razor.js", AutoInvokeInit = false, AutoInvokeDispose = false)]
public partial class PopConfirmButtonContent
{
    /// <summary>
    /// 获得 关闭按钮样式
    /// </summary>
    private string? CloseButtonClass => CssBuilder.Default("btn btn-xs")
        .AddClass($"btn-{CloseButtonColor.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// 获得 关闭按钮样式
    /// </summary>
    private string? ConfirmButtonClass => CssBuilder.Default("btn btn-xs")
        .AddClass($"btn-{ConfirmButtonColor.ToDescriptionString()}")
        .Build();

    private string? IconString => CssBuilder.Default("text-info")
        .AddClass(Icon)
        .Build();

    /// <summary>
    /// 获得/设置 是否显示确认按钮
    /// </summary>
    [Parameter]
    public bool ShowConfirmButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示关闭按钮
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 自定义组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮显示图标
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮显示文字
    /// </summary>
    [Parameter]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Parameter]
    public Color CloseButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 确认按钮显示图标
    /// </summary>
    [Parameter]
    public string? ConfirmButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮显示文字
    /// </summary>
    [Parameter]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Parameter]
    public Color ConfirmButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 确认框图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.PopConfirmButtonConfirmIcon);
        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.PopConfirmButtonContentCloseButtonIcon);
        ConfirmButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.PopConfirmButtonContentConfirmButtonIcon);
    }

    /// <summary>
    /// 点击关闭按钮调用此方法
    /// </summary>
    public async Task OnCloseClick()
    {
        await InvokeVoidAsync("hide", Id);
        if (OnClose != null)
        {
            await OnClose();
        }
    }

    /// <summary>
    /// 点击确认按钮调用此方法
    /// </summary>
    public async Task OnConfirmClick()
    {
        await InvokeVoidAsync("hide", Id);
        if (OnConfirm != null)
        {
            await OnConfirm();
        }
    }
}
