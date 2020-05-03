using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TabContent 组件负责渲染 Tab 内容
    /// </summary>
    public class TabContent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 当前显示的 TabItem 实例
        /// </summary>
        private TabItem? Item { get; set; }

        /// <summary>
        /// 获得/设置 所属 Tab 实例
        /// </summary>
        [CascadingParameter]
        protected TabBase? TabSet { get; set; }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var item = Item ?? TabSet?.Items.FirstOrDefault(i => i.IsActive);
            builder.AddContent(0, item?.ChildContent);
        }

        /// <summary>
        /// Render 方法
        /// </summary>
        /// <param name="item"></param>
        public void Render(TabItem item)
        {
            Item = item;
            StateHasChanged();
        }

        /// <summary>
        /// 清空方法
        /// </summary>
        public void Clear()
        {
            Item = null;
            StateHasChanged();
        }
    }
}
