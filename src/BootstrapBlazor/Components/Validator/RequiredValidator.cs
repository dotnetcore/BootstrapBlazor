// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
        [Inject]
        [NotNull]
        private IStringLocalizer<RequiredValidator>? Localizer { get; set; }

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
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ErrorMessage ??= Localizer[nameof(ErrorMessage)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
            if (propertyValue == null)
            {
                results.Add(new ValidationResult(ErrorMessage, memberNames));
            }
            else if (propertyValue.GetType() == typeof(string))
            {
                var val = propertyValue.ToString();
                if (!AllowEmptyString && val == string.Empty)
                {
                    results.Add(new ValidationResult(ErrorMessage, memberNames));
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
                if (index == 0)
                {
                    results.Add(new ValidationResult(ErrorMessage, memberNames));
                }
            }
        }
    }
}
