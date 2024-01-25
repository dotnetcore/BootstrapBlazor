// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
