// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Filter 过滤条件项目 属性 <see cref="Filters"/> 为条件表达式，其之间关系由 <see cref="FilterLogic"/> 来决定</para>
/// <para lang="en">Filter Condition Item Property <see cref="Filters"/> is condition expression, relationship determined by <see cref="FilterLogic"/></para>
/// </summary>
[JsonConverter(typeof(JsonFilterKeyValueActionConverter))]
public class FilterKeyValueAction
{
    /// <summary>
    /// <para lang="zh">获得/设置 Filter 项字段名称</para>
    /// <para lang="en">Gets or sets Filter Item Field Name</para>
    /// </summary>
    public string? FieldKey { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Filter 项字段值</para>
    /// <para lang="en">Gets or sets Filter Item Field Value</para>
    /// </summary>
    public object? FieldValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Filter 项与其他 Filter 逻辑关系</para>
    /// <para lang="en">Gets or sets Logical Relationship between Filter Item and other Filters</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterLogic FilterLogic { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Filter 条件行为</para>
    /// <para lang="en">Gets or sets Filter Condition Behavior</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterAction FilterAction { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子过滤条件集合</para>
    /// <para lang="en">Gets or sets Child Filter Condition Collection</para>
    /// </summary>
    public List<FilterKeyValueAction> Filters { get; set; } = [];
}
