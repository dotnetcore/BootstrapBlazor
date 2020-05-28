using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Spinners
    {
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
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "Size",
                Description = "尺寸",
                Type = "Size",
                ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "SpinnerType",
                Description = "图标类型",
                Type = "SpinnerType",
                ValueList = " Border / Grow ",
                DefaultValue = "SpinnerType.Border"
            },
        };
    }
}
