// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Toast 组件类型</para>
/// <para lang="en">Toast componenttype</para>
/// </summary>
public enum ToastCategory
{
    /// <summary>
    /// <para lang="zh">成功信息</para>
    /// <para lang="en">成功信息</para>
    /// </summary>
    [Description("success")]
    Success,

    /// <summary>
    /// <para lang="zh">提示信息</para>
    /// <para lang="en">提示信息</para>
    /// </summary>
    [Description("info")]
    Information,

    /// <summary>
    /// <para lang="zh">错误信息</para>
    /// <para lang="en">错误信息</para>
    /// </summary>
    [Description("danger")]
    Error,

    /// <summary>
    /// <para lang="zh">警告信息</para>
    /// <para lang="en">警告信息</para>
    /// </summary>
    [Description("warning")]
    Warning
}
