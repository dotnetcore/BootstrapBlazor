// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// InputUpload 组件
/// </summary>
public partial class InputUpload<TValue>
{
    private string? InputValueClassString => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? RemoveButtonClassString => CssBuilder.Default()
        .AddClass(DeleteButtonClass)
        .Build();

    private bool IsDeleteButtonDisabled => IsDisabled || CurrentFile == null;

    private string? BrowserButtonClassString => CssBuilder.Default("btn-browser")
        .AddClass(BrowserButtonClass)
        .Build();

    private string? GetFileName() => CurrentFile?.GetFileName() ?? Value?.ToString();

    /// <summary>
    /// 获得/设置 浏览按钮图标
    /// </summary>
    [Parameter]
    public string? BrowserButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 上传按钮样式 默认 btn-primary
    /// </summary>
    [Parameter]
    public string BrowserButtonClass { get; set; } = "btn-primary";

    /// <summary>
    /// 获得/设置 浏览按钮显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BrowserButtonText { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮样式 默认 btn-danger
    /// </summary>
    [Parameter]
    public string DeleteButtonClass { get; set; } = "btn-danger";

    /// <summary>
    /// 获得/设置 删除按钮图标
    /// </summary>
    [Parameter]
    public string? DeleteButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 重置按钮显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DeleteButtonText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示删除按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowDeleteButton { get; set; }

    /// <summary>
    /// 获得/设置 PlaceHolder 占位符文本
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<TValue>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DeleteButtonText ??= Localizer[nameof(DeleteButtonText)];
        BrowserButtonText ??= Localizer[nameof(BrowserButtonText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        BrowserButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputUploadBrowserButtonIcon);
        DeleteButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputUploadDeleteButtonIcon);
    }

    /// <summary>
    /// 上传文件改变时回调方法
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override async Task OnFileChange(InputFileChangeEventArgs args)
    {
        CurrentFile = new UploadFile()
        {
            OriginFileName = args.File.Name,
            Size = args.File.Size,
            File = args.File,
            Uploaded = false
        };

        UploadFiles.Clear();
        UploadFiles.Add(CurrentFile);

        await base.OnFileChange(args);

        if (OnChange != null)
        {
            await OnChange(CurrentFile);
        }
        CurrentFile.Uploaded = true;
    }

    private async Task OnDeleteFile()
    {
        if (CurrentFile != null)
        {
            var ret = await OnFileDelete(CurrentFile);
            if (ret)
            {
                CurrentFile = null;
                CurrentValue = default;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="results"></param>
    public override void ToggleMessage(IEnumerable<ValidationResult> results)
    {
        if (results.Any())
        {
            ErrorMessage = results.First().ErrorMessage;
            IsValid = false;
        }
        else
        {
            ErrorMessage = null;
            IsValid = true;
        }
        OnValidate(IsValid);
    }
}
