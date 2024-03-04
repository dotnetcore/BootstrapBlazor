// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
