// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// CountUp 组件
/// </summary>
public partial class CountUp<TValue>
{
    /// <summary>
    /// 获得/设置 Value 值
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Value { get; set; }

    /// <summary>
    /// 获得/设置 计数配置项 默认 null
    /// </summary>
    [Parameter]
    public CountUpOption? Option { get; set; }

    /// <summary>
    /// 获得/设置 计数结束回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnCompleted { get; set; }

    [NotNull]
    private TValue? PreviousValue { get; set; }

    private string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!typeof(TValue).IsNumber())
        {
            throw new InvalidOperationException();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Module != null)
        {
            if (!PreviousValue.Equals(Value))
            {
                PreviousValue = Value;

                await Module.InvokeVoidAsync("init", Id, Interop, Value, OnCompleted != null ? nameof(OnCompleteCallback) : null, Option);
            }
        }
    }

    /// <summary>
    /// OnCompleted 回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnCompleteCallback()
    {
        if (OnCompleted != null)
        {
            await OnCompleted();
        }
    }
}
