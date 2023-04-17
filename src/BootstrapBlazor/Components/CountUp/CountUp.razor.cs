﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// CountUp 组件
/// </summary>
[JSModuleAutoLoader()]
public partial class CountUp<TValue>
{
    /// <summary>
    /// 获得/设置 Value 值
    /// </summary>
    [Parameter]
    [NotNull]
    public TValue? Value { get; set; }

    [NotNull]
    private TValue? PreviousValue { get; set; }

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

        if (firstRender)
        {
            PreviousValue = Value;
        }
        else if (!PreviousValue.Equals(Value))
        {
            await Update(Value);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Value);

    /// <summary>
    /// 更新数据方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private async ValueTask Update(TValue? value)
    {
        PreviousValue = value;

        if (Module != null)
        {
            await Module.InvokeVoidAsync("update", Id, Value);
        }
    }
}
