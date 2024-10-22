﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ICheckableNode<TItem> : IExpandableNode<TItem>
{
    /// <summary>
    /// 获得/设置 是否被选中
    /// </summary>
    CheckboxState CheckedState { get; set; }
}
