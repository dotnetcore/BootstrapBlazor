// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
