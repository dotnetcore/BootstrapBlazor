using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Checkboxs
    {
        class Foo
        {
            [DisplayName("标签文字")]
            public bool BindValue { get; set; }
        }

        private Foo Model { get; set; } = new Foo();

        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Logger? BinderLog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        private Task OnStateChanged(CheckboxState state, string value)
        {
            Trace?.Log($"Checkbox state changed State: {state}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        private Task OnItemChanged(CheckboxState state, bool value)
        {
            BinderLog?.Log($"CheckboxState: {state} - Bind Value: {value}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        private Task OnItemChangedString(CheckboxState state, string value)
        {
            BinderLog?.Log($"CheckboxState: {state} - Bind Value: {value}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        private string BindString { get; set; } = "我爱 Blazor";

        /// <summary>
        /// 
        /// </summary>
        private bool BindValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "ShowLabel",
                    Description = "是否显示前置标签",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowAfterLabel",
                    Description = "是否显示后置标签",
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "DisplayText",
                    Description = "前置标签显示文本",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem(){
                    Name = "IsDisabled",
                    Description = "是否禁用",
                    Type = "boolean",
                    ValueList = "true / false",
                    DefaultValue = "false"
                },
                new AttributeItem()
                {
                    Name = "State",
                    Description = "控件类型",
                    Type = "CheckboxState",
                    ValueList = "Mixed / Checked / UnChecked",
                    DefaultValue = "UnChecked"
                },
            };
        }

        /// <summary>
        /// 获得事件方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EventItem> GetEvents() => new EventItem[]
        {
            new EventItem()
            {
                Name = "OnStateChanged",
                Description="选择框状态改变时回调此方法",
                Type ="Action<CheckboxState, TItem>"
            },
            new EventItem()
            {
                Name = "StateChanged",
                Description="State 状态改变回调方法",
                Type ="EventCallback<CheckboxState>"
            }
        };
    }
}
