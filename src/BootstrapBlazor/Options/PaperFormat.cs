// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">纸张规格配置类</para>
/// <para lang="en">Paper format configuration class</para>
/// </summary>
[ExcludeFromCodeCoverage]
public record PaperFormat
{
    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat Letter => new(8.5m, 11m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat Legal => new(8.5m, 14m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat Tabloid => new(11m, 17m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat Ledger => new(17m, 11m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat A0 => new(33.1102m, 46.811m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat A1 => new(23.3858m, 33.1102m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat A2 => new(16.5354m, 23.3858m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat A3 => new(11.6929m, 16.5354m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat A4 => new(8.2677m, 11.6929m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat A5 => new(5.8268m, 8.2677m);

    /// <summary>
    /// 
    /// </summary>
    public static PaperFormat A6 => new(4.1339m, 5.8268m);

    /// <summary>
    /// 
    /// </summary>
    public decimal Width { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal Height { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public PaperFormat(decimal width, decimal height)
    {
        Width = width;
        Height = height;
    }
}
