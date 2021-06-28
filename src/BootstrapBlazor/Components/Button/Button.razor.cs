// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Button
    {
        /// <summary>
        /// 获得 ValidateForm 实例
        /// </summary>
        [CascadingParameter]
        protected ValidateForm? ValidateForm { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (IsAsync && ValidateForm != null)
            {
                // 开启异步操作时与 ValidateForm 联动
                ValidateForm.RegisterAsyncSubmitButton(this);
            }
        }

        /// <summary>
        /// 触发按钮异步操作方法
        /// </summary>
        /// <param name="loading">true 时显示正在操作 false 时表示结束</param>
        internal void TriggerAsync(bool loading)
        {
            IsAsyncLoading = loading;
            ButtonIcon = loading ? LoadingIcon : Icon;
            SetDisable(loading);
        }
    }
}
