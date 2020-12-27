// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITooltip 接口
    /// </summary>
    public interface ITooltip
    {
        /// <summary>
        /// 获得/设置 位置
        /// </summary>
        Placement Placement { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        string? Title { get; set; }

        /// <summary>
        /// 获得/设置 显示内容
        /// </summary>
        string? Content { get; set; }

        /// <summary>
        /// 获得/设置 内容是否为 Html
        /// </summary>
        bool IsHtml { get; set; }

        /// <summary>
        /// 获得/设置 弹出方式 默认为 Tooltip
        /// </summary>
        PopoverType PopoverType { get; set; }

        /// <summary>
        /// 获得/设置 触发方式 可组合 click focus hover 默认为 focus hover
        /// </summary>
        string Trigger { get; set; }
    }
}
