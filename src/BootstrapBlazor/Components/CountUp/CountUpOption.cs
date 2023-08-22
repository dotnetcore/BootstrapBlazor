// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// CountUp 配置类
/// </summary>
public struct CountUpOption
{
    /// <summary>
    /// 构造方法
    /// </summary>
    public CountUpOption()
    {

    }

    /// <summary>
    /// 开始计数值
    /// </summary>
    public decimal StartValue { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalPlaces { get; set; }

    /// <summary>
    /// 动画时长 默认 2.0 单位秒
    /// </summary>
    public float Duration { get; set; } = 2.0f;

    /// <summary>
    /// 是否使用分隔符 默认 true
    /// </summary>
    public bool UseIndianSeparators { get; set; } = true;

    /// <summary>
    /// 是否使用过渡动画 默认 true
    /// </summary>
    public bool UseEasing { get; set; } = true;

    /// <summary>
    /// 是否分组 默认 true
    /// </summary>
    public bool UseGrouping { get; set; } = true;

    /// <summary>
    /// 分隔符 默认 逗号 ,
    /// </summary>
    public string Separator { get; set; } = ",";

    /// <summary>
    /// 小数点符号 默认 点 .
    /// </summary>
    public string Decimal { get; set; } = ".";

    /// <summary>
    /// 前缀文本 默认 null 未设置
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// 后缀文本 默认 null 未设置
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// 动画阈值 默认 999
    /// </summary>
    public int SmartEasingThreshold { get; set; } = 999;

    /// <summary>
    /// 动画总量 默认 333
    /// </summary>
    public int SmartEasingAmount { get; set; } = 333;

    /// <summary>
    /// 是否启用滚动监听 默认 false
    /// </summary>
    public bool EnableScrollSpy { get; set; }

    /// <summary>
    /// 滚动延时 默认 200 毫秒
    /// </summary>
    public int ScrollSpyDelay { get; set; } = 200;

    /// <summary>
    /// 滚动监听一次 默认 false
    /// </summary>
    public bool ScrollSpyOnce { get; set; }
}
