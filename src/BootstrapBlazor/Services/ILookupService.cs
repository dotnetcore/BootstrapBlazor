// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ILookupService 接口
/// </summary>
public interface ILookupService
{
    /// <summary>
    /// 根据指定键值获取 Lookup 集合方法
    /// </summary>
    /// <param name="key">获得 Lookup 数据集合键值</param>
    IEnumerable<SelectedItem>? GetItemsByKey(string? key);

    /// <summary>
    /// 根据指定键值获取 Lookup 集合方法
    /// </summary>
    /// <param name="key">获得 Lookup 数据集合键值</param>
    /// <param name="data">Lookup 键值附加数据</param>
    IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data);
}
