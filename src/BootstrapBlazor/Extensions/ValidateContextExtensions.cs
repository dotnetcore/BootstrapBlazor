// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ValidationContext 扩展方法</para>
///  <para lang="en">ValidationContext 扩展方法</para>
/// </summary>
public static class ValidationContextExtensions
{
    /// <summary>
    ///  <para lang="zh">从 <see cref="MetadataTypeAttribute"/> 中获取指定类型实例</para>
    ///  <para lang="en">从 <see cref="MetadataTypeAttribute"/> 中获取指定typeinstance</para>
    /// </summary>
    /// <typeparam name="T"><para lang="zh">验证接口类型</para><para lang="en">验证接口type</para></typeparam>
    /// <param name="context"></param>
    /// <returns><para lang="zh">没有实现 <typeparamref name="T"/> 接口，则返回 <see langword="null"/></para><para lang="en">没有实现 <typeparamref name="T"/> 接口，则返回 <see langword="null"/></para></returns>
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
    ///  <para lang="zh">获得 <see cref="ValidationResult"/> 实例</para>
    ///  <para lang="en">Gets <see cref="ValidationResult"/> instance</para>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public static ValidationResult GetValidationResult(this ValidationContext context, string? errorMessage)
    {
        var memberNames = string.IsNullOrEmpty(context.MemberName) ? null : new string[] { context.MemberName };
        return new ValidationResult(errorMessage, memberNames);
    }

    internal static List<UploadValidateItem> GetInvalidItems(this IReadOnlyCollection<ValidationResult> source, bool isInValidOnAddItem, string? newId) => isInValidOnAddItem
        ? [new UploadValidateItem() { Id = newId, ErrorMessage = source.First().ErrorMessage }]
        : source.Select(i => new UploadValidateItem() { Id = i.MemberNames.FirstOrDefault(), ErrorMessage = i.ErrorMessage }).ToList();
}
