// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 导出 Excel 配置类
/// </summary>
public class TableExportOptions
{
    /// <summary>
    /// 获得/设置 是否使用格式化 默认 true 如果设置 <see cref="TableColumn{TItem, TType}.FormatString"/> 或者 <see cref="TableColumn{TItem, TType}.Formatter"/> 后使用格式化值
    /// </summary>
    /// <remarks>注意格式化后返回值是 <code>string</code> 会导致原始值类型改变</remarks>
    public bool EnableFormat { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否使用 Lookup 默认 true
    /// </summary>
    public bool EnableLookup { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否将数组类型值进行合并操作 默认 true
    /// </summary>
    public bool AutoMergeArray { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否使用枚举类型的 <see cref="DescriptionAttribute"/> 标签 默认 true
    /// </summary>
    public bool UseEnumDescription { get; set; } = true;

    /// <summary>
    /// 获得/设置 数组类型合并操作时使用的分隔符 默认使用逗号
    /// </summary>
    /// <remarks>注意格式化后返回值是 <code>string</code> 会导致原始值类型改变</remarks>
    public string ArrayDelimiter { get; set; } = ",";

    /// <summary>
    /// 获得/设置 是否启用 Excel 自动筛选 默认 true
    /// </summary>
    public bool AutoFilter { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否启用 Excel 自动宽度 默认 false
    /// </summary>
    public bool EnableAutoWidth { get; set; }
}
