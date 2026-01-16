// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">CountUp 配置类</para>
/// <para lang="en">CountUp configuration class</para>
/// </summary>
public class CountUpOption
{
    /// <summary>
    /// <para lang="zh">开始计数值 默认 0</para>
    /// <para lang="en">Start value, default is 0</para>
    /// </summary>
    [JsonPropertyName("startVal")]
    public decimal StartValue { get; set; }

    /// <summary>
    /// <para lang="zh">小数位数 默认 0</para>
    /// <para lang="en">Decimal places, default is 0</para>
    /// </summary>
    public int DecimalPlaces { get; set; }

    /// <summary>
    /// <para lang="zh">动画时长 默认 2.0 单位秒</para>
    /// <para lang="en">Animation duration, default is 2.0 seconds</para>
    /// </summary>
    public float Duration { get; set; } = 2.0f;

    /// <summary>
    /// <para lang="zh">是否使用分隔符 默认 false</para>
    /// <para lang="en">Whether to use separator, default is false</para>
    /// </summary>
    public bool UseIndianSeparators { get; set; }

    /// <summary>
    /// <para lang="zh">是否使用过渡动画 默认 true</para>
    /// <para lang="en">Whether to use easing animation, default is true</para>
    /// </summary>
    public bool UseEasing { get; set; } = true;

    /// <summary>
    /// <para lang="zh">是否分组 默认 true</para>
    /// <para lang="en">Whether to use grouping, default is true</para>
    /// </summary>
    public bool UseGrouping { get; set; } = true;

    /// <summary>
    /// <para lang="zh">分隔符 默认 逗号 ,</para>
    /// <para lang="en">Separator, default is comma ,</para>
    /// </summary>
    public string Separator { get; set; } = ",";

    /// <summary>
    /// <para lang="zh">小数点符号 默认 点 .</para>
    /// <para lang="en">Decimal point symbol, default is dot .</para>
    /// </summary>
    public string Decimal { get; set; } = ".";

    /// <summary>
    /// <para lang="zh">前缀文本 默认 string.Empty 未设置</para>
    /// <para lang="en">Prefix text, default is string.Empty (not set)</para>
    /// </summary>
    public string Prefix { get; set; } = string.Empty;

    /// <summary>
    /// <para lang="zh">后缀文本 默认 string.Empty 未设置</para>
    /// <para lang="en">Suffix text, default is string.Empty (not set)</para>
    /// </summary>
    public string Suffix { get; set; } = string.Empty;

    /// <summary>
    /// <para lang="zh">动画阈值 默认 999</para>
    /// <para lang="en">Animation threshold, default is 999</para>
    /// </summary>
    public int SmartEasingThreshold { get; set; } = 999;

    /// <summary>
    /// <para lang="zh">动画总量 默认 333</para>
    /// <para lang="en">Animation amount, default is 333</para>
    /// </summary>
    public int SmartEasingAmount { get; set; } = 333;

    /// <summary>
    /// <para lang="zh">是否启用滚动监听 默认 false</para>
    /// <para lang="en">Whether to enable scroll spy, default is false</para>
    /// </summary>
    public bool EnableScrollSpy { get; set; }

    /// <summary>
    /// <para lang="zh">滚动延时 默认 200 毫秒</para>
    /// <para lang="en">Scroll spy delay, default is 200 ms</para>
    /// </summary>
    public int ScrollSpyDelay { get; set; } = 200;

    /// <summary>
    /// <para lang="zh">滚动监听一次 默认 false</para>
    /// <para lang="en">Scroll spy once, default is false</para>
    /// </summary>
    public bool ScrollSpyOnce { get; set; }

    /// <summary>
    /// numeral glyph substitution default null
    /// </summary>
    public char[]? Numerals { get; set; }
}
