// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 网络监控器组件
/// </summary>
public partial class NetworkMonitor
{
    /// <summary>
    /// 获得 样式字符串
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-network-monitor")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
