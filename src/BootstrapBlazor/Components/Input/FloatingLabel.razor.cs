// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// FloatingLabel 组件
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class FloatingLabel<TValue>
{
    private string? ClassString => CssBuilder.Default("form-floating")
        .AddClass("is-group", IsGroupBox)
        .Build();

    /// <summary>
    /// 获得/设置 是否为 GroupBox 样式 默认 false
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
