// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">列集合接口
///</para>
/// <para lang="en">列collection接口
///</para>
/// </summary>
public interface IColumnCollection
{
    /// <summary>
    /// <para lang="zh">获得 ITableColumn 集合
    ///</para>
    /// <para lang="en">Gets ITableColumn collection
    ///</para>
    /// </summary>
    List<ITableColumn> Columns { get; }
}
