using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class MinValidator<TValue> : ValidatorComponentBase
    {
#nullable disable
        /// <summary>
        /// 获得/设置 最小值数值
        /// </summary>
        [Parameter]
        public TValue Value { get; set; }
#nullable restore

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            if (!typeof(TValue).IsNumber()) throw new InvalidOperationException($"The type '{typeof(TValue)}' is not a supported numeric type.");

            base.OnInitialized();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            if (propertyValue != null)
            {
                var invoker = GreaterThanOrEqualCache.GetOrAdd(typeof(TValue), key => Value.GetGreaterThanOrEqualLambda().Compile());
                var ret = invoker(Value, propertyValue);
                if (ret) results.Add(new ValidationResult(ErrorMessage, new string[] { context.MemberName }));
            }
        }

        private static readonly ConcurrentDictionary<Type, Func<TValue, object, bool>> GreaterThanOrEqualCache = new ConcurrentDictionary<Type, Func<TValue, object, bool>>();
    }
}
