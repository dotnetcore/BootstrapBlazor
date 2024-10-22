﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Waterfall 组件数据类
/// </summary>
public class WaterfallItem
{
    /// <summary>
    /// 获得/设置 id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// the url of image element
    /// </summary>
    public string? Url { get; set; }
}
