using BootstrapBlazor.WebConsole.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Tooltips
    {
        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Placement",
                Description = "位置",
                Type = "Placement",
                ValueList = "Auto / Top / Left / Bottom / Right",
                DefaultValue = "Auto"
            }
        };
    }
}
