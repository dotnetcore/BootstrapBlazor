using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class RequiredValidator : ValidatorComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        public RequiredValidator()
        {
            ErrorMessage = "这是必填字段";
        }

        /// <summary>
        /// 获得/设置 是否允许空字符串 默认 false 不允许
        /// </summary>
        [Parameter]
        public bool AllowEmptyString { get; set; }

        /// <summary>
        /// 获得/设置 是否允许空集合 默认 false 不允许
        /// </summary>
        [Parameter]
        public bool AllowEmptyList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            if (propertyValue == null)
            {
                results.Add(new ValidationResult(ErrorMessage, new string[] { context.MemberName }));
            }
            else if (propertyValue.GetType() == typeof(string))
            {
                var val = propertyValue.ToString();
                if (!AllowEmptyString && val == string.Empty)
                {
                    results.Add(new ValidationResult(ErrorMessage, new string[] { context.MemberName }));
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(propertyValue.GetType()))
            {
                var v = propertyValue as IEnumerable;
                var index = 0;
                foreach (var item in v!)
                {
                    index++;
                    break;
                }
                if(index == 0)
                {
                    results.Add(new ValidationResult(ErrorMessage, new string[] { context.MemberName }));
                }
            }
        }
    }
}
