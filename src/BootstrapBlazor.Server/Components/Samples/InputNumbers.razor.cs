﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

    private double InputValue { get; set; } = 12.01;

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Value",
            Description = Localizer["InputNumbersAtt1"],
            Type = "sbyte|byte|int|long|short|float|double|decimal",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "Max",
            Description = Localizer["InputNumbersAtt2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Min",
            Description = Localizer["InputNumbersAtt3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Step",
            Description = Localizer["InputNumbersAtt4"],
            Type = "int|long|short|float|double|decimal",
            ValueList = " — ",
            DefaultValue = "1"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["InputNumbersAtt5"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["InputNumbersAtt6"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["InputNumbersAtt7"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
