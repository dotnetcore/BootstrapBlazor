// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

class TabItemContent : IComponent, IHandlerException, IDisposable
{
    /// <summary>
    /// Gets or sets the component content. Default is null
    /// </summary>
    [Parameter, NotNull]
    public TabItem? Item { get; set; }

    [CascadingParameter]
    private Layout? Layout { get; set; }

    [CascadingParameter, NotNull]
    private Tab? TabSet { get; set; }

    [Inject, NotNull]
    private DialogService? DialogService { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    private IErrorLogger? _logger;

    private RenderHandle _renderHandle;

    void IComponent.Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    Task IComponent.SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        RenderContent();
        return Task.CompletedTask;
    }

    private void RenderContent()
    {
        _renderHandle.Render(BuildRenderTree);
    }

    private Guid _key = Guid.NewGuid();

    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<ErrorLogger>(0);
        builder.SetKey(_key);
        builder.AddAttribute(1, nameof(ErrorLogger.ChildContent), Item.ChildContent);

        var enableErrorLogger = TabSet.EnableErrorLogger ?? Options.CurrentValue.EnableErrorLogger;
        builder.AddAttribute(2, nameof(ErrorLogger.EnableErrorLogger), enableErrorLogger);

        // TabItem 不需要 Toast 提示错误信息
        builder.AddAttribute(3, nameof(ErrorLogger.ShowToast), false);
        builder.AddAttribute(4, nameof(ErrorLogger.ToastTitle), TabSet.ErrorLoggerToastTitle);
        builder.AddAttribute(5, nameof(ErrorLogger.OnInitializedCallback), new Func<IErrorLogger, Task>(logger =>
        {
            _logger = logger;
            _logger.Register(this);
            return Task.CompletedTask;
        }));
        builder.AddAttribute(6, nameof(ErrorLogger.OnErrorHandleAsync), Layout?.OnErrorHandleAsync);
        builder.CloseComponent();
    }

    /// <summary>
    /// Render method
    /// </summary>
    public void Render()
    {
        _key = Guid.NewGuid();
        RenderContent();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorContent"></param>
    public Task HandlerExceptionAsync(Exception ex, RenderFragment<Exception> errorContent) => DialogService.ShowErrorHandlerDialog(errorContent(ex));

    /// <summary>
    /// IDispose 方法用于释放资源
    /// </summary>
    public void Dispose()
    {
        _logger?.UnRegister(this);
        GC.SuppressFinalize(this);
    }
}
