// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FullScreenButton 组件</para>
/// <para lang="en">FullScreenButton Component</para>
/// </summary>
public partial class FullScreenButton
{
    /// <summary>
    /// <para lang="zh">获得/设置 全屏图标 默认 fa-solid fa-maximize</para>
    /// <para lang="en">Gets or sets Full Screen Icon Default fa-solid fa-maximize</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 Icon 参数；Deprecated. Please use Icon parameter")]
    [ExcludeFromCodeCoverage]
    public string? FullScreenIcon { get => FullScreenExitIcon; set => FullScreenExitIcon = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 退出全屏图标 默认 fa-solid fa-compress</para>
    /// <para lang="en">Gets or sets Exit Full Screen Icon Default fa-solid fa-compress</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? FullScreenExitIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 全屏元素 Id 默认为 null</para>
    /// <para lang="en">Gets or sets Full Screen Element Id Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? TargetId { get; set; }

    [Inject]
    [NotNull]
    private FullScreenService? FullScreenService { get; set; }

    private string? ClassString => CssBuilder.Default("btn btn-fs")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ButtonIconString => CssBuilder.Default(Icon)
        .AddClass("bb-fs-off")
        .Build();

    private string? FullScreenExitIconString => CssBuilder.Default(FullScreenExitIcon)
        .AddClass("bb-fs-on")
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.FullScreenButtonIcon);
        FullScreenExitIcon ??= IconTheme.GetIconByKey(ComponentIcons.FullScreenExitButtonIcon);
    }

    private Task ToggleFullScreen() => FullScreenService.Toggle(new FullScreenOption() { Id = TargetId });
}
