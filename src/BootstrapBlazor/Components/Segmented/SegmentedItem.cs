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
public class SegmentedItem : SelectedItem
{
    /// <summary>
    /// 
    /// </summary>
    public SegmentedItem()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="text"></param>
    public SegmentedItem(string value, string text)
           : base(value, text)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public string? Icon { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public RenderFragment<SegmentedItem>? ChildContent { get; set; }
}
