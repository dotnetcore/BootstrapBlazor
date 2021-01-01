// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
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
    public sealed partial class SearchDialogs
    {
        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }

        [NotNull]
        private Logger? Trace { get; set; }

        private async Task ShowDialog()
        {
            var option = new SearchDialogOption<BindItem>()
            {
                Title = "搜索弹出框",
                Model = new BindItem(),
                OnCloseAsync = () =>
                {
                    Trace.Log("关闭按钮被点击");
                    return Task.CompletedTask;
                },
                OnResetSearchClick = () =>
                {
                    Trace.Log("重置按钮被点击");
                    return Task.CompletedTask;
                },
                OnSearchClick = () =>
                {
                    Trace.Log("搜索按钮被点击");
                    return Task.CompletedTask;
                }
            };

            await DialogService.ShowSearchDialog(option);
        }

        private async Task ShowColumnsDialog()
        {
            var model = new BindItem();
            var option = new SearchDialogOption<BindItem>()
            {
                Title = "搜索弹出框",
                Model = model,
                Columns = model.GenerateColumns(p => p.Name == nameof(BindItem.Name) || p.Name == nameof(BindItem.Address))
            };
            await DialogService.ShowSearchDialog(option);
        }
    }
}
