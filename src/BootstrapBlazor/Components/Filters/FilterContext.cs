// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FilterContext 类</para>
/// <para lang="en">FilterContext class</para>
/// </summary>
public class FilterContext
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否为表头行 默认为 false</para>
    /// <para lang="en">Gets or sets whether the filter is header row. Default is false</para>
    /// </summary>
    public bool IsHeaderRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列字段键 默认为 null</para>
    /// <para lang="en">Gets or sets the column field key. Default is null</para>
    /// </summary>
    public string? FieldKey { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过滤计数 默认为 0</para>
    /// <para lang="en">Gets or sets the filter counter. Default is 0</para>
    /// </summary>
    public int Count { get; set; }
}
