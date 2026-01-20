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
    /// <para lang="en">Start value, Default is 0</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [JsonPropertyName("startVal")]
    public decimal StartValue { get; set; }

    /// <summary>
    /// <para lang="zh">小数位数 默认 0</para>
    /// <para lang="en">Decimal places, Default is 0</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public int DecimalPlaces { get; set; }

    /// <summary>
    /// <para lang="zh">动画时长 默认 2.0 单位秒</para>
    /// <para lang="en">Animation duration, Default is 2.0 seconds</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public float Duration { get; set; } = 2.0f;

    /// <summary>
    /// <para lang="zh">是否使用分隔符 默认 false</para>
    /// <para lang="en">Whether to use separator, Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public bool UseIndianSeparators { get; set; }

    /// <summary>
    /// <para lang="zh">是否使用过渡动画 默认 true</para>
    /// <para lang="en">Whether to use easing animation, Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public bool UseEasing { get; set; } = true;

    /// <summary>
    /// <para lang="zh">是否分组 默认 true</para>
    /// <para lang="en">Whether to use grouping, Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public bool UseGrouping { get; set; } = true;

    /// <summary>
    /// <para lang="zh">分隔符 默认 逗号 ,</para>
    /// <para lang="en">Separator, Default is comma ,</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public string Separator { get; set; } = ",";

    /// <summary>
    /// <para lang="zh">小数点符号 默认 点 .</para>
    /// <para lang="en">Decimal point symbol, Default is dot .</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public string Decimal { get; set; } = ".";

    /// <summary>
    /// <para lang="zh">前缀文本 默认 string.Empty 未设置</para>
    /// <para lang="en">Prefix text, Default is <see cref="string.Empty"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public string Prefix { get; set; } = string.Empty;

    /// <summary>
    /// <para lang="zh">后缀文本 默认 string.Empty 未设置</para>
    /// <para lang="en">Suffix text, Default is <see cref="string.Empty"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public string Suffix { get; set; } = string.Empty;

    /// <summary>
    /// <para lang="zh">动画阈值 默认 999</para>
    /// <para lang="en">Animation threshold, Default is 999</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public int SmartEasingThreshold { get; set; } = 999;

    /// <summary>
    /// <para lang="zh">动画总量 默认 333</para>
    /// <para lang="en">Animation amount, Default is 333</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public int SmartEasingAmount { get; set; } = 333;

    /// <summary>
    /// <para lang="zh">是否启用滚动监听 默认 false</para>
    /// <para lang="en">Whether to enable scroll spy, Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public bool EnableScrollSpy { get; set; }

    /// <summary>
    /// <para lang="zh">滚动延时 默认 200 毫秒</para>
    /// <para lang="en">Scroll spy delay, Default is 200 ms</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public int ScrollSpyDelay { get; set; } = 200;

    /// <summary>
    /// <para lang="zh">滚动监听一次 默认 false</para>
    /// <para lang="en">Scroll spy once, Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public bool ScrollSpyOnce { get; set; }

    /// <summary>
    /// <para lang="zh">数字字形替换数组 默认值为空</para>
    /// <para lang="en">The numeric glyph replacement array. Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    public char[]? Numerals { get; set; }
}
