// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
