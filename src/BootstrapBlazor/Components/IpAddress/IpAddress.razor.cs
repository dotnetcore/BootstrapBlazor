// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapInputTextBase 组件</para>
/// <para lang="en">BootstrapInputTextBase component</para>
/// </summary>
public partial class IpAddress
{
    private string? Value1 { get; set; } = "0";

    private string? Value2 { get; set; } = "0";

    private string? Value3 { get; set; } = "0";

    private string? Value4 { get; set; } = "0";

    /// <summary>
    /// <para lang="zh">获得 class 样式集合</para>
    /// <para lang="en">Gets class stylecollection</para>
    /// </summary>
    protected string? ClassName => CssBuilder.Default("bb-ip form-control")
        .AddClass("disabled", IsDisabled)
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var ipSegments = CurrentValueAsString.Split(".", System.StringSplitOptions.RemoveEmptyEntries);
        if (ipSegments.Length == 4)
        {
            Value1 = ipSegments[0].Trim();
            Value2 = ipSegments[1].Trim();
            Value3 = ipSegments[2].Trim();
            Value4 = ipSegments[3].Trim();
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

    /// <summary>
    /// <para lang="zh">更新 值方法供 JS 调用</para>
    /// <para lang="en">更新 值方法供 JS 调用</para>
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <param name="v4"></param>
    [JSInvokable]
    public void TriggerUpdate(int v1, int v2, int v3, int v4)
    {
        Value1 = v1.ToString();
        Value2 = v2.ToString();
        Value3 = v3.ToString();
        Value4 = v4.ToString();

        UpdateValue();
    }
}
