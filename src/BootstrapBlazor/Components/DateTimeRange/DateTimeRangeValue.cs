// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimeRangeValue 对象
/// </summary>
public class DateTimeRangeValue
{
    /// <summary>
    /// 获得/设置 开始时间
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// 获得/设置 结束时间
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// 获得/设置 可为空开始时间
    /// </summary>
    public DateTime? NullStart
    {
        get => Start == DateTime.MinValue ? null : Start;
        set => Start = value ?? DateTime.MinValue;
    }

    /// <summary>
    /// 获得/设置 可为空结束时间
    /// </summary>
    public DateTime? NullEnd
    {
        get => End == DateTime.MinValue ? null : End;
        set => End = value ?? DateTime.MinValue;
    }

    /// <summary>
    /// ToString 方法
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
