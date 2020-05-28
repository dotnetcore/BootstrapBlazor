using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Dialogs
    {
        private Alert? AlertTest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private DialogService? DialogService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task OnClick()
        {
            DialogService?.Show(new DialogOption()
            {
                Title = "我是服务创建的弹出框",
                BodyTemplate = new RenderFragment(builder =>
                {
                    var index = 0;
                    builder.OpenComponent<Button>(index++);
                    builder.AddAttribute(index++, nameof(Button.ChildContent), new RenderFragment(builder =>
                    {
                        builder.AddContent(0, "服务创建的按钮");
                    }));
                    builder.CloseComponent();
                })
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task OnClickRef()
        {
            DialogService?.Show(new DialogOption()
            {
                Title = "我是服务创建的弹出框",
                BodyComponent = AlertTest
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "BodyTemplate",
                    Description = "模态主体 ModalBody 组件",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "BodyComponent",
                    Description = "对话框 Body 中引用的组件实例",
                    Type = "ComponentBase",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "FooterTemplate",
                    Description = "模态底部 ModalFooter 组件",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "IsCentered",
                    Description = "是否垂直居中",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "true"
                },
                new AttributeItem() {
                    Name = "IsScrolling",
                    Description = "是否弹窗正文超长时滚动",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowCloseButton",
                    Description = "是否显示关闭按钮",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "true"
                },
                new AttributeItem() {
                    Name = "ShowFooter",
                    Description = "是否显示 Footer",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "true"
                },
                new AttributeItem() {
                    Name = "Size",
                    Description = "尺寸",
                    Type = "Size",
                    ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                    DefaultValue = "Large"
                },
                new AttributeItem() {
                    Name = "Title",
                    Description = "弹窗标题",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " 未设置 "
                },
            };
        }
    }
}
