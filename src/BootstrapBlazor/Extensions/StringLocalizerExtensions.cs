// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Linq.Expressions;

namespace Microsoft.Extensions.Localization;

/// <summary>
/// IStringLocalizer 扩展方法
/// </summary>
public static class StringLocalizerExtensions
{
    /// <summary>
    /// 通过 Lambda 表达式获取指定类型的语言文字信息
    /// </summary>
    /// <typeparam name="TResource"></typeparam>
    /// <param name="stringLocalizer"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    public static LocalizedString? GetString<TResource>(
        this IStringLocalizer stringLocalizer,
        Expression<Func<TResource, string>> propertyExpression)
    {
        if (propertyExpression.Body is not MemberExpression member)
        {
            throw new InvalidOperationException($"{nameof(propertyExpression)}'s Body property must be a instance of {nameof(MemberExpression)}");
        }

        return stringLocalizer[member.Member.Name];
    }
}
