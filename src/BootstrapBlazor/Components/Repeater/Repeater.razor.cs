// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public partial class Repeater<TValue>
{

    private string? RepeaterClassString => CssBuilder.Default("repeater")
        .Build();

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<TValue>? Items { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment<IEnumerable<TValue>>? BodyTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment? FooterTemplate { get; set; }
}
