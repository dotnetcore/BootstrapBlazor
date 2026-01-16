// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">service for storage meta数据 类型/数据 类型 pair for Table component more details: <see cref="TableMetadataForAttribute"/></para>
/// <para lang="en">service for storage metadata type/data type pair for Table component more details: <see cref="TableMetadataForAttribute"/></para>
/// </summary>
public static class TableMetadataTypeService
{
    private static ConcurrentDictionary<Type, Type> _metadataTypeCache { get; } = new();

    /// <summary>
    /// <para lang="zh">register meta数据 类型 for target model/数据 类型</para>
    /// <para lang="en">register metadata type for target model/data type</para>
    /// </summary>
    /// <param name="metadataType">Table ui metadata type</param>
    /// <param name="targetType">the target model/data type</param>
    [Obsolete("已弃用，单词拼写错误，Deprecated, typo")]
    [ExcludeFromCodeCoverage]
    public static void RegisterMatadataType(Type metadataType, Type targetType)
    {
        _metadataTypeCache.AddOrUpdate(targetType, metadataType, (to, tn) => tn);
    }

    /// <summary>
    /// <para lang="zh">注册数据类型</para>
    /// <para lang="en">register metadata type for target model/data type</para>
    /// </summary>
    /// <param name="metadataType">Table ui metadata type</param>
    /// <param name="targetType">the target model/data type</param>
    public static void RegisterMetadataType(Type metadataType, Type targetType)
    {
        _metadataTypeCache.AddOrUpdate(targetType, metadataType, (to, tn) => tn);
    }

    /// <summary>
    /// <para lang="zh">通过指定数据类型获得其 Metadata 数据</para>
    /// <para lang="en">get metadata type for target data type. return data type itself if metadata type not registered.</para>
    /// </summary>
    /// <param name="targetType">the target data type</param>
    /// <returns>metadata type</returns>
    public static Type GetMetadataType(Type targetType) => _metadataTypeCache.TryGetValue(targetType, out var type) ? type : targetType;

    /// <summary>
    /// <para lang="zh">register meta数据 类型s from assemblies by using reflection</para>
    /// <para lang="en">register metadata types from assemblies by using reflection</para>
    /// </summary>
    /// <param name="assemblies">Assemblies contains metadata types</param>
    public static void RegisterMetadataTypes(params Assembly[] assemblies)
    {
        foreach (var asm in assemblies)
        {
            var mapTypes = asm.GetTypes()
                              .Where(o => o.IsDefined(typeof(TableMetadataForAttribute), true))
                              .Select(o => new
                              {
                                  MetadataType = o,
                                  o.GetCustomAttribute<TableMetadataForAttribute>(true)!.DataType
                              });
            foreach (var mt in mapTypes)
            {
                RegisterMetadataType(mt.MetadataType, mt.DataType);
            }
        }
    }
}
