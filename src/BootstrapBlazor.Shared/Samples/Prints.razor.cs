// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Prints
{
    /// <summary>
    /// 获得 弹窗注入服务
    /// </summary>
    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    /// <summary>
    /// 获得 弹窗注入服务
    /// </summary>
    [Inject]
    [NotNull]
    private PrintService? PrintService { get; set; }

    private async Task OnClickPrint()
    {
        var op = new DialogOption()
        {
            Title = Localizer["DialogTitle"],
            ShowPrintButton = true,
            ShowPrintButtonInHeader = true,
            ShowFooter = false,
            BodyContext = 1
        };
        op.BodyTemplate = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>(new Dictionary<string, object?>
        {
            [nameof(DataDialogComponent.OnClose)] = new Action(async () => await op.Dialog.Close())
        }).Render();

        await DialogService.Show(op);
    }

    private Task OnClickPrintService() => PrintService.PrintAsync<DataDialogComponent>(op =>
    {
            // 弹窗配置
            op.Title = Localizer["DialogTitle"];
        op.ShowPrintButton = true;
        op.ShowPrintButtonInHeader = true;
        op.ShowFooter = false;
        op.BodyContext = 2;

            // 弹窗组件所需参数
            return new Dictionary<string, object?>
        {
            [nameof(DataDialogComponent.OnClose)] = new Action(async () => await op.Dialog.Close())
        };
    });
}
