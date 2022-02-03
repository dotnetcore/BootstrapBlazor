// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Bootstrap Blazor 组件基类
/// </summary>
public abstract class BootstrapComponentBase : ComponentBase, IHandleEvent
{
    /// <summary>
    /// 获得/设置 用户自定义属性
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// 异常捕获组件
    /// </summary>
    [CascadingParameter]
    protected IErrorLogger? ErrorLogger { get; set; }

    /// <summary>
    /// 获得/设置 IJSRuntime 实例
    /// </summary>
    [Inject]
    [NotNull]
    protected IJSRuntime? JSRuntime { get; set; }

    /// <summary>
    /// 获得/设置 是否需要 Render 组件 默认 false 需要重新渲染组件
    /// </summary>
    protected bool IsNotRender { get; set; }

    private async Task CallStateHasChangedOnAsyncCompletion(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception ex) // avoiding exception filters for AOT runtime support
        {
            // Ignore exceptions from task cancellations, but don't bother issuing a state change.
            if (task.IsCanceled)
            {
                return;
            }

            if (ErrorLogger != null)
            {
                IsNotRender = true;
                await ErrorLogger.HandlerExceptionAsync(ex);
            }
            else
            {
                // 未开启全局捕获
                throw;
            }
        }

        if (!IsNotRender)
        {
            StateHasChanged();
        }
        else
        {
            IsNotRender = false;
        }
    }

    Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
    {
        var task = callback.InvokeAsync(arg);
        var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
            task.Status != TaskStatus.Canceled;

        if (!IsNotRender)
        {
            // After each event, we synchronously re-render (unless !ShouldRender())
            // This just saves the developer the trouble of putting "StateHasChanged();"
            // at the end of every event callback.
            StateHasChanged();
        }
        else
        {
            IsNotRender = false;
        }

        return shouldAwaitTask ?
            CallStateHasChangedOnAsyncCompletion(task) :
            Task.CompletedTask;
    }
}
