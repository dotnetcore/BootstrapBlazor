using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 支持客户端验证的文本框基类
    /// </summary>
    public abstract class ValidateBase<TValue> : TooltipComponentBase, IDisposable
    {
        private readonly EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;
        private bool _previousParsingAttemptFailed;
        private ValidationMessageStore? _parsingValidationMessages;
        private Type? _nullableUnderlyingType;

        [CascadingParameter] EditContext? CascadedEditContext { get; set; }

#nullable disable
        /// <summary>
        /// Gets or sets the value of the input. This should be used with two-way binding.
        /// </summary>
        /// <example>
        /// @bind-Value="model.PropertyName"
        /// </example>
        [Parameter] public TValue Value { get; set; }
#nullable restore

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter] public Expression<Func<TValue>>? ValueExpression { get; set; }

        /// <summary>
        /// Gets the associated <see cref="EditContext"/>.
        /// </summary>
        protected EditContext? EditContext { get; set; }

        /// <summary>
        /// Gets the <see cref="FieldIdentifier"/> for the bound value.
        /// </summary>
        protected FieldIdentifier? FieldIdentifier { get; set; }

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
                    if (FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
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

                bool parsingFailed;

                if (_nullableUnderlyingType != null && string.IsNullOrEmpty(value))
                {
                    // Assume if it's a nullable type, null/empty inputs should correspond to default(T)
                    // Then all subclasses get nullable support almost automatically (they just have to
                    // not reject Nullable<T> based on the type itself).
                    parsingFailed = false;
#nullable disable
                    CurrentValue = default;
#nullable restore
                }
                else if (TryParseValueFromString(value, out var parsedValue, out var validationErrorMessage))
                {
                    parsingFailed = false;
                    CurrentValue = parsedValue;
                }
                else
                {
                    parsingFailed = true;

                    if (_parsingValidationMessages == null && EditContext != null)
                    {
                        _parsingValidationMessages = new ValidationMessageStore(EditContext);
                    }

                    if (FieldIdentifier != null)
                    {
                        _parsingValidationMessages?.Add(FieldIdentifier.Value, validationErrorMessage);

                        // Since we're not writing to CurrentValue, we'll need to notify about modification from here
                        EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
                    }
                }

                // We can skip the validation notification if we were previously valid and still are
                if (parsingFailed || _previousParsingAttemptFailed)
                {
                    EditContext?.NotifyValidationStateChanged();
                    _previousParsingAttemptFailed = parsingFailed;
                }
            }
        }

#nullable disable
        /// <summary>
        /// Constructs an instance of <see cref="InputBase{TValue}"/>.
        /// </summary>
        protected ValidateBase()
        {
            _validationStateChangedHandler = (sender, eventArgs) => StateHasChanged();
        }
#nullable restore

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
        protected abstract bool TryParseValueFromString(string value, out TValue result, out string? validationErrorMessage);

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
        protected string CssClass
        {
            get
            {
                if (AdditionalAttributes != null &&
                    AdditionalAttributes.TryGetValue("class", out var @class) &&
                    !string.IsNullOrEmpty(Convert.ToString(@class)))
                {
                    return $"{@class} {FieldClass}";
                }

                return FieldClass; // Never null or empty
            }
        }

        /// <summary>
        /// 
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

                if (CascadedEditContext != null) EditContext = CascadedEditContext;
                if (ValueExpression != null) FieldIdentifier = Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Create(ValueExpression);

                _nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));

                if (EditContext != null) EditContext.OnValidationStateChanged += _validationStateChangedHandler;
            }
            // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
            return base.SetParametersAsync(ParameterView.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

        }

        void IDisposable.Dispose()
        {
            if (EditContext != null)
            {
                EditContext.OnValidationStateChanged -= _validationStateChangedHandler;
            }
            Dispose(disposing: true);
        }
    }
}
