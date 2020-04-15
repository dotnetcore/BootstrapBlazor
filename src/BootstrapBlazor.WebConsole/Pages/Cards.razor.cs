using BootstrapBlazor.WebConsole.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// Card展示组件
    /// </summary>
    public partial class Cards
    {
        /// <summary>
        /// Card属性
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem{
                Name = "Color",
                Description = "设置卡片边框颜色",
                Type = "Color",
                ValueList = "None / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark",
                DefaultValue = " — "
            },
            new AttributeItem{
                Name = "IsCenter",
                Description = "通过设置,IsCenter=true 使内容居中",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem{
                Name = "IsShowHeader",
                Description = "通过设置,IsShowHeader=true,显示Header区域",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            },
            new AttributeItem{
                Name = "IsShowFooter",
                Description = "通过设置,IsShowFooter=true,显示Footer区域",
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            }
        };
    }
}

