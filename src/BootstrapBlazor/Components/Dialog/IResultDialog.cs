// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IResultDialog 接口定义</para>
/// <para lang="en">IResultDialog Interface Definition</para>
/// </summary>
public interface IResultDialog
{
    /// <summary>
    /// <para lang="zh">关闭之前回调方法 返回 true 时关闭弹窗 返回 false 时阻止关闭弹窗</para>
    /// <para lang="en">Callback Method Before Closing. Return true to close, false to prevent closing</para>
    /// </summary>
    /// <returns></returns>
    Task<bool> OnClosing(DialogResult result) => Task.FromResult(true);

    /// <summary>
    /// <para lang="zh">关闭后回调方法</para>
    /// <para lang="en">Callback Method After Closing</para>
    /// </summary>
    Task OnClose(DialogResult result);
}
