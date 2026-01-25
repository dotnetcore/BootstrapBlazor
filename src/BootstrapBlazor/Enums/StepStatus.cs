// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">步骤状态枚举</para>
/// <para lang="en">步骤状态enum</para>
/// </summary>
public enum StepStatus
{
    /// <summary>
    /// <para lang="zh">未开始</para>
    /// <para lang="en">未开始</para>
    /// </summary>
    [Description("wait")]
    Wait,

    /// <summary>
    /// <para lang="zh">进行中</para>
    /// <para lang="en">进行中</para>
    /// </summary>
    [Description("process")]
    Process,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("finish")]
    Finish,

    /// <summary>
    /// <para lang="zh">已完成</para>
    /// <para lang="en">已完成</para>
    /// </summary>
    [Description("success")]
    Success,

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    [Description("error")]
    Error,
}
