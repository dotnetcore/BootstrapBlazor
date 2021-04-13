// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Bootstrap Blazor 组件基类
    /// </summary>
    public abstract class BootstrapComponentBase : ComponentBase
    {
        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject]
        [NotNull]
        protected IJSRuntime? JSRuntime { get; set; }

        [Inject]
        [NotNull]
        private IServiceProvider? Provider { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ServiceProviderHelper.RegisterProvider(Provider);
        }
    }
}
