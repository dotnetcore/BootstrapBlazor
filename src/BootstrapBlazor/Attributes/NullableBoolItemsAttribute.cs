// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">可为空布尔类型转换器</para>
/// <para lang="en">Nullable boolean type converter</para>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class NullableBoolItemsAttribute : Attribute
{
    /// <summary>
    /// <para lang="zh">获得/设置 空值显示文本</para>
    /// <para lang="en">Gets or sets the display text for null value</para>
    /// </summary>
    public string? NullValueDisplayText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 True 值显示文本</para>
    /// <para lang="en">Gets or sets the display text for true value</para>
    /// </summary>
    public string? TrueValueDisplayText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 False 值显示文本</para>
    /// <para lang="en">Gets or sets the display text for false value</para>
    /// </summary>
    public string? FalseValueDisplayText { get; set; }
}
