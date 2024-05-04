// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ExportPdfButtons 组件
/// </summary>
public partial class ExportPdfButtons
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbies { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Items = Foo.GenerateFoo(FooLocalizer);
        Hobbies = Foo.GenerateHobbies(FooLocalizer);
        Model = Foo.Generate(FooLocalizer);
    }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(ExportPdfButton.ElementId),
            Description = Localizer["AttributeElementId"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ExportPdfButton.Selector),
            Description = Localizer["AttributeSelector"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ExportPdfButton.StyleTags),
            Description = Localizer["AttributeStyleTags"],
            Type = "List<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ExportPdfButton.ScriptTags),
            Description = Localizer["AttributeScriptTags"],
            Type = "List<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ExportPdfButton.PdfFileName),
            Description = Localizer["AttributePdfFileName"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
