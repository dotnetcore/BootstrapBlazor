// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件列可见性类
/// </summary>
/// <param name="name"></param>
/// <param name="visible"></param>
[JsonConverter(typeof(ColumnVisibleItemConverter))]
public class ColumnVisibleItem(string name, bool visible)
{
    /// <summary>
    /// 获得 列名称
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// 获得 列名称
    /// </summary>
    [JsonIgnore]
    public string? DisplayName { get; set; }

    /// <summary>
    /// 获得 列可见性
    /// </summary>
    public bool Visible { get; set; } = visible;
}
