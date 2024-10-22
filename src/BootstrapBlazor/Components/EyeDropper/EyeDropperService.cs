﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// EyeDropper 服务用于屏幕吸色
/// </summary>
public class EyeDropperService : BootstrapServiceBase<EyeDropperOption>
{
    /// <summary>
    /// 全屏方法，已经全屏时再次调用后退出全屏
    /// </summary>
    /// <returns></returns>
    public async Task<string?> PickAsync()
    {
        var op = new EyeDropperOption();
        await Invoke(op);
        return op.Value;
    }
}
