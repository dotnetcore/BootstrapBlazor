// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BootstrapBlazor.Components
{
    class MaxValidator : IValidator
    {
        public int Value { get; set; }

        public string? ErrorMessage { get; set; }

        public void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            if (propertyValue != null && propertyValue is int v)
            {
                if (v > Value)
                {
                    var errorMessage = string.Format(CultureInfo.CurrentCulture, ErrorMessage ?? "", Value);
                    results.Add(new ValidationResult(errorMessage, new string[] { context.MemberName ?? "" }));
                }
            }
        }
    }
}
