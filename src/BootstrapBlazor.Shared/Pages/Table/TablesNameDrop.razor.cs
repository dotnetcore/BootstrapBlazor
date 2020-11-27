// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TablesNameDrop
    {
#nullable disable
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public BindItem Model { get; set; }
#nullable restore

        private readonly List<SelectedItem> items = new List<SelectedItem>()
        {
            new SelectedItem { Text = "自定义姓名1", Value = "自定义姓名1" },
            new SelectedItem { Text = "自定义姓名2", Value = "自定义姓名2" },
            new SelectedItem { Text = "自定义姓名3", Value = "自定义姓名3" },
            new SelectedItem { Text = "自定义姓名4", Value = "自定义姓名4" },
        };
    }
}
