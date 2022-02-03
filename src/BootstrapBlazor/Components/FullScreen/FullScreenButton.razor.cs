// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class FullScreenButton
{
    /// <summary>
    /// 获得/设置 按钮图标 默认 fa fa-arrows-alt
    /// </summary>
    [Parameter]
    public string? ButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 全屏图标 默认 fa fa-arrows-alt
    /// </summary>
    [Parameter]
    public string? FullScreenIcon { get; set; }

    /// <summary>
    /// 获得/设置 鼠标悬浮提示条信息
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    [Inject]
    [NotNull]
    private FullScreenService? FullScrenService { get; set; }

    private string? ClassString => CssBuilder.Default("bb-fs btn btn-outline-link")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ButtonIconString => CssBuilder.Default()
        .AddClass(ButtonIcon)
        .AddClass("fs", !string.IsNullOrEmpty(FullScreenIcon))
        .Build();

    private string? FullScreenIconString => CssBuilder.Default("fs-on")
        .AddClass(FullScreenIcon)
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ButtonIcon ??= "fa fa-arrows-alt";
    }

    private Task ToggleFullScreen() => FullScrenService.Toggle();
}
