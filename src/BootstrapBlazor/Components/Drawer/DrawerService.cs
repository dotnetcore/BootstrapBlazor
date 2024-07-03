// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Toast 弹出窗服务类
/// </summary>
public class DrawerService : BootstrapServiceBase<DrawerOption>
{
    /// <summary>
    /// Show 方法
    /// </summary>
    /// <param name="option">DrawerOption 实例</param>
    public Task Show(DrawerOption option) => Invoke(option);
}
