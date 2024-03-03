// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 内部默认字典服务实现类
/// </summary>
class NullLookupService : LookupServiceBase
{
    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data) => null;
}
