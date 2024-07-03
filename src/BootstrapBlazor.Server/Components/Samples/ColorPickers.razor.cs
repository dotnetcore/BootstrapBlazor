// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/
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

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private Task OnColorChanged(string color)
    {
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

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "OnValueChanged",
            Description = Localizer["Event1"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ColorPicker.Template),
            Description = Localizer["EventTemplate"],
            Type = "Func<string>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
