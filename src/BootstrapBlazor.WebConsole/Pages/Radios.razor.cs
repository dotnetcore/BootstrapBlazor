using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Radios
    {
        /// <summary>
        /// 
        /// </summary>
        protected Logger? Trace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Logger? BinderLog { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        protected void OnStateChanged(CheckboxState state, SelectedItem value)
        {
            Trace?.Log($"Checkbox state changed State: {state}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        protected void OnItemChanged(CheckboxState state, SelectedItem value)
        {
            BinderLog?.Log($"Selected Value: {value.Text}");
        }

        /// <summary>
        /// 
        /// </summary>
        protected string? BindValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<SelectedItem> Items => new SelectedItem[]
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海")
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem()
                {
                    Name = "State",
                    Description = "控件类型",
                    Type = "CheckboxState",
                    ValueList = "Mixed / Checked / UnChecked",
                    DefaultValue = "text"
                }
            };
        }
    }
}
