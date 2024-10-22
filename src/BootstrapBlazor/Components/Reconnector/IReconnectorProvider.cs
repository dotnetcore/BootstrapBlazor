// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
