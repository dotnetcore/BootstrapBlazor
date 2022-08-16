// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IReconnector 接口
/// </summary>
public interface IReconnector
{
    /// <summary>
    /// 获得/设置 正在尝试重试连接对话框的模板
    /// </summary>
    RenderFragment? ReconnectingTemplate { get; set; }

    /// <summary>
    /// 获得/设置 连接失败对话框的模板
    /// </summary>
    RenderFragment? ReconnectFailedTemplate { get; set; }

    /// <summary>
    /// 获得/设置 连接被拒绝对话框的模板
    /// </summary>
    RenderFragment? ReconnectRejectedTemplate { get; set; }
}
