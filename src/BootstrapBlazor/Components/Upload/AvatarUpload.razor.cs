// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 头像上传组件
/// <para>AvatarUpload Component</para>
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
    /// Gets or sets the border radius. Default is null.
    /// </summary>
    [Parameter]
    public string? BorderRadius { get; set; }

    /// <summary>
    /// 获得/设置 图标文件扩展名集合 ".png"
    /// </summary>
    [Parameter]
    public List<string>? AllowExtensions { get; set; }

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

    /// <summary>
    /// 获得/设置 是否允许预览回调方法 默认 null
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
    /// 获得/设置 预览框 Style 属性
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
    /// <returns></returns>
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
    /// 预览当前头像方法
    /// </summary>
    /// <returns></returns>
    public async Task Preview(string? id)
    {
        if(ShowPreviewList && !string.IsNullOrWhiteSpace(id))
        {
            var index = Files.FindIndex(r => r.ValidateId == id);
            if (index != -1)
                await InvokeVoidAsync("preview", PreviewerId, index);
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

        var invalidItems = IsInValiadOnAddItem
            ? [new { Id = AddId, _results.First().ErrorMessage }]
            : _results.Select(i => new { Id = i.MemberNames.FirstOrDefault(), i.ErrorMessage }).ToList();

        var items = IsInValiadOnAddItem
            ? [AddId]
            : Files.Select(i => i.ValidateId).ToList();

        var addId = IsInValiadOnAddItem ? null : AddId;
        await ValidateModule.InvokeVoidAsync("executeUpload", items, invalidItems, addId);
    }

    private bool IsInValiadOnAddItem => Files.Count == 0 && _results.Count > 0;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override ValueTask ShowValidResult() => ValueTask.CompletedTask;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="validateId"></param>
    /// <returns></returns>
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
    /// <returns></returns>
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
                var items = IsInValiadOnAddItem
                    ? [AddId]
                    : Files.Select(i => i.ValidateId).ToList();

                await ValidateModule.InvokeVoidAsync("disposeUpload", items);
            }
        }

        await base.DisposeAsync(false);
    }
}
