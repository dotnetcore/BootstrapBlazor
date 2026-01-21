// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">定位数据类</para>
/// <para lang="en">Geolocation Data Class</para>
/// </summary>
public class GeolocationPosition
{
    /// <summary>
    /// <para lang="zh">获得/设置 纬度</para>
    /// <para lang="en">Gets or sets Latitude</para>
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 经度</para>
    /// <para lang="en">Gets or sets Longitude</para>
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 位置精度</para>
    /// <para lang="en">Gets or sets Accuracy</para>
    /// </summary>
    public decimal Accuracy { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 海拔高度单位米</para>
    /// <para lang="en">Gets or sets Altitude (meters)</para>
    /// </summary>
    public decimal Altitude { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 海拔精度</para>
    /// <para lang="en">Gets or sets Altitude Accuracy</para>
    /// </summary>
    public decimal AltitudeAccuracy { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 方向 从正北开始以度计</para>
    /// <para lang="en">Gets or sets Heading (degrees relative to true north)</para>
    /// </summary>
    public decimal Heading { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 速度 以米/每秒计</para>
    /// <para lang="en">Gets or sets Speed (meters/second)</para>
    /// </summary>
    public decimal Speed { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间戳</para>
    /// <para lang="en">Gets or sets Timestamp</para>
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 时间</para>
    /// <para lang="en">Gets or sets Time</para>
    /// </summary>
    public DateTime LastUpdateTime { get => UnixTimeStampToDateTime(Timestamp); }

    /// <summary>
    /// <para lang="zh">获得/设置 移动距离</para>
    /// <para lang="en">Gets or sets Moving Distance</para>
    /// </summary>
    public decimal CurrentDistance { get; set; } = 0.0M;

    /// <summary>
    /// <para lang="zh">获得/设置 总移动距离</para>
    /// <para lang="en">Gets or sets Total Moving Distance</para>
    /// </summary>
    public decimal TotalDistance { get; set; } = 0.0M;

    /// <summary>
    /// <para lang="zh">获得/设置 最后一次获取到的纬度</para>
    /// <para lang="en">Gets or sets Last Acquired Latitude</para>
    /// </summary>
    public decimal LastLat { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最后一次获取到的经度</para>
    /// <para lang="en">Gets or sets Last Acquired Longitude</para>
    /// </summary>
    public decimal LastLong { get; set; }

    private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
}
