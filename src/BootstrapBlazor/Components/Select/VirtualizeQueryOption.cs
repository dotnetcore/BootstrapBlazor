// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件虚拟滚动参数类
/// </summary>
public class VirtualizeQueryOption
{
    /// <summary>
    /// 请求记录开始索引
    /// </summary>
    public int StartIndex { get; internal set; }

    /// <summary>
    /// 请求记录总数
    /// </summary>
    public int Count { get; internal set; }

    /// <summary>
    /// Select 组件搜索文本
    /// </summary>
    public string? SearchText { get; internal set; }
}
