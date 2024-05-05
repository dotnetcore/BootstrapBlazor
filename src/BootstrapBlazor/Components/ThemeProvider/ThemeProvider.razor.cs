// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ThemeProvider 组件
/// </summary>
public partial class ThemeProvider
{
    /// <summary>
    /// 获得/设置 自动模式图标 默认 null
    /// </summary>
    [Parameter]
    public string? AutoModeIcon { get; set; }

    /// <summary>
    /// 获得/设置 自动模式文本 默认 null 未设置使用本地化资源
    /// </summary>
    [Parameter]
    public string? AutoModeText { get; set; }

    /// <summary>
    /// 获得/设置 暗黑模式图标 默认 null
    /// </summary>
    [Parameter]
    public string? DarkModeIcon { get; set; }

    /// <summary>
    /// 获得/设置 暗黑模式文本 默认 null 未设置使用本地化资源
    /// </summary>
    [Parameter]
    public string? DarkModeText { get; set; }

    /// <summary>
    /// 获得/设置 明亮模式图标 默认 null
    /// </summary>
    [Parameter]
    public string? LightModeIcon { get; set; }

    /// <summary>
    /// 获得/设置 明亮模式文本 默认 null 未设置使用本地化资源
    /// </summary>
    [Parameter]
    public string? LightModeText { get; set; }

    /// <summary>
    /// 获得/设置 当前选中模式图标 默认 null
    /// </summary>
    [Parameter]
    public string? ActiveIcon { get; set; }

    [Inject, NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<ThemeProvider>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("dropdown bb-theme-mode")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AutoModeIcon ??= IconTheme.GetIconByKey(ComponentIcons.ThemeProviderAutoModeIcon);
        DarkModeIcon ??= IconTheme.GetIconByKey(ComponentIcons.ThemeProviderDarkModeIcon);
        LightModeIcon ??= IconTheme.GetIconByKey(ComponentIcons.ThemeProviderLightModeIcon);
        ActiveIcon ??= IconTheme.GetIconByKey(ComponentIcons.ThemeProviderActiveModeIcon);

        AutoModeText ??= Localizer[nameof(AutoModeText)];
        DarkModeText ??= Localizer[nameof(DarkModeText)];
        LightModeText ??= Localizer[nameof(LightModeText)];
    }
}
