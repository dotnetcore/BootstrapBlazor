// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// 头像上传组件
/// </summary>
public partial class AvatarUpload<TValue>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected new string? GetItemClassString(UploadFile item) => CssBuilder.Default(ItemClassString)
        .AddClass("is-valid", !IsDisabled && item.IsValid.HasValue && item.IsValid.Value)
        .AddClass("is-invalid", !IsDisabled && item.IsValid.HasValue && !item.IsValid.Value)
        .AddClass("is-valid", !IsDisabled && !item.IsValid.HasValue && item.Uploaded && item.Code == 0)
        .AddClass("is-invalid", !IsDisabled && !item.IsValid.HasValue && item.Code != 0)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    protected override string? ItemClassString => CssBuilder.Default(base.ItemClassString)
        .AddClass("is-circle", IsCircle)
        .AddClass("is-single", IsSingle)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// 获得/设置 预览框 Style 属性
    /// </summary>
    private string? PrevStyleString => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Height}px;", Height > 0 && !IsCircle)
        .AddClass($"height: {Width}px;", IsCircle)
        .Build();

    private string? ValidStatusIconString => CssBuilder.Default("valid-icon valid")
        .AddClass(ValidStatusIcon)
        .Build();

    private string? InvalidStatusIconString => CssBuilder.Default("valid-icon invalid")
        .AddClass(InvalidStatusIcon)
        .Build();

    /// <summary>
    /// 获得/设置 文件预览框宽度
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 100;

    /// <summary>
    /// 获得/设置 文件预览框高度
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 100;

    /// <summary>
    /// 获得/设置 是否圆形图片框 Avatar 模式时生效 默认为 false
    /// </summary>
    [Parameter]
    public bool IsCircle { get; set; }

    /// <summary>
    /// 获得/设置 删除图标
    /// </summary>
    [Parameter]
    public string? DeleteIcon { get; set; }

    /// <summary>
    /// 获得/设置 加载图标
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 新建图标
    /// </summary>
    [Parameter]
    public string? AddIcon { get; set; }

    /// <summary>
    /// 获得/设置 状态正常图标
    /// </summary>
    [Parameter]
    public string? ValidStatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 状态正常图标
    /// </summary>
    [Parameter]
    public string? InvalidStatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 继续上传按钮是否在列表前 默认 false
    /// </summary>
    [Parameter]
    public bool IsUploadButtonAtFirst { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DeleteIcon ??= IconTheme.GetIconByKey(ComponentIcons.AvatarUploadDeleteIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.AvatarUploadLoadingIcon);
        AddIcon ??= IconTheme.GetIconByKey(ComponentIcons.AvatarUploadAddIcon);
        ValidStatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.AvatarUploadValidStatusIcon);
        InvalidStatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.AvatarUploadInvalidStatusIcon);
    }

    /// <summary>
    /// <inheritdoc/>
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
        CurrentFile.ValidateId = $"{Id}_{CurrentFile.GetHashCode()}";

        if (IsSingle)
        {
            // 单图片模式
            DefaultFileList?.Clear();
            UploadFiles.Clear();
        }

        UploadFiles.Add(CurrentFile);

        await base.OnFileChange(args);

        // ValidateFile 后 IsValid 才有值
        CurrentFile.IsValid = IsValid;

        if (OnChange != null)
        {
            await OnChange(CurrentFile);
        }
        else
        {
            await CurrentFile.RequestBase64ImageFileAsync(CurrentFile.File.ContentType, 320, 240);
        }
    }

    /// <summary>
    /// 获得 弹窗客户端 ID
    /// </summary>
    /// <returns></returns>
    protected override string? RetrieveId() => CurrentFile?.ValidateId;
}
