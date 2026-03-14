// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">选择类型搜索元数据类</para>
/// <para lang="en">Select type search metadata class</para>
/// </summary>
public class SelectSearchMetaData : StringSearchMetaData
{
    /// <summary>
    /// <para lang="zh">获得/设置 选择项集合</para>
    /// <para lang="en">Gets or sets the collection of select items</para>
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SelectedItem>? Items { get; set; }
}
