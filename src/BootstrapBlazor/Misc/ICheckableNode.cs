// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
