// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Inputs
{
    private string? PlaceHolderText { get; set; }

    private byte[] ByteArray { get; set; } = new byte[] { 0x01, 0x12, 0x34, 0x56 };

    private static string ByteArrayFormatter(byte[] source) => Convert.ToBase64String(source);

    [NotNull]
    private Foo? Model { get; set; }

    private static string DateTimeFormatter(DateTime source) => source.ToString("yyyy-MM-dd");

    [NotNull]
    private BlockLogger? Trace { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PlaceHolderText = Localizer["PlaceHolder"];
        Model = new Foo() { Name = Localizer["TestName"] };
    }

    private Task OnEnterAsync(string val)
    {
        Trace.Log($"Enter {Localizer["Log"]}: {val}");
        return Task.CompletedTask;
    }

    private Task OnEscAsync(string val)
    {
        Trace.Log($"Esc {Localizer["Log"]}: {val}");
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new AttributeItem() {
            Name = "ChildContent",
            Description = Localizer["Att1"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ShowLabel",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["Att3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Color",
            Description = Localizer["Att4"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new AttributeItem() {
            Name = "FormatString",
            Description = Localizer["Att5"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Formatter",
            Description = Localizer["Att6"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "type",
            Description = Localizer["Att7"],
            Type = "string",
            ValueList = "text / number / email / url / password",
            DefaultValue = "text"
        },
        new AttributeItem() {
            Name = "OnEnterAsync",
            Description = Localizer["Att8"],
            Type = "Func<TValue, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnEscAsync",
            Description = Localizer["Att9"],
            Type = "Func<TValue, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["Att10"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IsAutoFocus",
            Description = Localizer["Att11"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(BootstrapInput<string>.IsSelectAllTextOnFocus),
            Description = Localizer[nameof(BootstrapInput<string>.IsSelectAllTextOnFocus)].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(BootstrapInput<string>.IsTrim),
            Description = Localizer[nameof(BootstrapInput<string>.IsTrim)].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(BootstrapInput<string>.ValidateRules),
            Description = Localizer[nameof(BootstrapInput<string>.ValidateRules)].Value,
            Type = "List<IValidator>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
