﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// ErrorLogger 全局异常组件
/// </summary>
public class ErrorLogger : ComponentBase, IErrorLogger
{
    [Inject]
    [NotNull]
    private IStringLocalizer<ErrorLogger>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? EnableErrorLogger { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool? ShowToast { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ToastTitle { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 异常显示模板 默认 null
    /// </summary>
    /// <remarks>用于自定义异常显示 UI</remarks>
    [Parameter]
    public RenderFragment<Exception>? ErrorContent { get; set; }

    /// <summary>
    /// Gets or sets the callback function to be invoked during initialization.
    /// </summary>
    [Parameter]
    public Func<ErrorLogger, Task>? OnInitializedCallback { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    [NotNull]
    private BootstrapBlazorErrorBoundary? _errorBoundary = default;

    private bool _enableErrorLogger => EnableErrorLogger ?? Options.CurrentValue.EnableErrorLogger;

    private bool _showToast => ShowToast ?? Options.CurrentValue.ShowErrorLoggerToast;

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
    /// <returns></returns>
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
        builder.OpenComponent<CascadingValue<IErrorLogger>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<IErrorLogger>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<IErrorLogger>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<IErrorLogger>.ChildContent), RenderContent);
        builder.CloseComponent();
    }

    private RenderFragment? RenderContent => _enableErrorLogger ? RenderError : ChildContent;

    private RenderFragment RenderError => builder =>
    {
        builder.OpenComponent<BootstrapBlazorErrorBoundary>(0);
        builder.AddAttribute(1, nameof(BootstrapBlazorErrorBoundary.OnErrorHandleAsync), OnErrorHandleAsync);
        builder.AddAttribute(2, nameof(BootstrapBlazorErrorBoundary.ShowToast), _showToast);
        builder.AddAttribute(3, nameof(BootstrapBlazorErrorBoundary.ToastTitle), ToastTitle);
        builder.AddAttribute(4, nameof(BootstrapBlazorErrorBoundary.ErrorContent), ErrorContent);
        builder.AddAttribute(5, nameof(BootstrapBlazorErrorBoundary.ChildContent), ChildContent);
        builder.AddComponentReferenceCapture(5, obj => _errorBoundary = (BootstrapBlazorErrorBoundary)obj);
        builder.CloseComponent();
    };

    /// <summary>
    /// 由实现 <see cref="BootstrapComponentBase"/> 组件实现类调用
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public Task HandlerExceptionAsync(Exception exception) => _errorBoundary.RenderException(exception, _cache.LastOrDefault());

    private readonly List<IHandlerException> _cache = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="component"></param>
    public void Register(IHandlerException component)
    {
        _cache.Add(component);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="component"></param>
    public void UnRegister(IHandlerException component)
    {
        _cache.Remove(component);
    }
}
