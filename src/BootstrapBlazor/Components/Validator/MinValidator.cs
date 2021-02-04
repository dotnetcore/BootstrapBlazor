// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
