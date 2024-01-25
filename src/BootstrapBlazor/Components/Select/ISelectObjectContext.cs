// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Data;

namespace BootstrapBlazor.Components;

/// <summary>
/// SelectObject 上下文
/// </summary>
public interface ISelectObjectContext<TItem>
{
    /// <summary>
    /// 获得/设置 SelectObject 组件实例对象引用
    /// </summary>
    [NotNull]
    SelectObject<TItem>? Component { get; set; }
}
