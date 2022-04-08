// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音识别服务
/// </summary>
public class RecognizerService
{
    private IRecognizerProvider Provider { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="provider"></param>
    public RecognizerService(IRecognizerProvider provider)
    {
        Provider = provider;
    }

    /// <summary>
    /// 语音识别回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public Task InvokeAsync(RecognizerOption option) => Provider.InvokeAsync(option);
}
