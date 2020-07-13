using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MenuLink
    {
        private string? ClassString => CssBuilder.Default()
            .AddClass("active", Item?.IsActive ?? false)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        private string? GetHrefString => DisableNavigation ? null : ((Item?.Items.Any() ?? false) ? "#" : Item?.Url);

        /// <summary>
        /// 获得/设置 是否禁止导航 默认为 false 允许导航
        /// </summary>
        [Parameter]
        public bool DisableNavigation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public MenuItem? Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task>? OnClick { get; set; }

        private async Task OnClickLink()
        {
            if (OnClick != null && Item != null) await OnClick(Item);
        }
    }
}
