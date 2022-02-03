// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 获得/设置 原始文件名
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
    /// 获取 base64 格式图片字符串
    /// </summary>
    /// <param name="format"></param>
    /// <param name="maxWidth"></param>
    /// <param name="maxHeight"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task RequestBase64ImageFileAsync(string format, int maxWidth, int maxHeight, long maxAllowedSize = 512000, CancellationToken token = default)
    {
        if (File != null)
        {
            try
            {
                var imageFile = await File.RequestImageFileAsync(format, maxWidth, maxHeight);
                using var fileStream = imageFile.OpenReadStream(maxAllowedSize, token);
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream, token);
                PrevUrl = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
                Uploaded = true;
            }
            catch (Exception ex)
            {
                Code = 1004;
                Error = ex.Message;
                PrevUrl = null;
            }
        }
    }

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> SaveToFile(string fileName, long maxAllowedSize = 512000, CancellationToken token = default)
    {
        var ret = false;
        if (File != null)
        {
            // 文件保护，如果文件存在则先删除
            if (System.IO.File.Exists(fileName))
            {
                try
                {
                    System.IO.File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    Code = 1002;
                    Error = ex.Message;
                }
            }

            var folder = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (Code == 0)
            {
                using var uploadFile = System.IO.File.OpenWrite(fileName);

                try
                {
                    // 打开文件流
                    var stream = File.OpenReadStream(maxAllowedSize, token);

                    var buffer = new byte[4 * 1096];
                    int bytesRead = 0;
                    double totalRead = 0;

                    // 开始读取文件
                    while ((bytesRead = await stream.ReadAsync(buffer, token)) > 0)
                    {
                        totalRead += bytesRead;
                        await uploadFile.WriteAsync(buffer.AsMemory(0, bytesRead), token);

                        if (UpdateCallback != null)
                        {
                            var percent = (int)((totalRead / File.Size) * 100);
                            if (percent > ProgressPercent)
                            {
                                ProgressPercent = percent;
                                UpdateCallback(this);
                            }
                        }
                    }
                    Uploaded = true;
                    ret = true;
                }
                catch (Exception ex)
                {
                    Code = 1003;
                    Error = ex.Message;
                }
            }
        }
        return ret;
    }
}
