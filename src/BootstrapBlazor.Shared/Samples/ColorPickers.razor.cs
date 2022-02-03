// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class ColorPickers
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    private string? Value1 { get; set; }

    private string Value2 { get; set; } = "#FFFFFF";

    private string Value3 { get; set; } = "#DDDDDD";

    private string? Value5 { get; set; }

    [NotNull]
    private Foo? Dummy { get; set; } = new Foo() { Name = "#dddddd" };

    private Task OnColorChanged(string color)
    {
        Trace.Log($"Selected color: {color}");
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem()
            {
                Name = "OnValueChanged",
                Description = Localizer["Event1"],
                Type = "Func<string, Task>",
                ValueList = "",
                DefaultValue = ""
            }
    };
}
