// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

        PlaceHolder ??= FieldIdentifier.HasValue ? FieldIdentifier.Value.GetDisplayName() : "PlaceHolder";
    }
}
