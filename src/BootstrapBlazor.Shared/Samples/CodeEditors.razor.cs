﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Samples;
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
            Code = @"
                     function main() {
                          console.log('Hello World!')
                     }
                    ";
        }

        if (item.Text == "CSharp")
        {
            Language = "csharp";
            Code = @"
             using System;

             void Main()
             {
                 Console.WriteLine(""Hello World"");
             }
                    ";
        }

        if (item.Text == "Json")
        {
            Language = "json";
            Code = @"
            {
                ""name"": ""Hello World"",
                ""age"": 25
            }
                    ";
        }

        if (item.Text == "Razor")
        {
            Language = "razor";
            Code = @"
            <Select TValue=""string"" OnSelectedItemChanged=""@OnSelectedItemChanged"">
                <Options>
                    <SelectOption Text=""JavaScript"" Value=""JavaScript""></ SelectOption>
                    <SelectOption Text=""CSharp"" Value=""CSharp"" ></ SelectOption >
                    <SelectOption Text=""Razor"" Value=""Razor"" ></ SelectOption >
                    <SelectOption Text=""Json"" Value=""Json"" ></ SelectOption >
                </Options>
            </Select>
                    ";
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
}
