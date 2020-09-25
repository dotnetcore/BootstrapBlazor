using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class SweetAlerts
    {
        private Task OnSwal(SwalCategory cate)
        {
            SwalService.Show(new SwalOption()
            {
                Category = cate,
                Title = "Sweet 弹窗"
            });
            return Task.CompletedTask;
        }

        private Task ShowTitle()
        {
            SwalService.Show(new SwalOption()
            {
                Category = SwalCategory.Success,
                Title = "我是 Title"
            });
            return Task.CompletedTask;
        }

        private Task ShowContent()
        {
            SwalService.Show(new SwalOption()
            {
                Category = SwalCategory.Success,
                Content = "我是 Content"
            });
            return Task.CompletedTask;
        }

        private Task ShowButtons()
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
            SwalService.Show(op);
            return Task.CompletedTask;
        }

        private Task ShowComponent()
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
            SwalService.Show(op);
            return Task.CompletedTask;
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
                    ValueList = "Success/Information/Error/Warning",
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
                    ValueList = "",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsHtml",
                    Description = "内容中是否包含 Html 代码",
                    Type = "boolean",
                    ValueList = "",
                    DefaultValue = "false"
                }
            };
        }
    }
}
