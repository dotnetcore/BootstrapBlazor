// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    /// <summary>
    /// 获得/设置 下拉框是否显示阴影效果 默认 true
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    /// <summary>
    /// 获得/设置 下拉框对其方式 默认 Right
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; } = Alignment.Right;

    /// <summary>
    /// 获得/设置 主题切换回调方法
    /// </summary>
    [Parameter]
    public Func<ThemeValue, Task>? OnThemeChangedAsync { get; set; }

    /// <summary>
    /// 主题类型
    /// </summary>
    [Parameter]
    public ThemeValue ThemeValue { get; set; } = ThemeValue.UseLocalStorage;

    /// <summary>
    /// 主题类型改变回调方法
    /// </summary>
    [Parameter]
    public EventCallback<ThemeValue> ThemeValueChanged { get; set; }

    [Inject, NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<ThemeProvider>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IThemeProvider? ThemeProviderService { get; set; }

    private string? ClassString => CssBuilder.Default("dropdown bb-theme-mode")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? DropdownClassString => CssBuilder.Default("dropdown-menu")
        .AddClass($"dropdown-menu-{Alignment.ToDescriptionString()}", Alignment != Alignment.None)
        .AddClass("shadow", ShowShadow)
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, ThemeValue, nameof(OnThemeChanged));

    /// <summary>
    /// JavaScript 回调方法
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnThemeChanged(ThemeValue name)
    {
        if (ThemeValueChanged.HasDelegate)
        {
            await ThemeValueChanged.InvokeAsync(name);
        }

        if (OnThemeChangedAsync != null)
        {
            await OnThemeChangedAsync(name);
        }

        ThemeProviderService.TriggerThemeChanged(name.ToDescriptionString());
    }
}
