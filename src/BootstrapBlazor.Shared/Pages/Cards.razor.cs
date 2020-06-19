using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// Card展示组件
    /// </summary>
    public sealed partial class Cards
    {
        /// <summary>
        /// Card属性
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem() {
                Name = "CardBody",
                Description = "获得/设置 CardBody",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CardFooter",
                Description = "获得/设置 CardFooter",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "CardHeader",
                Description = "获得/设置 CardHeader",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
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
        };
    }
}

