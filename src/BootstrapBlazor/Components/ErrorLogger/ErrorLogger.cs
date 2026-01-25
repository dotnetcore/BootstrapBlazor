// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ErrorLogger 全局异常组件</para>
/// <para lang="en">ErrorLogger Global Exception Component</para>
/// </summary>
public class ErrorLogger : ComponentBase, IErrorLogger
{
    /// <summary>
    /// <inheritdoc cref="IErrorLogger.EnableErrorLogger"/>
    /// </summary>
    [Parameter]
    public bool EnableErrorLogger { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="IErrorLogger.EnableILogger"/>
    /// </summary>
    [Parameter]
    public bool EnableILogger { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="IErrorLogger.ShowToast"/>
    /// </summary>
    [Parameter]
    public bool ShowToast { get; set; } = true;

    /// <summary>
    /// <inheritdoc cref="IErrorLogger.ToastTitle"/>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ToastTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义错误处理回调方法</para>
    /// <para lang="en">Gets or sets Custom Error Handler</para>
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets Child Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 异常显示模板 默认 null</para>
    /// <para lang="en">Gets or sets Exception Display Template Default null</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">用于自定义异常显示 UI</para>
    /// <para lang="en">Used to customize exception display UI</para>
    /// </remarks>
    [Parameter]
    public RenderFragment<Exception>? ErrorContent { get; set; }

    [NotNull]
    private BootstrapBlazorErrorBoundary? _errorBoundary = default;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (EnableErrorLogger)
        {
            builder.OpenComponent<CascadingValue<IErrorLogger>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<>.Value), this);
            builder.AddAttribute(2, nameof(CascadingValue<>.IsFixed), true);
            builder.AddAttribute(3, nameof(CascadingValue<>.ChildContent), RenderError);
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(10, ChildContent);
        }
    }

    private RenderFragment RenderError => builder =>
    {
        builder.OpenComponent<BootstrapBlazorErrorBoundary>(0);
        builder.AddAttribute(1, nameof(BootstrapBlazorErrorBoundary.OnErrorHandleAsync), OnErrorHandleAsync);
        builder.AddAttribute(2, nameof(BootstrapBlazorErrorBoundary.ShowToast), ShowToast);
        builder.AddAttribute(3, nameof(BootstrapBlazorErrorBoundary.ToastTitle), ToastTitle);
        builder.AddAttribute(4, nameof(BootstrapBlazorErrorBoundary.ErrorContent), ErrorContent);
        builder.AddAttribute(5, nameof(BootstrapBlazorErrorBoundary.ChildContent), ChildContent);
        builder.AddAttribute(6, nameof(BootstrapBlazorErrorBoundary.EnableILogger), EnableILogger);
        builder.AddComponentReferenceCapture(7, obj => _errorBoundary = obj as BootstrapBlazorErrorBoundary);
        builder.CloseComponent();
    };

    /// <summary>
    /// <inheritdoc cref="IErrorLogger.HandlerExceptionAsync(Exception)"/>
    /// </summary>
    public async Task HandlerExceptionAsync(Exception exception)
    {
        if (_errorBoundary != null)
        {
            await _errorBoundary.HandlerExceptionAsync(exception);
        }
    }
}
