// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
