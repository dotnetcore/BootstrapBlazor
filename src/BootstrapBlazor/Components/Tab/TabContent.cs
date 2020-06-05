using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TabContent 组件负责渲染 Tab 内容
    /// </summary>
    public class TabContent : ComponentBase
    {
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
            if (TabSet != null)
            {
                var index = 0;
                foreach (var item in TabSet.Items)
                {
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", GetContentClassString(item));
                    builder.AddContent(index++, item.ChildContent);
                    builder.CloseElement();
                }
            }
        }

        private static string? GetContentClassString(TabItem item) => CssBuilder.Default()
            .AddClass("d-none", !item.IsActive)
            .Build();

        /// <summary>
        /// Render 方法
        /// </summary>
        public void Render()
        {
            StateHasChanged();
        }
    }
}
