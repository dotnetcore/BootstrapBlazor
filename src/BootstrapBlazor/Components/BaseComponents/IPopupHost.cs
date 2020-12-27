// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IPopupOption 接口定义
    /// </summary>
    public interface IPopupHost
    {
        /// <summary>
        /// 获得/设置 弹窗主体实例 默认为空
        /// </summary>
        public ComponentBase? Host { get; set; }
    }
}
