// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Data;

/// <summary>
/// 样式项目类
/// </summary>
public class ThemeOption
{
    /// <summary>
    /// 获得/设置 样式键值
    /// </summary>
    [NotNull]
    public string? Key { get; set; }

    /// <summary>
    /// 获得/设置 样式名称
    /// </summary>
    [NotNull]
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 样式文件集合
    /// </summary>
    [NotNull]
    public string[]? Files { get; set; }
}
