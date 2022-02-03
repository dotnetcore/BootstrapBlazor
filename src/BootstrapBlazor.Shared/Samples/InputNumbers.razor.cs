// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class InputNumbers
{
    /// <summary>
    /// 
    /// </summary>
    public int BindValue { get; set; } = 5;

    /// <summary>
    /// 
    /// </summary>
    public sbyte BindSByteValue { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    public byte BindByteValue { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    public long BindLongValue { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    public short BindShortValue { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    public double BindDoubleValue { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    public float BindFloatValue { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    public decimal BindDecimalValue { get; set; } = 10;

    private Foo Model { get; set; } = new Foo() { Count = 10 };

    private static string Formatter(double val) => val.ToString("0.0");

    private IEnumerable<AttributeItem> GetAttributes()
    {
        return new AttributeItem[]
        {
                new AttributeItem() {
                    Name = "Value",
                    Description = Localizer["Att1"],
                    Type = "sbyte|byte|int|long|short|float|double|decimal",
                    ValueList = " — ",
                    DefaultValue = "0"
                },
                new AttributeItem() {
                    Name = "Max",
                    Description = Localizer["Att2"],
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem()
                {
                    Name = "Min",
                    Description = Localizer["Att3"],
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                },
                new AttributeItem()
                {
                    Name = "Step",
                    Description = Localizer["Att4"],
                    Type = "int|long|short|float|double|decimal",
                    ValueList = " — ",
                    DefaultValue = "1"
                },
                new AttributeItem()
                {
                    Name = "IsDisabled",
                    Description = Localizer["Att5"],
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowLabel",
                    Description = Localizer["Att6"],
                    Type = "bool",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "DisplayText",
                    Description = Localizer["Att7"],
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " — "
                }
        };
    }
}
