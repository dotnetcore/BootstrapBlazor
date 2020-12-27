// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 弹窗类配置项基类
    /// </summary>
    public abstract class PopupOptionBase
    {
        /// <summary>
        /// 获得/设置 Toast Body 子组件
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏时间间隔
        /// </summary>
        public int Delay { get; set; } = 4000;

        /// <summary>
        /// 获得/设置 是否强制使用本实例的延时时间，防止值被全局配置覆盖 默认 false
        /// </summary>
        public bool ForceDelay { get; set; }
    }
}
