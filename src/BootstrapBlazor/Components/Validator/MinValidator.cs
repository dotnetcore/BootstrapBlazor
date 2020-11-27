// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
    public class MinValidator<TValue> : ValidatorComponentBase where TValue : struct
    {
        /// <summary>
        /// 获得/设置 最小值数值
        /// </summary>
        [Parameter]
        public TValue Value { get; set; }

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
                var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
                if (ret) results.Add(new ValidationResult(ErrorMessage, memberNames));
            }
        }

        private static readonly ConcurrentDictionary<Type, Func<TValue, object, bool>> GreaterThanOrEqualCache = new ConcurrentDictionary<Type, Func<TValue, object, bool>>();
    }
}
