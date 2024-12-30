// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.Globalization;
using System.Security.AccessControl;

namespace BootstrapBlazor.Components;

/// <summary>
/// Required 验证实现类
/// </summary>
public class RequiredValidator : ValidatorBase
{
    /// <summary>
    /// 获得/设置 错误描述信息 默认为 null 需要赋值
    /// </summary>
    public string? ErrorMessage { get; set; }

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
    /// 验证方法
    /// </summary>
    /// <param name="propertyValue">待校验值</param>
    /// <param name="context">ValidateContext 实例</param>
    /// <param name="results">ValidateResult 集合实例</param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (string.IsNullOrEmpty(ErrorMessage))
        {
            var localizer = context.GetRequiredService<IStringLocalizer<ValidateBase<string>>>();
            var l = localizer["DefaultRequiredErrorMessage"];
            if (!l.ResourceNotFound)
            {
                ErrorMessage = l.Value;
            }
        }
        if (propertyValue == null)
        {
            results.Add(CreateValidationResult(context));
        }
        else if (propertyValue is string val)
        {
            if (!AllowEmptyString && val == string.Empty)
            {
                results.Add(CreateValidationResult(context));
            }
        }
        else if (propertyValue is IEnumerable v)
        {
            var enumerator = v.GetEnumerator();
            var valid = enumerator.MoveNext();
            if (!valid)
            {
                results.Add(CreateValidationResult(context));
            }
        }
        else if (propertyValue is DateTimeRangeValue dv && dv is { NullStart: null, NullEnd: null })
        {
            results.Add(CreateValidationResult(context));
        }
    }
    /// <summary>
    /// 生成错误提示信息。
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private ValidationResult CreateValidationResult(ValidationContext context)
    {
        var errorMessage = GetLocalizerErrorMessage(context, LocalizerFactory, Options);
        var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
        return new ValidationResult(errorMessage, memberNames);
    }


    /// <summary>
    /// 获得当前验证规则资源文件中 Key 格式
    /// </summary>
    /// <returns></returns>
    protected virtual string GetRuleKey() => GetType().Name.Split(".").Last().Replace("Validator", "");

    /// <summary>
    /// 通过资源文件获取 ErrorMessage 方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="localizerFactory"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    protected virtual string? GetLocalizerErrorMessage(ValidationContext context, IStringLocalizerFactory? localizerFactory = null, JsonLocalizationOptions? options = null)
    {
        var errorMessage = ErrorMessage;
        if (!string.IsNullOrEmpty(context.MemberName) && !string.IsNullOrEmpty(errorMessage))
        {
            // 查找 resx 资源文件中的 ErrorMessage
            var memberName = context.MemberName;

            if (localizerFactory != null)
            {
                // 查找微软格式 resx 格式资源文件
                var isResx = false;
                if (options is { ResourceManagerStringLocalizerType: not null })
                {
                    var localizer = localizerFactory.Create(options.ResourceManagerStringLocalizerType);
                    if (localizer.TryGetLocalizerString(errorMessage, out var resx))
                    {
                        errorMessage = resx;
                        isResx = true;
                    }
                }

                // 查找 json 格式资源文件
                if (!isResx && localizerFactory.Create(context.ObjectType).TryGetLocalizerString($"{memberName}.{GetRuleKey()}", out var msg))
                {
                    errorMessage = msg;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                var displayName = new FieldIdentifier(context.ObjectInstance, context.MemberName).GetDisplayName();
                errorMessage = string.Format(CultureInfo.CurrentCulture, errorMessage, displayName);
            }
        }
        return errorMessage;
    }
}
