// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Configuration;

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
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    [Inject]
    [NotNull]
    private IConfiguration? Configuration { get; set; }

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

    private bool EnableErrorLogger => TabSet.EnableErrorLogger ?? Options.CurrentValue.EnableErrorLogger;
    private bool EnableErrorLoggerILogger => TabSet.EnableErrorLoggerILogger ?? Options.CurrentValue.EnableErrorLoggerILogger;
    private bool ShowErrorLoggerToast => TabSet.ShowErrorLoggerToast ?? Options.CurrentValue.ShowErrorLoggerToast;
    private string ToastTitle => TabSet.ErrorLoggerToastTitle ?? "Error";

    private void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<ErrorLogger>(0);
        builder.SetKey(_key);
        builder.AddAttribute(1, nameof(ErrorLogger.ChildContent), Item.ChildContent);
        builder.AddAttribute(2, nameof(ErrorLogger.EnableErrorLogger), EnableErrorLogger);
        builder.AddAttribute(3, nameof(ErrorLogger.EnableILogger), EnableErrorLoggerILogger);
        builder.AddAttribute(4, nameof(ErrorLogger.ShowToast), ShowErrorLoggerToast);
        builder.AddAttribute(5, nameof(ErrorLogger.ToastTitle), TabSet.ErrorLoggerToastTitle);
        builder.AddAttribute(6, nameof(ErrorLogger.OnInitializedCallback), new Func<IErrorLogger, Task>(logger =>
        {
            _logger = logger;
            _logger.Register(this);
            return Task.CompletedTask;
        }));
        builder.AddAttribute(7, nameof(ErrorLogger.OnErrorHandleAsync), TabSet.OnErrorHandleAsync);
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

    private bool? _errorDetails;
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorContent"></param>
    public async Task HandlerExceptionAsync(Exception ex, RenderFragment<Exception> errorContent)
    {
        _errorDetails ??= Configuration.GetValue("DetailedErrors", false);

        if (_errorDetails is true)
        {
            await DialogService.ShowErrorHandlerDialog(errorContent(ex));
        }
        else if (ShowErrorLoggerToast)
        {
            await ToastService.Error(ToastTitle, ex.Message);
        }
        else
        {
            await DialogService.ShowErrorHandlerDialog(errorContent(ex));
        }
    }

    /// <summary>
    /// IDispose 方法用于释放资源
    /// </summary>
    public void Dispose()
    {
        _logger?.UnRegister(this);
        GC.SuppressFinalize(this);
    }
}
