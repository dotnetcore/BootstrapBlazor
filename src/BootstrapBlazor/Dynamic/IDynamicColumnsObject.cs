// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">动态对象接口</para>
/// <para lang="en">Dynamic object interface</para>
/// </summary>
public interface IDynamicColumnsObject : IDynamicObject
{
    /// <summary>
    /// <para lang="zh">获得设置 列与列数值集合</para>
    /// <para lang="en">Gets or sets the collection of columns and their values</para>
    /// </summary>
    public Dictionary<string, object?> Columns { get; set; }
}
