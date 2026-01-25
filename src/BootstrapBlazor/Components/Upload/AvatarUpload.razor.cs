// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">头像上传组件</para>
/// <para lang="en">Avatar Upload Component</para>
/// </summary>
public partial class AvatarUpload<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 文件预览框宽度</para>
    /// <para lang="en">Gets or sets the width of the file preview box</para>
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 100;

    /// <summary>
    /// <para lang="zh">获得/设置 文件预览框高度</para>
    /// <para lang="en">Gets or sets the height of the file preview box</para>
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 100;

    /// <summary>
    /// <para lang="zh">获得/设置 是否圆形图片框，Avatar 模式时生效，默认为 false</para>
    /// <para lang="en">Gets or sets whether to use circular image frame. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool IsCircle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 边框圆角，默认为 null</para>
    /// <para lang="en">Gets or sets the border radius. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? BorderRadius { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 允许的文件扩展名集合，".png"</para>
    /// <para lang="en">Gets or sets the allowed file extensions collection. ".png"</para>
    /// </summary>
    [Parameter]
    public List<string>? AllowExtensions { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除图标</para>
    /// <para lang="en">Gets or sets the delete icon</para>
    /// </summary>
    [Parameter]
    public string? DeleteIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载中图标</para>
    /// <para lang="en">Gets or sets the loading icon</para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 新增图标</para>
    /// <para lang="en">Gets or sets the add icon</para>
    /// </summary>
    [Parameter]
    public string? AddIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传成功状态图标</para>
    /// <para lang="en">Gets or sets the valid status icon</para>
    /// </summary>
    [Parameter]
    public string? ValidStatusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传失败状态图标</para>
    /// <para lang="en">Gets or sets the invalid status icon</para>
    /// </summary>
    [Parameter]
    public string? InvalidStatusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 继续上传按钮是否在列表前，默认 false</para>
    /// <para lang="en">Gets or sets whether the upload button is displayed before the list. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool IsUploadButtonAtFirst { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许预览的回调方法，默认 null</para>
    /// <para lang="en">Gets or sets the callback method to determine whether preview is allowed. Default is null.</para>
    /// </summary>
    [Parameter]
    public Func<UploadFile, bool>? CanPreviewCallback { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(string? classString = null) => CssBuilder.Default("upload-item")
        .AddClass(classString)
        .AddClass("is-circle", IsCircle)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 预览框样式字符串</para>
    /// <para lang="en">Gets the preview item style string</para>
    /// </summary>
    private string? ItemStyleString => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .AddClass($"height: {Height}px;", Height > 0 && !IsCircle)
        .AddClass($"height: {Width}px;", IsCircle)
        .AddClass($"--bb-upload-item-border-radius: {BorderRadius};", IsCircle && !string.IsNullOrEmpty(BorderRadius))
        .Build();

    private string? ValidStatusIconString => CssBuilder.Default("valid-icon valid")
        .AddClass(ValidStatusIcon)
        .Build();

    private string? InvalidStatusIconString => CssBuilder.Default("valid-icon invalid")
        .AddClass(InvalidStatusIcon)
        .Build();

    private bool ShowPreviewList => Files.Count != 0;

    private string PreviewerId => $"prev_{Id}";

    private List<string?> PreviewList => [.. Files.Select(i => i.PrevUrl)];

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
    /// <param name="file"></param>
    protected override async Task TriggerOnChanged(UploadFile file)
    {
        // 从客户端获得预览地址不使用 base64 编码
        if (file.IsImage(AllowExtensions, CanPreviewCallback))
        {
            file.PrevUrl = await InvokeAsync<string?>("getPreviewUrl", Id, file.OriginFileName);
        }
        await base.TriggerOnChanged(file);
    }

    /// <summary>
    /// <para lang="zh">预览当前头像方法</para>
    /// <para lang="en">预览当前头像方法</para>
    /// </summary>
    public async Task Preview()
    {
        if (ShowPreviewList)
        {
            await InvokeVoidAsync("preview", PreviewerId, 0);
        }
    }

    private async Task Preview(UploadFile file)
    {
        if (!string.IsNullOrEmpty(file.PrevUrl))
        {
            var index = Files.FindIndex(r => r.PrevUrl == file.PrevUrl);
            if (index != -1)
            {
                await InvokeVoidAsync("preview", PreviewerId, index);
            }
        }
    }

    private IReadOnlyCollection<ValidationResult> _results = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="results"></param>
    public override async Task ToggleMessage(IReadOnlyCollection<ValidationResult> results)
    {
        _results = results;
        IsValid = results.Count == 0;

        ValidateModule ??= await LoadValidateModule();

        var items = IsInValidOnAddItem
            ? [AddId]
            : Files.Select(i => i.ValidateId).ToList();
        var invalidItems = _results.GetInvalidItems(IsInValidOnAddItem, AddId);
        var addId = IsInValidOnAddItem ? null : AddId;
        await ValidateModule.InvokeVoidAsync("executeUpload", items, invalidItems, addId);
    }

    private bool IsInValidOnAddItem => Files.Count == 0 && _results.Count > 0;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override ValueTask ShowValidResult() => ValueTask.CompletedTask;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="validateId"></param>
    protected override async ValueTask RemoveValidResult(string? validateId = null)
    {
        if (!string.IsNullOrEmpty(validateId))
        {
            await base.RemoveValidResult(validateId);
        }
    }

    private string? AddId => $"{Id}_new";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (ValidateForm != null && FieldIdentifier.HasValue)
            {
                ValidateForm.TryRemoveValidator((FieldIdentifier.Value.FieldName, FieldIdentifier.Value.Model.GetType()), out _);
            }

            if (ValidateModule != null)
            {
                var items = IsInValidOnAddItem
                    ? [AddId]
                    : Files.Select(i => i.ValidateId).ToList();

                await ValidateModule.InvokeVoidAsync("disposeUpload", items);
            }
        }

        await base.DisposeAsync(false);
    }
}
