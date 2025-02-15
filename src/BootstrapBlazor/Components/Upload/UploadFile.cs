// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// 上传组件返回类
/// </summary>
public class UploadFile
{
    /// <summary>
    /// 获得/设置 文件名
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 获得/设置 原始文件名(上传时 IBrowserFile.Name 实例赋值)
    /// </summary>
    public string? OriginFileName { get; internal set; }

    /// <summary>
    /// 获得/设置 文件大小
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 获得/设置 文件上传结果 0 表示成功 非零表示失败
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 获得/设置 文件预览地址
    /// </summary>
    public string? PrevUrl { get; set; }

    /// <summary>
    /// 获得/设置 错误信息
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// 获得/设置 上传文件实例
    /// </summary>
    public IBrowserFile? File { get; set; }

    /// <summary>
    /// 获得/设置 上传文件数量
    /// </summary>
    public int FileCount { get; init; } = 1;

    /// <summary>
    /// 获得/设置 更新进度回调委托
    /// </summary>
    internal Action<UploadFile>? UpdateCallback { get; set; }

    /// <summary>
    /// 获得/设置 更新进度回调委托
    /// </summary>
    internal int ProgressPercent { get; set; }

    /// <summary>
    /// 获得/设置 文件是否上传处理完毕
    /// </summary>
    internal bool Uploaded { get; set; } = true;

    /// <summary>
    /// 获得/设置 用于客户端验证 Id
    /// </summary>
    internal string? ValidateId { get; set; }

    /// <summary>
    /// 获得/设置 组件是否合规 默认为 null 未检查
    /// </summary>
    internal bool? IsValid { get; set; }

    /// <summary>
    /// 获得 UploadFile 文件名
    /// </summary>
    /// <returns></returns>
    public string? GetFileName() => OriginFileName ?? FileName;

    /// <summary>
    /// 获得 UploadFile 文件扩展名
    /// </summary>
    /// <returns></returns>
    public string? GetExtension() => Path.GetExtension(GetFileName());
}
