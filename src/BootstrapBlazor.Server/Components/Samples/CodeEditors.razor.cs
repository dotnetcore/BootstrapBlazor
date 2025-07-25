// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// CodeEditor 示例
/// </summary>
public partial class CodeEditors
{
    private string _code { get; set; } = """
using System;

void Main()
{
    Console.WriteLine(""Hello World"");
}
""";

    private string _language { get; set; } = "CSharp";

    private string _theme { get; set; } = "vs";

    private Task OnSelectedItemChanged(SelectedItem item)
    {
        if (item.Text == "JavaScript")
        {
            _code = """
function main() {
    console.log('Hello World!')
}
""";
        }

        if (item.Text == "CSharp")
        {
            _code = """
using System;

void Main()
{
    Console.WriteLine(""Hello World"");
}
""";
        }

        if (item.Text == "Json")
        {
            _code = """
{
    "name": "Hello World",
    "age": 25
}
""";
        }

        if (item.Text == "Razor")
        {
            _code = """
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
        return Task.CompletedTask;
    }

    private static List<AttributeItem> GetAttributeItems()
    {
        return [
            //new()
            //{
            //    Name = nameof(CodeEditor.Value),
            //    Type = "string",
            //    DefaultValue = "-",
            //    Description = Localizer["Value"]
            //},
            //new()
            //{
            //    Name = nameof(CodeEditor.Theme),
            //    Type = "string",
            //    DefaultValue = "vs",
            //    Description = Localizer["Theme"]
            //},
            //new()
            //{
            //    Name = nameof(CodeEditor.Language),
            //    Type = "string",
            //    DefaultValue = "csharp",
            //    Description = Localizer["Language"]
            //},
            //new()
            //{
            //    Name = nameof(CodeEditor.ValueChanged),
            //    Type = "EventCallback<string?>",
            //    DefaultValue = "-",
            //    Description = Localizer["ValueChanged"]
            //},
            //new()
            //{
            //    Name = nameof(CodeEditor.OnValueChanged),
            //    Type = "Func<string?,Task>",
            //    DefaultValue = "-",
            //    Description = Localizer["ValueChanged"]
            //}
        ];
    }
}
