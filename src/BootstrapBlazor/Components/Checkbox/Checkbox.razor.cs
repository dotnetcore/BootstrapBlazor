// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Checkbox 组件</para>
/// <para lang="en">Checkbox component</para>
/// </summary>
[BootstrapModuleAutoLoader(JSObjectReference = true)]
public partial class Checkbox<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得 class 样式集合</para>
    /// <para lang="en">Get the class style collection</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("form-check")
        .AddClass("is-label", IsShowAfterLabel)
        .AddClass($"form-check-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"form-check-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass("disabled", IsDisabled)
        .AddClass(ValidCss)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private bool IsShowAfterLabel => ShowAfterLabel && !string.IsNullOrEmpty(DisplayText);

    /// <summary>
    /// <para lang="zh">Input 元素样式</para>
    /// <para lang="en">Input element style</para>
    /// </summary>
    protected string? InputClassString => CssBuilder.Default("form-check-input")
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// <para lang="zh">Check 状态字符串</para>
    /// <para lang="en">Check status string</para>
    /// </summary>
    protected string? CheckedString => State switch
    {
        CheckboxState.Checked => "checked",
        _ => null
    };

    /// <summary>
    /// <para lang="zh">判断双向绑定类型是否为 boolean 类型</para>
    /// <para lang="en">Determine whether the two-way binding type is boolean</para>
    /// </summary>
    private bool IsBoolean { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮颜色 默认为 None 未设置</para>
    /// <para lang="en">Gets or sets the button color. Default is None (not set)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Size 大小 默认为 None</para>
    /// <para lang="en">Gets or sets the Size. Default is None</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Checkbox 后置 label 文字 默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the Checkbox post label text. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowAfterLabel { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择框状态</para>
    /// <para lang="en">Gets or sets the checkbox state</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public CheckboxState State { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 State 状态改变回调方法</para>
    /// <para lang="en">Gets or sets the State change callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public EventCallback<CheckboxState> StateChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中状态改变前回调此方法 返回 false 可以阻止状态改变</para>
    /// <para lang="en">Gets or sets the callback method before the selected state changes. Returning false can prevent the state change</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<CheckboxState, Task<bool>>? OnBeforeStateChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选择框状态改变时回调此方法</para>
    /// <para lang="en">Gets or sets the callback method when the checkbox state changes</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<CheckboxState, TValue, Task>? OnStateChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否事件冒泡 默认为 false</para>
    /// <para lang="en">Gets or sets whether event bubbling. Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件 RenderFragment 实例</para>
    /// <para lang="en">Gets or sets the child component RenderFragment instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        IsBoolean = (Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue)) == typeof(bool);
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
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
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        _paddingStateChanged = false;
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        await InvokeVoidAsync("update", Id, State == CheckboxState.Indeterminate, State == CheckboxState.Checked);
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnStateChangedAsync));

    /// <summary>
    /// <para lang="zh">点击组件触发方法 内部调用 <see cref="OnBeforeStateChanged"/> 回调方法</para>
    /// <para lang="en">Click component trigger method. Internally calls <see cref="OnBeforeStateChanged"/> callback method</para>
    /// </summary>
    /// <returns></returns>
    public async Task OnToggleClick()
    {
        var valid = true;
        CheckboxState state;
        if (State == CheckboxState.Indeterminate)
        {
            state = CheckboxState.Checked;
        }
        else
        {
            state = State == CheckboxState.Checked ? CheckboxState.UnChecked : CheckboxState.Checked;
        }
        if (OnBeforeStateChanged != null)
        {
            valid = await OnBeforeStateChanged(state);
        }

        if (valid)
        {
            await InternalStateChanged(state);
            StateHasChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">触发 Click 方法 由 JavaScript 调用</para>
    /// <para lang="en">Trigger Click method. Called by JavaScript</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public ValueTask OnStateChangedAsync(CheckboxState state)
    {
        State = state;
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">此变量为了提高性能，避免循环更新</para>
    /// <para lang="en">This variable is to improve performance and avoid circular updates</para>
    /// </summary>
    private bool _paddingStateChanged;

    private async Task<bool> InternalStateChanged(CheckboxState state)
    {
        var ret = true;
        _paddingStateChanged = true;

        if (IsBoolean)
        {
            CurrentValue = (TValue)(object)(state == CheckboxState.Checked);

            if (ValueChanged.HasDelegate)
            {
                ret = false;
            }
        }

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
        return ret;
    }

    /// <summary>
    /// <para lang="zh">设置 复选框状态方法</para>
    /// <para lang="en">Set checkbox state method</para>
    /// </summary>
    /// <param name="state"></param>
    public async Task SetState(CheckboxState state)
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
}
