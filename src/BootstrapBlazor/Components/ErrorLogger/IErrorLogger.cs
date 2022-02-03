// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}
