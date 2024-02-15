// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 工具栏按钮接口
/// </summary>
public interface IToolbarComponent
{
    /// <summary>
    /// 获得/设置 是否显示 默认 true 显示
    /// </summary>
    bool IsShow { get; set; }
}
