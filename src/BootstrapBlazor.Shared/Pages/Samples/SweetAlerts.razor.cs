// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SweetAlerts
    {
        private Task OnSwal(SwalCategory cate) => SwalService.Show(new SwalOption()
        {
            Category = cate,
            Title = "Sweet 弹窗"
        });

        private Task ShowTitle() => SwalService.Show(new SwalOption()
        {
            Category = SwalCategory.Success,
            Title = "我是 Title"
        });

        private Task ShowContent() => SwalService.Show(new SwalOption()
        {
            Category = SwalCategory.Success,
            Content = "我是 Content"
        });

        private async Task ShowButtons()
        {
            var op = new SwalOption()
            {
                Category = SwalCategory.Success,
                Title = "我是 Title",
                Content = "我是 Content",
                ShowClose = false
            };
            op.ButtonTemplate = new RenderFragment(builder =>
            {
                builder.OpenComponent<Button>(0);
                builder.AddAttribute(1, nameof(Button.Text), "自定义关闭按钮");
                builder.AddAttribute(2, nameof(Button.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, async () => await op.Close()));
                builder.CloseComponent();
            });
            await SwalService.Show(op);
        }

        private async Task ShowComponent()
        {
            var op = new SwalOption()
            {
                BodyTemplate = new RenderFragment(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddAttribute(1, "class", "text-center");
                    builder.OpenComponent<Counter>(2);
                    builder.CloseComponent();
                    builder.CloseElement();
                })
            };
            await SwalService.Show(op);
        }

        private async Task ShowFooterComponent()
        {
            var op = new SwalOption()
            {
                Category = SwalCategory.Error,
                Title = "Oops...",
                Content = "Something went wrong!",
                ShowFooter = true,
                FooterTemplate = DynamicComponent.CreateComponent<SwalFooter>().Render()
            };
            await SwalService.Show(op);
        }

        private async Task ShowAutoCloseSwal()
        {
            var op = new SwalOption()
            {
                Category = SwalCategory.Error,
                Title = "Oops...",
                Content = "Something went wrong!",
                ShowFooter = true,
                IsAutoHide = true,
                Delay = 4000,
                FooterTemplate = DynamicComponent.CreateComponent<SwalFooter>().Render()
            };
            await SwalService.Show(op);
        }

        private Logger? Trace { get; set; }

        private async Task ShowModal()
        {
            var op = new SwalOption()
            {
                Title = "模态对话框示例",
                Content = "模态对话框内容，不同按钮返回不同值",
                IsConfirm = true
            };
            var ret = await SwalService.ShowModal(op);

            Trace?.Log($"模态弹窗返回值为：{ret}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "Category",
                    Description = "弹出框类型",
                    Type = "SwalCategory",
                    ValueList = "Success/Error/Information/Warning/Question",
                    DefaultValue = "Success"
                },
                new AttributeItem() {
                    Name = "Title",
                    Description = "弹窗标题",
                    Type = "string",
                    ValueList = "—",
                    DefaultValue = ""
                },
                new AttributeItem() {
                    Name = "Cotent",
                    Description = "弹窗内容",
                    Type = "string",
                    ValueList = "—",
                    DefaultValue = ""
                },
                new AttributeItem() {
                    Name = "Delay",
                    Description = "自动隐藏时间间隔",
                    Type = "int",
                    ValueList = "—",
                    DefaultValue = "4000"
                },
                new AttributeItem() {
                    Name = "IsAutoHide",
                    Description = "是否自动隐藏",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowClose",
                    Description = "是否显示关闭按钮",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "true"
                },
                new AttributeItem() {
                    Name = "ShowFooter",
                    Description = "是否显示页脚模板",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsConfirm",
                    Description = "是否为确认弹窗模式",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "KeepChildrenState",
                    Description = "是否保持弹窗内组件状态",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "BodyContext",
                    Description = "弹窗传参",
                    Type = "object",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "BodyTemplate",
                    Description = "模态主体显示组件",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "FooterTemplate",
                    Description = "模态主体页脚组件",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "ButtonTemplate",
                    Description = "模态按钮模板",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                }
            };
        }
    }
}
