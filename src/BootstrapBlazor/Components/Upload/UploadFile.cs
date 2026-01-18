// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">上传文件类</para>
/// <para lang="en">Upload File Class</para>
/// </summary>
public class UploadFile
{
    /// <summary>
    /// <para lang="zh">获得/设置 文件名，由用户指定，上传时此参数未设置默认为 null</para>
    /// <para lang="en">Gets or sets the file name specified by the user. Default is null if not set during upload.</para>
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 原始文件名（上传时由 IBrowserFile.Name 赋值）</para>
    /// <para lang="en">Gets or sets the original file name (assigned from IBrowserFile.Name during upload)</para>
    /// </summary>
    public string? OriginFileName { get; internal set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文件大小</para>
    /// <para lang="en">Gets or sets the file size</para>
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文件上传结果，0 表示成功，非零表示失败</para>
    /// <para lang="en">Gets or sets the upload result. 0 indicates success, non-zero indicates failure.</para>
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文件预览地址</para>
    /// <para lang="en">Gets or sets the file preview URL</para>
    /// </summary>
    public string? PrevUrl { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 错误信息</para>
    /// <para lang="en">Gets or sets the error message</para>
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传文件实例</para>
    /// <para lang="en">Gets or sets the upload file instance</para>
    /// </summary>
    public IBrowserFile? File { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传文件总数量</para>
    /// <para lang="en">Gets or sets the total number of files to upload</para>
    /// </summary>
    public int FileCount { get; init; } = 1;

    /// <summary>
    /// <para lang="zh">获得/设置 更新进度回调委托</para>
    /// <para lang="en">Gets or sets the progress update callback delegate</para>
    /// </summary>
    internal Action<UploadFile>? UpdateCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传进度百分比</para>
    /// <para lang="en">Gets or sets the upload progress percentage</para>
    /// </summary>
    internal int ProgressPercent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文件是否上传处理完毕</para>
    /// <para lang="en">Gets or sets whether the file upload has been completed</para>
    /// </summary>
    internal bool Uploaded { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 用于客户端验证的 ID</para>
    /// <para lang="en">Gets or sets the ID for client-side validation</para>
    /// </summary>
    internal string? ValidateId { get; set; }

    /// <summary>
    /// <para lang="zh">获得 UploadFile 的文件名</para>
    /// <para lang="en">Gets the file name of the UploadFile</para>
    /// </summary>
    /// <returns></returns>
    public string? GetFileName() => FileName ?? OriginFileName ?? File?.Name;

    /// <summary>
    /// <para lang="zh">获得 UploadFile 的文件扩展名</para>
    /// <para lang="en">Gets the file extension of the UploadFile</para>
    /// </summary>
    /// <returns></returns>
    public string? GetExtension() => Path.GetExtension(GetFileName());
}
