// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Lookup 接口定义</para>
/// <para lang="en">Lookup interface definition</para>
/// </summary>
public interface ILookup
{
    /// <summary>
    /// <para lang="zh">获得/设置 数据集用于 CheckboxList Select 组件 通过 Value 显示 Text 使用 默认 null</para>
    /// <para lang="en">Gets or sets data set for CheckboxList Select component display Text via Value default null</para>
    /// <para lang="zh">设置 <see cref="Lookup"/> 参收后，<see cref="LookupServiceKey"/> 和 <see cref="LookupServiceData"/> 两个参数均失效</para>
    /// <para lang="en">After setting <see cref="Lookup"/> parameter, <see cref="LookupServiceKey"/> and <see cref="LookupServiceData"/> parameters are invalid</para>
    /// </summary>
    IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 字典数据源字符串比较规则 默认 <see cref="StringComparison.OrdinalIgnoreCase" /> 大小写不敏感</para>
    /// <para lang="en">Gets or sets dictionary data source string comparison rule default <see cref="StringComparison.OrdinalIgnoreCase" /> case insensitive</para>
    /// </summary>
    StringComparison LookupStringComparison { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作，可以通过 <see cref="LookupServiceData"/> 传递自定义数据</para>
    /// <para lang="en">Gets or sets <see cref="ILookupService"/> service get Lookup data collection key value often used for foreign key automatic conversion to name operation, can pass custom data through <see cref="LookupServiceData"/></para>
    /// <para lang="zh">未设置 <see cref="Lookup"/> 时生效</para>
    /// <para lang="en">Effective when <see cref="Lookup"/> is not set</para>
    /// </summary>
    string? LookupServiceKey { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值自定义数据，通过 <see cref="LookupServiceKey"/> 指定键值</para>
    /// <para lang="en">Gets or sets <see cref="ILookupService"/> service get Lookup data collection key value custom data, specify key value through <see cref="LookupServiceKey"/></para>
    /// <para lang="zh">未设置 <see cref="Lookup"/> 时生效</para>
    /// <para lang="en">Effective when <see cref="Lookup"/> is not set</para>
    /// </summary>
    object? LookupServiceData { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 <see cref="ILookupService"/> 服务实例</para>
    /// <para lang="en">Gets or sets <see cref="ILookupService"/> service instance</para>
    /// </summary>
    ILookupService? LookupService { get; set; }
}
