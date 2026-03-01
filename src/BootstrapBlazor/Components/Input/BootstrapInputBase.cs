// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Input 组件基类</para>
/// <para lang="en">Base class for BootstrapInput components</para>
/// </summary>
[BootstrapModuleAutoLoader("Input/BootstrapInput.razor.js", JSObjectReference = true, AutoInvokeInit = false)]
public abstract class BootstrapInputBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 class 属性值</para>
    /// <para lang="en">Gets the class attribute value</para>
    /// </summary>
    protected virtual string? ClassName => CssBuilder.Default("form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 占位符属性值</para>
    /// <para lang="en">Gets or sets the placeholder attribute value</para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 回车键按下时的回调方法，默认值为 null</para>
    /// <para lang="en">Gets or sets the callback method for Enter key press, default is null</para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnEnterAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Esc 键按下时的回调方法，默认值为 null</para>
    /// <para lang="en">Gets or sets the callback method for Esc key press, default is null</para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnEscAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮颜色</para>
    /// <para lang="en">Gets or sets the button color</para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.None;

    /// <summary>
    /// <para lang="zh">获得/设置 格式化函数</para>
    /// <para lang="en">Gets or sets the formatter function</para>
    /// </summary>
    [Parameter]
    public Func<TValue?, string>? Formatter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 格式字符串，例如 "yyyy-MM-dd" 用于日期类型</para>
    /// <para lang="en">Gets or sets the format string, e.g., "yyyy-MM-dd" for date types</para>
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动获取焦点，默认值为 false</para>
    /// <para lang="en">Gets or sets whether to automatically focus, default is false</para>
    /// </summary>
    [Parameter]
    public bool IsAutoFocus { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在获取焦点时自动选择所有文本，默认值为 false</para>
    /// <para lang="en">Gets or sets whether to automatically select all text on focus, default is false</para>
    /// </summary>
    [Parameter]
    public bool IsSelectAllTextOnFocus { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在按下回车键时自动选择所有文本，默认值为 false</para>
    /// <para lang="en">Gets or sets whether to automatically select all text on Enter key press, default is false</para>
    /// </summary>
    [Parameter]
    public bool IsSelectAllTextOnEnter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动去除空格，默认值为 false</para>
    /// <para lang="en">Gets or sets whether to automatically trim whitespace, default is false</para>
    /// </summary>
    [Parameter]
    public bool IsTrim { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 失去焦点时的回调方法，默认值为 null</para>
    /// <para lang="en">Gets or sets the callback method for blur event, default is null</para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnBlurAsync { get; set; }

    [CascadingParameter]
    private Modal? Modal { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 输入框类型，默认值为 "text"</para>
    /// <para lang="en">Gets or sets the input type, default is "text"</para>
    /// </summary>
    protected string Type { get; set; } = "text";

    /// <summary>
    /// <para lang="zh">方法以聚焦元素</para>
    /// <para lang="en">Method to focus the element</para>
    /// </summary>
    public async Task FocusAsync() => await InvokeVoidAsync("focus", GetInputId());

    /// <summary>
    /// <para lang="zh">方法以选择所有文本</para>
    /// <para lang="en">Method to select all text</para>
    /// </summary>
    public async ValueTask SelectAllTextAsync() => await InvokeVoidAsync("select", Id);

    /// <summary>
    /// <para lang="zh">获得/设置 是否跳过 Enter/Esc 键处理的 JS 脚本注册，默认值为 false</para>
    /// <para lang="en">Gets or sets whether to skip JS script registration for Enter/Esc key handling, default is false</para>
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
    /// <para lang="zh">获得输入元素的 Id</para>
    /// <para lang="en">Gets the input element Id</para>
    /// </summary>
    protected virtual string? GetInputId() => Id;

    /// <summary>
    /// <para lang="zh">值格式化委托方法</para>
    /// <para lang="en">Value formatting delegate method</para>
    /// </summary>
    /// <param name="value"></param>
    protected override string? FormatValueAsString(TValue? value) => Formatter != null
        ? Formatter.Invoke(value)
        : (!string.IsNullOrEmpty(FormatString) && value != null
            ? Utility.Format(value, FormatString)
            : base.FormatValueAsString(value));

    /// <summary>
    /// <para lang="zh">尝试从字符串解析值</para>
    /// <para lang="en">Tries to parse a value from a string</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage) => base.TryParseValueFromString(IsTrim ? value.Trim() : value, out result, out validationErrorMessage);

    /// <summary>
    /// <para lang="zh">OnBlur 方法</para>
    /// <para lang="en">OnBlur method</para>
    /// </summary>
    protected virtual async Task OnBlur()
    {
        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }

    /// <summary>
    /// <para lang="zh">客户端 EnterCallback 方法</para>
    /// <para lang="en">Client-side EnterCallback method</para>
    /// </summary>
    [JSInvokable]
    public async Task EnterCallback()
    {
        if (OnEnterAsync != null)
        {
            await OnEnterAsync(Value);
        }
    }

    /// <summary>
    /// <para lang="zh">客户端 EscCallback 方法</para>
    /// <para lang="en">Client-side EscCallback method</para>
    /// </summary>
    [JSInvokable]
    public async Task EscCallback()
    {
        if (OnEscAsync != null)
        {
            await OnEscAsync(Value);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        Modal?.UnRegisterShownCallback(this);
    }
}
