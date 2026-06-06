// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">StepSettings 配置类</para>
/// <para lang="en">StepSettings configuration class</para>
/// </summary>
public class StepSettings
{
    /// <summary>
    /// <para lang="zh">获得/设置 sbyte 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets sbyte type step default null</para>
    /// </summary>
    public string? SByte { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 byte 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets byte type step default null</para>
    /// </summary>
    public string? Byte { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 uint 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets uint type step default null</para>
    /// </summary>
    public string? UInt { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 int 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets int type step default null</para>
    /// </summary>
    public string? Int { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 long 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets long type step default null</para>
    /// </summary>
    public string? Long { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ulong 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets ulong type step default null</para>
    /// </summary>
    public string? ULong { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 short 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets short type step default null</para>
    /// </summary>
    public string? Short { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ushort 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets ushort type step default null</para>
    /// </summary>
    public string? UShort { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 float 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets float type step default null</para>
    /// </summary>
    public string? Float { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 double 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets double type step default null</para>
    /// </summary>
    public string? Double { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 decimal 数据类型步长 默认 null 未设置</para>
    /// <para lang="en">Gets or sets decimal type step default null</para>
    /// </summary>
    public string? Decimal { get; set; }

    /// <summary>
    /// <para lang="zh">获得步长字符串</para>
    /// <para lang="en">Get step string</para>
    /// </summary>
    /// <param name="type"></param>
    public string? GetStep(Type type)
    {
        string? ret = null;
        if (type == typeof(sbyte))
        {
            ret = SByte;
        }
        else if (type == typeof(byte))
        {
            ret = Byte;
        }
        else if (type == typeof(uint))
        {
            ret = UInt;
        }
        else if (type == typeof(int))
        {
            ret = Int;
        }
        else if (type == typeof(long))
        {
            ret = Long;
        }
        else if (type == typeof(short))
        {
            ret = Short;
        }
        else if (type == typeof(ushort))
        {
            ret = UShort;
        }
        else if (type == typeof(ulong))
        {
            ret = ULong;
        }
        else if (type == typeof(float))
        {
            ret = Float;
        }
        else if (type == typeof(double))
        {
            ret = Double;
        }
        else if (type == typeof(decimal))
        {
            ret = Decimal;
        }
        return ret;
    }
}
