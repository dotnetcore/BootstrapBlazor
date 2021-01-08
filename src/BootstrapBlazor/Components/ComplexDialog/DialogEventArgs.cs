// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 对话框事件
    /// </summary>
    public class DialogEventArgs : EventArgs
    {
        /// <summary>
        /// 关闭方式
        /// </summary>
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// 是否取消，只有Closing事件有用
        /// </summary>
        public bool Cancel { get; set; }
    }
}
