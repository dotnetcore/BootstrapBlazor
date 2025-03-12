// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Define a group of UI generation metadata for target data type
/// Usually model types are at different layer to the blazor component UI layer.
/// In this case, use <see cref="TableMetadataForAttribute"/> to define a metadata type for Table component.
/// Then register metadata type with <seealso cref="TableMetadataTypeService"/>
///
/// <example>
///     Example:
///     the Pig data type is usually at biz or data layer
///     <code>
///     public class Pig
///     {
///         public string? Name1 { get; set; }
///
///         public string? Name2 { get; set; }
///     }
///     </code>
///     the PigMetadata can be defined at UI/component layer
///     <code>
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
///     </code>
///     before using the metadata, it needs to register the metadata types.
///     register metadata types in assembly
///     <code>
///     TableMetadataTypeService.RegisterMetadataTypes(typeof(Pig).Assembly);
///     var cols = Utility.GetTableColumns&lt;Pig&gt;().ToList();
///     Assert.Single(cols);
///     </code>
///     or you can register types individually
///     <code>
///     TableMetadataTypeService.RegisterMetadataType(metadataType, dataType);
///     </code>
/// </example>
/// </summary>
/// <remarks>
/// Constructor TableMetadataForAttribute for target data type
/// </remarks>
/// <param name="dataType">The target model/data type</param>
[AttributeUsage(AttributeTargets.Class)]
public class TableMetadataForAttribute(Type dataType) : Attribute
{
    /// <summary>
    /// Gets the target model/data type
    /// </summary>
    public Type DataType => dataType;
}
