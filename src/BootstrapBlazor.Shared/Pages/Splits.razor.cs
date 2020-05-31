using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Splits
    {
        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "IsVertical",
                Description = "样式",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Basis",
                Description = "第一个窗格位置占比",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "50%"
            },
            new AttributeItem() {
                Name = "FirstPaneTemplate",
                Description = "第一个窗格模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SecondPaneTemplate",
                Description = "第二个窗格模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
