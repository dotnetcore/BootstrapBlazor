// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Delay 配置类
    /// </summary>
    public class BootstrapBlazorOptions
    {
        /// <summary>
        /// 获得/设置 Toast 组件 Delay 默认值 默认为 0
        /// </summary>
        public int ToastDelay { get; set; }

        /// <summary>
        /// 获得/设置 Message 组件 Delay 默认值 默认为 0
        /// </summary>
        public int MessageDelay { get; set; }

        /// <summary>
        /// 获得/设置 Swal 组件 Delay 默认值 默认为 0
        /// </summary>
        public int SwalDelay { get; set; }

        /// <summary>
        /// 获得/设置 默认 UI 文化信息 默认为 null 未设置采用系统设置
        /// </summary>
        public string? DefaultUICultureInfoName { get; set; }

        /// <summary>
        /// 获得 组件内置本地化语言列表
        /// </summary>
        public IEnumerable<string> SupportedCultures { get; } = new string[] { "zh-CN", "en-US" };
    }
}
