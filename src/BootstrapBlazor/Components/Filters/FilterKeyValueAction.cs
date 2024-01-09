// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Filter 过滤条件项目 属性 <see cref="Filters"/> 为条件表达式，其之间关系由 <see cref="FilterLogic"/> 来决定
/// </summary>
public class FilterKeyValueAction
{
    /// <summary>
    /// 获得/设置 Filter 项字段名称
    /// </summary>
    public string? FieldKey { get; set; }

    /// <summary>
    /// 获得/设置 Filter 项字段值
    /// </summary>
    public object? FieldValue { get; set; }

    /// <summary>
    /// 获得/设置 Filter 项与其他 Filter 逻辑关系
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterLogic FilterLogic { get; set; }

    /// <summary>
    /// 获得/设置 Filter 条件行为
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterAction FilterAction { get; set; }

    /// <summary>
    /// 获得/设置 子过滤条件集合
    /// </summary>
    public List<FilterKeyValueAction>? Filters { get; set; }
}
