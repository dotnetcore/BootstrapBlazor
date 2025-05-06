// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    private Task OnBeforeExport() => ToastService.Information(Localizer["ToastTitle"], Localizer["ToastContent"]);

    private static string PdfFileName => $"Pdf-{DateTime.Now:HHmmss}.pdf";

    private Task OnAfterDownload(string fileName) => ToastService.Success(Localizer["ToastDownloadTitle"], Localizer["ToastDownloadContent", fileName]);

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
            Name = nameof(ExportPdfButton.FileName),
            Description = Localizer["AttributePdfFileName"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ExportPdfButton.AutoDownload),
            Description = Localizer["AttributeAutoDownload"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(ExportPdfButton.OnBeforeExport),
            Description = Localizer["AttributeOnBeforeExport"],
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ExportPdfButton.OnBeforeDownload),
            Description = Localizer["AttributeOnBeforeDownload"],
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(ExportPdfButton.OnAfterDownload),
            Description = Localizer["AttributeOnAfterDownload"],
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
