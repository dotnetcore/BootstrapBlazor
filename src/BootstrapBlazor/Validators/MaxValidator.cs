// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">选项最大数验证实现类</para>
/// <para lang="en">Max validator implementation class</para>
/// </summary>
public class MaxValidator : ValidatorBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 错误描述信息</para>
    /// <para lang="en">Get/Set error message</para>
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 值</para>
    /// <para lang="en">Get/Set value</para>
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分割回调方法 默认 使用 ',' 分割</para>
    /// <para lang="en">Get/Set split callback method default use ',' split</para>
    /// </summary>
    public Func<string, int> SplitCallback { get; set; } = value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;

    /// <summary>
    /// <para lang="zh">获得 ErrorMessage 方法</para>
    /// <para lang="en">Get ErrorMessage method</para>
    /// </summary>
    protected virtual string GetErrorMessage() => ErrorMessage ?? "At most {0} items can be selected";

    /// <summary>
    /// <para lang="zh">验证方法</para>
    /// <para lang="en">Validation method</para>
    /// </summary>
    /// <param name="propertyValue"><para lang="zh">待校验值</para><para lang="en">Value to be validated</para></param>
    /// <param name="context"><para lang="zh">ValidateContext 实例</para><para lang="en">ValidateContext instance</para></param>
    /// <param name="results"><para lang="zh">ValidateResult 集合实例</para><para lang="en">ValidateResult collection instance</para></param>
    public override void Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (!Validate(propertyValue))
        {
            var errorMessage = string.Format(CultureInfo.CurrentCulture, GetErrorMessage(), Value);
            results.Add(new ValidationResult(errorMessage, new string[] { context.MemberName ?? context.DisplayName }));
        }
    }

    /// <summary>
    /// <para lang="zh">检查 Value 是否合法 返回 true 表示合法</para>
    /// <para lang="en">Check if Value is valid return true if valid</para>
    /// </summary>
    /// <param name="propertyValue"></param>
    protected virtual bool Validate(object? propertyValue)
    {
        var ret = true;
        if (propertyValue != null)
        {
            var type = propertyValue.GetType();
            if (propertyValue is string value)
            {
                var count = SplitCallback(value);
                ret = Validate(count);
            }
            else if (type.IsGenericType || type.IsArray)
            {
                ret = Validate(LambdaExtensions.ElementCount(propertyValue));
            }
        }
        else
        {
            ret = false;
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">验证方法 小于等于 Value 时 返回 true</para>
    /// <para lang="en">Validation method return true when less than or equal to Value</para>
    /// </summary>
    protected virtual bool Validate(int count) => count <= Value;
}
