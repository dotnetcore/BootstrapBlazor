// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreenButton 组件
/// </summary>
public partial class FullScreenButton
{
    /// <summary>
    /// 获得/设置 退出全屏图标 默认 fa-solid fa-maximize
    /// </summary>
    [Parameter]
    [Obsolete("已启用，请使用 Icon 参数 Please use Icon parameter")]
    [ExcludeFromCodeCoverage]
    public string? FullScreenIcon { get => FullScreenExitIcon; set => FullScreenExitIcon = value; }

    /// <summary>
    /// 获得/设置 退出全屏图标 默认 fa-solid fa-compress
    /// </summary>
    [Parameter]
    public string? FullScreenExitIcon { get; set; }

    /// <summary>
    /// 获得/设置 全屏元素 Id 默认为 null
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
