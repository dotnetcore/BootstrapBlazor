// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toast 组件</para>
/// <para lang="en">Toast Component</para>
/// </summary>
public partial class Toast
{
    private string? AutoHide => Options.IsAutoHide ? null : "false";

    private string? ClassString => CssBuilder.Default("toast")
        .AddClass(Options.ClassString)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ProgressClass => CssBuilder.Default("toast-progress")
        .AddClass($"bg-{Options.Category.ToDescriptionString()}")
        .Build();

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

    private string? DelayString => Options.IsAutoHide ? Options.Delay.ToString() : null;

    private string? AnimationString => Options.Animation ? null : "false";

    /// <summary>
    /// <para lang="zh">获得/设置 ToastOption 实例</para>
    /// <para lang="en">Gets or sets the ToastOption instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public ToastOption? Options { get; set; }

    [CascadingParameter]
    private ToastContainer? ToastContainer { get; set; }

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
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(Close));

    /// <summary>
    /// <para lang="zh">关闭 Toast 方法</para>
    /// <para lang="en">Closes the Toast</para>
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
