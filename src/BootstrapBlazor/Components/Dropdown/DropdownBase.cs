// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 下拉框组件基类
    /// </summary>
    public abstract class DropdownBase<TItem> : SelectBase<TItem>
    {
        /// <summary>
        /// 是否开启分裂式
        /// </summary>
        [Parameter]
        public bool ShowSplit { get; set; }

        /// <summary>
        /// 获取菜单对齐方式
        /// </summary>
        [Parameter] public Alignment MenuAlignment { get; set; }

        /// <summary>
        /// 下拉选项方向 
        /// </summary>
        [Parameter]
        public Direction Direction { get; set; }

        /// <summary>
        /// 组件尺寸
        /// </summary>
        [Parameter]
        public Size Size { get; set; }

        /// <summary>
        /// 下拉框渲染类型
        /// </summary>
        [Parameter]
        public DropdownType DropdownType { get; set; }
    }
}
