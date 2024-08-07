// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FocusGuide 组件
/// </summary>
public partial class DriverJs
{
    /// <summary>
    /// 获得/设置 是否自动开始向导 默认 true
    /// </summary>
    [Parameter]
    public bool AutoDrive { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件配置 <see cref="DriverJsConfig"/> 实例 默认 null
    /// </summary>
    [Parameter]
    public DriverJsConfig? Config { get; set; }

    /// <summary>
    /// 获得/设置 组件销毁前回调方法 返回 false 时阻止销毁
    /// </summary>
    [Parameter]
    public Func<DriverJsConfig, int, Task<string?>>? OnBeforeDestroyAsync { get; set; }

    /// <summary>
    /// 获得/设置 组件销毁回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnDestroyedAsync { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<DriverJs>? Localizer { get; set; }

    private readonly List<DriverJsStep> _steps = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop);

    /// <summary>
    /// 开始方法
    /// </summary>
    /// <returns></returns>
    public async Task Start(int? index = 0)
    {
        Config ??= new();
        Config.Steps = _steps;
        Config.ProgressText ??= Localizer[nameof(Config.ProgressText)];

        if (OnBeforeDestroyAsync != null)
        {
            Config.OnDestroyStartedAsync = nameof(OnBeforeDestroy);
        }
        if (OnBeforeDestroyAsync != null)
        {
            Config.OnDestroyedAsync = nameof(OnDestroyed);
        }

        await InvokeVoidAsync("start", Id, Config, new
        {
            AutoDrive,
            Index = index
        });
    }

    /// <summary>
    /// 组件销毁前回调方法由 JavaScript 调用 返回 false 阻止销毁
    /// </summary>
    [JSInvokable]
    public async Task<string?> OnBeforeDestroy(int index)
    {
        string? ret = null;
        if (OnBeforeDestroyAsync != null)
        {
            // Config 不为空
            ret = await OnBeforeDestroyAsync(Config!, index);
        }

        return ret;
    }

    /// <summary>
    /// 组件销毁后回调方法由 JavaScript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnDestroyed()
    {
        if (OnDestroyedAsync != null)
        {
            await OnDestroyedAsync();
        }
    }

    /// <summary>
    /// Starts at step 0
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Task Drive(int? index) => InvokeVoidAsync("drive", Id, index);

    /// <summary>
    /// Move to the next step
    /// </summary>
    /// <returns></returns>
    public Task MoveNext() => InvokeVoidAsync("moveNext", Id);

    /// <summary>
    /// Move to the previous step
    /// </summary>
    /// <returns></returns>
    public Task MovePrevious() => InvokeVoidAsync("movePrevious", Id);

    /// <summary>
    /// Move to the step
    /// </summary>
    /// <returns></returns>
    public Task MoveTo(int index) => InvokeVoidAsync("moveTo", Id, index);

    /// <summary>
    /// Is there a next step
    /// </summary>
    /// <returns></returns>
    public Task<bool> HasNextStep() => InvokeAsync<bool>("hasNextStep", Id);

    /// <summary>
    /// Is there a previous step
    /// </summary>
    /// <returns></returns>
    public Task<bool> HasPreviousStep() => InvokeAsync<bool>("hasPreviousStep", Id);

    /// <summary>
    /// Is the current step the first step
    /// </summary>
    /// <returns></returns>
    public Task<bool> IsFirstStep() => InvokeAsync<bool>("isFirstStep", Id);

    /// <summary>
    /// Is the current step the last step
    /// </summary>
    /// <returns></returns>
    public Task<bool> IsLastStep() => InvokeAsync<bool>("isLastStep", Id);

    /// <summary>
    /// Gets the active step index
    /// </summary>
    /// <returns></returns>
    public Task<int> GetActiveIndex() => InvokeAsync<int>("getActiveIndex", Id);

    /// <summary>
    /// Gets the active step configuration
    /// </summary>
    /// <returns></returns>
    public async Task<DriverJsStep?> GetActiveStep()
    {
        DriverJsStep? step = null;
        if (Config != null)
        {
            var index = await GetActiveIndex();
            step = Config.Steps.ElementAtOrDefault(index + 1);
        }

        return step;
    }

    /// <summary>
    /// Gets the previous step configuration
    /// </summary>
    /// <returns></returns>
    public async Task<DriverJsStep?> GetPreviousStep()
    {
        DriverJsStep? step = null;
        if (Config != null)
        {
            var index = await GetActiveIndex();
            step = Config.Steps.ElementAtOrDefault(index - 1);
        }

        return step;
    }

    /// <summary>
    /// Is the tour or highlight currently active
    /// </summary>
    /// <returns></returns>
    public Task<bool> IsActive() => InvokeAsync<bool>("isActive", Id);

    /// <summary>
    /// Recalculate and redraw the highlight
    /// </summary>
    /// <returns></returns>
    public Task Refresh() => InvokeVoidAsync("refresh", Id);

    /// <summary>
    /// Look at the DriveStep section of configuration for format of the step
    /// </summary>
    /// <returns></returns>
    public Task Highlight() => InvokeVoidAsync("highlight", Id);

    /// <summary>
    /// 添加步骤方法
    /// </summary>
    /// <param name="step"></param>
    public void AddStep(DriverJsStep step)
    {
        _steps.Add(step);
    }

    /// <summary>
    /// 移除步骤方法
    /// </summary>
    /// <param name="step"></param>
    public void RemoveStep(DriverJsStep step)
    {
        _steps.Remove(step);
    }
}
