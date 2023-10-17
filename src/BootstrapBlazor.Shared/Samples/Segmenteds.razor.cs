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
        new SegmentedItem(){ Value = "Daily", Text = "Daily" },
        new SegmentedItem(){ Value = "Weekly", Text = "Weekly" },
        new SegmentedItem(){ Value = "Monthly", Text = "Monthly" },
        new SegmentedItem(){ Value = "Quarterly", Text = "Quarterly" },
        new SegmentedItem(){ Value = "Yearly", Text = "Yearly" }
    };

    private List<SegmentedItem> LongTextItems { get; set; } = new List<SegmentedItem>()
    {
        new SegmentedItem(){ Value = "123", Text = "123" },
        new SegmentedItem(){ Value = "456", Text = "456" },
        new SegmentedItem(){ Value = "longtext-longtext-longtext-longtext", Text = "longtext-longtext-longtext-longtext" }
    };

    private List<SegmentedItem> DisableItems { get; set; } = new List<SegmentedItem>()
    {
        new SegmentedItem(){ Value = "123", Text = "123" },
        new SegmentedItem(){ Value = "456", Text = "456", IsDisabled = true },
        new SegmentedItem(){ Value = "789", Text = "789" },
    };

    private List<SegmentedItem> IconItems { get; set; } = new List<SegmentedItem>()
    {
        new SegmentedItem(){ Value = "list", Text = "List", Icon = "fas fa-bars" },
        new SegmentedItem(){ Value = "chart", Text = "Chart", Icon = "fas fa-chart-column" }
    };

    private string? Value { get; set; }

    private string? Value1 { get; set; } = "Daily";

    private string? BlockValue { get; set; }

    private string? DisableValue { get; set; }

    private void ValueChanged(string item)
    {
        Value1 = item;
        ConsoleLogger?.Log(item);
    }
}
