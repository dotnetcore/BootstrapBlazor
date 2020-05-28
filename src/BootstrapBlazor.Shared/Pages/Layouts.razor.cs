using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Layouts
    {
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Header",
                Description = "页头组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Side",
                Description = "侧边栏组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SideWidth",
                Description = "侧边栏宽度，支持百分比",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "300px"
            },
            new AttributeItem() {
                Name = "Main",
                Description = "内容组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Footer",
                Description = "页脚组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsFullSide",
                Description = "侧边栏是否占满整个左边",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
    }
}
