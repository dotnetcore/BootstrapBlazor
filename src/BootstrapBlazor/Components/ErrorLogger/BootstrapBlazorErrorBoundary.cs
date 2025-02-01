// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// 内部使用 自定义异常组件
/// </summary>
class BootstrapBlazorErrorBoundary : ErrorBoundaryBase
{
    [Inject]
    [NotNull]
    private ILogger<ErrorLogger>? Logger { get; set; }

    [Inject]
    [NotNull]
    private IConfiguration? Configuration { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

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
    /// 获得/设置 Toast 弹窗标题
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ToastTitle { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override async Task OnErrorAsync(Exception exception)
    {
        // 由框架调用
        if (OnErrorHandleAsync != null)
        {
            await OnErrorHandleAsync(Logger, exception);
        }
        else
        {
            if (ShowToast)
            {
                await ToastService.Error(ToastTitle, exception.Message);
            }

            Logger.LogError(exception, "{BootstrapBlazorErrorBoundary} {OnErrorAsync} log this error occurred at {Page}", nameof(BootstrapBlazorErrorBoundary), nameof(OnErrorAsync), NavigationManager.Uri);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (OnErrorHandleAsync == null)
        {
            var ex = CurrentException ?? _exception;
            if (ex != null)
            {
                _exception = null;
                builder.AddContent(0, ExceptionContent(ex));
            }
        }
        builder.AddContent(1, ChildContent);
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
    /// 渲染异常信息方法
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="handler"></param>
    public async Task RenderException(Exception exception, IHandlerException? handler)
    {
        await OnErrorAsync(exception);

        if (handler != null)
        {
            await handler.HandlerException(exception, ExceptionContent);
        }
        else
        {
            _exception = exception;
            StateHasChanged();
        }
    }
}
