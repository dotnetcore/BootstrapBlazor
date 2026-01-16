// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 组件列可见性类
///</para>
/// <para lang="en">Table component列可见性类
///</para>
/// </summary>
/// <param name="name"></param>
/// <param name="visible"></param>
[JsonConverter(typeof(ColumnVisibleItemConverter))]
public class ColumnVisibleItem(string name, bool visible)
{
    /// <summary>
    /// <para lang="zh">获得 列名称
    ///</para>
    /// <para lang="en">Gets 列名称
    ///</para>
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// <para lang="zh">获得 列名称
    ///</para>
    /// <para lang="en">Gets 列名称
    ///</para>
    /// </summary>
    [JsonIgnore]
    public string? DisplayName { get; set; }

    /// <summary>
    /// <para lang="zh">获得 列可见性
    ///</para>
    /// <para lang="en">Gets 列可见性
    ///</para>
    /// </summary>
    public bool Visible { get; set; } = visible;
}
