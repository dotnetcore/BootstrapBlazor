// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IRecognizerProvider 服务接口定义</para>
/// <para lang="en">IRecognizerProvider Interface Definition</para>
/// </summary>
public interface IRecognizerProvider
{
    /// <summary>
    /// <para lang="zh">识别语音回调方法</para>
    /// <para lang="en">Recognize Speech Callback Method</para>
    /// </summary>
    Task InvokeAsync(RecognizerOption option);
}
