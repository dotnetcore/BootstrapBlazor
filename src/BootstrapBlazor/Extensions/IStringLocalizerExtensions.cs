// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// IStringLocalizer 实例扩展类
/// </summary>
internal static class IStringLocalizerExtensions
{
    /// <summary>
    /// 获取指定 Type 的资源文件
    /// </summary>
    /// <param name="localizer"></param>
    /// <param name="key"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool TryGetLocalizerString(this IStringLocalizer? localizer, string key, [MaybeNullWhen(false)] out string? text)
    {
        var ret = false;
        text = null;
        var l = localizer?[key];
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
