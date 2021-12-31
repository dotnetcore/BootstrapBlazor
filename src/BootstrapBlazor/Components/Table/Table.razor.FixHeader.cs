// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

public partial class Table<TItem>
{
    /// <summary>
    /// 获得/设置 Table 组件引用
    /// </summary>
    /// <value></value>
    protected ElementReference TableElement { get; set; }

    /// <summary>
    /// 获得/设置 Table 高度 默认为 null
    /// </summary>
    /// <remarks>开启固定表头功能时生效 <see cref="IsFixedHeader"/></remarks>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// 获得/设置 固定表头 默认 false
    /// </summary>
    /// <remarks>固定表头时设置 <see cref="Height"/> 即可出现滚动条，未设置时尝试自适应</remarks>
    [Parameter]
    public bool IsFixedHeader { get; set; }

    /// <summary>
    /// 获得/设置 多表头模板
    /// </summary>
    [Parameter]
    public RenderFragment? MultiHeaderTemplate { get; set; }
}
