// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ErrorLogger 全局异常组件</para>
///  <para lang="en">ErrorLogger Global Exception Component</para>
/// </summary>
public class ErrorLogger : ComponentBase, IErrorLogger
{
    [Inject]
    [NotNull]
    private IStringLocalizer<ErrorLogger>? Localizer { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool EnableErrorLogger { get; set; } = true;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool EnableILogger { get; set; } = true;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowToast { get; set; } = true;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ToastTitle { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 自定义错误处理回调方法</para>
    ///  <para lang="en">Get/Set Custom Error Handler</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 子组件</para>
    ///  <para lang="en">Get/Set Child Content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 异常显示模板 默认 null</para>
    ///  <para lang="en">Get/Set Exception Display Template Default null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>
    ///  <para lang="zh">用于自定义异常显示 UI</para>
    ///  <para lang="en">Used to customize exception display UI</para>
    /// </remarks>
    [Parameter]
    public RenderFragment<Exception>? ErrorContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the 回调 function to be invoked during initialization.</para>
    ///  <para lang="en">Gets or sets the callback function to be invoked during initialization.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IErrorLogger, Task>? OnInitializedCallback { get; set; }

    [NotNull]
    private BootstrapBlazorErrorBoundary? _errorBoundary = default;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ToastTitle ??= Localizer[nameof(ToastTitle)];
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
        builder.AddComponentReferenceCapture(7, obj => _errorBoundary = (BootstrapBlazorErrorBoundary)obj);
        builder.CloseComponent();
    };

    /// <summary>
    ///  <para lang="zh">由实现 <see cref="BootstrapComponentBase"/> 组件实现类调用</para>
    ///  <para lang="en">Called by implementing <see cref="BootstrapComponentBase"/> component implementation class</para>
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public Task HandlerExceptionAsync(Exception exception) => _errorBoundary.RenderException(exception, GetLastOrDefaultHandler());

    private readonly List<IHandlerException> _cache = [];

    internal IHandlerException? GetLastOrDefaultHandler() => _cache.LastOrDefault();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="component"></param>
    public void Register(IHandlerException component)
    {
        _cache.Add(component);
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="component"></param>
    public void UnRegister(IHandlerException component)
    {
        _cache.Remove(component);
    }
}
