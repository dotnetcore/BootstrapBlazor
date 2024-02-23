// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics;

namespace BootstrapBlazor.Components;

/// <summary>
/// Define a group of UI generation metadata for target data type <br/>
/// Usually model types are at different layer to the blazor component UI layer. <br/>
/// In this case, use <see cref="TableMetadataForAttribute"/> to define a metadata type for Table component. <br/>
/// Then register metadata type with <seealso cref="TableMetadataTypeService"/> <br/>
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
///         [AutoGenerateColumn(Align = Alignment.Center, Order = -2)]
///         public string? Name2 { get; set; }
///     }
///     </code>
///     before using the metadata, it needs to register the metadata types. <br/>
///     register metadata types in assembly
///     <code>
///     TableMetadataTypeService.RegisterMetadataTypes(typeof(Pig).Assembly);
///     var cols = Utility.GetTableColumns&lt;Pig&gt;().ToList();
///     Assert.Single(cols);
///     </code>
///     or you can register types individually
///     <code>
///     TableMetadataTypeService.RegisterMatadataType(metadataType, dataType);
///     </code>
/// </example>
/// </summary>
[DebuggerStepThrough()]
[AttributeUsage(AttributeTargets.Class)]
public class TableMetadataForAttribute : Attribute
{
    /// <summary>
    /// Constructor TableMetadataForAttribute for target data type
    /// </summary>
    /// <param name="dataType">The target model/data type</param>
    public TableMetadataForAttribute(Type dataType)
    {
        DataType = dataType;
    }
    /// <summary>
    /// The target model/data type
    /// </summary>
    public Type DataType { get; private set; }
}
