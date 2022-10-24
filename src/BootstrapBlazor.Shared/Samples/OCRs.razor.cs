// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Ocr.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 光学字符识别 OCR
/// </summary>
public partial class OCRs
{
    string ocrStatus = "拍照";
    string? log;
    long maxFileSize = 1024 * 1024 * 15;
    [Inject] OcrService? OcrService { get; set; }
    List<string>? res { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    protected async Task OnChange(InputFileChangeEventArgs e)
    {
        ocrStatus = "识别中";
        if (e.File == null)
        {
            ocrStatus = "拍照";
            return;
        }
        try
        {
            using var stream = e.File.OpenReadStream(maxFileSize);
            ocrStatus = "识别中..";
            var res = await OcrService!.StartOcr(image: stream);
            ocrStatus = "处理资料";
            await OnResult(res);
            ocrStatus = "拍照";
        }
        catch
        {
        }
    }
    
    private Task OnStatus(string res)
    {
        log = res;
        System.Console.WriteLine(res);
        StateHasChanged();
        return Task.CompletedTask;

    }
    private Task OnResult(List<string> res)
    {
        this.res = res;
        StateHasChanged();
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem()
        {
            Name = "OnResult",
            Description = "签名结果回调方法",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "OnAlert",
            Description = "手写签名警告信息回调",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "OnError",
            Description = "错误回调方法",
            Type = "Func<string, Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "OnClose",
            Description = "手写签名关闭信息回调",
            Type = "Func<Task>?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
