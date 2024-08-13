// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// ErrorLogger 全局异常组件
/// </summary>
public class ErrorLogger
#if NET6_0_OR_GREATER
    : ErrorBoundaryBase, IErrorLogger
#else
    : ComponentBase, IErrorLogger
#endif
{
    [Inject]
    [NotNull]
    private ILogger<ErrorLogger>? Logger { get; set; }

    [Inject]
    [NotNull]
    private IConfiguration? Configuration { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ErrorLogger>? Localizer { get; set; }

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
    /// 获得/设置 自定义错误处理回调方法
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

#if NET6_0_OR_GREATER
    [Inject]
    [NotNull]
    private IErrorBoundaryLogger? ErrorBoundaryLogger { get; set; }
#else
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public RenderFragment<Exception>? ErrorContent { get; set; }
#endif
    private Exception? Exception { get; set; }

    private int errorRenderCount = 0;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ToastTitle ??= Localizer[nameof(ToastTitle)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Exception = null;

#if NET6_0_OR_GREATER
        Recover();
#endif
    }

    /// <summary>
    /// OnErrorAsync 方法
    /// </summary>
    /// <param name="exception"></param>
#if NET6_0_OR_GREATER
    protected override async Task OnErrorAsync(Exception exception)
#else
    protected async Task OnErrorAsync(Exception exception)
#endif
    {
        errorRenderCount++;

#if !NET6_0_OR_GREATER
        if (OnErrorHandleAsync == null)
        {
            // 此处注意 内部 logLevel=Warning
            await ErrorBoundaryLogger.LogErrorAsync(exception);
        }
#else
        Exception = exception;
        if (OnErrorHandleAsync == null)
        {
            Logger.LogError(exception, "");
        }
#endif
    }

    /// <summary>
    /// BuildRenderTree 方法
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
#if NET6_0_OR_GREATER
        var ex = Exception ?? CurrentException;
#else
        var ex = Exception;
#endif

        int sequence = 0;
        if (ex is null)
        {
            builder.AddContent(sequence++, ChildContent);
        }
        else if (ErrorContent is not null)
        {
            if (Cache.Count > 0)
            {
                var component = Cache.Last();
                if (component is IHandlerException handler)
                {
                    handler.HandlerException(ex, ErrorContent);
                }
            }
            else
            {
                builder.AddContent(sequence++, ErrorContent(ex));
            }
        }
        else
        {
            if (errorRenderCount > 1)
            {
                builder.OpenElement(sequence++, "div");
                builder.AddAttribute(sequence++, "class", "blazor-error-boundary");
                builder.CloseElement();
                errorRenderCount = 0;
            }
            else if (Cache.Count > 0)
            {
                var component = Cache.Last();
                if (component is IHandlerException handler)
                {
                    handler.HandlerException(ex, RenderException());
                }
            }
            else
            {
                builder.AddContent(sequence++, ChildContent);
                builder.OpenComponent<ErrorLoggerExceptionHandler>(sequence++);
                builder.AddAttribute(sequence++, "ToastTitle", ToastTitle);
                builder.AddAttribute(sequence++, "ShowToast", ShowToast);
                builder.AddAttribute(sequence++, "Exception", ex);
                builder.AddAttribute(sequence++, "OnErrorHandleAsync", OnErrorHandleAsync);
                builder.AddAttribute(sequence++, "ExceptionHandled", EventCallback.Factory.Create(this, ExceptionHandled));
                builder.CloseComponent();
            }
        }
    }

    private void ExceptionHandled()
    {
        Exception = null;
        errorRenderCount = 0;
        Recover();
    }

    private RenderFragment<Exception> RenderException() => ex => builder =>
    {
        var index = 0;
        builder.OpenElement(index++, "div");
        builder.AddAttribute(index++, "class", "error-stack");
        builder.AddContent(index++, ex.FormatMarkupString(Configuration.GetEnvironmentInformation()));
        builder.CloseElement();
    };

    /// <summary>
    /// 由接口调用
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public async Task HandlerExceptionAsync(Exception exception)
    {
        await OnErrorAsync(exception);
    }

    private List<ComponentBase> Cache { get; } = [];

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    public void Register(ComponentBase component)
    {
        Cache.Add(component);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    public void UnRegister(ComponentBase component)
    {
        Cache.Remove(component);
    }
}
