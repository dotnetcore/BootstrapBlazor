﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Checkbox 组件
/// </summary>
[BootstrapModuleAutoLoader(JSObjectReference = true)]
public partial class Checkbox<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    private string? ClassString => CssBuilder.Default("form-check")
        .AddClass("is-label", IsShowAfterLabel)
        .AddClass("is-checked", State == CheckboxState.Checked && !IsBoolean)
        .AddClass("is-indeterminate", State == CheckboxState.Indeterminate)
        .AddClass($"form-check-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"form-check-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass("disabled", IsDisabled)
        .AddClass(ValidCss)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private bool IsShowAfterLabel => ShowAfterLabel && !string.IsNullOrEmpty(DisplayText);

    /// <summary>
    /// Input 元素样式
    /// </summary>
    protected string? InputClassString => CssBuilder.Default("form-check-input")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// Check 状态字符串
    /// </summary>
    protected string? CheckedString => State switch
    {
        CheckboxState.Checked => "checked",
        _ => null
    };

    /// <summary>
    /// 判断双向绑定类型是否为 boolean 类型
    /// </summary>
    private bool IsBoolean { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色 默认为 None 未设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得/设置 Size 大小 默认为 None
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Checkbox 后置 label 文字 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowAfterLabel { get; set; }

    /// <summary>
    /// 获得/设置 选择框状态
    /// </summary>
    [Parameter]
    public CheckboxState State { get; set; }

    /// <summary>
    /// 获得/设置 State 状态改变回调方法
    /// </summary>
    /// <value></value>
    [Parameter]
    public EventCallback<CheckboxState> StateChanged { get; set; }

    /// <summary>
    /// 获得/设置 选中状态改变前回调此方法 返回 false 可以阻止状态改变
    /// </summary>
    [Parameter]
    public Func<CheckboxState, Task<bool>>? OnBeforeStateChanged { get; set; }

    /// <summary>
    /// 获得/设置 选择框状态改变时回调此方法
    /// </summary>
    [Parameter]
    public Func<CheckboxState, TValue, Task>? OnStateChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否事件冒泡 默认为 false
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; }

    private string? StopPropagationString => StopPropagation ? "true" : null;

    private string? TriggerBeforeValueString => OnBeforeStateChanged == null ? null : "true";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        IsBoolean = (Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue)) == typeof(bool);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (ShowAfterLabel)
        {
            DisplayText ??= FieldIdentifier?.GetDisplayName();
            ShowLabel = false;
        }

        if (IsBoolean && Value != null && State != CheckboxState.Indeterminate && BindConverter.TryConvertToBool(Value, CultureInfo.InvariantCulture, out var v))
        {
            State = v ? CheckboxState.Checked : CheckboxState.UnChecked;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        _paddingStateChanged = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        await InvokeVoidAsync("setIndeterminate", Id, State == CheckboxState.Indeterminate);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new
    {
        TriggerOnBeforeStateChanged = nameof(TriggerOnBeforeStateChanged),
        TriggerClick = nameof(TriggerClick),
        SyncStateCallback = nameof(SyncStateCallback)
    });

    private CheckboxState NextState => State == CheckboxState.Checked ? CheckboxState.UnChecked : CheckboxState.Checked;

    /// <summary>
    /// 触发 OnBeforeStateChanged 回调方法 由 JavaScript 调用
    /// </summary>
    [JSInvokable]
    public async ValueTask TriggerOnBeforeStateChanged()
    {
        if (OnBeforeStateChanged != null)
        {
            var ret = await OnBeforeStateChanged(NextState);
            if (ret)
            {
                await TriggerClick();
            }
        }
    }

    /// <summary>
    /// 同步 <see cref="State"/> 值方法 由 JavaScript 调用
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    [JSInvokable]
    public ValueTask SyncStateCallback(CheckboxState state)
    {
        State = state;
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 触发 Click 方法 由 JavaScript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async ValueTask TriggerClick()
    {
        var render = await InternalStateChanged(NextState);
        if (render)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 此变量为了提高性能，避免循环更新
    /// </summary>
    private bool _paddingStateChanged;

    private async Task<bool> InternalStateChanged(CheckboxState state)
    {
        var ret = true;
        _paddingStateChanged = true;

        if (IsBoolean)
        {
            CurrentValue = (TValue)(object)(state == CheckboxState.Checked);
        }

        if (State != state)
        {
            State = state;
            if (StateChanged.HasDelegate)
            {
                await StateChanged.InvokeAsync(State);
                ret = false;
            }

            if (OnStateChanged != null)
            {
                await OnStateChanged(State, Value);
            }
        }
        return ret;
    }

    /// <summary>
    /// 设置 复选框状态方法
    /// </summary>
    /// <param name="state"></param>
    public virtual async Task SetState(CheckboxState state)
    {
        if (!_paddingStateChanged)
        {
            var render = await InternalStateChanged(state);
            if (render)
            {
                StateHasChanged();
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing && Module != null)
        {
            await Module.DisposeAsync();
            Module = null;
        }
        await base.DisposeAsync(disposing);
    }
}
