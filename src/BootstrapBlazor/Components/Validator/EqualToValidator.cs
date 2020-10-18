using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 值相等验证组件
    /// </summary>
    public class EqualToValidator : ValidatorComponentBase, IValidator
    {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private IStringLocalizer<EqualToValidator>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Value { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ErrorMessage = Localizer[nameof(ErrorMessage)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            var val = propertyValue?.ToString() ?? "";
            if (val != Value)
                results.Add(new ValidationResult(ErrorMessage, new string[] { context.MemberName }));
        }
    }
}
