// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">可关闭接口</para>
/// <para lang="en">the closable inerface</para>
/// </summary>
public interface IClosable
{
    /// <summary>
    /// <para lang="zh">获得/设置 弹出窗口关闭时的回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate when the popup is closed</para>
    /// </summary>
    Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">关闭之前回调方法 返回 true 时关闭弹窗 返回 false 时阻止关闭弹窗</para>
    /// <para lang="en">Callback Method Before Closing. Return true to close, false to prevent closing</para>
    /// </summary>
    Func<Task<bool>>? OnClosingAsync { get; set; }
}
