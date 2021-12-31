// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 预览方式
/// </summary>
public enum PreviewStyle
{
    /// <summary>
    /// 左右树形预览
    /// </summary>
    [Description("vertical")]
    Vertical,

    /// <summary>
    /// tab页预览
    /// </summary>
    [Description("tab")]
    Tab
}
