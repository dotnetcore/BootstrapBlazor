// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">排序顺序枚举类型</para>
/// <para lang="en">排序顺序enumtype</para>
/// </summary>
public enum SortOrder
{
    /// <summary>
    /// <para lang="zh">未设置</para>
    /// <para lang="en">未Sets</para>
    /// </summary>
    Unset,
    /// <summary>
    /// <para lang="zh">升序 0-9 A-Z</para>
    /// <para lang="en">升序 0-9 A-Z</para>
    /// </summary>
    Asc,
    /// <summary>
    /// <para lang="zh">降序 9-0 Z-A</para>
    /// <para lang="en">降序 9-0 Z-A</para>
    /// </summary>
    Desc,
}
