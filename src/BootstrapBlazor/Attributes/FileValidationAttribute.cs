// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">上传文件扩展名验证标签类</para>
/// <para lang="en">Upload file extension validation attribute class</para>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class FileValidationAttribute : ValidationAttribute
{
    private IStringLocalizer? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 允许的扩展名</para>
    /// <para lang="en">Gets or sets allowed extensions</para>
    /// </summary>
    public string[] Extensions { get; set; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 文件大小 默认为 0 未限制</para>
    /// <para lang="en">Gets or sets file size. Default is 0 (no limit)</para>
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// <para lang="zh">是否合规判断方法</para>
    /// <para lang="en">Is valid method</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ValidationResult? ret = null;

        if (value is IBrowserFile file)
        {
            Localizer = Utility.CreateLocalizer<UploadBase<object>>();
            if (Localizer != null)
            {
                if (Extensions.Length > 0 && !Extensions.Contains(Path.GetExtension(file.Name), StringComparer.OrdinalIgnoreCase))
                {
                    var errorMessage = Localizer["FileExtensions", string.Join(", ", Extensions)];
                    ret = new ValidationResult(errorMessage.Value, GetMemberNames(validationContext));
                }
                if (ret == null && FileSize > 0 && file.Size > FileSize)
                {
                    var errorMessage = Localizer["FileSizeValidation", FileSize.ToFileSizeString()];
                    ret = new ValidationResult(errorMessage.Value, GetMemberNames(validationContext));
                }
            }
        }
        return ret;
    }

    private static IEnumerable<string>? GetMemberNames(ValidationContext validationContext) => validationContext == null || string.IsNullOrEmpty(validationContext.MemberName) ? [] : [validationContext.MemberName];
}
