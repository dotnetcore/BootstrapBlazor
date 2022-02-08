// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 定位数据类
/// </summary>
public class GeolocationItem
{
    /// <summary>
    /// 获得/设置 纬度
    /// </summary>
    /// <returns></returns>
    public decimal Latitude { get; set; }

    /// <summary>
    /// 获得/设置 经度
    /// </summary>
    /// <returns></returns>
    public decimal Longitude { get; set; }

    /// <summary>
    /// 获得/设置 位置精度
    /// </summary>
    public decimal Accuracy { get; set; }

    /// <summary>
    /// 获得/设置 海拔高度单位米
    /// </summary>
    public decimal Altitude { get; set; }

    /// <summary>
    /// 获得/设置 海拔精度
    /// </summary>
    public decimal AltitudeAccuracy { get; set; }

    /// <summary>
    /// 获得/设置 方向 从正北开始以度计
    /// </summary>
    public decimal Heading { get; set; }

    /// <summary>
    /// 获得/设置 速度 以米/每秒计
    /// </summary>
    public decimal Speed { get; set; }

    /// <summary>
    /// 获得/设置 时间戳
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// 获得/设置 时间
    /// </summary>
    public DateTime LastUpdateTime { get => UnixTimeStampToDateTime(Timestamp); }

    /// <summary>
    /// 获得/设置 移动距离
    /// </summary>
    public decimal CurrentDistance { get; set; } = 0.0M;

    /// <summary>
    /// 获得/设置 总移动距离
    /// </summary>
    public decimal TotalDistance { get; set; } = 0.0M;

    /// <summary>
    /// 获得/设置 最后一次获取到的纬度
    /// </summary>
    public decimal LastLat { get; set; }

    /// <summary>
    /// 获得/设置 最后一次获取到的经度
    /// </summary>
    public decimal LastLong { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="unixTimeStamp"></param>
    /// <returns></returns>
    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
}
