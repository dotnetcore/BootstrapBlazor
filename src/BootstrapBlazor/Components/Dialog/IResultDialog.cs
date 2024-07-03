// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
