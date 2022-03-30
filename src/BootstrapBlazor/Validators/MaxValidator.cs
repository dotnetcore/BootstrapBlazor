﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 最大值验证实现类
/// </summary>
class MaxValidator : ValidatorBase
{
    /// <summary>
    /// 获得/设置 值
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// 获得/设置 分割回调方法 默认 使用 ',' 分割
    /// </summary>
    public Func<string, int> SplitCallback { get; set; } = value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;

    /// <summary>
    /// 验证方法
    /// </summary>
    /// <param name="propertyValue">待校验值</param>
    /// <param name="context">ValidateContext 实例</param>
    /// <param name="results">ValidateResult 集合实例</param>
    public override Task Validate(object? propertyValue, ValidationContext context, List<ValidationResult> results)
    {
        if (!Validate(propertyValue))
        {
            var errorMessage = string.Format(CultureInfo.CurrentCulture, ErrorMessage ?? "", Value);
            results.Add(new ValidationResult(errorMessage, new string[] { context.MemberName ?? context.DisplayName }));
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 检查 Value 是否合法 返回 true 表示合法
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
                var count = SplitCallback == null ? value.Length : SplitCallback(value);
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
    /// 验证方法 小于等于 Value 时 返回 true
    /// </summary>
    protected virtual bool Validate(int count) => count <= Value;
}
