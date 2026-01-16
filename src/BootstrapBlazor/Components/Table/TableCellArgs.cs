// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">单元格数据类</para>
/// <para lang="en">单元格data类</para>
/// </summary>
public class TableCellArgs
{
    /// <summary>
    /// <para lang="zh">获得 当前单元格行数据 请自行转化为绑定模型</para>
    /// <para lang="en">Gets 当前单元格行data 请自行转化为绑定模型</para>
    /// </summary>
    [NotNull]
    public object? Row { get; internal set; }

    /// <summary>
    /// <para lang="zh">获得 当前单元格绑定列名称</para>
    /// <para lang="en">Gets 当前单元格绑定列名称</para>
    /// </summary>
    [NotNull]
    public string? ColumnName { get; internal set; }

    /// <summary>
    /// <para lang="zh">获得/设置 合并单元格数量 默认 0</para>
    /// <para lang="en">Gets or sets 合并单元格数量 Default is 0</para>
    /// </summary>
    public int Colspan { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前单元格样式 默认 null</para>
    /// <para lang="en">Gets or sets 当前单元格style Default is null</para>
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前单元格显示内容</para>
    /// <para lang="en">Gets or sets 当前单元格displaycontent</para>
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前单元格内容模板</para>
    /// <para lang="en">Gets or sets 当前单元格contenttemplate</para>
    /// </summary>
    public RenderFragment? ValueTemplate { get; set; }
}
