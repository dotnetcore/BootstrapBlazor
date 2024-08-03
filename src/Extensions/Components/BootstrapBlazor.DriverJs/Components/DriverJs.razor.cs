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
    protected override Task InvokeInitAsync()
    {
        Config ??= new();
        Config.Steps = _steps;
        Config.ProgressText ??= Localizer[nameof(Config.ProgressText)];
        return InvokeVoidAsync("init", Id, Interop);
    }

    /// <summary>
    /// 开始方法
    /// </summary>
    /// <returns></returns>
    public async Task Start()
    {
        await InvokeVoidAsync("start", Id, Config);
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
