// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 逻辑运算符
/// </summary>
public enum FilterLogic
{
    /// <summary>
    /// 并且
    /// </summary>
    [Description("并且")]
    And,

    /// <summary>
    /// 或者
    /// </summary>
    [Description("或者")]
    Or
}
