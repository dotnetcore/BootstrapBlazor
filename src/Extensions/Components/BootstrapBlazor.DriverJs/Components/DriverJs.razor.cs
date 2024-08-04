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
    /// 获得/设置 组件配置 <see cref="DriverJsConfig"/> 实例 默认 null
    /// </summary>
    [Parameter]
    public DriverJsConfig? Config { get; set; }

    /// <summary>
    /// 获得/设置 组件销毁前回调方法
    /// </summary>
    [Parameter]
    public Func<Task<bool>>? OnBeforeDestroyAsync { get; set; }

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
    public async Task Start()
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
        await InvokeVoidAsync("start", Id, Config);
    }

    /// <summary>
    /// 组件销毁前回调方法由 JavaScript 调用 返回 false 阻止销毁
    /// </summary>
    [JSInvokable]
    public async Task<bool> OnBeforeDestroy(DriverJsStep step, DriverJsState state)
    {
        var ret = true;
        if (OnBeforeDestroyAsync != null)
        {
            ret = await OnBeforeDestroyAsync();
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
