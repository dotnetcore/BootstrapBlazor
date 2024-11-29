// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// 事件说明类
/// </summary>
public class EventItem
{
    /// <summary>
    /// 获得/设置 参数
    /// </summary>
    [DisplayName("参数")]
    public string Name { get; set; } = "";

    /// <summary>
    /// 获得/设置 说明
    /// </summary>
    [DisplayName("说明")]
    public string Description { get; set; } = "";

    /// <summary>
    /// 获得/设置 类型
    /// </summary>
    [DisplayName("类型")]
    public string Type { get; set; } = "";
}
