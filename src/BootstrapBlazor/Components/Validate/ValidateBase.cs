// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 支持客户端验证的文本框基类
/// </summary>
public abstract class ValidateBase<TValue> : DisplayBase<TValue>, IValidateComponent
{
    private ValidationMessageStore? _parsingValidationMessages;

    /// <summary>
    /// 获得/设置 上一次转化是否失败 为 true 时表示上一次转化失败
    /// </summary>
    protected bool PreviousParsingAttemptFailed { get; set; }

    /// <summary>
    /// 获得/设置 上一次转化失败错误描述信息
    /// </summary>
    protected string PreviousErrorMessage { get; set; } = "";

    /// <summary>
    /// Gets the associated <see cref="EditContext"/>.
    /// </summary>
    protected EditContext? EditContext { get; set; }

    /// <summary>
    /// 获得/设置 错误描述信息
    /// </summary>
    protected string? ErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 数据合规样式
    /// </summary>
    protected string? ValidCss => IsValid.HasValue ? (IsValid.Value ? "is-valid" : "is-invalid") : null;

    /// <summary>
    /// 获得/设置 Tooltip 命令
    /// </summary>
    protected string? TooltipMethod { get; set; }

    /// <summary>
    /// 获得/设置 组件是否合规 默认为 null 未检查
    /// </summary>
    protected bool? IsValid { get; set; }

    /// <summary>
    /// 获得 组件是否被禁用属性值
    /// </summary>
    protected string? Disabled => IsDisabled ? "disabled" : null;

    /// <summary>
    /// 是否显示 必填项标记
    /// </summary>
    protected string? Required { get; set; }

    /// <summary>
    /// Gets or sets the current value of the input.
    /// </summary>
    protected TValue CurrentValue
    {
        get => Value;
        set
        {
            var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Value);
            if (hasChanged)
            {
                Value = value;

                if (FieldIdentifier != null)
                {
                    ValidateForm?.NotifyFieldChanged(FieldIdentifier.Value, Value);
                }
                if (ValueChanged.HasDelegate)
                {
                    _ = ValueChanged.InvokeAsync(value);
                }
                if (OnValueChanged != null)
                {
                    _ = OnValueChanged.Invoke(value);
                }
                if (IsNeedValidate && FieldIdentifier != null)
                {
                    EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the current value of the input, represented as a string.
    /// </summary>
    protected string CurrentValueAsString
    {
        get => FormatValueAsString(CurrentValue) ?? "";
        set
        {
            _parsingValidationMessages?.Clear();

            if (NullableUnderlyingType != null && string.IsNullOrEmpty(value))
            {
                // Assume if it's a nullable type, null/empty inputs should correspond to default(T)
                // Then all subclasses get nullable support almost automatically (they just have to
                // not reject Nullable<T> based on the type itself).
                PreviousParsingAttemptFailed = false;
                CurrentValue = default!;
            }
            else if (typeof(TValue) == typeof(object))
            {
                PreviousParsingAttemptFailed = false;
                CurrentValue = (TValue)(object)value;
            }
            else if (TryParseValueFromString(value, out var parsedValue, out var validationErrorMessage))
            {
                PreviousParsingAttemptFailed = false;
                CurrentValue = parsedValue;
            }
            else
            {
                PreviousParsingAttemptFailed = true;
                PreviousErrorMessage = validationErrorMessage ?? "";

                if (_parsingValidationMessages == null && EditContext != null)
                {
                    _parsingValidationMessages = new ValidationMessageStore(EditContext);
                }

                if (FieldIdentifier != null)
                {
                    _parsingValidationMessages?.Add(FieldIdentifier.Value, PreviousErrorMessage);

                    // Since we're not writing to CurrentValue, we'll need to notify about modification from here
                    EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
                }
            }

            // We can skip the validation notification if we were previously valid and still are
            if (PreviousParsingAttemptFailed)
            {
                EditContext?.NotifyValidationStateChanged();
            }
        }
    }

    /// <summary>
    /// 获得/设置 Value 改变时回调方法
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 类型转化失败格式化字符串 默认为 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ParsingErrorMessage { get; set; }

    private string? _id;
    /// <summary>
    /// 获得/设置 当前组件 Id
    /// </summary>
    [Parameter]
    public override string? Id
    {
        get { return (ValidateForm != null && !string.IsNullOrEmpty(ValidateForm.Id) && FieldIdentifier != null) ? $"{ValidateForm.Id}_{FieldIdentifier.Value.Model.GetHashCode()}_{FieldIdentifier.Value.FieldName}" : _id; }
        set { _id = value; }
    }

    /// <summary>
    /// 获得/设置 子组件 RenderFragment 实例
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    [Parameter]
    public bool SkipValidate { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得 父组件的 EditContext 实例
    /// </summary>
    [CascadingParameter]
    protected EditContext? CascadedEditContext { get; set; }

    /// <summary>
    /// Parses a string to create an instance of <typeparamref name="TValue"/>. Derived classes can override this to change how
    /// <see cref="CurrentValueAsString"/> interprets incoming values.
    /// </summary>
    /// <param name="value">The string value to be parsed.</param>
    /// <param name="result">An instance of <typeparamref name="TValue"/>.</param>
    /// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
    /// <returns>True if the value could be parsed; otherwise false.</returns>
    protected virtual bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        var ret = false;
        validationErrorMessage = null;
        if (value.TryConvertTo<TValue>(out result))
        {
            ret = true;
        }
        else
        {
            result = default!;
            validationErrorMessage = FormatParsingErrorMessage();
        }
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual string? FormatParsingErrorMessage() => ParsingErrorMessage;

    private bool IsRequired() => FieldIdentifier
        ?.Model.GetType().GetPropertyByName(FieldIdentifier.Value.FieldName)!.GetCustomAttribute<RequiredAttribute>(true) != null
        || (ValidateRules?.OfType<FormItemValidator>().Select(i => i.Validator).OfType<RequiredAttribute>().Any() ?? false);

    /// <summary>
    /// Gets a string that indicates the status of the field being edited. This will include
    /// some combination of "modified", "valid", or "invalid", depending on the status of the field.
    /// </summary>
    private string FieldClass => (EditContext != null && FieldIdentifier != null) ? EditContext.FieldCssClass(FieldIdentifier.Value) : "";

    /// <summary>
    /// Gets a CSS class string that combines the <c>class</c> attribute and <see cref="FieldClass"/>
    /// properties. Derived components should typically use this value for the primary HTML element's
    /// 'class' attribute.
    /// </summary>
    protected string? CssClass => CssBuilder.Default()
        .AddClass(FieldClass, IsNeedValidate)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (EditContext == null)
        {
            // This is the first run
            // Could put this logic in OnInit, but its nice to avoid forcing people who override OnInit to call base.OnInit()
            if (CascadedEditContext != null)
            {
                EditContext = CascadedEditContext;
            }
        }

        // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
        return base.SetParametersAsync(ParameterView.Empty);
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (ValidateForm != null && FieldIdentifier.HasValue)
        {
            ValidateForm.AddValidator((FieldIdentifier.Value.FieldName, ModelType: FieldIdentifier.Value.Model.GetType()), (FieldIdentifier.Value, this));
        }
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Required = (IsNeedValidate && !string.IsNullOrEmpty(DisplayText) && (ValidateForm?.ShowRequiredMark ?? false) && IsRequired()) ? "true" : null;
    }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender && !string.IsNullOrEmpty(TooltipMethod))
        {
            await ShowTooltip();
            TooltipMethod = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override string RetrieveMethod() => TooltipMethod ?? "";

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override string RetrieveTitle() => Tooltip?.Title ?? ErrorMessage ?? "";

    #region Validation
    /// <summary>
    /// 获得 数据验证方法集合
    /// </summary>
    protected List<IValidator> Rules { get; } = new();

    /// <summary>
    /// 获得/设置 自定义验证集合
    /// </summary>
    [Parameter]
    public List<IValidator>? ValidateRules { get; set; }

    /// <summary>
    /// 获得/设置 是否不进行验证 默认为 false
    /// </summary>
    public bool IsNeedValidate => !IsDisabled && !SkipValidate;

    /// <summary>
    /// 属性验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    public void ValidateProperty(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        // 如果禁用移除验证信息
        if (IsNeedValidate)
        {
            // 增加数值类型验证如 泛型 TValue 为 int 输入为 Empty 时
            ValidateType(context, results);

            // 接口验证规则
            if (results.Count == 0)
            {
                foreach (var validator in Rules)
                {
                    validator.Validate(propertyValue, context, results);
                    if (results.Count > 0)
                    {
                        break;
                    }
                }
            }

            // 自定义验证集合
            if (results.Count == 0 && ValidateRules != null)
            {
                foreach (var validator in ValidateRules)
                {
                    validator.Validate(propertyValue, context, results);
                    if (results.Count > 0)
                    {
                        break;
                    }
                }
            }
        }
    }

    private void ValidateType(ValidationContext context, List<ValidationResult> results)
    {
        // 增加数据基础类型验证 如泛型约定为 int 文本框值为 Empty
        // 可为空泛型约束时不检查
        if (NullableUnderlyingType == null)
        {
            if (PreviousParsingAttemptFailed)
            {
                var memberNames = new string[] { context.MemberName! };
                results.Add(new ValidationResult(PreviousErrorMessage, memberNames));
            }
        }
    }

    /// <summary>
    /// 显示/隐藏验证结果方法
    /// </summary>
    /// <param name="results"></param>
    /// <param name="validProperty">是否对本属性进行数据验证</param>
    public virtual void ToggleMessage(IEnumerable<ValidationResult> results, bool validProperty)
    {
        if (FieldIdentifier != null)
        {
            var messages = results.Where(item => item.MemberNames.Any(m => m == FieldIdentifier.Value.FieldName));
            if (messages.Any())
            {
                ErrorMessage = messages.First().ErrorMessage;
                IsValid = false;

                TooltipMethod = validProperty ? "show" : "enable";
            }
            else
            {
                ErrorMessage = null;
                IsValid = true;
                TooltipMethod = "dispose";
            }

            if (Tooltip != null)
            {
                Tooltip.Title = ErrorMessage;
            }

            OnValidate(IsValid);
        }
    }

    /// <summary>
    /// 客户端检查完成时调用此方法
    /// </summary>
    /// <param name="valid">检查结果</param>
    protected virtual void OnValidate(bool? valid)
    {
        if (valid.HasValue)
        {
            AdditionalAttributes ??= new Dictionary<string, object>();
            AdditionalAttributes["aria-invalid"] = valid.Value ? "false" : "true";
        }
    }

    /// <summary>
    /// DisposeAsyncCore 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsyncCore(bool disposing)
    {
        if (disposing)
        {
            if (ValidateForm != null && FieldIdentifier.HasValue)
            {
                ValidateForm.TryRemoveValidator((FieldIdentifier.Value.FieldName, FieldIdentifier.Value.Model.GetType()), out _);
            }
        }

        await base.DisposeAsyncCore(disposing);
    }
    #endregion

    /// <summary>
    /// 设置是否可用状态
    /// </summary>
    /// <param name="disable"></param>
    public void SetDisable(bool disable)
    {
        IsDisabled = disable;
        StateHasChanged();
    }

    /// <summary>
    /// 设置 Value 值
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(TValue value)
    {
        CurrentValue = value;

        // 未双向绑定时手动刷新 UI
        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
    }

    /// <summary>
    /// 设置 Label 值
    /// </summary>
    /// <param name="label"></param>
    public void SetLabel(string label)
    {
        DisplayText = label;
        StateHasChanged();
    }
}
