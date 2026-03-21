// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">定义目标数据类型的 UI 生成元数据组，通常模型类型位于与 Blazor 组件 UI 层不同的层，在这种情况下，使用 <see cref="TableMetadataForAttribute"/> 为 Table 组件定义元数据类型，然后使用 <seealso cref="TableMetadataTypeService"/> 注册元数据类型</para>
/// <para lang="en">Defines a group of UI generation metadata for the target data type. Usually, model types are located in a different layer than the Blazor component UI layer. In this case, use <see cref="TableMetadataForAttribute"/> to define a metadata type for the Table component, and then use <seealso cref="TableMetadataTypeService"/> to register the metadata type.</para>
/// <example>
/// <para>示例：Pig 数据类型通常位于业务层或数据层; Example: the Pig data type is usually at biz or data layer</para>
/// <code>
///     public class Pig
///     {
///         public string? Name1 { get; set; }
///
///         public string? Name2 { get; set; }
///     }
///     
///     [TableMetadataFor(typeof(Pig))]
///     [AutoGenerateClass(Align = Alignment.Center)]
///     public class PigMetadata
///     {
///         [AutoGenerateColumn(Ignore = true)]
///         public string? Name1 { get; set; }
///         
///         [AutoGenerateColumn(Align = Alignment.Center, Order = -2)]
///         public string? Name2 { get; set; }
///     }
/// </code>
/// <para>register metadata types in assembly</para>
/// <code>
///     TableMetadataTypeService.RegisterMetadataTypes(typeof(Pig).Assembly);
///     // or
///     TableMetadataTypeService.RegisterMetadataType(metadataType, dataType);
/// </code>
/// </example>
/// </summary>
/// <param name="dataType">
///   <para lang="zh">目标模型/数据类型</para>
///   <para lang="en">The target model/data type</para>
/// </param>
[AttributeUsage(AttributeTargets.Class)]
public class TableMetadataForAttribute(Type dataType) : Attribute
{
    /// <summary>
    /// <para lang="zh">获得 目标模型/数据类型</para>
    /// <para lang="en">Gets the target model/data type</para>
    /// </summary>
    public Type DataType => dataType;
}
