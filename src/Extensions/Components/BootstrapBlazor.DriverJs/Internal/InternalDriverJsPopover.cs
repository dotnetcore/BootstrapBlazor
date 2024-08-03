// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class InternalDriverJsPopover : IDriverJsPopover
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Side { get; set; }
    public string? Align { get; set; }
    public List<string>? ShowButtons { get; set; }
    public List<string>? DisableButtons { get; set; }
    public string? NextBtnText { get; set; }
    public string? PrevBtnText { get; set; }
    public string? DoneBtnText { get; set; }
    public bool? ShowProgress { get; set; }
    public string? ProgressText { get; set; }
    public string? PopoverClass { get; set; }
}
