// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ToastBox 组件
/// </summary>
[JSModuleAutoLoader(JSObjectReference = true)]
public partial class Toast
{
    /// <summary>
    /// 获得/设置 弹出框类型
    /// </summary>
    private string? AutoHide => Options.IsAutoHide ? null : "false";

    /// <summary>
    /// 获得/设置 弹出框类型
    /// </summary>
    private string? ClassString => CssBuilder.Default("toast")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 进度条样式
    /// </summary>
    private string? ProgressClass => CssBuilder.Default("toast-progress")
        .AddClass($"bg-{Options.Category.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// 获得/设置 图标样式
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

    /// <summary>
    /// 获得/设置 弹出框自动关闭时长
    /// </summary>
    protected string? DelayString => Options.IsAutoHide ? Convert.ToString(Options.Delay) : null;

    /// <summary>
    /// 获得/设置 是否开启动画效果 
    /// </summary>
    protected string? AnimationString => Options.Animation ? null : "false";

    /// <summary>
    /// 获得/设置 ToastOption 实例
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public ToastOption? Options { get; set; }

    /// <summary>
    /// 获得/设置 Toast 实例
    /// </summary>
    /// <value></value>
    [CascadingParameter]
    protected ToastContainer? ToastContainer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Options.Toast = this;
    }

    /// <summary>
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(Close));

    /// <summary>
    /// 清除 ToastBox 方法
    /// </summary>
    [JSInvokable]
    public void Close()
    {
        ToastContainer?.Close(Options);
    }
}
