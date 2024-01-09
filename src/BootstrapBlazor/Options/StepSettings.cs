// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// StepSettings 配置类
/// </summary>
public class StepSettings
{
    /// <summary>
    /// 获得/设置 int 数据类型步长 默认 null 未设置
    /// </summary>
    public int? Int { get; set; }

    /// <summary>
    /// 获得/设置 long 数据类型步长 默认 null 未设置
    /// </summary>
    public int? Long { get; set; }

    /// <summary>
    /// 获得/设置 short 数据类型步长 默认 null 未设置
    /// </summary>
    public int? Short { get; set; }

    /// <summary>
    /// 获得/设置 float 数据类型步长 默认 null 未设置
    /// </summary>
    public float? Float { get; set; }

    /// <summary>
    /// 获得/设置 double 数据类型步长 默认 null 未设置
    /// </summary>
    public double? Double { get; set; }

    /// <summary>
    /// 获得/设置 decimal 数据类型步长 默认 null 未设置
    /// </summary>
    public decimal? Decimal { get; set; }

    /// <summary>
    /// 获得步长字符串
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string? GetStep(Type type)
    {
        string? ret = null;
        if (type == typeof(int))
        {
            ret = Int?.ToString();
        }
        if (type == typeof(long))
        {
            ret = Long?.ToString();
        }
        if (type == typeof(short))
        {
            ret = Short?.ToString();
        }
        if (type == typeof(float))
        {
            ret = Float?.ToString();
        }
        if (type == typeof(double))
        {
            ret = Double?.ToString();
        }
        if (type == typeof(decimal))
        {
            ret = Decimal?.ToString();
        }
        return ret;
    }
}
