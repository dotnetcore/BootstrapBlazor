// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Toggle 开关组件
/// </summary>
public class ToggleBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得 Style 集合
    /// </summary>
    protected virtual string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .Build();

    /// <summary>
    /// 获得/设置 组件宽度
    /// </summary>
    [Parameter]
    public virtual int Width { get; set; } = 120;

    /// <summary>
    /// 获得/设置 组件 On 时显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public virtual string? OnText { get; set; }

    /// <summary>
    /// 获得/设置 组件 Off 时显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public virtual string? OffText { get; set; }
}
