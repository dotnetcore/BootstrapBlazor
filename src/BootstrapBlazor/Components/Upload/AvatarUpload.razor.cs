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

    private string? GetItemClassString(UploadFile? item = null) => CssBuilder.Default("upload-item")
        .AddClass("is-circle", IsCircle)
        .AddClass("disabled", IsDisabled)
        .AddClass(GetValidStatus(item))
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

    private string? ActionClassString => CssBuilder.Default("upload-item-actions")
        .AddClass("btn-browser", IsDisabled == false)
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
    protected override async ValueTask ShowValidResult()
    {
        ValidateModule ??= await LoadValidateModule();

        var items = Files.Count == 0 && _results.Count > 0
            ? [new { Id = AddId, _results.First().ErrorMessage }]
            : _results.Select(i => new { Id = i.MemberNames.FirstOrDefault(), i.ErrorMessage });
        await ValidateModule.InvokeVoidAsync("executeBatch", items);

        if (Files.Count > 0)
        {
            await ValidateModule.InvokeVoidAsync("dispose", AddId);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async ValueTask RemoveValidResult(string? validateId = null)
    {
        ValidateModule ??= await LoadValidateModule();

        var items = new List<string?>();
        if (!string.IsNullOrEmpty(validateId))
        {
            items.Add(validateId);
        }
        else
        {
            items.AddRange(Files.Select(f => f.ValidateId));
        }
        await ValidateModule.InvokeVoidAsync("disposeBatch", items);
    }

    private IReadOnlyCollection<ValidationResult> _results = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="results"></param>
    public override Task ToggleMessage(IReadOnlyCollection<ValidationResult> results)
    {
        _results = results;
        IsValid = results.Count == 0;
        return Task.CompletedTask;
    }

    private string? AddId => $"{Id}_new";

    private string? GetValidStatus(UploadFile? item = null)
    {
        if (IsDisabled || ValidateForm == null)
        {
            return null;
        }

        if (item == null && Files.Count > 0)
        {
            // 如果没有文件则使用组件本身的 IsValid 状态
            return null;
        }

        var state = item?.IsValid ?? IsValid;
        return state.HasValue
            ? state.Value ? "is-valid" : "is-invalid"
            : null;
    }
}
