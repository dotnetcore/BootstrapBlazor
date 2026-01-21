// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ErrorLogger 全局异常组件</para>
/// <para lang="en">ErrorLogger Global Exception Component</para>
/// </summary>
public class ErrorLogger : ComponentBase, IErrorLogger
{
    [Inject, NotNull]
    private ILogger<ErrorLogger>? Logger { get; set; }

    [Inject, NotNull]
    private IErrorBoundaryLogger? ErrorBoundaryLogger { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ErrorLogger>? Localizer { get; set; }

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

    /// <summary>
    /// <para lang="zh">获得/设置 the 回调 function to be invoked during initialization.</para>
    /// <para lang="en">Gets or sets the callback function to be invoked during initialization.</para>
    /// </summary>
    [Parameter]
    public Func<IErrorLogger, Task>? OnInitializedCallback { get; set; }

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
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (OnInitializedCallback is not null)
        {
            await OnInitializedCallback(this);
        }
    }

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
        builder.CloseComponent();
    };

    /// <summary>
    /// <inheritdoc cref="IErrorLogger.HandlerExceptionAsync(Exception)"/>
    /// </summary>
    public async Task HandlerExceptionAsync(Exception exception)
    {
        if (EnableILogger)
        {
            await ErrorBoundaryLogger.LogErrorAsync(exception);
        }

        var handler = _cache.LastOrDefault();
        if (handler is not null)
        {
            await handler.HandlerExceptionAsync(exception, ex => builder =>
            {
                if (ErrorContent is null)
                {
                    builder.AddContent(0, RenderErrorContent(exception));
                }
                else
                {
                    builder.AddContent(10, ErrorContent(exception));
                }
            });
        }
        if (OnErrorHandleAsync is not null)
        {
            await OnErrorHandleAsync(Logger, exception);
        }
        if (ShowToast)
        {
            var option = new ToastOption()
            {
                Category = ToastCategory.Error,
                Title = ToastTitle,
                ChildContent = ErrorContent == null
                    ? RenderErrorContent(exception)
                    : ErrorContent(exception)
            };
            await ToastService.Show(option);
        }
    }

    private static RenderFragment RenderErrorContent(Exception ex) => builder =>
    {
        builder.OpenComponent<ErrorRender>(0);
        builder.AddAttribute(1, "Exception", ex);
        builder.CloseElement();
    };

    private readonly List<IHandlerException> _cache = [];

    /// <summary>
    /// <inheritdoc cref="IErrorLogger.Register(IHandlerException)"/>
    /// </summary>
    public void Register(IHandlerException component)
    {
        _cache.Add(component);
    }

    /// <summary>
    /// <inheritdoc cref="IErrorLogger.UnRegister(IHandlerException)"/>
    /// </summary>
    public void UnRegister(IHandlerException component)
    {
        _cache.Remove(component);
    }
}
