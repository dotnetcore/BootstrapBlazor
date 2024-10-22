// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 排序顺序枚举类型
/// </summary>
public enum SortOrder
{
    /// <summary>
    /// 未设置
    /// </summary>
    Unset,
    /// <summary>
    /// 升序 0-9 A-Z
    /// </summary>
    Asc,
    /// <summary>
    /// 降序 9-0 Z-A
    /// </summary>
    Desc,
}
