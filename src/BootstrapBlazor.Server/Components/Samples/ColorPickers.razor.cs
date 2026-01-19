// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ColorPickers
/// </summary>
public partial class ColorPickers
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Foo? Dummy { get; set; } = new Foo() { Name = "#DDDDDD" };

    private string Value { get; set; } = "#FFFFFF";

    private string _opacityValue = "#dd0324";

    private bool _opacityIsDisabled = false;

    private bool _opacityIsSupport = true;

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private Task OnColorChanged(string color)
    {
        Value = color;
        NormalLogger.Log($"Selected color: {color}");
        return Task.CompletedTask;
    }

    private static string FormatValue(string v)
    {
        var ret = "";
        if (!string.IsNullOrEmpty(v) && v.Length > 1)
        {
            ret = $"#FF{v[1..].ToUpperInvariant()}";
        }
        return ret;
    }

    private static async Task<string> FormatValueAsync(string v)
    {
        // 模拟延时
        await Task.Delay(0);

        var ret = "";
        if (!string.IsNullOrEmpty(v) && v.Length > 1)
        {
            ret = $"#FF{v[1..].ToUpperInvariant()}";
        }
        return ret;
    }
}
