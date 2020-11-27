// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ValidateForm 组件类
    /// </summary>
    public sealed partial class ValidateForm
    {
        /// <summary>
        /// A callback that will be invoked when the form is submitted.
        /// If using this parameter, you are responsible for triggering any validation
        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be valid.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnValidSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be invalid.
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnInvalidSubmit { get; set; }
    }
}
