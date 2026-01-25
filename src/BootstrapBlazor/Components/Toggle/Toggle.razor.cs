// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toggle 组件</para>
/// <para lang="en">Toggle Component</para>
/// </summary>
public partial class Toggle
{
    private string? ClassName => CssBuilder.Default("btn btn-toggle")
        .AddClass("off", !Value)
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? ToggleOnClassString => CssBuilder.Default("toggle on")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
        .Build();

    private string? WrapperClassString => CssBuilder.Default("toggle")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 组件颜色，默认为 Success</para>
    /// <para lang="en">Gets or sets the component color. Default is Success.</para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Success;

    [Inject]
    [NotNull]
    private IStringLocalizer<Toggle>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        OnText ??= Localizer[nameof(OnText)];
        OffText ??= Localizer[nameof(OffText)];
    }

    private async Task OnClick()
    {
        if (!IsDisabled)
        {
            Value = !Value;
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            OnValueChanged?.Invoke(Value);
        }
    }
}
