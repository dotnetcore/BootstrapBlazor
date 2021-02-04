// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 支持客户端验证的文本框基类
    /// </summary>
    public abstract class ValidateBase<TValue> : TooltipComponentBase, IValidateComponent, IValidateRules
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
        /// 获得/设置 泛型参数 TValue 可为空类型 Type 实例
        /// </summary>
        protected Type? NullableUnderlyingType { get; set; }

        /// <summary>
        /// Gets the associated <see cref="EditContext"/>.
        /// </summary>
        protected EditContext? EditContext { get; set; }

        /// <summary>
        /// Gets the <see cref="FieldIdentifier"/> for the bound value.
        /// </summary>
        protected FieldIdentifier? FieldIdentifier { get; set; }

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
        protected string TooltipMethod { get; set; } = "";

        /// <summary>
        /// 获得/设置 组件是否合规 默认为 null 未检查
        /// </summary>
        protected bool? IsValid { get; set; }

        /// <summary>
        /// 获得 组件是否被禁用属性值
        /// </summary>
        protected string? DisabledString => IsDisabled ? "disabled" : null;

        /// <summary>
        /// 是否显示 标签
        /// </summary>
        protected bool IsShowLabel { get; set; }

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
                    _ = ValueChanged.InvokeAsync(value);
                    if (!SkipValidate && FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
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
        /// 获得/设置 类型转化失败格式化字符串 默认为 {0}字段值必须为{1}类型
        /// </summary>
        [Parameter]
        public string ParsingErrorMessage { get; set; } = "{0}字段值必须为{1}类型";

#nullable disable
        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        /// <example>
        /// @bind-Value="model.PropertyName"
        /// </example>
        [Parameter]
        public TValue Value { get; set; }
#nullable restore

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter]
        public Expression<Func<TValue>>? ValueExpression { get; set; }

        private string? _id;
        /// <summary>
        /// 获得/设置 当前组件 Id
        /// </summary>
        [Parameter]
        public override string? Id
        {
            get { return (EditForm != null && !string.IsNullOrEmpty(EditForm.Id) && FieldIdentifier != null) ? $"{EditForm.Id}_{FieldIdentifier.Value.FieldName}" : _id; }
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
        /// 获得/设置 是否显示前置标签 默认值为 false
        /// </summary>
        [Parameter]
        public bool ShowLabel { get; set; }

        /// <summary>
        /// 获得/设置 显示名称
        /// </summary>
        [Parameter]
        public string? DisplayText { get; set; }

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
        /// 获得 ValidateFormBase 实例
        /// </summary>
        [CascadingParameter]
        protected ValidateFormBase? EditForm { get; set; }

        /// <summary>
        /// Formats the value as a string. Derived classes can override this to determine the formating used for <see cref="CurrentValueAsString"/>.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>A string representation of the value.</returns>
        protected virtual string? FormatValueAsString(TValue value) => value?.ToString();

        /// <summary>
        /// Parses a string to create an instance of <typeparamref name="TValue"/>. Derived classes can override this to change how
        /// <see cref="CurrentValueAsString"/> interprets incoming values.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="result">An instance of <typeparamref name="TValue"/>.</param>
        /// <param name="validationErrorMessage">If the value could not be parsed, provides a validation error message.</param>
        /// <returns>True if the value could be parsed; otherwise false.</returns>
        protected virtual bool TryParseValueFromString(string value, out TValue result, out string? validationErrorMessage)
        {
            var ret = false;
            validationErrorMessage = null;
            try
            {
                var valueType = typeof(TValue);
                var isBoolean = valueType == typeof(bool) || valueType == typeof(bool?);
                var v = isBoolean ? (object)value.Equals("true", StringComparison.CurrentCultureIgnoreCase) : value;

                if (BindConverter.TryConvertTo<TValue>(v, CultureInfo.InvariantCulture, out var v1))
                {
                    result = v1;
                    ret = true;
                }
                else
                {
                    result = default!;
                }
            }
            catch (Exception ex)
            {
                validationErrorMessage = ex.Message;
                result = default!;
            }

            if (!ret && validationErrorMessage == null)
            {
                var fieldName = FieldIdentifier.HasValue ? FieldIdentifier.Value.GetDisplayName() : "";
                var typeName = typeof(TValue).GetTypeDesc();
                validationErrorMessage = string.Format(ParsingErrorMessage, fieldName, typeName);
            }
            return ret;
        }

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
            .AddClass(FieldClass)
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

            NullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));

            if (EditContext == null)
            {
                // This is the first run
                // Could put this logic in OnInit, but its nice to avoid forcing people who override OnInit to call base.OnInit()

                if (CascadedEditContext != null) EditContext = CascadedEditContext;
                if (ValueExpression != null) FieldIdentifier = Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Create(ValueExpression);
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

            if (EditForm != null && FieldIdentifier != null)
            {
                // 组件被禁用时不进行客户端验证
                if (!IsDisabled) EditForm.AddValidator((FieldIdentifier.Value.Model.GetType(), FieldIdentifier.Value.FieldName), this);

                // 内置到验证组件时才使用绑定属性值获取 DisplayName
                if (DisplayText == null) DisplayText = FieldIdentifier.Value.GetDisplayName();
            }

            //显式设置显示标签时一定显示
            IsShowLabel = ShowLabel || EditForm != null;
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
                TooltipMethod = "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string RetrieveMethod() => TooltipMethod;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string RetrieveTitle() => Tooltip?.Title ?? ErrorMessage ?? "";

        #region Validation
        /// <summary>
        /// 获得 数据验证方法集合
        /// </summary>
        public ICollection<IValidator> Rules { get; } = new HashSet<IValidator>();

        /// <summary>
        /// 验证组件添加时调用此方法
        /// </summary>
        /// <param name="validator"></param>
        public virtual void OnRuleAdded(IValidator validator)
        {

        }

        /// <summary>
        /// 属性验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public void ValidateProperty(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            // 如果禁用移除验证信息
            if (IsDisabled)
            {
                results.Clear();
            }
            else
            {
                // 模型验证设置 验证属性名称
                // 验证组件内部使用
                if (string.IsNullOrEmpty(context.MemberName) && FieldIdentifier.HasValue) context.MemberName = FieldIdentifier.Value.FieldName;

                // 增加数值类型验证如 泛型 TValue 为 int 输入为 Empty 时
                ValidateType(context, results);

                Rules.ToList().ForEach(validator => validator.Validate(propertyValue, context, results));
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
                    var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
                    results.Add(new ValidationResult(PreviousErrorMessage, memberNames));
                }
            }
        }

        /// <summary>
        /// 显示/隐藏验证结果方法
        /// </summary>
        /// <param name="results"></param>
        /// <param name="validProperty">是否对本属性进行数据验证</param>
        public void ToggleMessage(IEnumerable<ValidationResult> results, bool validProperty)
        {
            if (FieldIdentifier != null)
            {
                var messages = results.Where(item => item.MemberNames.Any(m => m == FieldIdentifier.Value.FieldName));
                if (messages.Any())
                {
                    ErrorMessage = messages.First().ErrorMessage;
                    IsValid = false;

                    // 控件自身数据验证时显示 tooltip
                    // EditForm 数据验证时调用 tooltip('enable') 保证 tooltip 组件生成
                    // 调用 tooltip('hide') 后导致鼠标悬停时 tooltip 无法正常显示
                    TooltipMethod = validProperty ? "show" : "enable";
                }
                else
                {
                    ErrorMessage = null;
                    IsValid = true;
                    TooltipMethod = "dispose";
                }

                if (Tooltip != null) Tooltip.Title = ErrorMessage;

                OnValidate(IsValid ?? true);
            }
        }

        /// <summary>
        /// 客户端检查完成时调用此方法
        /// </summary>
        /// <param name="valid">检查结果</param>
        protected virtual void OnValidate(bool valid)
        {
            if (AdditionalAttributes != null) AdditionalAttributes["aria-invalid"] = !valid;
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && EditContext != null)
            {
                base.Dispose(true);
            }
        }
        #endregion
    }
}
