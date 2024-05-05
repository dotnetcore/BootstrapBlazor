// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// <returns></returns>
    Task SetThemeAsync(string themeName);
}
