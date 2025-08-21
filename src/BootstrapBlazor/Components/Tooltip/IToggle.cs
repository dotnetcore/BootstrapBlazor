// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///     状态可切换接口定义
/// </summary>
public interface IToggle
{
    /// <summary>
    /// 显示弹窗方法
    /// </summary>
    /// <param name="delay">延时指定毫秒后显示弹窗 默认 null 不延时</param>
    /// <returns></returns>
    Task Show(int? delay = null);

    /// <summary>
    /// 关闭弹窗方法
    /// </summary>
    /// <param name="delay">延时指定毫秒后关闭弹窗 默认 null 不延时</param>
    /// <returns></returns>
    Task Hide(int? delay = null);

    /// <summary>
    /// 切换弹窗当前状态方法
    /// </summary>
    /// <param name="delay">延时指定毫秒后切换弹窗方法 默认 null 不延时</param>
    /// <returns></returns>
    Task Toggle(int? delay = null);
}
