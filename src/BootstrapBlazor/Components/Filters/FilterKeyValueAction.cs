// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Filter 过滤条件项目 属性 <see cref="Filters"/> 为条件表达式，其之间关系由 <see cref="FilterLogic"/> 来决定
/// </summary>
[JsonConverter(typeof(JsonFilterKeyValueActionConverter))]
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
    public List<FilterKeyValueAction> Filters { get; set; } = [];
}
