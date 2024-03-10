// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BaiduIPLocatorV2 第二个版本实现类
/// </summary>
public class BaiduIPLocatorV2 : DefaultIPLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BaiduIPLocatorV2()
    {
        Url = "https://qifu-api.baidubce.com/ip/geo/v1/district?ip={0}";
    }

    /// <summary>
    /// 定位方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public override Task<string?> Locate(IPLocatorOption option) => Locate<BaiduIPLocatorV2Data>(option);
}

class BaiduIPLocatorV2Data
{
    public string? Code { get; set; }

    [NotNull]
    public BaiduIPLocatorV2DataDetail? Data { get; set; }
    public bool Charge { get; set; }
    public string? Msg { get; set; }
    public string? Ip { get; set; }
    public string? CoordSys { get; set; }

    public override string ToString()
    {
        return Code == "Success" ? $"{Data.Continent} {Data.Country} {Data.Prov} {Data.City} {Data.District} {Data.Isp}" : "XX XX";
    }
}

class BaiduIPLocatorV2DataDetail
{
    public string? Continent { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? TimeZone { get; set; }
    public string? Accuracy { get; set; }
    public string? Owner { get; set; }
    public string? Isp { get; set; }
    public string? Source { get; set; }
    public string? AreaCode { get; set; }
    public string? AdCode { get; set; }
    public string? AsNumber { get; set; }
    public string? Lat { get; set; }
    public string? Lng { get; set; }
    public string? Radius { get; set; }
    public string? Prov { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
}

//{
//    "code": "Success",
//    "data": {
//        "continent": "亚洲",
//        "country": "新加坡",
//        "zipcode": "04",
//        "timezone": "UTC+8",
//        "accuracy": "城市",
//        "owner": "微软公司",
//        "isp": "微软公司",
//        "source": "数据挖掘",
//        "areacode": "SG",
//        "adcode": "",
//        "asnumber": "8075",
//        "lat": "1.286529",
//        "lng": "103.853519",
//        "radius": "",
//        "prov": "新加坡",
//        "city": "新加坡",
//        "district": ""
//    },
//    "charge": false,
//    "msg": "查询成功",
//    "ip": "20.205.243.16",
//    "coordsys": "WGS84"
//}
