// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 内部使用 自定义异常组件
/// </summary>
class BootstrapBlazorErrorBoundary : ErrorBoundaryBase
{
    [Inject]
    [NotNull]
    private ILogger<BootstrapBlazorErrorBoundary>? Logger { get; set; }

    [Inject]
    [NotNull]
    private IConfiguration? Configuration { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private IHostEnvironment? HostEnvironment { get; set; }

    /// <summary>
    /// 获得/设置 自定义错误处理回调方法
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否显示弹窗 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowToast { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否启用日志记录功能 默认 true 启用
    /// </summary>
    [Parameter]
    public bool EnableILogger { get; set; } = true;

    /// <summary>
    /// 获得/设置 Toast 弹窗标题
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ToastTitle { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="exception"></param>
    protected override Task OnErrorAsync(Exception exception)
    {
        if (EnableILogger && Logger.IsEnabled(LogLevel.Error))
        {
            Logger.LogError(exception, "BootstrapBlazorErrorBoundary OnErrorAsync log this error occurred at {Page}", NavigationManager.Uri);
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // 页面生命周期内异常直接调用这里
        var ex = CurrentException ?? _exception;
        if (ex != null)
        {
            // 处理自定义异常逻辑
            if (OnErrorHandleAsync != null)
            {
                _ = OnErrorHandleAsync(Logger, ex);
                return;
            }

            // 渲染异常内容
            builder.AddContent(0, ExceptionContent(ex));

            // 重置 CurrentException
            ResetException();
        }
        else
        {
            // 渲染正常内容
            builder.AddContent(1, ChildContent);
        }
    }

    private PropertyInfo? _currentExceptionPropertyInfo;

    private void ResetException()
    {
        _exception = null;

        _currentExceptionPropertyInfo ??= GetType().BaseType!.GetProperty(nameof(CurrentException), BindingFlags.NonPublic | BindingFlags.Instance)!;
        _currentExceptionPropertyInfo.SetValue(this, null);
    }

    private Exception? _exception = null;

    private RenderFragment<Exception> ExceptionContent => ex => builder =>
    {
        if (ErrorContent != null)
        {
            builder.AddContent(0, ErrorContent(ex));
        }
        else
        {
            var index = 0;
            builder.OpenElement(index++, "div");
            builder.AddAttribute(index++, "class", "error-stack");
            builder.AddContent(index++, GetErrorContentMarkupString(ex));
            builder.CloseElement();
        }
    };

    private bool? _errorDetails;

    private MarkupString GetErrorContentMarkupString(Exception ex)
    {
        _errorDetails ??= Configuration.GetValue("DetailedErrors", false);
        return _errorDetails is true
            ? ex.FormatMarkupString(Configuration.GetEnvironmentInformation())
            : new MarkupString(ex.Message);
    }

    /// <summary>
    /// BootstrapBlazor 组件导致异常渲染方法
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="handler"></param>
    public async Task RenderException(Exception exception, IHandlerException? handler)
    {
        // 外部调用
        if (OnErrorHandleAsync != null)
        {
            await OnErrorHandleAsync(Logger, exception);
            return;
        }

        // 记录日志
        await OnErrorAsync(exception);

        if (handler != null)
        {
            if (HostEnvironment.IsDevelopment())
            {
                // IHandlerException 处理异常逻辑
                await handler.HandlerException(exception, ExceptionContent);
            }
            else
            {
                // 非开发模式下弹窗提示错误信息
                await ToastService.Error(ToastTitle, exception.Message);
            }
            return;
        }

        // 显示异常信息
        await ShowErrorToast(exception);
        _exception = exception;
        StateHasChanged();
    }

    private async Task ShowErrorToast(Exception exception)
    {
        if (ShowToast)
        {
            await ToastService.Error(ToastTitle, exception.Message);
        }
    }
}
