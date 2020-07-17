using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

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
        [Parameter]
        public int Length
        {
            get { return _length; }
            set
            {
                _length = value;
                ErrorMessage = $"最多可以输入 {_length} 个字符";
            }
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
            if (val.Length > Length) results.Add(new ValidationResult(ErrorMessage, new string[] { context.MemberName }));
        }
    }
}
