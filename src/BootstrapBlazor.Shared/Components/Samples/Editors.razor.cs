// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">Editor 组件示例代码</para>
/// <para lang="en">Editor component examples.</para>
/// </summary>
public sealed partial class Editors
{
    [Inject]
    [NotNull]
    private SwalService? SwalService { get; set; }

    [Inject]
    [NotNull]
    private PackageVersionService? VersionManager { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Editors>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IEditorUploadService? UploadService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

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

    /// <summary>
    /// <para lang="zh">通过当前运行模式注册的服务上传图片，失败时提示并阻止回退为本地 Blob URL。</para>
    /// <para lang="en">Uploads images through the service registered for the current host and reports failures without falling back to a local Blob URL.</para>
    /// </summary>
    private async Task<string> OnFileUpload(EditorUploadFile uploadFile)
    {
        try
        {
            if (uploadFile.FileSize > IEditorUploadService.MaxFileSize)
            {
                throw new InvalidDataException(Localizer["UploadTooLarge"]);
            }
            var url = await UploadService.UploadAsync(uploadFile);
            return NavigationManager.ToAbsoluteUri(url).AbsoluteUri;
        }
        catch (Exception ex)
        {
            uploadFile.Error = ex;
            await ToastService.Error(Localizer["UploadFailed"], ex.Message);
            throw;
        }
    }

    private string? _editorCode;

    private async Task OnGetCode()
    {
        _editorCode = await Editor.GetCode();
    }
}
