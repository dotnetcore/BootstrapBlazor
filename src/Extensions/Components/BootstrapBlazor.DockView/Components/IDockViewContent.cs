// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// IDockViewContent 接口
/// </summary>
public interface IDockViewContent : IDockViewComponentBase
{
    /// <summary>
    /// 获得/设置 子项集合
    /// </summary>
    [JsonConverter(typeof(DockViewComponentConverter))]
    [JsonPropertyName("content")]
    List<IDockViewComponentBase> Items { get; }

    /// <summary>
    /// 获得/设置 组件宽度百分比 默认 null 未设置
    /// </summary>
    int? Width { get; set; }

    /// <summary>
    /// 获得/设置 组件高度百分比 默认 null 未设置
    /// </summary>
    int? Height { get; set; }
}
