// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// ColorPickers
/// </summary>
public partial class ColorPickers
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Foo? Dummy { get; set; } = new Foo() { Name = "#dddddd" };

    private string Value { get; set; } = "#FFFFFF";

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private Task OnColorChanged(string color)
    {
        NormalLogger.Log($"Selected color: {color}");
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "OnValueChanged",
            Description = Localizer["Event1"],
            Type = "Func<string, Task>",
            ValueList = "",
            DefaultValue = ""
        }
    };
}
