// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FloatingLabel 组件</para>
/// <para lang="en">FloatingLabel Component</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class FloatingLabel<TValue>
{
    private string? ClassString => CssBuilder.Default("form-floating")
        .AddClass("is-group", IsGroupBox)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 是否为 GroupBox 样式 默认 false</para>
    /// <para lang="en">Get/Set Whether it is GroupBox style. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsGroupBox { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PlaceHolder ??= FieldIdentifier.HasValue ? FieldIdentifier.Value.GetDisplayName() : DisplayText;
    }
}
