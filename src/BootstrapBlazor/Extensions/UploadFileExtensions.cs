// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// UploadFile 扩展方法类
/// </summary>
public static class UploadFileExtensions
{
    /// <summary>
    /// 获取 base64 格式图片字符串
    /// </summary>
    /// <param name="upload"></param>
    /// <param name="format"></param>
    /// <param name="maxWidth"></param>
    /// <param name="maxHeight"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="token"></param>
    [ExcludeFromCodeCoverage]
    public static async Task RequestBase64ImageFileAsync(this UploadFile upload, string format, int maxWidth, int maxHeight, long maxAllowedSize = 512000, CancellationToken token = default)
    {
        if (upload.File != null)
        {
            try
            {
                var imageFile = await upload.File.RequestImageFileAsync(format, maxWidth, maxHeight);
                using var fileStream = imageFile.OpenReadStream(maxAllowedSize, token);
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream, token);
                upload.PrevUrl = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
                upload.Uploaded = true;
            }
            catch (Exception ex)
            {
                upload.Code = 1004;
                upload.PrevUrl = null;
                upload.Error = ex.Message;
            }
        }
    }

    /// <summary>
    /// 保存到文件方法
    /// </summary>
    /// <param name="upload"></param>
    /// <param name="fileName"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public static async Task<bool> SaveToFileAsync(this UploadFile upload, string fileName, long maxAllowedSize = 512000, CancellationToken token = default)
    {
        var ret = false;
        if (upload.File != null)
        {
            // 文件保护，如果文件存在则先删除
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    upload.Code = 1002;
                    upload.Error = ex.Message;
                }
            }

            if (upload.Code == 0)
            {
                var folder = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                using var uploadFile = File.OpenWrite(fileName);
                try
                {
                    // 打开文件流
                    var stream = upload.File.OpenReadStream(maxAllowedSize, token);
                    var buffer = new byte[4 * 1096];
                    int bytesRead = 0;
                    double totalRead = 0;

                    // 开始读取文件
                    while ((bytesRead = await stream.ReadAsync(buffer, token)) > 0)
                    {
                        totalRead += bytesRead;
                        await uploadFile.WriteAsync(buffer.AsMemory(0, bytesRead), token);

                        if (upload.UpdateCallback != null)
                        {
                            var percent = (int)((totalRead / upload.File.Size) * 100);
                            if (percent > upload.ProgressPercent)
                            {
                                upload.ProgressPercent = percent;
                                upload.UpdateCallback(upload);
                            }
                        }
                    }
                    upload.Uploaded = true;
                    ret = true;
                }
                catch (Exception ex)
                {
                    upload.Code = 1003;
                    upload.Error = ex.Message;
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// 获得图片字节数组方法
    /// </summary>
    /// <param name="upload"></param>
    /// <param name="format"></param>
    /// <param name="maxWidth"></param>
    /// <param name="maxHeight"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public static async Task<byte[]?> GetBytesAsync(this UploadFile upload, string format, int maxWidth, int maxHeight, long maxAllowedSize = 512000, CancellationToken token = default)
    {
        byte[]? ret = null;
        if (upload.File != null)
        {
            try
            {
                var imageFile = await upload.File.RequestImageFileAsync(format, maxWidth, maxHeight);
                using var fileStream = imageFile.OpenReadStream(maxAllowedSize, token);
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream, token);
                ret = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                upload.Code = 1004;
                upload.Error = ex.Message;
                upload.PrevUrl = null;
            }
        }
        return ret;
    }

    /// <summary>
    /// 获得图片字节数组方法
    /// </summary>
    /// <param name="upload"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public static async Task<byte[]?> GetBytesAsync(this UploadFile upload, long maxAllowedSize = 512000, CancellationToken token = default)
    {
        byte[]? ret = null;
        if (upload.File != null)
        {
            try
            {
                using var fileStream = upload.File.OpenReadStream(maxAllowedSize, token);
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream, token);
                ret = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                upload.Code = 1004;
                upload.Error = ex.Message;
                upload.PrevUrl = null;
            }
        }
        return ret;
    }
}
