// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Base class for BootstrapInput components
/// </summary>
[BootstrapModuleAutoLoader("Input/BootstrapInput.razor.js", JSObjectReference = true, AutoInvokeInit = false)]
public abstract class BootstrapInputBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// Gets the class attribute value
    /// </summary>
    protected virtual string? ClassName => CssBuilder.Default("form-control")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// Gets or sets Element reference instance
    /// </summary>
    protected ElementReference FocusElement { get; set; }

    /// <summary>
    /// Gets or sets the placeholder attribute value
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// Gets or sets the callback method for Enter key press, default is null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnEnterAsync { get; set; }

    /// <summary>
    /// Gets or sets the callback method for Esc key press, default is null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnEscAsync { get; set; }

    /// <summary>
    /// Gets or sets the button color
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.None;

    /// <summary>
    /// Gets or sets the formatter function
    /// </summary>
    [Parameter]
    public Func<TValue?, string>? Formatter { get; set; }

    /// <summary>
    /// Gets or sets the format string, e.g., "yyyy-MM-dd" for date types
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically focus, default is false
    /// </summary>
    [Parameter]
    public bool IsAutoFocus { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically select all text on focus, default is false
    /// </summary>
    [Parameter]
    public bool IsSelectAllTextOnFocus { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically select all text on Enter key press, default is false
    /// </summary>
    [Parameter]
    public bool IsSelectAllTextOnEnter { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically trim whitespace, default is false
    /// </summary>
    [Parameter]
    public bool IsTrim { get; set; }

    /// <summary>
    /// Gets or sets the callback method for blur event, default is null
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnBlurAsync { get; set; }

    [CascadingParameter]
    private Modal? Modal { get; set; }

    /// <summary>
    /// Gets the input type, default is "text"
    /// </summary>
    protected string Type { get; set; } = "text";

    /// <summary>
    /// Method to focus the element
    /// </summary>
    /// <returns></returns>
    public async Task FocusAsync() => await FocusElement.FocusAsync();

    /// <summary>
    /// Method to select all text
    /// </summary>
    /// <returns></returns>
    public async ValueTask SelectAllTextAsync() => await InvokeVoidAsync("select", Id);

    /// <summary>
    /// Gets or sets whether to skip JS script registration for Enter/Esc key handling, default is false
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
    /// Gets the input element Id
    /// </summary>
    protected virtual string? GetInputId() => Id;

    /// <summary>
    /// Value formatting delegate method
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(TValue? value) => Formatter != null
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
    /// OnBlur method
    /// </summary>
    protected virtual async Task OnBlur()
    {
        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }

    /// <summary>
    /// Client-side EnterCallback method
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
    /// Client-side EscCallback method
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
