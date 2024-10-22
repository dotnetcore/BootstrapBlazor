// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Title 服务
/// </summary>
public class TitleService : BootstrapServiceBase<TitleOption>
{
    /// <summary>
    /// 设置当前网页 Title 方法
    /// </summary>
    /// <returns></returns>
    public async Task SetTitle(string title)
    {
        var op = new TitleOption() { Title = title };
        await Invoke(op);
    }
}
