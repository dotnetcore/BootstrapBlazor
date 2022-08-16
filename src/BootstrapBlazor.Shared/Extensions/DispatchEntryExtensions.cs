// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Extensions;

internal static class DispatchEntryExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static bool CanDispatch(this DispatchEntry<GiteePostBody> entry)
    {
        return entry.Entry != null && (entry.Entry.HeadCommit != null || entry.Entry.Commits?.Count > 0);
    }
}
