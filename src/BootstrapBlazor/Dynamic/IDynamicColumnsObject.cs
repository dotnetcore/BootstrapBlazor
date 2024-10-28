// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态对象接口
/// </summary>
public interface IDynamicColumnsObject : IDynamicObject
{
    /// <summary>
    /// 获得设置 列与列数值集合
    /// </summary>
    public Dictionary<string, object?> Columns { get; set; }
}
