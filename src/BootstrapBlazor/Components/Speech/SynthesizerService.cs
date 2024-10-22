// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音合成服务
/// </summary>
/// <param name="provider"></param>
public class SynthesizerService(ISynthesizerProvider provider)
{
    /// <summary>
    /// 语音合成回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public Task InvokeAsync(SynthesizerOption option) => provider.InvokeAsync(option);
}
