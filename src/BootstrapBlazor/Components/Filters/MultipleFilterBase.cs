// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 多条件过滤器基类
/// </summary>
public abstract class MultipleFilterBase : FilterBase
{
    /// <summary>
    /// 获得/设置 条件数量
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 获得/设置 多个条件逻辑关系符号
    /// </summary>
    protected FilterLogic Logic { get; set; }
}
