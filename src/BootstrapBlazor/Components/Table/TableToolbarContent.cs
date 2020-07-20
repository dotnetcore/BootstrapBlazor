using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table Toolbar 按钮呈现组件
    /// </summary>
    public class TableToolbarContent<TItem> : ComponentBase
    {
        /// <summary>
        /// 获得/设置 Table Toolbar 实例
        /// </summary>
        [CascadingParameter]
        protected TableToolbar<TItem>? Toolbar { get; set; }

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
                        if (button is TableToolbarButton<TItem> b)
                        {
                            builder.OpenComponent<Button>(i++);
                            builder.AddAttribute(i++, nameof(Button.OnClick), CreateCallback(b));
                            builder.AddAttribute(i++, nameof(Button.Color), b.Color);
                            builder.AddAttribute(i++, nameof(Button.Icon), b.Icon);
                            builder.AddAttribute(i++, nameof(Button.Text), b.Text);
                            builder.CloseComponent(); // end button
                        }
                        else if (button is TableToolbarPopconfirmButton popButton)
                        {
                            builder.OpenComponent<PopConfirmButton>(i++);
                            builder.AddMultipleAttributes(i++, popButton.AdditionalAttributes);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.ConfirmIcon), popButton.ConfirmIcon);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.Color), popButton.Color);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.Icon), popButton.Icon);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.OnBeforeClick), popButton.OnBeforeClick);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.OnClose), popButton.OnClose);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.OnConfirm), popButton.OnConfirm);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.Icon), popButton.Icon);
                            builder.AddAttribute(i++, nameof(PopConfirmButton.Text), popButton.Text);
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

                    if (button is TableToolbarButton<TItem> b)
                    {
                        builder.AddAttribute(index++, "class", b.Icon);
                        builder.AddAttribute(index++, "onclick", CreateCallback(b));
                    }
                    else if (button is TableToolbarPopconfirmButton popButton)
                    {
                        builder.AddAttribute(index++, "class", popButton.Icon);

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

        private EventCallback<MouseEventArgs> CreateCallback(TableToolbarButton<TItem> button) => EventCallback.Factory.Create<MouseEventArgs>(this, async e =>
        {
            if (button.OnClick.HasDelegate) await button.OnClick.InvokeAsync(e);

            // 传递当前选中行给回调委托方法
            if (Toolbar != null && button.OnClickCallback != null)
            {
                await button.OnClickCallback.Invoke(Toolbar.OnGetSelectedRows());
            }
        });
    }
}
