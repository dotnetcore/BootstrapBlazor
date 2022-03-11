// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Print 服务
/// </summary>
public class PrintService : BootstrapServiceBase<DialogOption>
{
    /// <summary>
    /// 打印方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public Task PrintAsync(DialogOption option) => Invoke(option);
}
