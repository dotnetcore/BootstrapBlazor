// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 打印组件示例代码
/// </summary>
public partial class Print
{
    private async Task OnClickPrint()
    {
        var op = new DialogOption
        {
            Title = Localizer["PrintsDialogTitle"],
            ShowPrintButton = true,
            ShowPrintButtonInHeader = true,
            ShowFooter = false,
            BodyContext = 1,
            Component = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>()
        };

        await DialogService.Show(op);
    }

    private Task OnClickPrintService() => PrintService.PrintAsync<DataDialogComponent>(op =>
    {
        // 弹窗配置
        op.Title = Localizer["PrintsDialogTitle"];
        op.ShowPrintButton = true;
        op.ShowPrintButtonInHeader = true;
        op.ShowFooter = false;
        op.BodyContext = 2;

        // 弹窗组件所需参数
        return new Dictionary<string, object?>();
    });
}
