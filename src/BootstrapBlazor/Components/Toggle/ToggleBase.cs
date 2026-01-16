// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toggle 开关组件</para>
/// <para lang="en">Toggle 开关component</para>
/// </summary>
public class ToggleBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 Style 集合</para>
    /// <para lang="en">Gets Style collection</para>
    /// </summary>
    protected virtual string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 组件宽度</para>
    /// <para lang="en">Gets or sets componentwidth</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public virtual int Width { get; set; } = 120;

    /// <summary>
    /// <para lang="zh">获得/设置 组件 On 时显示文本</para>
    /// <para lang="en">Gets or sets component On 时display文本</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public virtual string? OnText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件 Off 时显示文本</para>
    /// <para lang="en">Gets or sets component Off 时display文本</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public virtual string? OffText { get; set; }
}
