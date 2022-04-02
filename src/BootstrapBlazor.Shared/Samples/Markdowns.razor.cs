// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Globalization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Markdown 示例代码
/// </summary>
public partial class Markdowns
{
    private string? MarkdownString { get; set; }

    private string? HtmlString { get; set; }

    /// <summary>
    /// 获得/设置 版本号字符串
    /// </summary>
    private string Version { get; set; } = "fetching";

    private string? Language { get; set; }

    private string? AsyncValue { get; set; }

    private string JsString { get; set; } = @"```js
console.log('test');
```";

    [NotNull]
    private Markdown? Markdown { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Language = CultureInfo.CurrentUICulture.Name;
        MarkdownString = $"### {Localizer["MarkdownString"]}";
        Version = await VersionManager.GetVersionAsync("bootstrapblazor.markdown");
    }

    private async Task GetAsyncString()
    {
        await Task.Delay(600);
        AsyncValue = $"### {DateTime.Now}";
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem(){
                Name = "Height",
                Description = Localizer["Att1"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "300"
            },
            new AttributeItem(){
                Name = "MinHeight",
                Description = Localizer["Att2"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "200"
            },
            new AttributeItem(){
                Name = "InitialEditType",
                Description = Localizer["Att3"],
                Type = "InitialEditType",
                ValueList = "Markdown/Wysiwyg",
                DefaultValue = "Markdown"
            },
            new AttributeItem(){
                Name = "PreviewStyle",
                Description = Localizer["Att4"],
                Type = "PreviewStyle",
                ValueList = "Tab/Vertical",
                DefaultValue = "Vertical"
            },
            new AttributeItem(){
                Name = "Language",
                Description = Localizer["Att5"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "Placeholder",
                Description = Localizer["Att6"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem(){
                Name = "IsViewer",
                Description = Localizer["Att7"],
                Type = "bool",
                ValueList = " true/false ",
                DefaultValue = " false "
            },
            new AttributeItem(){
                Name = "IsDark",
                Description = "是否为暗黑模式",
                Type = "bool",
                ValueList = " true/false ",
                DefaultValue = " false "
            },
            new AttributeItem(){
                Name = "EnableHighlight",
                Description = "是否启用代码高亮插件，启用前需要引入插件对应的js css",
                Type = "bool",
                ValueList = " true/false ",
                DefaultValue = " false "
            }
    };
}
