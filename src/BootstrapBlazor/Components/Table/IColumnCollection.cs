﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 列集合接口
/// </summary>
public interface IColumnCollection
{
    /// <summary>
    /// 获得 ITableColumn 集合
    /// </summary>
    List<ITableColumn> Columns { get; }
}
