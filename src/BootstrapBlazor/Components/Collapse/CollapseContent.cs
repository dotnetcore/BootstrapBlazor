using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// CollapseContent 组件
    /// </summary>
    public class CollapseContent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 点击 CollapseItem 时的回调委托
        /// </summary>
        [Parameter]
        public Action<CollapseItem>? OnClick { get; set; }

        /// <summary>
        /// 获得/设置 所属 Collapse 实例
        /// </summary>
        [CascadingParameter]
        protected CollapseBase? Collapse { get; set; }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Collapse != null)
            {
                foreach (var item in Collapse.Items)
                {
                    var index = 0;
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "card");

                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "card-header");

                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "card-header-wrapper");

                    builder.OpenElement(index++, "button");
                    builder.AddAttribute(index, "class", GetButtonClassString(item.IsCollapsed));
                    builder.AddAttribute(index, "type", "button");
                    builder.AddAttribute(index, "data-toggle", "collapse");
                    builder.AddAttribute(index, "aria-expanded", item.IsCollapsed ? "false" : "true");
                    builder.AddContent(index++, item.Text);
                    builder.CloseElement(); // end button

                    if (Collapse.ShowArrow)
                    {
                        builder.OpenElement(index++, "i");
                        builder.AddAttribute(index++, "class", "fa fa-angle-down");
                        builder.CloseElement();
                    }
                    builder.CloseElement(); // end div

                    builder.CloseElement(); // end div

                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", GetClassString(item.IsCollapsed));
                    builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(this, () => OnClick?.Invoke(item)));

                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "card-body");
                    builder.AddContent(index++, item.ChildContent);

                    builder.CloseElement(); // end body

                    builder.CloseElement(); // end div
                    builder.CloseElement(); // end card
                }
            }
        }

        private string? GetButtonClassString(bool collapsed) => CssBuilder.Default("btn btn-link")
            .AddClass("collapsed", collapsed)
            .Build();

        private string? GetClassString(bool collpased) => CssBuilder.Default("collapse-item")
            .AddClass("collapse", collpased)
            .AddClass("collapse show", !collpased)
            .Build();
    }
}
