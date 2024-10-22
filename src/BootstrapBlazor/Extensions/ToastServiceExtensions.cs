﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ToastService 扩展方法
/// </summary>
public static class ToastServiceExtensions
{
    // 特别备注：此处方法使用三个参数被 UniLite 插件系统使用，请勿删除

    /// <summary>
    /// Toast 调用成功快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    public static Task Success(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Success(service, title, content, autoHide, true);

    /// <summary>
    /// Toast 调用成功快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    /// <param name="showClose">是否显示关闭按钮 默认 true</param>
    public static Task Success(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Success,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });

    /// <summary>
    /// Toast 调用错误快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    public static Task Error(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Error(service, title, content, autoHide, true);

    /// <summary>
    /// Toast 调用错误快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    /// <param name="showClose">是否显示关闭按钮 默认 true</param>
    public static Task Error(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Error,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });

    /// <summary>
    /// Toast 调用提示信息快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    public static Task Information(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Information(service, title, content, autoHide, true);

    /// <summary>
    /// Toast 调用提示信息快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    /// <param name="showClose">是否显示关闭按钮 默认 true</param>
    public static Task Information(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Information,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });

    /// <summary>
    /// Toast 调用警告信息快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    public static Task Warning(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Warning(service, title, content, autoHide, true);

    /// <summary>
    /// Toast 调用警告信息快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    /// <param name="showClose">是否显示关闭按钮 默认 true</param>
    public static Task Warning(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Warning,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });
}
