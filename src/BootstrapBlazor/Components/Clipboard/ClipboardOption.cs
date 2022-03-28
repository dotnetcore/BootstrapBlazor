// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Clipboard 配置类
/// </summary>
public class ClipboardOption
{
    /// <summary>
    /// 获得/设置 要拷贝的文字 默认为 null
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 拷贝完成后回调委托 默认为 null
    /// </summary>
    public Func<Task>? Callback { get; set; }
}
