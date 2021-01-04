// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 上传文件验证标签类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FileValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="allowedExtensions"></param>
        public FileValidationAttribute(string[] allowedExtensions)
        {
            AllowedExtensions = allowedExtensions;
        }

        private string[] AllowedExtensions { get; }

        /// <summary>
        /// 是否合规判断方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ValidationResult? ret = null;

            if (value != null)
            {
                var file = (IBrowserFile)value;

                var extension = System.IO.Path.GetExtension(file.Name);

                if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"File must have one of the following extensions: {string.Join(", ", AllowedExtensions)}.", new[] { validationContext.MemberName! });
                }
                ret = ValidationResult.Success;
            }

            return ret;
        }
    }
}
