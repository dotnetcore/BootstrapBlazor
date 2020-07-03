using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
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
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectedItem> RadioItems { get; private set; } = new SelectedItem[] {
            new SelectedItem("false", "不保持状态") { Active = true },
            new SelectedItem("true", "保持状态")
        };

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        private DialogService? DialogService { get; set; }

        private Task OnStateChanged(CheckboxState state, SelectedItem item)
        {
            KeepState = bool.Parse(item.Value);
            return Task.CompletedTask;
        }

        private bool KeepState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task OnClick()
        {
            DialogService?.Show(new DialogOption()
            {
                Title = "我是服务创建的弹出框",
                BodyTemplate = DynamicComponent.CreateComponent<Button>(new KeyValuePair<string, object>[]
                {
                    new KeyValuePair<string, object>(nameof(Button.ChildContent), new RenderFragment(builder => builder.AddContent(0, "我是服务创建的按钮")))
                })
                .Render()
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task OnClickCounter()
        {
            DialogService?.Show(new DialogOption()
            {
                Title = "自带的 Counter 组件",
                KeepChildrenState = KeepState,
                Component = DynamicComponent.CreateComponent<Counter>()
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task OnClickParameter()
        {
            DialogService?.Show(new DialogOption()
            {
                Title = "自带的 Counter 组件",
                BodyContext = "我是传参",
                BodyTemplate = builder =>
                {
                    var index = 0;
                    builder.OpenComponent<DemoComponent>(index++);
                    builder.CloseComponent();
                }
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
                    Name = "Component",
                    Description = "对话框 Body 中引用的组件的参数",
                    Type = "DynamicComponent",
                    ValueList = " — ",
                    DefaultValue = " — "
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
                    Description = "模态主体 ModalBody 组件",
                    Type = "RenderFragment",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem() {
                    Name = "KeepChildrenState",
                    Description = "是否保持弹窗内组件状态",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
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
