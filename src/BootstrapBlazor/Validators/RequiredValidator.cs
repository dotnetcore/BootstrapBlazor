// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Required 验证实现类</para>
/// <para lang="en">Required validator implementation class</para>
/// </summary>
public class RequiredValidator : ValidatorBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 错误描述信息 默认为 null 需要赋值</para>
    /// <para lang="en">Gets or sets error message default null need to be assigned</para>
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许空字符串 默认 false 不允许</para>
    /// <para lang="en">Gets or sets whether to allow empty string default false not allowed</para>
    /// </summary>
    public bool AllowEmptyString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 IStringLocalizerFactory 注入服务实例 默认为 null</para>
    /// <para lang="en">Gets or sets IStringLocalizerFactory injection service instance default null</para>
    /// </summary>
    public IStringLocalizerFactory? LocalizerFactory { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Json 资源文件配置 默认为 null</para>
    /// <para lang="en">Gets or sets Json resource file configuration default null</para>
    /// </summary>
    public JsonLocalizationOptions? Options { get; set; }

    /// <summary>
    /// <para lang="zh">验证方法</para>
    /// <para lang="en">Validation method</para>
    /// </summary>
    /// <param name="propertyValue"><para lang="zh">待校验值</para><para lang="en">Value to be validated</para></param>
    /// <param name="context"><para lang="zh">ValidateContext 实例</para><para lang="en">ValidateContext instance</para></param>
    /// <param name="results"><para lang="zh">ValidateResult 集合实例</para><para lang="en">ValidateResult collection instance</para></param>
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
            results.Add(GetValidationResult(context));
        }
        else if (propertyValue is string val)
        {
            if (!AllowEmptyString && val == string.Empty)
            {
                results.Add(GetValidationResult(context));
            }
        }
        else if (propertyValue is IEnumerable v)
        {
            var enumerator = v.GetEnumerator();
            var valid = enumerator.MoveNext();
            if (!valid)
            {
                results.Add(GetValidationResult(context));
            }
        }
        else if (propertyValue is DateTimeRangeValue dv && dv is { NullStart: null, NullEnd: null })
        {
            results.Add(GetValidationResult(context));
        }
    }

    private ValidationResult GetValidationResult(ValidationContext context)
    {
        var errorMessage = GetLocalizerErrorMessage(context, LocalizerFactory, Options);
        return context.GetValidationResult(errorMessage);
    }

    /// <summary>
    /// <para lang="zh">获得当前验证规则资源文件中 Key 格式</para>
    /// <para lang="en">Get Key format in current validation rule resource file</para>
    /// </summary>
    protected virtual string GetRuleKey() => GetType().Name.Split(".").Last().Replace("Validator", "");

    /// <summary>
    /// <para lang="zh">通过资源文件获取 ErrorMessage 方法</para>
    /// <para lang="en">Get ErrorMessage method by resource file</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="localizerFactory"></param>
    /// <param name="options"></param>
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
