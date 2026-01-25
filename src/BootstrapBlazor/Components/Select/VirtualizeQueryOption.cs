// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Select 组件虚拟滚动参数类</para>
/// <para lang="en">Select Component Virtual Scroll Option Class</para>
/// </summary>
public class VirtualizeQueryOption
{
    /// <summary>
    /// <para lang="zh">请求记录开始索引</para>
    /// <para lang="en">Request Start Index</para>
    /// </summary>
    public int StartIndex { get; set; }

    /// <summary>
    /// <para lang="zh">请求记录总数</para>
    /// <para lang="en">Request Total Count</para>
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// <para lang="zh">Select 组件搜索文本</para>
    /// <para lang="en">Select Component Search Text</para>
    /// </summary>
    public string? SearchText { get; set; }
}
