// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
