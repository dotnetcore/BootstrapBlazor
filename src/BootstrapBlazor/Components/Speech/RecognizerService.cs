﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音识别服务
/// </summary>
public class RecognizerService(IRecognizerProvider provider)
{
    /// <summary>
    /// 语音识别回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public Task InvokeAsync(RecognizerOption option) => provider.InvokeAsync(option);
}
