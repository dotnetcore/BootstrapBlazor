// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 步骤状态枚举
/// </summary>
public enum StepStatus
{
    /// <summary>
    /// 未开始
    /// </summary>
    [Description("wait")]
    Wait,

    /// <summary>
    /// 进行中
    /// </summary>
    [Description("process")]
    Process,

    /// <summary>
    /// 
    /// </summary>
    [Description("finish")]
    Finish,

    /// <summary>
    /// 已完成
    /// </summary>
    [Description("success")]
    Success,

    /// <summary>
    /// 
    /// </summary>
    [Description("error")]
    Error,
}
