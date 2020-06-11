using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Scrolls
    {
        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ChildContent",
                Description = "子组件",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Height",
                Description = "组件高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "Width",
                Description = "组件宽度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "IsDark",
                Description = "是否为暗黑模式",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAutoHide",
                Description = "是否自动隐藏",
                Type = "true",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Delay",
                Description = "自动隐藏延时时间",
                Type = "int",
                ValueList = "—",
                DefaultValue = "1000"
            }
        };
    }
}
