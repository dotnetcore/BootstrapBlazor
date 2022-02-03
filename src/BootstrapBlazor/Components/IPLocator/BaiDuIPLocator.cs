// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
///
/// </summary>
public class BaiDuIPLocator : DefaultIPLocator
{
    /// <summary>
    /// 
    /// </summary>
    public BaiDuIPLocator()
    {
        Url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={0}";
    }

    /// <summary>
    /// 详细地址信息
    /// </summary>
    public IEnumerable<LocationInfo>? Data { get; set; }

    /// <summary>
    /// 结果状态返回码
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public override Task<string?> Locate(IPLocatorOption option) => Locate<BaiDuIPLocator>(option);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Status == "0" ? (Data?.FirstOrDefault()?.Location ?? "XX XX") : "Error";
    }
}

/// <summary>
/// 
/// </summary>
public class LocationInfo
{
    /// <summary>
    /// 
    /// </summary>
    public string? Location { get; set; }
}
