// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class ErrorLogger
#if NET5_0
    : ComponentBase, IErrorLogger
#else
    : ErrorBoundaryBase, IErrorLogger
#endif
{
    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private ILogger<ErrorLogger>? Logger { get; set; }

    /// <summary>
    /// 
    /// </summary>
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
    public string? ToastTitle { get; set; }

    /// <summary>
    /// 获得/设置 自定义错误处理回调方法
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

#if NET5_0
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
#else
    [Inject]
    [NotNull]
    private IErrorBoundaryLogger? ErrorBoundaryLogger { get; set; }
#endif

    /// <summary>
    /// 
    /// </summary>
    protected Exception? Exception { get; set; }

    private bool ShowErrorDetails { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ToastTitle ??= Localizer[nameof(ToastTitle)];

        ShowErrorDetails = Configuration.GetValue<bool>("DetailedErrors", false);

        if (ShowErrorDetails)
        {
            ErrorContent ??= ex => builder =>
            {
                var index = 0;
                builder.OpenElement(index++, "div");
                builder.AddAttribute(index++, "class", "error-stack");
                builder.AddContent(index++, ex.FormatMarkupString(Configuration.GetEnvironmentInformation()));
                builder.CloseElement();
            };
        }
    }

    /// <summary>
    /// OnParametersSet 方法
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

#if NET5_0
        var ex = Exception;
#else
        var ex = Exception ?? CurrentException;
#endif
        if (ex != null && ErrorContent != null)
        {
            content = ErrorContent.Invoke(ex);
        }
        builder.AddAttribute(3, nameof(CascadingValue<IErrorLogger>.ChildContent), content);
        builder.CloseComponent();
    }

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
    /// <returns></returns>
#if NET5_0
    protected async Task OnErrorAsync(Exception exception)
#else
    protected override async Task OnErrorAsync(Exception exception)
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
}
