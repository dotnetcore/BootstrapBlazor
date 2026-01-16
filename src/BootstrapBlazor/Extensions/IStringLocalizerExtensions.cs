// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IStringLocalizer 实例扩展类</para>
/// <para lang="en">IStringLocalizer Instance Extensions</para>
/// </summary>
internal static class IStringLocalizerExtensions
{
    /// <summary>
    /// <para lang="zh">获取指定 Type 的资源文件</para>
    /// <para lang="en">Get the resource file of the specified Type</para>
    /// </summary>
    /// <param name="localizer"></param>
    /// <param name="key"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool TryGetLocalizerString(this IStringLocalizer localizer, string key, [MaybeNullWhen(false)] out string? text)
    {
        var ret = false;
        text = null;
        var l = localizer[key];
        if (l != null)
        {
            ret = !l.ResourceNotFound;
            if (ret)
            {
                text = l.Value;
            }
        }
        return ret;
    }
}
