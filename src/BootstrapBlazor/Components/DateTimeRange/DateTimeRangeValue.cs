// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DateTimeRangeValue 对象</para>
/// <para lang="en">DateTimeRangeValue Object</para>
/// </summary>
public class DateTimeRangeValue
{
    /// <summary>
    /// <para lang="zh">获得/设置 开始时间</para>
    /// <para lang="en">Get/Set Start Time</para>
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 结束时间</para>
    /// <para lang="en">Get/Set End Time</para>
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 可为空开始时间</para>
    /// <para lang="en">Get/Set Nullable Start Time</para>
    /// </summary>
    public DateTime? NullStart
    {
        get => Start == DateTime.MinValue ? null : Start;
        set => Start = value ?? DateTime.MinValue;
    }

    /// <summary>
    /// <para lang="zh">获得/设置 可为空结束时间</para>
    /// <para lang="en">Get/Set Nullable End Time</para>
    /// </summary>
    public DateTime? NullEnd
    {
        get => End == DateTime.MinValue ? null : End;
        set => End = value ?? DateTime.MinValue;
    }

    /// <summary>
    /// <para lang="zh">ToString 方法</para>
    /// <para lang="en">ToString Method</para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var ret = "";
        if (Start != DateTime.MinValue)
        {
            ret = Start.ToString();
        }
        if (End != DateTime.MinValue)
        {
            ret = $"{ret} - {End}";
        }
        return ret;
    }
}
