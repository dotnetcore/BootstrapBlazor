// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DateTimeRange 组件侧边栏快捷项目类
    /// </summary>
    public class DateTimeRangeSidebarItem
    {
        /// <summary>
        /// 获得/设置 快捷项目文本
        /// </summary>
        [NotNull]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 开始时间
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 获得/设置 开始时间
        /// </summary>
        public DateTime EndDateTime { get; set; }
    }
}
