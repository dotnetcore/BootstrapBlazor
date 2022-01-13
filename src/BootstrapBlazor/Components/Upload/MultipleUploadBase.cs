// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public abstract class MultipleUploadBase<TValue> : UploadBase<TValue>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string? GetItemClassString(UploadFile item) => CssBuilder.Default(ItemClassString)
        .AddClass("is-valid", item.Uploaded && item.Code == 0)
        .AddClass("is-invalid", item.Code != 0)
        .AddClass("disabled", IsDisabled)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    protected virtual string? ItemClassString => CssBuilder.Default("upload-item")
        .Build();

    /// <summary>
    /// 获得/设置 已上传文件集合
    /// </summary>
    [Parameter]
    public List<UploadFile>? DefaultFileList { get; set; }

    /// <summary>
    /// 获得/设置 是否显示上传进度 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowProgress { get; set; }

    /// <summary>
    /// OnFileDelete 回调委托
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected override async Task<bool> OnFileDelete(UploadFile item)
    {
        var ret = await base.OnFileDelete(item);
        if (ret && item != null)
        {
            UploadFiles.Remove(item);
            if (!string.IsNullOrEmpty(item.ValidateId))
            {
                await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", item.ValidateId, "dispose");
            }
            DefaultFileList?.Remove(item);
        }
        return ret;
    }

    /// <summary>
    /// 是否显示进度条方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected bool GetShowProgress(UploadFile item) => ShowProgress && !item.Uploaded;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual List<UploadFile> GetUploadFiles() => DefaultFileList == null ? UploadFiles : DefaultFileList.Concat(UploadFiles).ToList();

    /// <summary>
    /// 清空上传列表方法
    /// </summary>
    public override void Reset()
    {
        DefaultFileList?.Clear();
        base.Reset();
    }
}
