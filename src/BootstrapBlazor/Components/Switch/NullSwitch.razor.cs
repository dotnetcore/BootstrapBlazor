// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">可为空布尔值组件</para>
/// <para lang="en">Nullable Boolean Component</para>
/// </summary>
public partial class NullSwitch
{
    private string? ClassName => CssBuilder.Default("switch")
        .AddClass("is-checked", ComponentValue)
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? CoreClassName => CssBuilder.Default("switch-core")
        .AddClass($"border-{OnColor.ToDescriptionString()}", OnColor != Color.None && ComponentValue)
        .AddClass($"bg-{OnColor.ToDescriptionString()}", OnColor != Color.None && ComponentValue)
        .AddClass($"border-{OffColor.ToDescriptionString()}", OffColor != Color.None && !ComponentValue)
        .AddClass($"bg-{OffColor.ToDescriptionString()}", OffColor != Color.None && !ComponentValue)
        .Build();

    private string? GetInnerText()
    {
        string? ret = null;
        if (ShowInnerText)
        {
            ret = ComponentValue ? OnInnerText : OffInnerText;
        }
        return ret;
    }

    private string? Text => ComponentValue ? OnText : OffText;

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
    /// <para lang="en">Gets or sets On Color</para>
    /// </summary>
    [Parameter]
    public Color OnColor { get; set; } = Color.Success;

    /// <summary>
    /// <para lang="zh">获得/设置 关颜色</para>
    /// <para lang="en">Gets or sets Off Color</para>
    /// </summary>
    [Parameter]
    public Color OffColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件宽度 默认 40</para>
    /// <para lang="en">Gets or sets Component Width. Default 40</para>
    /// </summary>
    [Parameter]
    public override int Width { get; set; } = 40;

    /// <summary>
    /// <para lang="zh">获得/设置 控件高度默认 20px</para>
    /// <para lang="en">Gets or sets Component Height. Default 20px</para>
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 20;

    /// <summary>
    /// <para lang="zh">获得/设置 组件 On 时内置显示文本</para>
    /// <para lang="en">Gets or sets On Inner Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OnInnerText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件 Off 时内置显示文本</para>
    /// <para lang="en">Gets or sets Off Inner Text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? OffInnerText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示内置文字 默认 false 显示</para>
    /// <para lang="en">Gets or sets Whether to show inner text. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowInnerText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Switch>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 绑定值为空时的默认值 默认为 false</para>
    /// <para lang="en">Gets or sets Default value when null. Default false</para>
    /// </summary>
    [Parameter]
    public bool DefaultValueWhenNull { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件 Value 值</para>
    /// <para lang="en">Gets or Sets Component Value</para>
    /// </summary>
    protected bool ComponentValue => Value ?? DefaultValueWhenNull;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnInnerText ??= Localizer[nameof(OnInnerText)];
        OffInnerText ??= Localizer[nameof(OffInnerText)];
    }

    private async Task OnClick()
    {
        if (!IsDisabled)
        {
            Value = !ComponentValue;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            OnValueChanged?.Invoke(Value);
        }
    }
}
