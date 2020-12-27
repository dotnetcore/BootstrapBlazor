// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗参数配置类
    /// </summary>
    public class ToastOption : PopupOptionBase, IPopupHost
    {
        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        public ToastCategory Category { get; set; }

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 组件主体用于指定弹窗组件 默认为空
        /// </summary>
        /// <remarks>设置此属性值可指定弹窗主体组件</remarks>
        public ComponentBase? Host { get; set; }
    }
}
