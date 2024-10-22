// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 可为空布尔类型转换器
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class NullableBoolItemsAttribute : Attribute
{
    /// <summary>
    /// 获得/设置 空值显示文本
    /// </summary>
    public string? NullValueDisplayText { get; set; }

    /// <summary>
    /// 获得/设置 True 值显示文本
    /// </summary>
    public string? TrueValueDisplayText { get; set; }

    /// <summary>
    /// 获得/设置 False 值显示文本
    /// </summary>
    public string? FalseValueDisplayText { get; set; }
}
