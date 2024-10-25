﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 浏览器指纹接口
/// </summary>
public interface IBrowserFingerService
{
    /// <summary>
    /// 获得当前浏览器指纹方法
    /// </summary>
    /// <returns></returns>
    Task<string?> GetFingerCodeAsync(CancellationToken token = default);
}
