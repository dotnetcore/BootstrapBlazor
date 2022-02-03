// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 获得/设置 图标字符串 如 "fa fa"
    /// </summary>
    public string? Icon { get; set; }
}
