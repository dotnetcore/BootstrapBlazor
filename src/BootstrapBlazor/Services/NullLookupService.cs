// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">内部默认字典服务实现类</para>
/// <para lang="en">Internal Default Lookup Service Implementation</para>
/// </summary>
class NullLookupService : LookupServiceBase
{
    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public override IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data) => null;
}
