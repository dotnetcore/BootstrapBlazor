// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// BootstrapBlazorDataAnnotationsValidator 验证组件
    /// </summary>
    public class BootstrapBlazorDataAnnotationsValidator : ComponentBase
    {
        /// <summary>
        /// 获得/设置 当前编辑数据上下文
        /// </summary>
        [CascadingParameter]
        private EditContext? CurrentEditContext { get; set; }

        /// <summary>
        /// 获得/设置 当前编辑窗体上下文
        /// </summary>
        [CascadingParameter]
        public ValidateForm? EditForm { get; set; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            if (EditForm == null)
            {
                throw new InvalidOperationException($"{nameof(BootstrapBlazorDataAnnotationsValidator)} requires a cascading " +
                    $"parameter of type {nameof(ValidateForm)}. For example, you can use {nameof(BootstrapBlazorDataAnnotationsValidator)} " +
                    $"inside an {nameof(ValidateForm)}.");
            }

            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(BootstrapBlazorDataAnnotationsValidator)} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {nameof(BootstrapBlazorDataAnnotationsValidator)} inside an EditForm.");
            }

            CurrentEditContext.AddEditContextDataAnnotationsValidation(EditForm);
        }
    }
}
