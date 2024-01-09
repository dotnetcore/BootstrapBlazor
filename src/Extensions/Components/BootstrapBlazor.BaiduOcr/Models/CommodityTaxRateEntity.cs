// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Newtonsoft.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// 税率信息实体类
/// </summary>
public class CommodityTaxRateEntity
{
    /// <summary>
    /// 获得/设置 行号
    /// </summary>
    public string? Row { get; set; }

    /// <summary>
    /// 获得/设置 税率内容
    /// </summary>
    [JsonProperty("word")]
    public string? CommodityTaxRate { get; set; }
}
