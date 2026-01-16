// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">定义目标数据类型的 UI 生成元数据组</para>
///  <para lang="zh">通常模型类型位于与 Blazor 组件 UI 层不同的层。</para>
///  <para lang="zh">在这种情况下，使用 <see cref="TableMetadataForAttribute"/> 为 Table 组件定义元数据类型。</para>
///  <para lang="zh">然后使用 <seealso cref="TableMetadataTypeService"/> 注册元数据类型</para>
///  <para lang="en">Define a group of UI generation metadata for target data type</para>
///  <para lang="en">Usually model types are at different layer to the blazor component UI layer.</para>
///  <para lang="en">In this case, use <see cref="TableMetadataForAttribute"/> to define a metadata type for Table component.</para>
///  <para lang="en">Then register metadata type with <seealso cref="TableMetadataTypeService"/></para>
///
/// <example>
///  <para lang="zh">示例：</para>
///  <para lang="zh">Pig 数据类型通常位于业务层或数据层</para>
///  <para lang="en">Example:</para>
///  <para lang="en">the Pig data type is usually at biz or data layer</para>
///     <code>
///     public class Pig
///     {
///         public string? Name1 { get; set; }
///
///         public string? Name2 { get; set; }
///     }
///     </code>
///  <para lang="zh">PigMetadata 可以定义在 UI/组件层</para>
///  <para lang="en">the PigMetadata can be defined at UI/component layer</para>
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
///  <para lang="zh">在使用元数据之前，需要注册元数据类型。</para>
///  <para lang="zh">在程序集中注册元数据类型</para>
///  <para lang="en">before using the metadata, it needs to register the metadata types.</para>
///  <para lang="en">register metadata types in assembly</para>
///     <code>
///     TableMetadataTypeService.RegisterMetadataTypes(typeof(Pig).Assembly);
///     var cols = Utility.GetTableColumns&lt;Pig&gt;().ToList();
///     Assert.Single(cols);
///     </code>
///  <para lang="zh">或者您可以单独注册类型</para>
///  <para lang="en">or you can register types individually</para>
///     <code>
///     TableMetadataTypeService.RegisterMetadataType(metadataType, dataType);
///     </code>
/// </example>
/// </summary>
/// <remarks>
///  <para lang="zh">TableMetadataForAttribute 构造函数</para>
///  <para lang="en">Constructor TableMetadataForAttribute for target data type</para>
/// </remarks>
/// <param name="dataType"><para lang="zh">目标模型/数据类型</para><para lang="en">The target model/data type</para></param>
[AttributeUsage(AttributeTargets.Class)]
public class TableMetadataForAttribute(Type dataType) : Attribute
{
    /// <summary>
    ///  <para lang="zh">获得 目标模型/数据类型</para>
    ///  <para lang="en">Gets the target model/data type</para>
    /// </summary>
    public Type DataType => dataType;
}
