// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态对象接口
/// </summary>
public interface IDynamicObject
{
    /// <summary>
    /// 通过指定属性名获取属性值方法
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    /// <returns></returns>
    object? GetValue(string propertyName);

    /// <summary>
    /// 通过指定属性名设置属性值方法
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    /// <param name="value">属性值</param>
    void SetValue(string propertyName, object? value);

    /// <summary>
    /// 获得/设置 数据主键
    /// </summary>
    Guid DynamicObjectPrimaryKey { get; set; }
}
