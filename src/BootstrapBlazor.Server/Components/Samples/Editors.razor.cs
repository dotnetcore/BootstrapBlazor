// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Editor 组件示例代码
/// </summary>
public sealed partial class Editors
{
    [NotNull]
    private Editor? Editor { get; set; }

    private string? EditorValue { get; set; }

    private bool ShowSubmitButton { get; set; }

    private void ShowSubmit() => ShowSubmitButton = !ShowSubmitButton;

    private string ButtonText => ShowSubmitButton ? "Hide SubmitButton" : "Show SubmitButton";

    private string? ValueChangedValue { get; set; }

    private List<EditorToolbarButton>? EditorPluginItems { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditorValue = "This is ShowSubmit demo";

        ValueChangedValue = Localizer["EditorOnValueChangedInitValue"];

        EditorPluginItems =
        [
            new EditorToolbarButton()
            {
                IconClass = "fa-solid fa-pencil",
                ButtonName = "plugin1",
                Tooltip = Localizer["ToolTip1"]
            },
            new EditorToolbarButton()
            {
                IconClass = "fa-solid fa-house",
                ButtonName = "plugin2",
                Tooltip = Localizer["ToolTip2"]
            }
        ];
    }

    private void SetValue()
    {
        ValueChangedValue = Localizer["EditorOnValueChangedUpdateValue"];
    }

    private async Task<string?> PluginClick(string pluginItemName)
    {
        var ret = "";
        if (pluginItemName == "plugin1")
        {
            var op = new SwalOption()
            {
                Title = Localizer["Swal1Title"],
                Content = Localizer["Swal1Content"]
            };
            if (await SwalService.ShowModal(op))
            {
                ret = Localizer["Ret1"];
            }
        }
        if (pluginItemName == "plugin2")
        {
            var op = new SwalOption()
            {
                Title = Localizer["Swal2Title"],
                Content = Localizer["Swal2Content"]
            };
            if (await SwalService.ShowModal(op))
            {
                ret = Localizer["Ret2"];
            }
        }
        return ret;
    }

    private readonly List<object> ToolbarItems =
    [
        new List<object>
        {
            "style", new List<string>()
            {
                "style"
            }
        },
        new List<object>
        {
            "font", new List<string>()
            {
                "bold", "underline", "clear"
            }
        }
    ];

    private async Task InsertHtmlAsync()
    {
        await Editor.DoMethodAsync("pasteHTML", $"<h1>{Localizer["DoMethodAsyncPasteHTML"]}</h1>");
    }

    private async Task<string> OnFileUpload(EditorUploadFile uploadFile)
    {
        var url = Path.Combine("images", "uploader", $"{Path.GetFileNameWithoutExtension(uploadFile.FileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(uploadFile.FileName)}");
        var fileName = Path.Combine(WebsiteOption.CurrentValue.WebRootPath, url);
        await uploadFile.SaveToFileAsync(fileName);

        // 此处返回空字符串底层使用 URL.createObjectURL 方法创建 Blob 对象地址
        // 实战中可以返回 SSO 地址或者 base64 字符串等
        return "";
    }

    private string? _editorCode;

    private async Task OnGetCode()
    {
        _editorCode = await Editor.GetCode();
    }
}
