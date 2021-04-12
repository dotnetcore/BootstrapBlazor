// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    class RequiredValidator : ValidatorBase
    {
        /// <summary>
        /// 获得/设置 是否允许空字符串 默认 false 不允许
        /// </summary>
        public bool AllowEmptyString { get; set; }

        /// <summary>
        /// 获得/设置 IStringLocalizerFactory 注入服务实例 默认为 null
        /// </summary>
        public IStringLocalizerFactory? LocalizerFactory { get; set; }

        /// <summary>
        /// 获得/设置 Json 资源文件配置 默认为 null
        /// </summary>
        public JsonLocalizationOptions? Options { get; set; }

        /// <summary>
        /// 数据验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
        {
            var errorMessage = GetLocalizerErrorMessage(context, LocalizerFactory, Options);
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
    }
}
