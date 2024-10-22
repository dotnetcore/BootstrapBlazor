// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public interface IErrorLogger
{
    /// <summary>
    /// 自定义 Error 处理方法
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    Task HandlerExceptionAsync(Exception ex);

    /// <summary>
    /// 获得 是否显示 Error 提示弹窗 默认 true 显示
    /// </summary>
    bool ShowToast { get; }

    /// <summary>
    /// 获得 Error Toast 弹窗标题
    /// </summary>
    string? ToastTitle { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    void Register(ComponentBase component);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    void UnRegister(ComponentBase component);
}
