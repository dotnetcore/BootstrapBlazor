// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreen 配置类
/// </summary>
public class FullScreenOption
{
    /// <summary>
    /// 获得/设置 要全屏的 HTML Element 实例
    /// </summary>
    public ElementReference Element { get; set; }

    /// <summary>
    /// 获得/设置 要全屏的 HTML Element Id
    /// </summary>
    public string? Id { get; set; }
}
