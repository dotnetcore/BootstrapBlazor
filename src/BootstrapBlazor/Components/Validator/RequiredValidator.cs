// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    class RequiredValidator : IValidator
    {
        /// <summary>
        /// 获得/设置 是否允许空字符串 默认 false 不允许
        /// </summary>
        public bool AllowEmptyString { get; set; }

        /// <summary>
        /// 获得/设置 错误描述信息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            var errorMessage = GetErrorMessage(context);
            var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
            if (propertyValue == null)
            {
                results.Add(new ValidationResult(errorMessage, memberNames));
            }
            else if (propertyValue.GetType() == typeof(string))
            {
                var val = propertyValue.ToString();
                if (!AllowEmptyString && val == string.Empty)
                {
                    results.Add(new ValidationResult(errorMessage, memberNames));
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
                    results.Add(new ValidationResult(errorMessage, memberNames));
                }
            }
        }

        private string? GetErrorMessage(ValidationContext context)
        {
            var errorMesssage = ErrorMessage;
            if (!string.IsNullOrEmpty(context.MemberName) && !string.IsNullOrEmpty(errorMesssage))
            {
                // 查找 resx 资源文件中的 ErrorMessage
                var memberName = context.MemberName;

                var isResx = false;
                var resxType = ServiceProviderHelper.ServiceProvider.GetRequiredService<IOptions<JsonLocalizationOptions>>().Value.ResourceManagerStringLocalizerType;
                if (resxType != null && JsonHtmlLocalizerFactory.TryGetLocalizerString(resxType, errorMesssage, out var resx))
                {
                    errorMesssage = resx;
                    isResx = true;
                }

                if (!isResx && JsonHtmlLocalizerFactory.TryGetLocalizerString(context.ObjectType, $"{memberName}.Required", out var msg))
                {
                    errorMesssage = msg;
                }

                if (!string.IsNullOrEmpty(errorMesssage))
                {
                    var displayName = new FieldIdentifier(context.ObjectInstance, context.MemberName).GetDisplayName();
                    errorMesssage = string.Format(CultureInfo.CurrentCulture, errorMesssage, displayName ?? memberName);
                }
            }
            return errorMesssage;
        }
    }
}
