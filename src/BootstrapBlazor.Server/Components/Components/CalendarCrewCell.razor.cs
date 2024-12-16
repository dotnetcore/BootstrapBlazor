// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
