// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">百度搜索引擎 IP 定位器</para>
/// <para lang="en">Baidu Search Engine IP Locator</para>
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
public class BaiDuIPLocator : DefaultIPLocator
{
    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public BaiDuIPLocator()
    {
        Url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={0}";
    }

    /// <summary>
    /// <para lang="zh">获得/设置 详细地址信息</para>
    /// <para lang="en">Gets or sets Detailed Address Information</para>
    /// </summary>
    public IEnumerable<LocationInfo>? Data { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 结果状态返回码 为 0 时通讯正常</para>
    /// <para lang="en">Gets or sets Result Status Return Code. 0 means communication is normal</para>
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// <para lang="zh">定位方法</para>
    /// <para lang="en">Locate Method</para>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public override Task<string?> Locate(IPLocatorOption option) => Locate<BaiDuIPLocator>(option);

    /// <summary>
    /// <para lang="zh">ToString 方法</para>
    /// <para lang="en">ToString Method</para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Status == "0" ? (Data?.FirstOrDefault().Location ?? "XX XX") : "Error";
    }
}

/// <summary>
/// <para lang="zh">LocationInfo 结构体</para>
/// <para lang="en">LocationInfo Struct</para>
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
public struct LocationInfo
{
    /// <summary>
    /// <para lang="zh">获得/设置 定位信息</para>
    /// <para lang="en">Gets or sets Location Information</para>
    /// </summary>
    public string? Location { get; set; }
}
