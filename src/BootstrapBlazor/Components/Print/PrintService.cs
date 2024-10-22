// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
