// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">UploadFile 扩展方法类
///</para>
/// <para lang="en">UploadFile 扩展方法类
///</para>
/// </summary>
public static class UploadFileExtensions
{
    /// <summary>
    /// <para lang="zh">获取 base64 格式图片字符串
    ///</para>
    /// <para lang="en">获取 base64 格式图片字符串
    ///</para>
    /// </summary>
    /// <param name="upload"></param>
    /// <param name="format"></param>
    /// <param name="maxWidth"></param>
    /// <param name="maxHeight"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="allowExtensions"></param>
    /// <param name="token"></param>
    [ExcludeFromCodeCoverage]
    public static async Task RequestBase64ImageFileAsync(this UploadFile upload, string? format = null, int maxWidth = 320, int maxHeight = 240, long? maxAllowedSize = null, List<string>? allowExtensions = null, CancellationToken token = default)
    {
        if (upload.File != null)
        {
            try
            {
                format ??= upload.File.ContentType;
                if (upload.IsImage(allowExtensions))
                {
                    var imageFile = await upload.File.RequestImageFileAsync(format, maxWidth, maxHeight);

                    maxAllowedSize ??= upload.File.Size;
                    using var fileStream = imageFile.OpenReadStream(maxAllowedSize.Value, token);
                    using var memoryStream = new MemoryStream();
                    await fileStream.CopyToAsync(memoryStream, token);
                    upload.PrevUrl = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
                }
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
    /// <para lang="zh">保存到文件方法
    ///</para>
    /// <para lang="en">保存到文件方法
    ///</para>
    /// </summary>
    /// <param name="upload"></param>
    /// <param name="fileName"></param>
    /// <param name="maxAllowedSize"></param>
    /// <param name="bufferSize"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public static async Task<bool> SaveToFileAsync(this UploadFile upload, string fileName, long maxAllowedSize = 512000, int bufferSize = 64 * 1024, CancellationToken token = default)
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
                    var buffer = new byte[bufferSize];
                    int bytesRead = 0;
                    double totalRead = 0;

                    // 开始读取文件
                    while ((bytesRead = await stream.ReadAsync(buffer, token)) > 0)
                    {
                        totalRead += bytesRead;
                        await uploadFile.WriteAsync(buffer, 0, bytesRead, token);

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
    /// <para lang="zh">获得图片字节数组方法
    ///</para>
    /// <para lang="en">Gets图片字节数组方法
    ///</para>
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
    /// <para lang="zh">获得图片字节数组方法
    ///</para>
    /// <para lang="en">Gets图片字节数组方法
    ///</para>
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

    /// <summary>
    /// <para lang="zh">Check item 是否 is image extension method.
    ///</para>
    /// <para lang="en">Check item whether is image extension method.
    ///</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="allowExtensions"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static bool IsImage(this UploadFile item, List<string>? allowExtensions = null, Func<UploadFile, bool>? callback = null)
    {
        bool ret;
        if (callback != null)
        {
            ret = callback(item);
        }
        else if (item.File != null)
        {
            ret = item.File.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase) || item.IsAllowExtensions(allowExtensions);
        }
        else
        {
            ret = item.IsBase64Format() || item.IsAllowExtensions(allowExtensions);
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">Check item 是否 is base64 format image extension method.
    ///</para>
    /// <para lang="en">Check item whether is base64 format image extension method.
    ///</para>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool IsBase64Format(this UploadFile item) => !string.IsNullOrEmpty(item.PrevUrl) && item.PrevUrl.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// <para lang="zh">Check the extension 是否 in the allowExtensions list.
    ///</para>
    /// <para lang="en">Check the extension whether in the allowExtensions list.
    ///</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="allowExtensions"></param>
    /// <returns></returns>
    public static bool IsAllowExtensions(this UploadFile item, List<string>? allowExtensions = null)
    {
        var ret = false;
        allowExtensions ??= [".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp"];
        var fileName = item.File?.Name ?? item.FileName ?? item.PrevUrl;
        if (!string.IsNullOrEmpty(fileName))
        {
            ret = allowExtensions.Contains(Path.GetExtension(fileName), StringComparer.OrdinalIgnoreCase);
        }
        return ret;
    }
}
