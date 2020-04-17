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
                    builder.AddContent(index++, new RenderFragment(builder =>
                    {
                        var i = 0;
                        if (button is TableToolbarButton)
                        {
                            builder.OpenComponent<Button>(i++);
                            builder.AddAttribute(i++, nameof(Button.ChildContent), button.ChildContent);
                            builder.AddAttribute(i++, nameof(Button.OnClick), button.OnClick);
                            builder.AddAttribute(i++, nameof(Button.Color), button.Color);
                            builder.CloseComponent();
                        }
                        else if (button is TableToolbarPopconfirmButton)
                        {
                            var b = button as TableToolbarPopconfirmButton;
                            if (b != null)
                            {
                                builder.OpenComponent<PopConfirmButton>(i++);
                                builder.AddMultipleAttributes(i++, b.AdditionalAttributes);
                                builder.AddAttribute(i++, nameof(PopConfirmButton.ConfirmIcon), b.ConfirmIcon);
                                builder.AddAttribute(i++, nameof(PopConfirmButton.Color), b.Color);
                                builder.AddAttribute(i++, nameof(PopConfirmButton.Icon), b.Icon);
                                builder.AddAttribute(i++, nameof(PopConfirmButton.OnBeforeClick), b.OnBeforeClick);
                                builder.AddAttribute(i++, nameof(PopConfirmButton.OnClose), b.OnClose);
                                builder.AddAttribute(i++, nameof(PopConfirmButton.OnConfirm), b.OnConfirm);
                                builder.CloseComponent();
                            }
                        }
                    }));
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

                    /*
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
                    */
                }
                builder.CloseElement(); // end dropdown-menu
                builder.CloseElement(); // end div
            }
        }
    }
}
