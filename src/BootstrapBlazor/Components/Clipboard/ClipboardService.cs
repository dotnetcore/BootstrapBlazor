// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 粘贴板服务
/// </summary>
public class ClipboardService : BootstrapServiceBase<ClipboardOption>
{
    /// <summary>
    /// 拷贝方法
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public Task Copy(string? text) => Invoke(new ClipboardOption() { Text = text });
}
