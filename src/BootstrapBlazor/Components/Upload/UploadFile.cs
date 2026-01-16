// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">上传组件返回类</para>
///  <para lang="en">上传component返回类</para>
/// </summary>
public class UploadFile
{
    /// <summary>
    ///  <para lang="zh">获得/设置 文件名 由用户指定 上传文件时此参数未设置 默认为 null</para>
    ///  <para lang="en">Gets or sets 文件名 由用户指定 上传文件时此参数未Sets Default is为 null</para>
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 原始文件名(上传时 IBrowserFile.Name 实例赋值)</para>
    ///  <para lang="en">Gets or sets 原始文件名(上传时 IBrowserFile.Name instance赋值)</para>
    /// </summary>
    public string? OriginFileName { get; internal set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 文件大小</para>
    ///  <para lang="en">Gets or sets 文件大小</para>
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 文件上传结果 0 表示成功 非零表示失败</para>
    ///  <para lang="en">Gets or sets 文件上传结果 0 表示成功 非零表示失败</para>
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 文件预览地址</para>
    ///  <para lang="en">Gets or sets 文件预览地址</para>
    /// </summary>
    public string? PrevUrl { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 错误信息</para>
    ///  <para lang="en">Gets or sets 错误信息</para>
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 上传文件实例</para>
    ///  <para lang="en">Gets or sets 上传文件instance</para>
    /// </summary>
    public IBrowserFile? File { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 上传文件总数量</para>
    ///  <para lang="en">Gets or sets 上传文件总数量</para>
    /// </summary>
    public int FileCount { get; init; } = 1;

    /// <summary>
    ///  <para lang="zh">获得/设置 更新进度回调委托</para>
    ///  <para lang="en">Gets or sets 更新进度回调delegate</para>
    /// </summary>
    internal Action<UploadFile>? UpdateCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 更新进度回调委托</para>
    ///  <para lang="en">Gets or sets 更新进度回调delegate</para>
    /// </summary>
    internal int ProgressPercent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 文件是否上传处理完毕</para>
    ///  <para lang="en">Gets or sets 文件whether上传处理完毕</para>
    /// </summary>
    internal bool Uploaded { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 用于客户端验证 Id</para>
    ///  <para lang="en">Gets or sets 用于客户端验证 Id</para>
    /// </summary>
    internal string? ValidateId { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 UploadFile 文件名</para>
    ///  <para lang="en">Gets UploadFile 文件名</para>
    /// </summary>
    /// <returns></returns>
    public string? GetFileName() => FileName ?? OriginFileName ?? File?.Name;

    /// <summary>
    ///  <para lang="zh">获得 UploadFile 文件扩展名</para>
    ///  <para lang="en">Gets UploadFile 文件扩展名</para>
    /// </summary>
    /// <returns></returns>
    public string? GetExtension() => Path.GetExtension(GetFileName());
}
