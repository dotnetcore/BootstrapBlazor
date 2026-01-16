// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IContentSubscriber 接口</para>
/// <para lang="en">IContentSubscriber Interface</para>
/// </summary>
internal interface IReconnectorProvider
{
    /// <summary>
    /// <para lang="zh">注册回调方法</para>
    /// <para lang="en">Register Callback Method</para>
    /// </summary>
    /// <param name="action"></param>
    void Register(Action<RenderFragment?, RenderFragment?, RenderFragment?> action);

    /// <summary>
    /// <para lang="zh">内容变化通知方法</para>
    /// <para lang="en">Notify Content Changed Method</para>
    /// </summary>
    /// <param name="content"></param>
    void NotifyContentChanged(IReconnector content);
}
