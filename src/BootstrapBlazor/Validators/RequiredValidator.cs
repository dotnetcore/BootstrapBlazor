// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
        var errorMessage = GetLocalizerErrorMessage(context, LocalizerFactory, Options);
        var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
        if (propertyValue == null)
        {
            results.Add(new ValidationResult(errorMessage, memberNames));
        }
        else if (propertyValue is string val)
        {
            if (!AllowEmptyString && val == string.Empty)
            {
                results.Add(new ValidationResult(errorMessage, memberNames));
            }
        }
        else if (propertyValue is IEnumerable v)
        {
            var enumerator = v.GetEnumerator();
            var valid = enumerator.MoveNext();
            if (!valid)
            {
                results.Add(new ValidationResult(errorMessage, memberNames));
            }
        }
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
        var errorMesssage = ErrorMessage;
        if (!string.IsNullOrEmpty(context.MemberName) && !string.IsNullOrEmpty(errorMesssage))
        {
            // 查找 resx 资源文件中的 ErrorMessage
            var memberName = context.MemberName;

            if (localizerFactory != null)
            {
                // 查找微软格式 resx 格式资源文件
                var isResx = false;
                if (options != null && options.ResourceManagerStringLocalizerType != null)
                {
                    var localizer = localizerFactory.Create(options.ResourceManagerStringLocalizerType);
                    if (localizer.TryGetLocalizerString(errorMesssage, out var resx))
                    {
                        errorMesssage = resx;
                        isResx = true;
                    }
                }

                // 查找 json 格式资源文件
                if (!isResx && localizerFactory.Create(context.ObjectType).TryGetLocalizerString($"{memberName}.{GetRuleKey()}", out var msg))
                {
                    errorMesssage = msg;
                }
            }

            if (!string.IsNullOrEmpty(errorMesssage))
            {
                var displayName = new FieldIdentifier(context.ObjectInstance, context.MemberName).GetDisplayName();
                errorMesssage = string.Format(CultureInfo.CurrentCulture, errorMesssage, displayName);
            }
        }
        return errorMesssage;
    }
}
