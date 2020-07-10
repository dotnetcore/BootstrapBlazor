using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.ComponentModel;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class Switchs
    {
        class Foo
        {
            /// <summary>
            /// 
            /// </summary>
            [DisplayName("绑定标签")]
            public bool BindValue { get; set; }
        }

        private Foo Model { get; set; } = new Foo();

        /// <summary>
        /// 
        /// </summary>
        private bool BindValue { get; set; } = true;

        /// <summary>
        ///
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="val"></param>
        private void OnValueChanged(bool val)
        {
            BindValue = val;
            Trace?.Log($"Switch CurrentValue: {val}");
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Height",
                Description = "控件高度",
                Type = "int",
                ValueList = "—",
                DefaultValue = "20"
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OffColor",
                Description = "关颜色设置",
                Type = "Color",
                ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "OffText",
                Description = "组件 Off 时显示文本",
                Type = "string",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "OnColor",
                Description = "开颜色设置",
                Type = "Color",
                ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
                DefaultValue = "Color.Success"
            },
            new AttributeItem() {
                Name = "OnText",
                Description = "组件 On 时显示文本",
                Type = "string",
                ValueList = "—",
                DefaultValue = "—"
            },
            new AttributeItem() {
                Name = "Size",
                Description = "尺寸",
                Type = "Size",
                ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "Width",
                Description = "组件宽度",
                Type = "int",
                ValueList = "—",
                DefaultValue = "40"
            },
            new AttributeItem() {
                Name = "Value",
                Description = "获取值",
                Type = "boolean",
                ValueList = " ",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示前置标签",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = "前置标签显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
        };

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "ValueChanged",
                Description="获取选择改变的值",
                Type ="EventCallback<bool>"
            },
        };
    }
}
