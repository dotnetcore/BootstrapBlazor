﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class SkeletonAvatar
{
    private string? AvatarClassString => CssBuilder.Default("skeleton-avatar")
        .AddClass("circle", Circle)
        .Build();

    /// <summary>
    /// 获得/设置 是否为圆形 默认为 false
    /// </summary>
    [Parameter]
    public bool Circle { get; set; }
}
