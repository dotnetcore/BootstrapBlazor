// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ToastBox 组件</para>
///  <para lang="en">ToastBox component</para>
/// </summary>
public partial class Toast
{
    /// <summary>
    ///  <para lang="zh">获得/设置 弹出框类型</para>
    ///  <para lang="en">Gets or sets 弹出框type</para>
    /// </summary>
    private string? AutoHide => Options.IsAutoHide ? null : "false";

    /// <summary>
    ///  <para lang="zh">获得/设置 弹出框类型</para>
    ///  <para lang="en">Gets or sets 弹出框type</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("toast")
        .AddClass(Options.ClassString)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 进度条样式</para>
    ///  <para lang="en">Gets or sets 进度条style</para>
    /// </summary>
    private string? ProgressClass => CssBuilder.Default("toast-progress")
        .AddClass($"bg-{Options.Category.ToDescriptionString()}")
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 图标样式</para>
    ///  <para lang="en">Gets or sets iconstyle</para>
    /// </summary>
    private string? IconString => CssBuilder.Default()
        .AddClass(Options.SuccessIcon, Options.Category == ToastCategory.Success)
        .AddClass(Options.InformationIcon, Options.Category == ToastCategory.Information)
        .AddClass(Options.ErrorIcon, Options.Category == ToastCategory.Error)
        .AddClass(Options.WarningIcon, Options.Category == ToastCategory.Warning)
        .Build();

    private string? IconBarString => CssBuilder.Default("toast-bar me-2")
        .AddClass("text-success", Options.Category == ToastCategory.Success)
        .AddClass("text-info", Options.Category == ToastCategory.Information)
        .AddClass("text-danger", Options.Category == ToastCategory.Error)
        .AddClass("text-warning", Options.Category == ToastCategory.Warning)
        .Build();

    private string? StyleString => CssBuilder.Default(Options.StyleString)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 弹出框自动关闭时长</para>
    ///  <para lang="en">Gets or sets 弹出框自动关闭时长</para>
    /// </summary>
    private string? DelayString => Options.IsAutoHide ? Options.Delay.ToString() : null;

    /// <summary>
    ///  <para lang="zh">获得/设置 是否开启动画效果</para>
    ///  <para lang="en">Gets or sets whether开启动画效果</para>
    /// </summary>
    private string? AnimationString => Options.Animation ? null : "false";

    /// <summary>
    ///  <para lang="zh">获得/设置 ToastOption 实例</para>
    ///  <para lang="en">Gets or sets ToastOption instance</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public ToastOption? Options { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Toast 实例</para>
    ///  <para lang="en">Gets or sets Toast instance</para>
    /// </summary>
    /// <value></value>
    [CascadingParameter]
    private ToastContainer? ToastContainer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Options.Toast = this;
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Options.SuccessIcon ??= IconTheme.GetIconByKey(ComponentIcons.ToastSuccessIcon);
        Options.InformationIcon ??= IconTheme.GetIconByKey(ComponentIcons.ToastInformationIcon);
        Options.WarningIcon ??= IconTheme.GetIconByKey(ComponentIcons.ToastWarningIcon);
        Options.ErrorIcon ??= IconTheme.GetIconByKey(ComponentIcons.ToastErrorIcon);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(Close));

    /// <summary>
    ///  <para lang="zh">清除 ToastBox 方法</para>
    ///  <para lang="en">清除 ToastBox 方法</para>
    /// </summary>
    [JSInvokable]
    public async Task Close()
    {
        if (ToastContainer != null)
        {
            await ToastContainer.Close(Options);
        }
    }
}
