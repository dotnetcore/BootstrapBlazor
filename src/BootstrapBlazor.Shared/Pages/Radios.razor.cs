using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
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

        private IEnumerable<SelectedItem> DemoValues { get; set; } = new List<SelectedItem>(2)
        {
            new SelectedItem("1", "选项一"),
            new SelectedItem("2", "选项二")
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        private Task OnStateChanged(CheckboxState state, SelectedItem value)
        {
            Trace?.Log($"组件选中值: {value.Value} 显示值: {value.Text}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        private Task OnItemChanged(CheckboxState state, SelectedItem value)
        {
            BinderLog?.Log($"Selected Value: {value.Text}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        private string? BindValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<SelectedItem> Items { get; set; } = new SelectedItem[]
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海")
        };

        private SelectedItem BindRadioItem { get; set; } = new SelectedItem();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "DisplayText",
                    Description = "显示文字",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = "—"
                },
                new AttributeItem() {
                    Name = "IsDisabled",
                    Description = "是否禁用",
                    Type = "boolean",
                    ValueList = "true / false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "Items",
                    Description = "绑定数据源",
                    Type = "IEnumerable<TItem>",
                    ValueList = " — ",
                    DefaultValue = "—"
                },
                new AttributeItem() {
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
                Type ="Func<CheckboxState, TItem, Task>"
            }
        };
    }
}
