// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// OTP input component
/// </summary>
public partial class OtpInput
{
    /// <summary>
    /// Gets or sets the length of the OTP input. Default is 6.
    /// </summary>
    [Parameter]
    public int Digits { get; set; } = 6;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public OtpInputType Type { get; set; }

    private string? ClassString => CssBuilder.Default("bb-opt-input")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? SpanClassString => CssBuilder.Default("bb-opt-span")
        .AddClass(ValidCss)
        .Build();

    private char[] _values = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _values = Value?.ToCharArray() ?? [];
    }
}
