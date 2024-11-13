﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
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
    protected string? PreviousErrorMessage { get; set; }

    /// <summary>
    /// Gets the associated <see cref="EditContext"/>
    /// </summary>
    protected EditContext? EditContext { get; set; }

    /// <summary>
    /// 获得/设置 错误描述信息
    /// </summary>
    protected string? ErrorMessage { get; set; }

    /// <summary>
    /// 获得/设置 数据合规样式
    /// </summary>
    protected string? ValidCss => IsValid.HasValue ? GetValidString(IsValid.Value) : null;

    private static string GetValidString(bool valid) => valid ? "is-valid" : "is-invalid";

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
    protected TValue? CurrentValue
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
                PreviousErrorMessage = validationErrorMessage;

                if (_parsingValidationMessages == null && EditContext != null)
                {
                    _parsingValidationMessages = new ValidationMessageStore(EditContext);
                }

                if (FieldIdentifier != null)
                {
                    _parsingValidationMessages?.Add(FieldIdentifier.Value, PreviousErrorMessage ?? "");

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
    public Func<TValue?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 类型转化失败格式化字符串 默认为 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ParsingErrorMessage { get; set; }

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
    /// 获得/设置 是否显示必填项标记 默认为 null 未设置
    /// </summary>
    [Parameter]
    public bool? ShowRequired { get; set; }

    /// <summary>
    /// 获得/设置 必填项错误文本 默认为 null 未设置
    /// </summary>
    [Parameter]
    public string? RequiredErrorMessage { get; set; }

    /// <summary>
    /// 获得 父组件的 EditContext 实例
    /// </summary>
    [CascadingParameter]
    protected EditContext? CascadedEditContext { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<ValidateBase<string>>? Localizer { get; set; }

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
        if (value.TryConvertTo(out result))
        {
            ret = true;
        }
        else
        {
            result = default;
            validationErrorMessage = FormatParsingErrorMessage();
        }
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual string? FormatParsingErrorMessage() => ParsingErrorMessage;

    /// <summary>
    /// 判断是否为必填字段
    /// </summary>
    /// <returns></returns>
    protected virtual bool IsRequired() => ShowRequired ?? FieldIdentifier
        ?.Model.GetType().GetPropertyByName(FieldIdentifier.Value.FieldName)!.GetCustomAttribute<RequiredAttribute>(true) != null
        || (ValidateRules?.OfType<FormItemValidator>().Select(i => i.IsRequired).Any() ?? false)
        || (ValidateRules?.OfType<RequiredValidator>().Any() ?? false);

    /// <summary>
    /// Gets a string that indicates the status of the field being edited. This will include
    /// some combination of "modified", "valid", or "invalid", depending on the status of the field.
    /// </summary>
    protected string FieldClass => (EditContext != null && FieldIdentifier != null) ? EditContext.FieldCssClass(FieldIdentifier.Value) : "";

    /// <summary>
    /// Gets a CSS class string that combines the <c>class</c> attribute and <see cref="FieldClass"/>
    /// properties. Derived components should typically use this value for the primary HTML element's class attribute.
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

        Id = (!string.IsNullOrEmpty(ValidateForm?.Id) && FieldIdentifier != null)
                ? $"{ValidateForm.Id}_{FieldIdentifier.Value.Model.GetHashCode()}_{FieldIdentifier.Value.FieldName}"
                : base.Id;
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Required = (IsNeedValidate && !string.IsNullOrEmpty(DisplayText) && (ValidateForm?.ShowRequiredMark ?? false) && IsRequired()) ? "true" : null;

        if (ShowRequired is true)
        {
            Rules.Add(new RequiredValidator() { ErrorMessage = RequiredErrorMessage ?? GetDefaultRequiredErrorMessage() });
        }

        if (ValidateForm != null)
        {
            // IValidateCollection 支持组件间联动验证
            var fieldName = FieldIdentifier?.FieldName;
            if (!string.IsNullOrEmpty(fieldName))
            {
                var item = ValidateForm.InvalidMemberNames.Find(i => i.MemberNames.Any(m => m == fieldName));
                if (item != null)
                {
                    ValidateForm.InvalidMemberNames.Remove(item);
                    IsValid = false;
                    ErrorMessage = item.ErrorMessage;
                }
                else if (ValidateForm.ValidMemberNames.Remove(fieldName))
                {
                    IsValid = true;
                    ErrorMessage = null;
                }
            }
        }
    }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender && IsValid.HasValue)
        {
            var valid = IsValid.Value;
            if (valid)
            {
                await RemoveValidResult();
            }
            else
            {
                await ShowValidResult();
            }
        }
    }

    private string? _defaultRequiredErrorMessage;

    private string GetDefaultRequiredErrorMessage()
    {
        _defaultRequiredErrorMessage ??= Localizer["DefaultRequiredErrorMessage"];
        return _defaultRequiredErrorMessage;
    }

    #region Validation
    /// <summary>
    /// 获得 数据验证方法集合
    /// </summary>
    protected List<IValidator> Rules { get; } = [];

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
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public virtual bool IsComplexValue(object? value) => value != null
        && value is not string
        && !value.GetType().IsAssignableTo(typeof(System.Collections.IEnumerable))
        && value.GetType().IsClass;

    /// <summary>
    /// 属性验证方法
    /// </summary>
    /// <param name="propertyValue"></param>
    /// <param name="context"></param>
    /// <param name="results"></param>
    public async Task ValidatePropertyAsync(object? propertyValue, ValidationContext context, List<ValidationResult> results)
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
                    if (validator is IValidatorAsync v)
                    {
                        await v.ValidateAsync(propertyValue, context, results);
                    }
                    else
                    {
                        validator.Validate(propertyValue, context, results);
                    }
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
                    if (validator is IValidatorAsync v)
                    {
                        await v.ValidateAsync(propertyValue, context, results);
                    }
                    else
                    {
                        validator.Validate(propertyValue, context, results);
                    }
                    if (results.Count > 0)
                    {
                        var memberName = results[0].MemberNames.FirstOrDefault() ?? context.MemberName;
                        if (!string.IsNullOrEmpty(memberName))
                        {
                            var result = new ValidationResult(results[0].ErrorMessage, [memberName]);
                            results.Clear();
                            results.Add(result);
                        }
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
        if (NullableUnderlyingType == null && PreviousParsingAttemptFailed)
        {
            var memberNames = new string[] { context.MemberName! };
            results.Add(new ValidationResult(PreviousErrorMessage, memberNames));
        }
    }

    /// <summary>
    /// 显示/隐藏验证结果方法
    /// </summary>
    /// <param name="results"></param>
    public virtual void ToggleMessage(IEnumerable<ValidationResult> results)
    {
        if (FieldIdentifier != null)
        {
            var messages = results.Where(item => item.MemberNames.Any(m => m == FieldIdentifier.Value.FieldName));
            if (messages.Any())
            {
                ErrorMessage = messages.First().ErrorMessage;
                IsValid = false;
            }
            else
            {
                ErrorMessage = null;
                IsValid = true;
            }

            OnValidate(IsValid);
        }

        // 必须刷新一次 UI 保证状态正确
        StateHasChanged();
    }

    private JSModule? ValidateModule { get; set; }

    private Task<JSModule> LoadValidateModule() => JSRuntime.LoadModule("./_content/BootstrapBlazor/modules/validate.js");

    /// <summary>
    /// 增加客户端 Tooltip 方法
    /// </summary>
    /// <returns></returns>
    protected virtual async ValueTask ShowValidResult()
    {
        var id = RetrieveId();
        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(ErrorMessage))
        {
            ValidateModule ??= await LoadValidateModule();
            await ValidateModule.InvokeVoidAsync("execute", id, ErrorMessage);
        }
    }

    /// <summary>
    /// 移除客户端 Tooltip 方法
    /// </summary>
    /// <returns></returns>
    protected virtual async ValueTask RemoveValidResult(string? validateId = null)
    {
        var id = validateId ?? RetrieveId();
        if (!string.IsNullOrEmpty(id))
        {
            ValidateModule ??= await LoadValidateModule();
            await ValidateModule.InvokeVoidAsync("dispose", id);
        }
    }

    /// <summary>
    /// 客户端检查完成时调用此方法
    /// </summary>
    /// <param name="valid">检查结果</param>
    protected virtual void OnValidate(bool? valid)
    {

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (ValidateForm != null && FieldIdentifier.HasValue)
            {
                ValidateForm.TryRemoveValidator((FieldIdentifier.Value.FieldName, FieldIdentifier.Value.Model.GetType()), out _);
            }

            if (ValidateModule != null)
            {
                var id = RetrieveId();
                await ValidateModule.InvokeVoidAsync("dispose", id);
            }
        }

        await base.DisposeAsync(disposing);
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
