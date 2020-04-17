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
                        if (button is TableToolbarButton b)
                        {
                            builder.OpenElement(i++, "button");
                            builder.AddAttribute(i++, "type", "button");
                            builder.AddAttribute(i++, "role", "button");
                            builder.AddAttribute(i++, "onclick", b.OnClick);
                            builder.AddAttribute(i++, "class", CssBuilder.Default("btn").AddClass($"btn-{b.Color.ToDescriptionString()}").Build());

                            builder.OpenElement(i++, "i");
                            builder.AddAttribute(i++, "class", b.ButtonIcon);
                            builder.CloseElement(); // end i

                            builder.OpenElement(i++, "span");
                            builder.AddContent(i++, b.ButtonText);
                            builder.CloseElement(); // end span

                            builder.CloseElement(); // end button
                        }
                        else if (button is TableToolbarPopconfirmButton popButton)
                        {
                            builder.OpenComponent<PopConfirmButton>(i++);
                            builder.AddMultipleAttributes(i++, popButton.AdditionalAttributes);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.ConfirmIcon), popButton.ConfirmIcon);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.Color), popButton.Color);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.ButtonIcon), popButton.ButtonIcon);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.OnBeforeClick), popButton.OnBeforeClick);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.OnClose), popButton.OnClose);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.OnConfirm), popButton.OnConfirm);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.ButtonIcon), popButton.ButtonIcon);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.ButtonText), popButton.ButtonText);
                            builder.CloseComponent();
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
                    builder.OpenElement(index++, "div");
                    builder.AddAttribute(index++, "class", "dropdown-item");

                    // icon
                    builder.OpenElement(index++, "i");

                    // class="fa fa-plus" aria-hidden="true"

                    if (button is TableToolbarButton b)
                    {
                        builder.AddAttribute(index++, "class", b.ButtonIcon);
                        builder.AddAttribute(index++, "onclick", button.OnClick);
                    }
                    else if (button is TableToolbarPopconfirmButton popButton)
                    {
                        builder.AddAttribute(index++, "class", popButton.ButtonIcon);

                        // 移动端适配删除弹窗
                        builder.AddAttribute(index++, "onclick", EventCallback.Factory.Create(popButton, popButton.OnConfirm));
                    }
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
