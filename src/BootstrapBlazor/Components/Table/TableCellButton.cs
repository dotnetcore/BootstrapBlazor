// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

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
        [Parameter]
        public TItem? Item { get; set; }

        /// <summary>
        /// 获得/设置 按钮点击后的回调方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnClickCallback { get; set; }

        /// <summary>
        /// 获得/设置 OnClick 事件不刷新父组件
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnClickWithoutRenderCallback { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Size == Size.None) Size = Size.ExtraSmall;

            var onClick = OnClick;
            OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, async e =>
            {
                if (!IsDisabled)
                {
                    if (onClick.HasDelegate) await onClick.InvokeAsync(e);

                    if (Item != null && OnClickCallback != null) await OnClickCallback.Invoke(Item);
                    if (Item != null && OnClickWithoutRenderCallback != null) await OnClickWithoutRenderCallback.Invoke(Item);
                }
            });
        }
    }
}
