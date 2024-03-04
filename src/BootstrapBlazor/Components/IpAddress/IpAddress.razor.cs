// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInputTextBase 组件
/// </summary>
public partial class IpAddress
{
    private string? Value1 { get; set; } = "0";

    private string? Value2 { get; set; } = "0";

    private string? Value3 { get; set; } = "0";

    private string? Value4 { get; set; } = "0";

    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    protected string? ClassName => CssBuilder.Default("ipaddress form-control")
        .AddClass("disabled", IsDisabled)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var ipSegments = CurrentValueAsString.Split(".", System.StringSplitOptions.RemoveEmptyEntries);
        if (ipSegments.Length == 4)
        {
            Value1 = ipSegments[0];
            Value2 = ipSegments[1];
            Value3 = ipSegments[2];
            Value4 = ipSegments[3];
        }
        else
        {
            Value1 = "0";
            Value2 = "0";
            Value3 = "0";
            Value4 = "0";
        }
        UpdateValue();
    }

    private void ValueChanged1(ChangeEventArgs args)
    {
        Value1 = args.Value?.ToString();
        if (string.IsNullOrEmpty(Value1))
        {
            Value1 = "0";
        }
        if (Value1.Length > 3)
        {
            Value1 = Value1[0..3];
        }

        UpdateValue();
    }

    private void ValueChanged2(ChangeEventArgs args)
    {
        Value2 = args.Value?.ToString();
        if (string.IsNullOrEmpty(Value2))
        {
            Value2 = "0";
        }
        if (Value2.Length > 3)
        {
            Value2 = Value2[0..3];
        }
        UpdateValue();
    }

    private void ValueChanged3(ChangeEventArgs args)
    {
        Value3 = args.Value?.ToString();
        if (string.IsNullOrEmpty(Value3))
        {
            Value3 = "0";
        }
        if (Value3.Length > 3)
        {
            Value3 = Value3[0..3];
        }
        UpdateValue();
    }

    private void ValueChanged4(ChangeEventArgs args)
    {
        Value4 = args.Value?.ToString();
        if (string.IsNullOrEmpty(Value4))
        {
            Value4 = "0";
        }
        if (Value4.Length > 3)
        {
            Value4 = Value4[0..3];
        }
        UpdateValue();
    }

    private void UpdateValue()
    {
        CurrentValueAsString = $"{Value1}.{Value2}.{Value3}.{Value4}";
    }
}
