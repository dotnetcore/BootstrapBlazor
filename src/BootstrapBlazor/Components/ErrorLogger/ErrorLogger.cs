﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    private ToastService? ToastService { get; set; }

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

    private bool ShowErrorDetails { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ToastTitle ??= Localizer[nameof(ToastTitle)];

        ShowErrorDetails = Configuration.GetValue("DetailedErrors", false);

        if (ShowErrorDetails)
        {
            ErrorContent ??= RenderException();
        }
#if NET6_0_OR_GREATER
        MaximumErrorCount = 1;
#endif
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
    /// BuildRenderTree 方法
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<IErrorLogger>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<IErrorLogger>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<IErrorLogger>.IsFixed), true);

        var content = ChildContent;
#if NET6_0_OR_GREATER
        var ex = Exception ?? CurrentException;
#else
        var ex = Exception;
#endif
        if (ex != null && ErrorContent != null)
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
                content = ErrorContent.Invoke(ex);
            }
        }
        builder.AddAttribute(3, nameof(CascadingValue<IErrorLogger>.ChildContent), content);
        builder.CloseComponent();
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

        if (OnErrorHandleAsync is null && ShowErrorDetails)
        {
            Exception = exception;
            StateHasChanged();
        }
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

#if NET6_0_OR_GREATER
            // 此处注意 内部 logLevel=Warning
            await ErrorBoundaryLogger.LogErrorAsync(exception);
#else
            Logger.LogError(exception, "");
#endif
        }
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
