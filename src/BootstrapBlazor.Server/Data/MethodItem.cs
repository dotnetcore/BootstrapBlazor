// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 方法说明类
/// </summary>
public class MethodItem : EventItem
{
    /// <summary>
    /// 参数
    /// </summary>
    [DisplayName("参数")]
    public string Parameters { get; set; } = "";

    /// <summary>
    /// 返回值
    /// </summary>
    [DisplayName("返回值")]
    public string ReturnValue { get; set; } = "";
}
