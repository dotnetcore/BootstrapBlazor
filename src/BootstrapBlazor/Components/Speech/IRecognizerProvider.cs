// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ISpeechService 服务接口定义
/// </summary>
public interface IRecognizerProvider
{
    /// <summary>
    /// 识别语音回调方法
    /// </summary>
    /// <returns></returns>
    Task InvokeAsync(RecognizerOption option);
}
