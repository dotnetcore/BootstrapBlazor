// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FileListUploadBase 基类</para>
/// <para lang="en">FileListUploadBase Base Class</para>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class FileListUploadBase<TValue> : UploadBase<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示删除按钮，默认 false</para>
    /// <para lang="en">Gets or sets whether to display the delete button. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowDeleteButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除按钮图标</para>
    /// <para lang="en">Gets or sets the delete button icon</para>
    /// </summary>
    [Parameter]
    public string? DeleteIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示下载按钮，默认 false</para>
    /// <para lang="en">Gets or sets whether to display the download button. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowDownloadButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下载按钮图标</para>
    /// <para lang="en">Gets or sets the download button icon</para>
    /// </summary>
    [Parameter]
    public string? DownloadIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击下载按钮回调方法，默认 null</para>
    /// <para lang="en">Gets or sets the callback method for the download button click event. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnDownload { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮图标</para>
    /// <para lang="en">Gets or sets the cancel button icon</para>
    /// </summary>
    [Parameter]
    public string? CancelIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击取消按钮回调方法，默认 null</para>
    /// <para lang="en">Gets or sets the callback method for the cancel button click event. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnCancel { get; set; }

    /// <summary>
    /// <para lang="zh">IconTheme 服务实例</para>
    /// <para lang="en">Gets the IconTheme service instance</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DeleteIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadDeleteIcon);
        DownloadIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadDownloadIcon);
        CancelIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadCancelIcon);
    }
}
