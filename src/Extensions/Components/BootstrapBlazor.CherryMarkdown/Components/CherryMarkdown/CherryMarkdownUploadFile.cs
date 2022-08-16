// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 文件信息
/// </summary>
public class CherryMarkdownUploadFile
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 最后修改日期
    /// </summary>
    public string? LastModified { get; set; }

    /// <summary>
    /// 文件类型
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// 上传的文件流
    /// </summary>
    public Stream? UploadStream { get; set; }

    /// <summary>
    /// 返回码，0为成功，非0失败
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> SaveToFile(string fileName, CancellationToken token = default)
    {
        var ret = false;
        if (UploadStream != null)
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
                using var uploadFile = File.OpenWrite(fileName);

                try
                {
                    // 打开文件流
                    var stream = UploadStream;

                    var buffer = new byte[4 * 1096];
                    int bytesRead = 0;

                    // 开始读取文件
                    while ((bytesRead = await stream.ReadAsync(buffer, token)) > 0)
                    {
                        await uploadFile.WriteAsync(buffer.AsMemory(0, bytesRead), token);

                    }
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
