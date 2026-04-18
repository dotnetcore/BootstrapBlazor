// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">动态对象接口</para>
/// <para lang="en">Dynamic object interface</para>
/// </summary>
public interface IDynamicObject
{
    /// <summary>
    /// <para lang="zh">通过指定属性名获取属性值方法</para>
    /// <para lang="en">Gets the value of a property by its name</para>
    /// </summary>
    /// <param name="propertyName"><para lang="zh">属性名称</para><para lang="en">property name</para></param>
    object? GetValue(string propertyName);

    /// <summary>
    /// <para lang="zh">通过指定属性名设置属性值方法</para>
    /// <para lang="en">Sets the value of a property by its name</para>
    /// </summary>
    /// <param name="propertyName"><para lang="zh">属性名称</para><para lang="en">property name</para></param>
    /// <param name="value"><para lang="zh">属性值</para><para lang="en">property value</para></param>
    void SetValue(string propertyName, object? value);

    /// <summary>
    /// <para lang="zh">获得/设置 数据主键</para>
    /// <para lang="en">Gets or sets the data primary key</para>
    /// </summary>
    Guid DynamicObjectPrimaryKey { get; set; }

    /// <summary>
    /// <para lang="zh">撤销数据更改</para>
    /// <para lang="en">Cancels data changes</para>
    /// </summary>
    void Cancel();

    /// <summary>
    /// <para lang="zh">接受数据更改</para>
    /// <para lang="en">Accepts data changes</para>
    /// </summary>
    void Accept();
}
