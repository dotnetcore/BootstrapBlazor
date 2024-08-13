// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

internal class ErrorLoggerExceptionHandler : BootstrapComponentBase
{
    [Inject]
    [NotNull]
    private ILogger<ErrorLogger>? Logger { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Parameter]
    [NotNull]
    public string? ToastTitle { get; set; }

    [Parameter]
    [NotNull]
    public Exception? Exception { get; set; }

    [Parameter]
    public bool ShowToast { get; set; } = true;

    [Parameter]
    public EventCallback ExceptionHandled { get; set; }

    [Parameter]
    public Func<ILogger, Exception, Task>? OnErrorHandleAsync { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (OnErrorHandleAsync != null)
        {
            await OnErrorHandleAsync(Logger, Exception);
        }
        else if (ShowToast)
        {
            await ToastService.Error(ToastTitle, Exception.Message);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (ExceptionHandled.HasDelegate)
        {
            await InvokeAsync(async () => await ExceptionHandled.InvokeAsync());
        }
    }
}
