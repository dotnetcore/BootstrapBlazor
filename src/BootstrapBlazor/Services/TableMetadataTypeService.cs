﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// service for storage metadata type/data type pair for Table component
/// more details: <see cref="TableMetadataForAttribute"/>
/// </summary>
public static class TableMetadataTypeService
{
    private static ConcurrentDictionary<Type, Type> _metadataTypeCache { get; } = new();

    /// <summary>
    /// register metadata type for target model/data type
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
    /// register metadata type for target model/data type
    /// </summary>
    /// <param name="metadataType">Table ui metadata type</param>
    /// <param name="targetType">the target model/data type</param>
    public static void RegisterMetadataType(Type metadataType, Type targetType)
    {
        _metadataTypeCache.AddOrUpdate(targetType, metadataType, (to, tn) => tn);
    }

    /// <summary>
    /// get metadata type for target data type. return data type itself if metadata type not registered.
    /// </summary>
    /// <param name="targetType">the target data type</param>
    /// <returns>metadata type</returns>
    public static Type GetMetadataType(Type targetType) => _metadataTypeCache.TryGetValue(targetType, out var type) ? type : targetType;

    /// <summary>
    /// register metadata types from assemblies by using reflection
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
