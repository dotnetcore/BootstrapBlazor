// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class EditDialogs
    {
        private BindItem Model { get; set; } = new BindItem()
        {
            Name = "Name 1234",
            Address = "Address 1234"
        };

        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }

        [NotNull]
        private Logger? Trace { get; set; }

        private async Task ShowDialog()
        {
            var option = new EditDialogOption<BindItem>()
            {
                Title = "编辑对话框",
                Model = Model,
                OnCloseAsync = () =>
                {
                    Trace.Log("关闭按钮被点击");
                    return Task.CompletedTask;
                },
                OnSaveAsync = context =>
                {
                    Trace.Log("保存按钮被点击");
                    return Task.FromResult(true);
                }
            };

            await DialogService.ShowEditDialog(option);
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示标签",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Model",
                Description = "泛型参数用于呈现 UI",
                Type = "TModel",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = "编辑项集合",
                Type = "IEnumerable<IEditorItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DialogBodyTemplate",
                Description = "EditDialog Body 模板",
                Type = "RenderFragment<TModel>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CloseButtonText",
                Description = "关闭按钮文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "关闭"
            },
            new AttributeItem() {
                Name = "SaveButtonText",
                Description = "保存按钮文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "保存"
            },
            new AttributeItem() {
                Name = "OnSaveAsync",
                Description = "保存回调委托",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
