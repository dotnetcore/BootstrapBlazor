// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Toast 组件类型
/// </summary>
public enum ToastCategory
{
    /// <summary>
    /// 成功信息
    /// </summary>
    [Description("success")]
    Success,

    /// <summary>
    /// 提示信息
    /// </summary>
    [Description("info")]
    Information,

    /// <summary>
    /// 错误信息
    /// </summary>
    [Description("danger")]
    Error,

    /// <summary>
    /// 警告信息
    /// </summary>
    [Description("warning")]
    Warning
}
