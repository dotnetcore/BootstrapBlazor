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
    public class TableCellButton : Button
    {
        /// <summary>
        /// 获得/设置 按钮点击后的回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnClickCallback { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Size == Size.None)
            {
                Size = Size.ExtraSmall;
            }

            OnClickButton = EventCallback.Factory.Create<MouseEventArgs>(this, async e =>
            {
                if (IsAsync)
                {
                    ButtonIcon = LoadingIcon;
                    IsDisabled = true;
                }
                if (OnClickWithoutRender != null)
                {
                    await OnClickWithoutRender();
                }
                if (OnClickCallback != null)
                {
                    await OnClickCallback();
                }
                if (OnClick.HasDelegate)
                {
                    await OnClick.InvokeAsync(e);
                }
                if (IsAsync)
                {
                    ButtonIcon = Icon;
                    IsDisabled = false;
                }
            });
        }
    }
}
