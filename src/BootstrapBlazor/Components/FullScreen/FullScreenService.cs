// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreen 服务
/// </summary>
public class FullScreenService : BootstrapServiceBase<FullScreenOption>
{
    /// <summary>
    /// 全屏方法，已经全屏时再次调用后退出全屏
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public Task Toggle(FullScreenOption? option = null) => Invoke(option ?? new());
}
