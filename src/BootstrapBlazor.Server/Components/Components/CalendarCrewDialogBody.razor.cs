// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class CalendarCrewDialogBody
{
    /// <summary>
    /// 获得/设置 单元格值
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public CalendarCellValue? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public List<Crew>? Crews { get; set; }

    private static void OnUpdateValue(Crew crew, int interval)
    {
        crew.Value += interval;
    }
}
