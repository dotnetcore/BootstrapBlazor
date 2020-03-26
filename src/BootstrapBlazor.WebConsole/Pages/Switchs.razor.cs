using BootstrapBlazor.WebConsole.Common;
using BootstrapBlazor.WebConsole.Pages.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    ///
    /// </summary>
    public partial class Switchs
    {
        /// <summary>
        ///
        /// </summary>
        protected bool BindValue { get; set; }

        /// <summary>
        ///
        /// </summary>
        protected Logger? Trace { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="val"></param>
        protected void OnValueChanged(bool val) => Trace?.Log($"Switch CurrentValue: {val}");

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "OnColor",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "OnColor=Color.Info OffColor=Color.None "
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = "是否禁用",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OnColor OffColor",
                Description = "开关颜色设置",
                Type = "Color",
                ValueList = " Primary / Secondary / Success / Danger / Warning / Info / Dark ",
                DefaultValue = ""
            },
        };
    }
}
