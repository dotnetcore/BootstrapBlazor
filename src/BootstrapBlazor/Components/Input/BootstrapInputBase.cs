// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class BootstrapInputBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    protected virtual string? ClassName => CssBuilder.Default("form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    protected ElementReference FocusElement { get; set; }

    /// <summary>
    /// 获得/设置 input 类型 placeholder 属性
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 文本框 Enter 键回调委托方法 默认为 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnEnterAsync { get; set; }

    /// <summary>
    /// 获得/设置 文本框 Esc 键回调委托方法 默认为 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnEscAsync { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.None;

    /// <summary>
    /// 获得/设置 格式化字符串
    /// </summary>
    [Parameter]
    public Func<TValue, string>? Formatter { get; set; }

    /// <summary>
    /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// 获得/设置 是否自动获取焦点 默认 false 不自动获取焦点
    /// </summary>
    [Parameter]
    public bool IsAutoFocus { get; set; }

    /// <summary>
    /// 获得/设置 获得焦点后自动选择输入框内所有字符串 默认 false 未启用
    /// </summary>
    [Parameter]
    public bool IsSelectAllTextOnFocus { get; set; }

    /// <summary>
    /// 获得/设置 是否自动修剪空白 默认 false 未启用
    /// </summary>
    [Parameter]
    public bool IsTrim { get; set; }

    /// <summary>
    /// 获得 input 组件类型 默认 text
    /// </summary>
    protected string Type { get; set; } = "text";

    /// <summary>
    /// 自动获得焦点方法
    /// </summary>
    /// <returns></returns>
    public ValueTask FocusAsync() => FocusElement.FocusAsync();

    private JSInterop<BootstrapInputBase<TValue>>? Interop { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (string.IsNullOrEmpty(PlaceHolder) && FieldIdentifier.HasValue)
        {
            PlaceHolder = FieldIdentifier.Value.GetPlaceHolder();
        }

        if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("type", out var t))
        {
            var type = t.ToString();
            if (!string.IsNullOrEmpty(type))
            {
                Type = type;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (IsAutoFocus)
            {
                await FocusAsync();
            }
            if (OnEnterAsync != null || OnEscAsync != null)
            {
                Interop ??= new JSInterop<BootstrapInputBase<TValue>>(JSRuntime);
                await Interop.InvokeVoidAsync(this, FocusElement, "bb_input", OnEnterAsync != null, nameof(EnterCallback), OnEscAsync != null, nameof(EscCallback));
            }
            if (IsSelectAllTextOnFocus)
            {
                await JSRuntime.InvokeVoidAsync(FocusElement, "bb_input_selectAll");
            }
        }
    }

    /// <summary>
    /// 数值格式化委托方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(TValue value) => Formatter != null
        ? Formatter.Invoke(value)
        : (!string.IsNullOrEmpty(FormatString) && value != null
            ? Utility.Format(value, FormatString)
            : base.FormatValueAsString(value));

    /// <summary>
    /// TryParseValueFromString
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage) => base.TryParseValueFromString(IsTrim ? value.Trim() : value, out result, out validationErrorMessage);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task EnterCallback()
    {
        if (OnEnterAsync != null)
        {
            await OnEnterAsync(Value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task EscCallback(string val)
    {
        if (OnEscAsync != null)
        {
            CurrentValueAsString = val;
            await OnEscAsync(Value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            Interop?.Dispose();
            Interop = null;
        }
        await base.DisposeAsyncCore(disposing);
    }
}
