// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ITableToolbarButton 接口
/// </summary>
public interface ITableToolbarButton<TItem> : IToolbarComponent
{
    /// <summary>
    /// 获得/设置 选中一行时启用按钮 默认 false 均可用
    /// </summary>
    bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// 获得/设置 按钮是否被禁用回调方法
    /// </summary>
    Func<IEnumerable<TItem>, bool>? IsDisabledCallback { get; set; }
}
