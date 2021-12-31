// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
