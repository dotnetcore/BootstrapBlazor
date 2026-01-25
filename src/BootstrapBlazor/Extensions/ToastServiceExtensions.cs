// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ToastService 扩展方法</para>
/// <para lang="en">ToastService extension methods</para>
/// </summary>
public static class ToastServiceExtensions
{
    /// <summary>
    /// <para lang="zh">Toast 调用成功快捷方法</para>
    /// <para lang="en">Toast success shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    public static Task Success(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Success(service, title, content, autoHide, true);

    /// <summary>
    /// <para lang="zh">Toast 调用成功快捷方法</para>
    /// <para lang="en">Toast success shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    /// <param name="showClose"></param>
    public static Task Success(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Success,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });

    /// <summary>
    /// <para lang="zh">Toast 调用错误快捷方法</para>
    /// <para lang="en">Toast error shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    public static Task Error(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Error(service, title, content, autoHide, true);

    /// <summary>
    /// <para lang="zh">Toast 调用错误快捷方法</para>
    /// <para lang="en">Toast error shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    /// <param name="showClose"></param>
    public static Task Error(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Error,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });

    /// <summary>
    /// <para lang="zh">Toast 调用提示信息快捷方法</para>
    /// <para lang="en">Toast information shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    public static Task Information(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Information(service, title, content, autoHide, true);

    /// <summary>
    /// <para lang="zh">Toast 调用提示信息快捷方法</para>
    /// <para lang="en">Toast information shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    /// <param name="showClose"></param>
    public static Task Information(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Information,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });

    /// <summary>
    /// <para lang="zh">Toast 调用警告信息快捷方法</para>
    /// <para lang="en">Toast warning shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    public static Task Warning(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => Warning(service, title, content, autoHide, true);

    /// <summary>
    /// <para lang="zh">Toast 调用警告信息快捷方法</para>
    /// <para lang="en">Toast warning shortcut method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    /// <param name="showClose"></param>
    public static Task Warning(this ToastService service, string? title, string? content, bool autoHide, bool showClose) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Warning,
        IsAutoHide = autoHide,
        Title = title,
        Content = content,
        ShowClose = showClose
    });
}
