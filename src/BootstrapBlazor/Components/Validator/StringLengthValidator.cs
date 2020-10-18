using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class StringLengthValidator : ValidatorComponentBase
    {
        private int _length = 50;

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private IStringLocalizer<StringLengthValidator>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Length { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            ErrorMessage = Localizer[nameof(ErrorMessage), Length];
            var val = propertyValue?.ToString() ?? "";
            if (val.Length > Length) results.Add(new ValidationResult(ErrorMessage, new string[] { context.MemberName }));
        }
    }
}
