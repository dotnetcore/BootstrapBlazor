// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class CalendarCrewCell
{
    /// <summary>
    /// 获得/设置 单元格值
    /// </summary>
    [Parameter]
    [NotNull]
    public CalendarCellValue? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<CalendarCellValue> ValueChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public List<Crew>? Crews { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    private async Task OnClickCell()
    {
        await DialogService.Show(new DialogOption()
        {
            Title = $"明细查看 - {Value.CellValue:yyyy-MM-dd}",
            Component = BootstrapDynamicComponent.CreateComponent<CalendarCrewDialogBody>(new Dictionary<string, object?>()
            {
                [nameof(Value)] = Value,
                [nameof(Crews)] = Crews
            }),
            OnCloseAsync = async () =>
            {
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(Value);
                }
            }
        });
    }
}
