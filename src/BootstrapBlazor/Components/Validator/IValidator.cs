// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IValidator 接口
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// 获得/设置 错误描述信息
        /// </summary>
        string? ErrorMessage { get; set; }

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <param name="context"></param>
        /// <param name="results"></param>
        void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results);
    }
}
