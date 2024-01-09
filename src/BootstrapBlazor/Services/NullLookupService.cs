// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 内部默认字典服务实现类
/// </summary>
internal class NullLookupService : ILookupService
{
    /// <summary>
    /// 根据指定键值获取 Lookup 集合方法
    /// </summary>
    /// <param name="key">获得 Lookup 数据集合键值</param>
    public IEnumerable<SelectedItem>? GetItemsByKey(string? key) => null;
}
