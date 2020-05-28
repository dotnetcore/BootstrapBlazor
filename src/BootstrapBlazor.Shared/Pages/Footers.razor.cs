using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Footers
    {

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Text",
                Description = "页脚组件显示的文字",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Target",
                Description = "页脚组件控制的滚动条组件 ID",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
