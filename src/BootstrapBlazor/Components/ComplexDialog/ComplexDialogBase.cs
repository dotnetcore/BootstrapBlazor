// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 复杂对话框继承的基础类
    /// </summary>
    public class ComplexDialogBase: BootstrapComponentBase
    {

        /// <summary>
        /// 入参
        /// </summary>
        [CascadingParameter(Name = "BodyContext")]
        public object? BodyContext { get; set; }

        /// <summary>
        /// 内部反调
        /// </summary>
        public Action<DialogResult>? DialogCloseAction;

        /// <summary>
        /// 对话框关闭中
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnDialogClosing(DialogEventArgs e)
        {

        }

        /// <summary>
        /// 对话框关闭之后
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnDialogClosed(DialogEventArgs e)
        {

        }
    }
}
