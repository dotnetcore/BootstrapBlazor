// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// ValidationContext 扩展方法
/// </summary>
public static class ValidationContextExtensions
{
    /// <summary>
    /// 从 <see cref="MetadataTypeAttribute"/> 中获取指定类型实例
    /// </summary>
    /// <typeparam name="T">验证接口类型</typeparam>
    /// <param name="context"></param>
    /// <returns>没有实现 <typeparamref name="T"/> 接口，则返回 <see langword="null"/></returns>
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
}
