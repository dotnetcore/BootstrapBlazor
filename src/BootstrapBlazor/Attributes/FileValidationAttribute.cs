// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 上传文件扩展名验证标签类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FileValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FileValidationAttribute()
        {
            Localizer = JsonStringLocalizerFactory.CreateLocalizer<Upload>();
        }

        private IStringLocalizer Localizer { get; }

        /// <summary>
        /// 获得/设置 允许的扩展名
        /// </summary>
        public string[] Extensions { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 获得/设置 文件大小 默认为 0 未限制
        /// </summary>
        public long FileSize { get; set; }

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
                var file = (IBrowserFile?)value;
                if (file != null)
                {
                    if (Extensions.Any() && !Extensions.Contains(Path.GetExtension(file.Name), StringComparer.OrdinalIgnoreCase))
                    {
                        var errorMessage = Localizer["FileExtensions", string.Join(", ", Extensions)];
                        ret = new ValidationResult(errorMessage, new[] { validationContext.MemberName! });
                    }
                    if (ret == null && FileSize > 0 && file.Size > FileSize)
                    {
                        var errorMessage = Localizer["FileSizeValidation", FileSize.ToFileSizeString()];
                        ret = new ValidationResult(errorMessage, new[] { validationContext.MemberName! });
                    }
                }
            }

            return ret;
        }
    }
}
