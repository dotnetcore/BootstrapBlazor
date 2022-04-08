// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ISynthesizerProvider 语音合成接口
/// </summary>
public interface ISynthesizerProvider
{
    /// <summary>
    /// 语音合成回调方法
    /// </summary>
    /// <returns></returns>
    Task InvokeAsync(SynthesizerOption option);
}
