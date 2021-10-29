// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
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

        private async Task OnClickPrint()
        {
            var op = new DialogOption()
            {
                Title = "数据查询窗口",
                ShowPrintButton = true,
                ShowPrintButtonInHeader = true,
                ShowFooter = false,
                BodyContext = 1
            };
            op.BodyTemplate = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>(new KeyValuePair<string, object>[]
            {
                new(nameof(DataDialogComponent.OnClose), new Action(async () => await op.Dialog.Close()))
            }).Render();

            await DialogService.Show(op);
        }
    }
}
