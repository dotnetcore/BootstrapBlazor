// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 属性说明类
/// </summary>
public class AttributeItem
{
    /// <summary>
    /// 获得/设置 参数
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 说明
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 获得/设置 类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 获得/设置 可选值
    /// </summary>
    public string? ValueList { get; set; }

    /// <summary>
    /// 获得/设置 版本
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// 获得/设置 默认值
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// 获得/设置 是否已弃用
    /// </summary>
    public bool IsObsolete { get; set; }
}
