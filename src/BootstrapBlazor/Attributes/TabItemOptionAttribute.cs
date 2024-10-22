// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TabItem 标签页配置属性类
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TabItemOptionAttribute : Attribute
{
    /// <summary>
    /// 获得/设置 文本文字
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 当前 TabItem 是否可关闭 默认为 true 可关闭
    /// </summary>
    public bool Closable { get; set; } = true;

    /// <summary>
    /// 获得/设置 图标字符串
    /// </summary>
    public string? Icon { get; set; }
}
