// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
