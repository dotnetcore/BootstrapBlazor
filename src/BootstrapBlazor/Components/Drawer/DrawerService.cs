// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toast 弹出窗服务类</para>
/// <para lang="en">Drawer Service Class</para>
/// </summary>
public class DrawerService : BootstrapServiceBase<DrawerOption>
{
    /// <summary>
    /// <para lang="zh">Show 方法</para>
    /// <para lang="en">Show Method</para>
    /// </summary>
    /// <param name="option">DrawerOption 实例</param>
    public Task Show(DrawerOption option) => Invoke(option);
}
