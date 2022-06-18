// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IContentSubscriber 接口
/// </summary>
internal interface IReconnectorProvider
{
    /// <summary>
    /// 注册回调方法
    /// </summary>
    /// <param name="action"></param>
    void Register(Action<RenderFragment?, RenderFragment?, RenderFragment?> action);

    /// <summary>
    /// 内容变化通知方法
    /// </summary>
    /// <param name="content"></param>
    void NotifyContentChanged(IReconnector content);
}
