// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ToastService 扩展方法
/// </summary>
public static class ToastServiceExtensions
{
    /// <summary>
    /// Toast 调用成功快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title">Title 属性</param>
    /// <param name="content">Content 属性</param>
    /// <param name="autoHide">自动隐藏属性默认为 true</param>
    /// <returns></returns>
    public static Task Success(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Success,
        IsAutoHide = autoHide,
        Title = title ?? "",
        Content = content ?? ""
    });

    /// <summary>
    /// Toast 调用错误快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    /// <returns></returns>
    public static Task Error(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Error,
        IsAutoHide = autoHide,
        Title = title ?? "",
        Content = content ?? ""
    });

    /// <summary>
    /// Toast 调用提示信息快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    /// <returns></returns>
    public static Task Information(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Information,
        IsAutoHide = autoHide,
        Title = title ?? "",
        Content = content ?? ""
    });

    /// <summary>
    /// Toast 调用警告信息快捷方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="autoHide"></param>
    /// <returns></returns>
    public static Task Warning(this ToastService service, string? title = null, string? content = null, bool autoHide = true) => service.Show(new ToastOption()
    {
        Category = ToastCategory.Warning,
        IsAutoHide = autoHide,
        Title = title ?? "",
        Content = content ?? ""
    });
}
