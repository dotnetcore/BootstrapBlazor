// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
