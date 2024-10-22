// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Extensions;

/// <summary>
/// DispatchEntry 扩展方法
/// </summary>
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
