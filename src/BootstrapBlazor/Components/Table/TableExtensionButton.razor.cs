// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableExtensionButton
    {
        /// <summary>
        /// 获得 Toolbar 扩展按钮集合
        /// </summary>
        private List<ButtonBase> Buttons { get; } = new();

        /// <summary>
        /// Specifies the content to be rendered inside this
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Action? OnClickButton { get; set; }

        /// <summary>
        /// 添加按钮到工具栏方法
        /// </summary>
        public void AddButton(ButtonBase button) => Buttons.Add(button);

        /// <summary>
        /// 添加按钮到工具栏方法
        /// </summary>
        public void RemoveButton(ButtonBase button) => Buttons.Remove(button);

        private async Task OnClick(TableCellButton b)
        {
            if (b.AutoSelectedRowWhenClick)
            {
                OnClickButton?.Invoke();
            }
            if (b.OnClick.HasDelegate)
            {
                await b.OnClick.InvokeAsync();
            }
            if (b.OnClickWithoutRender != null)
            {
                await b.OnClickWithoutRender();
            }
        }
    }
}
