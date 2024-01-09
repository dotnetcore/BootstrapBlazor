// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Newtonsoft.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// 货物信息实体类
/// </summary>
public class CommodityEntity
{
    /// <summary>
    /// 获得/设置 行号
    /// </summary>
    public string? Row { get; set; }

    /// <summary>
    /// 获得/设置 货物名称内容
    /// </summary>
    [JsonProperty("word")]
    public string? CommodityName { get; set; }
}
