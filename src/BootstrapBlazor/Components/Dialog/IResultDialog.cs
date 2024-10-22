// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IResultDialog 接口定义
/// </summary>
public interface IResultDialog
{
    /// <summary>
    /// 关闭之前回调方法 返回 true 时关闭弹窗 返回 false 时阻止关闭弹窗
    /// </summary>
    /// <returns></returns>
    Task<bool> OnClosing(DialogResult result) => Task.FromResult(true);

    /// <summary>
    /// 关闭后回调方法
    /// </summary>
    Task OnClose(DialogResult result);
}
