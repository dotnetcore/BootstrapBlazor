using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 单元格内按钮组件
    /// </summary>
    public class TableCellButton<TItem> : Button where TItem : class, new()
    {
        /// <summary>
        /// 获得/设置 当前行绑定数据
        /// </summary>
        [Parameter] public TItem? Item { get; set; }

        /// <summary>
        /// 获得/设置 按钮点击后的回调方法
        /// </summary>
        [Parameter] public Action<TItem>? OnClickCallback { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Size == Size.None) Size = Size.ExtraSmall;

            var onClick = OnClick;
            OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, e =>
            {
                if (onClick.HasDelegate) onClick.InvokeAsync(e);

                if (Item != null) OnClickCallback?.Invoke(Item);
            });
        }
    }
}
