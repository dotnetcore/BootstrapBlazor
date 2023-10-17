// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Segmenteds
{
    private BootstrapBlazor.Components.ConsoleLogger? ConsoleLogger { get; set; }

    private List<SegmentedItem> Items { get; set; } = new List<SegmentedItem>()
    {
        new SegmentedItem(){ Value = "zhangsan", Text = "张三", Active = true },
        new SegmentedItem(){ Value = "lisi", Text = "李四", Active = false},
        new SegmentedItem(){ Value = "wangwu", Text = "王五", Active = false },
        new SegmentedItem(){ Value = "xiaoming", Text = "小明", Active = false},
    };

    private string? Value { get; set; } = "lisi";

    private void ValueChanged(string item)
    {
        ConsoleLogger?.Log(item);
    }
}
