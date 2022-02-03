// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Display 组件示例
/// </summary>
public partial class Displays
{
    [NotNull]
    private Foo? Model { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbys { get; set; }

    private byte[] ByteArray { get; set; } = new byte[] { 0x01, 0x12, 0x34, 0x56 };

    private IEnumerable<int> IntValue { get; set; } = new[] { 1, 2, 3 };

    private IEnumerable<SelectedItem> IntValueSource { get; set; } = new[]
    {
            new SelectedItem("1", "Text1"),
            new SelectedItem("2", "Text2"),
            new SelectedItem("3", "Text3")
        };

    private static async Task<string> ByteArrayFormatter(byte[] source)
    {
        await Task.Delay(10);
        return Convert.ToBase64String(source);
    }

    private static Task<string> DateTimeFormatter(DateTime source) => Task.FromResult(source.ToString("yyyy-MM-dd"));

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Model = Foo.Generate(FooLocalizer);
        Model.Hobby = Foo.GenerateHobbys(FooLocalizer).Take(3).Select(i => i.Text);
        Hobbys = Foo.GenerateHobbys(FooLocalizer);
    }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new AttributeItem() {
            Name = "ShowLabel",
            Description = Localizer["ShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["DisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FormatString",
            Description = Localizer["FormatString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Formatter",
            Description = Localizer["Formatter"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Display<string>.TypeResolver),
            Description = Localizer["TypeResolver"],
            Type = "Func<Assembly?, string, bool, Type?>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
