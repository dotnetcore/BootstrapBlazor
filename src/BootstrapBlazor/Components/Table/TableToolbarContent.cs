using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Toolbar 按钮呈现组件
    /// </summary>
    public class TableToolbarContent : ComponentBase
    {
        /// <summary>
        /// 获得/设置 Table Toolbar 实例
        /// </summary>
        [CascadingParameter]
        protected TableToolbar? Toolbar { get; set; }

        /// <summary>
        /// 渲染组件方法
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // 渲染正常按钮
            if (Toolbar != null && Toolbar.Buttons.Any())
            {
                // 渲染 Toolbar 按钮
                var index = 0;
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", "btn-toolbar btn-group d-none d-sm-inline-block");
                foreach (var button in Toolbar.Buttons)
                {
                    builder.OpenElement(index++, "button");
                    builder.AddAttribute(index++, "type", "button");
                    builder.AddMultipleAttributes(index++, button.AdditionalAttributes);
                    builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(button, button.OnClick));

                    // icon
                    builder.OpenElement(index++, "i");

                    // class="fa fa-plus" aria-hidden="true"
                    builder.AddAttribute(index++, "class", button.Icon);
                    builder.AddAttribute(index++, "aria-hidden", "true");
                    builder.CloseElement();

                    // span
                    builder.OpenElement(index++, "span");
                    builder.AddContent(index++, button.Title);
                    builder.CloseElement();
                    builder.CloseElement();
                }
                builder.CloseElement();

                // 渲染移动版按钮
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", "btn-gear btn-group d-sm-none");

                builder.OpenElement(index++, "button");
                builder.AddAttribute(index++, "class", "btn btn-secondary dropdown-toggle");
                builder.AddAttribute(index++, "data-toggle", "dropdown");
                builder.AddAttribute(index++, "type", "button");

                // i
                builder.OpenElement(index++, "i");
                builder.AddAttribute(index++, "class", "fa fa-gear");
                builder.CloseElement();
                builder.CloseElement(); // end button

                // div dropdown-menu
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", "dropdown-menu");

                foreach (var button in Toolbar.Buttons)
                {
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "dropdown-item");
                    builder.AddAttribute(index++, "title", button.Title);
                    builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(button, button.OnClick));

                    // icon
                    builder.OpenElement(index++, "i");

                    // class="fa fa-plus" aria-hidden="true"
                    builder.AddAttribute(index++, "class", button.Icon);
                    builder.AddAttribute(index++, "aria-hidden", "true");
                    builder.CloseElement(); // end i

                    builder.CloseElement(); // end div
                }
                builder.CloseElement(); // end dropdown-menu
                builder.CloseElement(); // end div
            }
        }
    }
}
