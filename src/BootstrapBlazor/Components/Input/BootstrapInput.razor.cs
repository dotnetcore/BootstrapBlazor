// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInput 组件
/// </summary>
public partial class BootstrapInput<TValue>
{
    /// <summary>
    /// 获得/设置 是否为只读 默认 false
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    private string? ReadonlyString => Readonly ? "true" : null;
}
