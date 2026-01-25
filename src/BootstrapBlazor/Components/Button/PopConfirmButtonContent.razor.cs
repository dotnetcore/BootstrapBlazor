// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Popover Confirm 组件</para>
/// <para lang="en">Popover Confirm component</para>
/// </summary>
[BootstrapModuleAutoLoader("Button/PopConfirmButtonContent.razor.js", AutoInvokeInit = false, AutoInvokeDispose = false)]
public partial class PopConfirmButtonContent
{
    /// <summary>
    /// <para lang="zh">获得 关闭按钮样式</para>
    /// <para lang="en">Gets the close button style</para>
    /// </summary>
    private string? CloseButtonClass => CssBuilder.Default("btn btn-xs")
        .AddClass($"btn-{CloseButtonColor.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// <para lang="zh">获得 确认按钮样式</para>
    /// <para lang="en">Gets the confirm button style</para>
    /// </summary>
    private string? ConfirmButtonClass => CssBuilder.Default("btn btn-xs")
        .AddClass($"btn-{ConfirmButtonColor.ToDescriptionString()}")
        .Build();

    private string? IconString => CssBuilder.Default("text-info")
        .AddClass(Icon)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示确认按钮</para>
    /// <para lang="en">Gets or sets whether to show confirm button</para>
    /// </summary>
    [Parameter]
    public bool ShowConfirmButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮</para>
    /// <para lang="en">Gets or sets whether to show close button</para>
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 显示标题</para>
    /// <para lang="en">Gets or sets the title</para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Gets or sets the content</para>
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义组件</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮显示图标</para>
    /// <para lang="en">Gets or sets the close button icon</para>
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮显示文字</para>
    /// <para lang="en">Gets or sets the close button text</para>
    /// </summary>
    [Parameter]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮颜色</para>
    /// <para lang="en">Gets or sets the close button color</para>
    /// </summary>
    [Parameter]
    public Color CloseButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮显示图标</para>
    /// <para lang="en">Gets or sets the confirm button icon</para>
    /// </summary>
    [Parameter]
    public string? ConfirmButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮显示文字</para>
    /// <para lang="en">Gets or sets the confirm button text</para>
    /// </summary>
    [Parameter]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮颜色</para>
    /// <para lang="en">Gets or sets the confirm button color</para>
    /// </summary>
    [Parameter]
    public Color ConfirmButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 确认框图标</para>
    /// <para lang="en">Gets or sets the confirm icon</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮回调方法</para>
    /// <para lang="en">Gets or sets the confirm callback</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮回调方法</para>
    /// <para lang="en">Gets or sets the close callback</para>
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
    /// <para lang="zh">点击关闭按钮调用此方法</para>
    /// <para lang="en">Method called when close button is clicked</para>
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
    /// <para lang="zh">点击确认按钮调用此方法</para>
    /// <para lang="en">Method called when confirm button is clicked</para>
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
