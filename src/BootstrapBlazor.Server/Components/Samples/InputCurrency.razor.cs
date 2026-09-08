// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Server.Components.Samples;

public partial class InputCurrency
{
    /// <summary>
    /// BindValue
    /// </summary>
    public double BindValue { get; set; } = 5;

    /// <summary>
    /// 
    /// </summary>
    public decimal BindDecimalValue { get; set; } = 1898.78m;

    /// <summary>
    /// 
    /// </summary>
    public decimal BindPercentValue { get; set; } = 35.356m;

    /// <summary>
    /// 
    /// </summary>
    public double BindMaxMinValue { get; set; } = 9.23;

    /// <summary>
    /// 
    /// </summary>
    public double? NullableValue { get; set; } = 5;

    private string Format1(double value)
    {
        double.TryParse("$235,623.235", NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out var number);
        double.TryParse("¥1235,623.235", NumberStyles.Currency, CultureInfo.GetCultureInfo("zh-CN"), out var number1);
        return $"$ {value.ToString("n0")}";
    }
    private string Format(double? value)
    {
        if (value != null)
            return $"$ {value?.ToString("n0")}";
        else
            return null;
    }

    private string FormatCustomize(decimal value)
    {
        return $"RMB {value.ToString("n2")}";
    }

    private (bool Success, decimal Value) Parser(string arg1, CultureInfo info)
    {
        var ret = (true, decimal.Parse(arg1.Replace("%", ""), info));
        return ret;
    }
}
