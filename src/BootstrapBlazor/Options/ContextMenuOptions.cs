// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuOptions 配置类
/// </summary>
public class ContextMenuOptions
{
    /// <summary>
    /// 获得/设置 移动端触屏右键菜单延时时长 默认 500 毫秒
    /// </summary>
    public int OnTouchDelay { get; set; } = 500;
}
