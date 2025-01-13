// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Select 组件虚拟滚动参数类
/// </summary>
public class VirtualizeQueryOption
{
    /// <summary>
    /// 请求记录开始索引
    /// </summary>
    public int StartIndex { get; set; }

    /// <summary>
    /// 请求记录总数
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Select 组件搜索文本
    /// </summary>
    public string? SearchText { get; set; }
}
