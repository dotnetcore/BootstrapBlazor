// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// CodeEditor 示例
/// </summary>
public partial class CodeEditors
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private string? Code { get; set; }

    [NotNull]
    private string? Language { get; set; }

    [NotNull]
    private string? Theme { get; set; }

    private Task OnSelectedItemChanged(SelectedItem item)
    {
        if (item.Text == "JavaScript")
        {
            Language = "javascript";
            Code = """
function main() {
    console.log('Hello World!')
}
""";
        }

        if (item.Text == "CSharp")
        {
            Language = "csharp";
            Code = """
using System;

void Main()
{
    Console.WriteLine(""Hello World"");
}
""";
        }

        if (item.Text == "Json")
        {
            Language = "json";
            Code = """
{
    "name": "Hello World",
    "age": 25
}
""";
        }

        if (item.Text == "Razor")
        {
            Language = "razor";
            Code = """
<Select TValue=""string"" OnSelectedItemChanged=""@OnSelectedItemChanged"">
    <Options>
        <SelectOption Text=""JavaScript"" Value=""JavaScript""></ SelectOption>
        <SelectOption Text=""CSharp"" Value=""CSharp"" ></ SelectOption >
        <SelectOption Text=""Razor"" Value=""Razor"" ></ SelectOption >
        <SelectOption Text=""Json"" Value=""Json"" ></ SelectOption >
    </Options>
</Select>
""";
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnValueChanged(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            Logger.Log(value);
        }
        return Task.CompletedTask;
    }

    private Task OnThemeSelectedItemChanged(SelectedItem item)
    {
        if (item.Value == "vs-dark")
        {
            Theme = item.Value;
        }

        if (item.Value == "vs")
        {
            Theme = item.Value;
        }

        if (item.Value == "hc-black")
        {
            Theme = item.Value;
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributeItems()
    {
        return new List<AttributeItem>()
        {
            new AttributeItem()
            {
                Name = nameof(CodeEditor.Value),
                Type = "string",
                DefaultValue = "-",
                Description = Localizer["Value"]
            },
            new AttributeItem()
            {
                Name = nameof(CodeEditor.Theme),
                Type = "string",
                DefaultValue = "vs",
                Description = Localizer["Theme"]
            },
            new AttributeItem()
            {
                Name = nameof(CodeEditor.Language),
                Type = "string",
                DefaultValue = "csharp",
                Description = Localizer["Language"]
            },
            new AttributeItem()
            {
                Name = nameof(CodeEditor.ValueChanged),
                Type = "EventCallback<string?>",
                DefaultValue = "-",
                Description = Localizer["ValueChanged"]
            },
            new AttributeItem()
            {
                Name = nameof(CodeEditor.OnValueChanged),
                Type = "Func<string?,Task>",
                DefaultValue = "-",
                Description = Localizer["ValueChanged"]
            },
        };
    }
}
