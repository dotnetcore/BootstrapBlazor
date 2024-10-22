﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
