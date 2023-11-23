// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Markdown 示例代码
/// </summary>
public partial class Markdowns
{
    private string? MarkdownString { get; set; }

    private string? HtmlString { get; set; }

    private string? Language { get; set; }

    private string? AsyncValue { get; set; }

    [NotNull]
    private Markdown? MarkdownSetValue { get; set; }

    [NotNull]
    private Markdown? Markdown { get; set; }

    private string JsString { get; set; } = @"```js
        console.log('test');
        ```";

    [NotNull]
    private Foo? Model { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    /// <returns></returns>
    protected override void OnInitialized()
    {
        Language = CultureInfo.CurrentUICulture.Name;
        MarkdownString = $"### {Localizer["MarkdownString"]}";

        Model = new() { Name = "Name", Education = EnumEducation.Primary, DateTime = DateTime.Now };
    }

    private async Task GetAsyncString()
    {
        await Task.Delay(600);
        AsyncValue = $"### {DateTime.Now}";
        await MarkdownSetValue.SetValue(AsyncValue);
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Height",
            Description = Localizer["Att1"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "300"
        },
        new()
        {
            Name = "MinHeight",
            Description = Localizer["Att2"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "200"
        },
        new()
        {
            Name = "InitialEditType",
            Description = Localizer["Att3"],
            Type = "InitialEditType",
            ValueList = "Markdown/Wysiwyg",
            DefaultValue = "Markdown"
        },
        new()
        {
            Name = "PreviewStyle",
            Description = Localizer["Att4"],
            Type = "PreviewStyle",
            ValueList = "Tab/Vertical",
            DefaultValue = "Vertical"
        },
        new()
        {
            Name = "Language",
            Description = Localizer["Att5"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Placeholder",
            Description = Localizer["Att6"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsViewer",
            Description = Localizer["Att7"],
            Type = "bool",
            ValueList = " true/false ",
            DefaultValue = " false "
        },
        new()
        {
            Name = "IsDark",
            Description = Localizer["Att8"],
            Type = "bool",
            ValueList = " true/false ",
            DefaultValue = " false "
        },
        new()
        {
            Name = "EnableHighlight",
            Description = Localizer["Att9"],
            Type = "bool",
            ValueList = " true/false ",
            DefaultValue = " false "
        }
    };
}
