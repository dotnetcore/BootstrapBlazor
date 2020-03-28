using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Checkboxs
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
        protected void OnStateChanged(CheckboxState state, string value)
        {
            Trace?.Log($"Checkbox state changed State: {state}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="value"></param>
        protected void OnItemChanged(CheckboxState state, bool value)
        {
            BinderLog?.Log($"CheckboxState: {state} - Bind Value: {value}");
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool BindValue { get; set; }

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
