// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
