// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IErrorLogger 接口</para>
/// <para lang="en">IErrorLogger Interface</para>
/// </summary>
public interface IErrorLogger
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否开启全局异常捕获 默认 true</para>
    /// <para lang="en">Gets or sets Whether to Enable Global Exception Capture Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    bool EnableErrorLogger { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用日志记录功能 默认 true 启用 设置 false 后关闭记录日志功能</para>
    /// <para lang="en">Gets or sets Whether to Enable Logging. Default value is true. Set false to disable logging</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    bool EnableILogger { get; }

    /// <summary>
    /// <para lang="zh">获得 是否显示 Error 提示弹窗 默认 true</para>
    /// <para lang="en">Get Whether to Show Error Toast Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    bool ShowToast { get; }

    /// <summary>
    /// <para lang="zh">获得 Error Toast 弹窗标题 默认读取资源文件内容</para>
    /// <para lang="en">Get Error Toast Title Default Read Resource File</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    string? ToastTitle { get; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义 Error 处理方法 默认 null</para>
    /// <para lang="en">Gets or sets Custom Error Handler Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <param name="ex"></param>
    Task HandlerExceptionAsync(Exception ex);

    /// <summary>
    /// <para lang="zh">注册方法</para>
    /// <para lang="en">Register Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <param name="component"></param>
    void Register(IHandlerException component);

    /// <summary>
    /// <para lang="zh">注销方法</para>
    /// <para lang="en">Unregister Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <param name="component"></param>
    void UnRegister(IHandlerException component);
}
