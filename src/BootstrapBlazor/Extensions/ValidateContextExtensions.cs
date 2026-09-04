// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ValidationContext 扩展方法</para>
/// <para lang="en">ValidationContext extension methods</para>
/// </summary>
public static class ValidationContextExtensions
{
    /// <summary>
    /// <para lang="zh">从 <see cref="MetadataTypeAttribute"/> 中获取指定类型实例</para>
    /// <para lang="en">Gets an instance of the specified type from <see cref="MetadataTypeAttribute"/></para>
    /// </summary>
    /// <typeparam name="T"><para lang="zh">验证接口类型</para><para lang="en">Validation interface type</para></typeparam>
    /// <param name="context"><para lang="zh">验证上下文</para><para lang="en">Validation context</para></param>
    /// <returns><para lang="zh">未配置 <see cref="MetadataTypeAttribute"/> 或元数据类型未实现 <typeparamref name="T"/> 接口时返回 <see langword="null"/></para><para lang="en">Returns <see langword="null"/> when <see cref="MetadataTypeAttribute"/> is not configured or the metadata type does not implement <typeparamref name="T"/></para></returns>
    public static T? GetInstanceFromMetadataType<T>(this ValidationContext context) where T : class
    {
        T? ret = default;
        var attribute = context.ObjectInstance.GetType().GetCustomAttribute<MetadataTypeAttribute>();
        if (attribute != null && attribute.MetadataClassType.GetInterfaces().Any(x => x.Equals(typeof(T))))
        {
            //此处是否需要缓存？
            ret = ActivatorUtilities.CreateInstance(context, attribute.MetadataClassType) as T;
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 <see cref="ValidationResult"/> 实例</para>
    /// <para lang="en">Gets <see cref="ValidationResult"/> instance</para>
    /// </summary>
    /// <param name="context"><para lang="zh">验证上下文</para><para lang="en">Validation context</para></param>
    /// <param name="errorMessage"><para lang="zh">错误信息</para><para lang="en">Error message</para></param>
    /// <returns><para lang="zh">验证结果实例</para><para lang="en">Validation result instance</para></returns>
    public static ValidationResult GetValidationResult(this ValidationContext context, string? errorMessage)
    {
        var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
        return new ValidationResult(errorMessage, memberNames);
    }

    internal static List<UploadValidateItem> GetInvalidItems(this IReadOnlyCollection<ValidationResult> source, bool isInValidOnAddItem, string? newId) => isInValidOnAddItem
        ? [new UploadValidateItem() { Id = newId, ErrorMessage = source.First().ErrorMessage }]
        : source.Select(i => new UploadValidateItem() { Id = i.MemberNames.FirstOrDefault(), ErrorMessage = i.ErrorMessage }).ToList();
}
