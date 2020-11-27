// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// MenuLink 组件内部封装 NavLink 组件
    /// </summary>
    public sealed partial class MenuLink
    {
        private string? ClassString => CssBuilder.Default()
            .AddClass("active", Item.IsActive && !Item.IsDisabled)
            .AddClass("disabled", Item.IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        private string? GetHrefString => (DisableNavigation || Item.IsDisabled) ? null : (Item.Items.Any() ? "#" : Item.Url?.TrimStart('/'));

        /// <summary>
        /// 获得/设置 是否禁止导航 默认为 false 允许导航
        /// </summary>
        [Parameter]
        public bool DisableNavigation { get; set; }

        /// <summary>
        /// 获得/设置 MenuItem 实例 不可为空
        /// </summary>
        [Parameter]
        [NotNull]
        public MenuItem? Item { get; set; }

        /// <summary>
        /// 获得/设置 点击菜单回调委托方法
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task>? OnClick { get; set; }

        private async Task OnClickLink()
        {
            if (OnClick != null) await OnClick(Item);
        }
    }
}
