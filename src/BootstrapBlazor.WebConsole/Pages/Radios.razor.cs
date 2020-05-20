using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Radios
    {
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
        private void OnStateChanged(CheckboxState state, SelectedItem value)
        {
            Trace?.Log($"Checkbox state changed State: {state}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        private void OnItemChanged(CheckboxState state, SelectedItem value)
        {
            BinderLog?.Log($"Selected Value: {value.Text}");
        }

        /// <summary>
        /// 
        /// </summary>
        private string? BindValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<SelectedItem> Items => new SelectedItem[]
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海")
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem(){
                    Name = "DisplayText",
                    Description = "显示文字",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "—"
                },
                new AttributeItem(){
                    Name = "IsDisabled",
                    Description = "是否禁用",
                    Type = "boolean",
                    ValueList = " — ",
                    DefaultValue = "false"
                },
                new AttributeItem(){
                    Name = "Items",
                    Description = "绑定数据源",
                    Type = "IEnumerable<TItem>",
                    ValueList = " — ",
                    DefaultValue = "—"
                },
                new AttributeItem(){
                    Name = "State",
                    Description = "控件类型",
                    Type = "CheckboxState",
                    ValueList = " Checked / UnChecked",
                    DefaultValue = "text"
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
            }
        };
    }
}
