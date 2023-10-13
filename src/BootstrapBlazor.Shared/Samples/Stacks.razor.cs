// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Stacks
{
    private bool IsRow => ShowModeString == "true";

    private StackJustifyContent Justify { get; set; }

    private StackAlignItems AlignItems { get; set; }

    private StackJustifyContent JustifySelf { get; set; }

    private StackAlignItems AlignSelf { get; set; }

    private bool IsWrap { get; set; }

    private bool IsReverse { get; set; }

    private List<SelectedItem> ShowMode => new List<SelectedItem>()
    {
        new("true", Localizer["RowMode"]),
        new("false", Localizer["ColumnMode"])
    };

    private string ShowModeString { get; set; } = "true";

    private IEnumerable<AttributeItem> GetAttributeItems() => new List<AttributeItem>()
    {

    };
}
