// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;
using System.Text.RegularExpressions;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// InputNumber 组件示例代码
/// </summary>
public sealed partial class InputNumbers
{
    /// <summary>
    /// NullableValue
    /// </summary>
    public int? NullableValue { get; set; } = 5;

    /// <summary>
    /// BindValue
    /// </summary>
    public int BindValue { get; set; } = 5;

    /// <summary>
    /// BindDoubleValue
    /// </summary>
    public double BindDoubleValue { get; set; } = 10;

    /// <summary>
    /// BindFloatValue
    /// </summary>
    public float BindFloatValue { get; set; } = 10;

    /// <summary>
    /// BindDecimalValue
    /// </summary>
    public decimal BindDecimalValue { get; set; } = 10;

    /// <summary>
    /// BindLongValue
    /// </summary>
    public long BindLongValue { get; set; } = 10;

    /// <summary>
    /// BindShortValue
    /// </summary>
    public short BindShortValue { get; set; } = 10;

    private Foo Model { get; set; } = new Foo() { Count = 10 };

    private static string Formatter(double val) => val.ToString("0.0");

    private double? InputNullableValue { get; set; } = 12.01;

    private decimal InputValue { get; set; } = 12.01m;

    private double BindInputNullableValue { get; set; } = 2;

    private int BindInputValue { get; set; } = 2;

    private string Format1(double value)
    {
        double.TryParse("$235,623.235", NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out var number);
        double.TryParse("￥1235,623.235", NumberStyles.Currency, CultureInfo.GetCultureInfo("zh-CN"), out var number1);
        return $"$ {value.ToString("n0")}";
    }

    private string Format2(decimal value)
    {        
        return $"{value.ToString("n2")}%";
    }

    private (bool Success, decimal Value) Parser1(string arg1, CultureInfo info)
    {
        var ret = (true, decimal.Parse(arg1.Replace("%", ""), info));
        return ret;
    }
}
