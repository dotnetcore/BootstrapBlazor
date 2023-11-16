// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
