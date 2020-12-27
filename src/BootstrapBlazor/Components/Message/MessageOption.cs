// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Message 组件配置类
    /// </summary>
    public class MessageOption : PopupOptionBase, IPopupHost
    {
        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示关闭按钮
        /// </summary>
        public bool ShowDismiss { get; set; }

        /// <summary>
        /// 获得/设置 显示图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 是否显示左侧 Bar
        /// </summary>
        public bool ShowBar { get; set; }

        /// <summary>
        /// 获得/设置 组件主体用于指定弹窗组件 默认为空
        /// </summary>
        /// <remarks>设置此属性值可指定弹窗主体组件</remarks>
        public ComponentBase? Host { get; set; }
    }
}
