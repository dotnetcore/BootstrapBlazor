﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// FooSortableListItem 组件
/// </summary>
public partial class FooSortableListItem
{
    /// <summary>
    /// 获得/设置 Foo 实例
    /// </summary>
    [Parameter, NotNull]
    public Foo? Value { get; set; }
}
