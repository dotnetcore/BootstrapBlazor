// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 主题提供器接口
/// </summary>
public interface IThemeProvider
{
    /// <summary>
    /// 设置主题方法
    /// </summary>
    /// <param name="themeName"></param>
    ValueTask SetThemeAsync(string themeName);

    /// <summary>
    /// 获得当前主题方法
    /// </summary>
    ValueTask<string?> GetThemeAsync();
}
