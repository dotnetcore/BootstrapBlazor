// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISelectedItem
    {
        /// <summary>
        /// 获得/设置 显示名称
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// 获得/设置 选项值
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 获得/设置 是否选中
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 分组名称
        /// </summary>
        string GroupName { get; set; }
    }
}
