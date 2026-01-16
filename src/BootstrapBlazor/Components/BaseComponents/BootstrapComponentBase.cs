// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Bootstrap Blazor 组件基类</para>
///  <para lang="en">Bootstrap Blazor component base class</para>
/// </summary>
public abstract class BootstrapComponentBase : ComponentBase, IHandleEvent
{
    /// <summary>
    ///  <para lang="zh">获得/设置 用户自定义属性</para>
    ///  <para lang="en">Gets or sets user defined attributes</para>
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    [JsonIgnore]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    ///  <para lang="zh">异常捕获组件</para>
    ///  <para lang="en">Exception handling component</para>
    /// </summary>
    [CascadingParameter]
    protected IErrorLogger? ErrorLogger { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 IJSRuntime 实例</para>
    ///  <para lang="en">Gets or sets IJSRuntime instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IJSRuntime? JSRuntime { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否需要 Render 组件 默认 false 需要重新渲染组件</para>
    ///  <para lang="en">Gets or sets whether to render the component. Default is false (re-render required)</para>
    /// </summary>
    protected bool IsNotRender { get; set; }

    [ExcludeFromCodeCoverage]
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

            if (ErrorLogger is { EnableErrorLogger: true })
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
