// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Lookup 接口定义
/// </summary>
public interface ILookup
{
    /// <summary>
    /// 获得/设置 数据集用于 CheckboxList Select 组件 通过 Value 显示 Text 使用 默认 null
    /// <para>设置 <see cref="Lookup"/> 参收后，<see cref="LookupServiceKey"/> 和 <see cref="LookupServiceData"/> 两个参数均失效</para>
    /// </summary>
    IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 <see cref="StringComparison.OrdinalIgnoreCase" /> 大小写不敏感 
    /// </summary>
    StringComparison LookupStringComparison { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作，可以通过 <see cref="LookupServiceData"/> 传递自定义数据
    /// <para>未设置 <see cref="Lookup"/> 时生效</para>
    /// </summary>
    string? LookupServiceKey { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值自定义数据，通过 <see cref="LookupServiceKey"/> 指定键值
    /// <para>未设置 <see cref="Lookup"/> 时生效</para>
    /// </summary>
    object? LookupServiceData { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务实例
    /// </summary>
    ILookupService? LookupService { get; set; }
}
