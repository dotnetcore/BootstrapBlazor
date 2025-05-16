// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 头像上传组件
/// </summary>
public partial class AvatarUpload<TValue>
{
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

    private string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(UploadFile item) => CssBuilder.Default(ItemClassString)
        .AddClass("is-valid", !IsDisabled && item.IsValid.HasValue && item.IsValid.Value)
        .AddClass("is-invalid", !IsDisabled && item.IsValid.HasValue && !item.IsValid.Value)
        .AddClass("is-valid", !IsDisabled && !item.IsValid.HasValue && item.Uploaded && item.Code == 0)
        .AddClass("is-invalid", !IsDisabled && !item.IsValid.HasValue && item.Code != 0)
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? ItemClassString => CssBuilder.Default("upload-item")
        .AddClass("is-circle", IsCircle)
        .AddClass("is-single", IsMultiple == false)
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
    /// <returns></returns>
    protected override bool CheckCanUpload()
    {
        // 允许多上传
        if(IsMultiple == true)
        {
            return true;
        }

        // 只允许单个上传
        return UploadFiles.Count == 0;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnFileUpload(List<UploadFile> items)
    {
        await base.OnFileUpload(items);

        foreach (var item in items)
        {
            item.ValidateId = $"{Id}_{item.GetHashCode()}";
            if (OnChange != null)
            {
                await item.RequestBase64ImageFileAsync();
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    //protected override string? RetrieveId() => _currentFile?.ValidateId;
}
