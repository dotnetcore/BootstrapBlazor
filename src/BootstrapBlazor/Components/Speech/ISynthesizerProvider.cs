// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ISynthesizerProvider 语音合成接口</para>
/// <para lang="en">ISynthesizerProvider Speech Synthesis Interface</para>
/// </summary>
public interface ISynthesizerProvider
{
    /// <summary>
    /// <para lang="zh">语音合成回调方法</para>
    /// <para lang="en">Speech Synthesis Callback Method</para>
    /// </summary>
    /// <returns></returns>
    Task InvokeAsync(SynthesizerOption option);
}
