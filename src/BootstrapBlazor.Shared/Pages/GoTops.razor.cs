using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GoTops
    {

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
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
