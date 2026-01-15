// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IErrorLogger 接口
/// </summary>
public interface IErrorLogger
{
    /// <summary>
    /// 获得/设置 是否开启全局异常捕获 默认 true
    /// </summary>
    bool EnableErrorLogger { get; set; }

    /// <summary>
    /// 获得/设置 是否启用日志记录功能 默认 true 启用
    /// <para>设置 false 后关闭记录日志功能</para>
    /// </summary>
    bool EnableILogger { get; }

    /// <summary>
    /// 获得 是否显示 Error 提示弹窗 默认 true
    /// </summary>
    bool ShowToast { get; }

    /// <summary>
    /// 获得 Error Toast 弹窗标题 默认读取资源文件内容
    /// </summary>
    string? ToastTitle { get; }

    /// <summary>
    /// 获得/设置 自定义 Error 处理方法 默认 null
    /// </summary>
    /// <param name="ex"></param>
    Task HandlerExceptionAsync(Exception ex);

    /// <summary>
    /// 注册方法
    /// </summary>
    /// <param name="component"></param>
    void Register(IHandlerException component);

    /// <summary>
    /// 注销方法
    /// </summary>
    /// <param name="component"></param>
    void UnRegister(IHandlerException component);
}
