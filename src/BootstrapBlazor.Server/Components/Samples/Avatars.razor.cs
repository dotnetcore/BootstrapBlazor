// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Avatars 组件
/// </summary>
public sealed partial class Avatars
{
    private async Task<string> GetUrlAsync()
    {
        // 模拟异步获取图像地址
        await Task.Delay(500);
        return $"{WebsiteOption.Value.AssetRootPath}images/Argo-C.png";
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
}
