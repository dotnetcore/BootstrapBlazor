// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 百度搜索引擎 IP 定位器
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
public class BaiDuIPLocator : DefaultIPLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BaiDuIPLocator()
    {
        Url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={0}";
    }

    /// <summary>
    /// 获得/设置 详细地址信息
    /// </summary>
    public IEnumerable<LocationInfo>? Data { get; set; }

    /// <summary>
    /// 获得/设置 结果状态返回码 为 0 时通讯正常
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 定位方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public override Task<string?> Locate(IPLocatorOption option) => Locate<BaiDuIPLocator>(option);

    /// <summary>
    /// ToString 方法
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Status == "0" ? (Data?.FirstOrDefault().Location ?? "XX XX") : "Error";
    }
}

/// <summary>
/// LocationInfo 结构体
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
public struct LocationInfo
{
    /// <summary>
    /// 获得/设置 定位信息
    /// </summary>
    public string? Location { get; set; }
}
