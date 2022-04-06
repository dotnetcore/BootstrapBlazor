// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class ProviderOption
{
    /// <summary>
    /// 获得/设置 语音识别指令名称
    /// </summary>
    public string? MethodName { get; set; }

    /// <summary>
    /// 获得/设置 IServiceProvider 实例
    /// </summary>
    public IServiceProvider? ServiceProvider { get; set; }

    /// <summary>
    /// 获得/设置 回调方法
    /// </summary>
    public Func<string, Task>? Callback { get; set; }
}
