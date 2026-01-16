// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Switch 组件</para>
/// <para lang="en">Switch Component</para>
/// </summary>
public partial class Switch
{
    private string? ClassName => CssBuilder.Default("switch")
        .AddClass("is-checked", Value)
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? CoreClassName => CssBuilder.Default("switch-core")
        .AddClass($"border-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
        .AddClass($"bg-{OnColor.ToDescriptionString()}", OnColor != Color.None && Value)
        .AddClass($"border-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
        .AddClass($"bg-{OffColor.ToDescriptionString()}", OffColor != Color.None && !Value)
        .Build();

    private string? GetInnerText()
    {
        string? ret = null;
        if (ShowInnerText)
        {
            ret = Value ? OnInnerText : OffInnerText;
        }

        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 显示文字</para>
    /// <para lang="en">Get Text</para>
    /// </summary>
    private string? Text => Value ? OnText : OffText;

    /// <summary>
    /// <para lang="zh">获得 组件最小宽度</para>
    /// <para lang="en">Get Component Minimum Width</para>
    /// </summary>
    private string? SwitchStyleName => CssBuilder.Default()
        .AddClass($"min-width: {Width}px;", Width > 0)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 Style 集合</para>
    /// <para lang="en">Get Style Name</para>
    /// </summary>
    protected override string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Height}px;", Height >= 20)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 开颜色</para>
    /// <para lang="en">Get/Set On Color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color OnColor { get; set; } = Color.Success;

    /// <summary>
    /// <para lang="zh">获得/设置 关颜色</para>
    /// <para lang="en">Get/Set Off Color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color OffColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件宽度 默认 40</para>
    /// <para lang="en">Get/Set Component Width. Default 40</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public override int Width { get; set; } = 40;

    /// <summary>
    /// <para lang="zh">获得/设置 控件高度默认 20px</para>
    /// <para lang="en">Get/Set Component Height. Default 20px</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 20;

    /// <summary>
    /// <para lang="zh">获得/设置 组件 On 时内置显示文本</para>
    /// <para lang="en">Get/Set On Inner Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OnInnerText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件 Off 时内置显示文本</para>
    /// <para lang="en">Get/Set Off Inner Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OffInnerText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示内置文字 默认 false 显示</para>
    /// <para lang="en">Get/Set Whether to show inner text. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowInnerText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Switch>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">OnInitialized 方法</para>
    /// <para lang="en">OnInitialized Method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnInnerText ??= Localizer[nameof(OnInnerText)];
        OffInnerText ??= Localizer[nameof(OffInnerText)];
    }

    /// <summary>
    /// <para lang="zh">点击控件时触发此方法</para>
    /// <para lang="en">Method triggered when clicking the control</para>
    /// </summary>
    private async Task OnClick()
    {
        if (!IsDisabled)
        {
            Value = !Value;

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            // 回调 OnValueChanged 再调用 EventCallback
            if (OnValueChanged != null)
            {
                await OnValueChanged(Value);
            }
        }
    }
}
