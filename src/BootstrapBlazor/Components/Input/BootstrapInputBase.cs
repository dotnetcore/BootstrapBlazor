﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInputBase 组件基类
/// </summary>
[BootstrapModuleAutoLoader("Input/BootstrapInput.razor.js", JSObjectReference = true, AutoInvokeInit = false)]
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
    /// 元素实例引用
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
    /// 获得/设置 Enter 键自动选择输入框内所有字符串 默认 false 未启用
    /// </summary>
    [Parameter]
    public bool IsSelectAllTextOnEnter { get; set; }

    /// <summary>
    /// 获得/设置 是否自动修剪空白 默认 false 未启用
    /// </summary>
    [Parameter]
    public bool IsTrim { get; set; }

    /// <summary>
    /// 获得/设置 失去焦点回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnBlurAsync { get; set; }

    [CascadingParameter]
    private Modal? Modal { get; set; }

    /// <summary>
    /// 获得 input 组件类型 默认 text
    /// </summary>
    protected string Type { get; set; } = "text";

    /// <summary>
    /// 自动获得焦点方法
    /// </summary>
    /// <returns></returns>
    public async Task FocusAsync() => await FocusElement.FocusAsync();

    /// <summary>
    /// 全选文字
    /// </summary>
    /// <returns></returns>
    public async ValueTask SelectAllTextAsync() => await InvokeVoidAsync("select", Id);

    /// <summary>
    /// 获得/设置 是否不注册 js 脚本处理 Enter/ESC 键盘处理函数 默认 false
    /// </summary>
    protected bool SkipRegisterEnterEscJSInvoke { get; set; }

    /// <summary>
    /// <inheritdoc/>
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

        if (IsAutoFocus)
        {
            Modal?.RegisterShownCallback(this, FocusAsync);
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
            if (!SkipRegisterEnterEscJSInvoke && (OnEnterAsync != null || OnEscAsync != null))
            {
                await InvokeVoidAsync("handleKeyUp", GetInputId(), Interop, OnEnterAsync != null, nameof(EnterCallback), OnEscAsync != null, nameof(EscCallback));
            }
            if (IsSelectAllTextOnFocus)
            {
                await InvokeVoidAsync("selectAllByFocus", GetInputId());
            }
            if (IsSelectAllTextOnEnter)
            {
                await InvokeVoidAsync("selectAllByEnter", GetInputId());
            }
            if (IsAutoFocus && Modal == null)
            {
                await FocusAsync();
            }
        }
    }

    /// <summary>
    /// 获得输入框 Id
    /// </summary>
    protected virtual string? GetInputId() => Id;

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
    /// OnBlur 方法
    /// </summary>
    protected virtual async Task OnBlur()
    {
        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }

    /// <summary>
    /// 客户端 EnterCallback 回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task EnterCallback(string val)
    {
        if (OnEnterAsync != null)
        {
            CurrentValueAsString = val;
            await OnEnterAsync(Value);
        }
    }

    /// <summary>
    /// 客户端 EscCallback 回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task EscCallback()
    {
        if (OnEscAsync != null)
        {
            await OnEscAsync(Value);
        }
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        Modal?.UnRegisterShownCallback(this);
    }
}
