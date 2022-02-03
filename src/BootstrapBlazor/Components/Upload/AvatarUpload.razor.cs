// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
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
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override async Task OnFileChange(InputFileChangeEventArgs args)
    {
        await base.OnFileChange(args);

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
        ValidateFile();

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
