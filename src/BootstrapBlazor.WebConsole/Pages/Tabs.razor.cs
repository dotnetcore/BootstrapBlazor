using BootstrapBlazor.Components;
using BootstrapBlazor.WebConsole.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Tabs
    {
        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<TabItem> TabItems => new List<TabItem>()
        {
            new TabItem("应用管理"),
            new TabItem("菜单管理"),
            new TabItem("数据管理"),
            new TabItem("用户管理"),
            new TabItem("部门管理"),
            new TabItem("权限管理"),
            new TabItem("字典管理")
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "IsBorderCard",
                Description = "是否为带边框卡片样式",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsCard",
                Description = "是否为卡片样式",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "TabItem 集合",
                Type = "IEnumerable<TabItemBase>",
                ValueList = " — ",
                DefaultValue = " — "
            },
        };
    }
}
