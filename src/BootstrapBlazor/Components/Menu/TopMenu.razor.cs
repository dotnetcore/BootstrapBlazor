using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TopMenu
    {
        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<MenuItem> Items { get; set; } = new MenuItem[0];

        /// <summary>
        /// 获得/设置 菜单项点击回调委托
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task> OnClick { get; set; } = _ => Task.CompletedTask;

        private async Task OnClickMenuItem(MenuItem item)
        {
            CascadingSetActive(Items);
            CascadingSetParentActive(item);

            await OnClick(item);
            StateHasChanged();
        }

        /// <summary>
        /// 级联设置菜单 active=true 方法
        /// </summary>
        /// <param name="item"></param>
        private static void CascadingSetParentActive(MenuItem item)
        {
            item.IsActive = true;
            var current = item;
            while (current.Parent != null)
            {
                current.Parent.IsActive = true;
                current = current.Parent;
            }
        }

        /// <summary>
        /// 级联设置菜单 Active=false 方法
        /// </summary>
        /// <param name="items"></param>
        private static void CascadingSetActive(IEnumerable<MenuItem> items)
        {
            foreach (var item in items)
            {
                item.IsActive = false;
                if (item.Items.Any()) CascadingSetActive(item.Items);
            }
        }

        /// <summary>
        /// 渲染菜单方法
        /// </summary>
        /// <returns></returns>
        private RenderFragment RenderDropdownItem(MenuItem item) => new RenderFragment(builder =>
        {
            var index = 0;
            builder.OpenComponent<SubMenu>(index++);
            builder.AddAttribute(index++, nameof(SubMenu.Item), item);
            builder.AddAttribute(index++, nameof(SubMenu.OnClick), new Func<MenuItem, Task>(i => OnClickMenuItem(i)));
            builder.AddAttribute(index++, "class", "dropdown-item");
            builder.CloseComponent();
        });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        private string? GetClassString(string className, bool active) => CssBuilder.Default(className)
            .AddClass("active", active)
            .Build();
    }
}
