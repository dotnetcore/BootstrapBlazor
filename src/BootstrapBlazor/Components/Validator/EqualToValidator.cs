// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    public class EqualToValidator : ValidatorComponentBase
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
            {
                var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
                results.Add(new ValidationResult(ErrorMessage, memberNames));
            }
        }
    }
}
