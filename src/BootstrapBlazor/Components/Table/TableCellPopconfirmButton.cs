// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 单元格内按钮组件
    /// </summary>
    public class TableCellPopconfirmButton : PopConfirmButtonBase, IDisposable
    {
        /// <summary>
        /// 获得/设置 Table 扩展按钮集合实例
        /// </summary>
        [CascadingParameter]
        protected TableExtensionButton? Buttons { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Buttons?.AddButton(this);

            if (Size == Size.None)
            {
                Size = Size.ExtraSmall;
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Buttons?.RemoveButton(this);
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
