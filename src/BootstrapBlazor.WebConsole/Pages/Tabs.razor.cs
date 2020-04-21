using BootstrapBlazor.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    partial class Tabs
    {
        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<TabItem> TabItems => new List<TabItem>()
        {
            new TabItem("应用管理"),
            new TabItem("菜单管理"),
            new TabItem("数据管理"),
            new TabItem("用户管理"),
            new TabItem("部门管理"),
            new TabItem("权限管理"),
            new TabItem("字典管理")
        };
    }
}
