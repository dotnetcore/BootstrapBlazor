// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class SingleUploadBase<TValue> : MultipleUploadBase<TValue>
{
    /// <summary>
    /// 获得/设置 是否仅上传一次 默认 false
    /// </summary>
    [Parameter]
    public bool IsSingle { get; set; }

    /// <summary>
    /// 是否显示上传组件
    /// </summary>
    protected bool CanUpload => !(IsSingle && GetUploadFiles().Count > 0);

    /// <summary>
    /// 获得当前图片集合
    /// </summary>
    /// <returns></returns>
    protected override List<UploadFile> GetUploadFiles()
    {
        var ret = new List<UploadFile>();
        if (IsSingle)
        {
            if (DefaultFileList?.Any() ?? false)
            {
                ret.Add(DefaultFileList.First());
            }
            if (ret.Count == 0 && UploadFiles.Any())
            {
                ret.Add(UploadFiles.First());
            }
        }
        else
        {
            if (DefaultFileList != null)
            {
                ret.AddRange(DefaultFileList);
            }
            ret.AddRange(UploadFiles);
        }
        return ret;
    }

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
            if (IsSingle)
            {
                UploadFiles.Clear();
            }
            else
            {
                UploadFiles.Remove(item);
            }
            if (!string.IsNullOrEmpty(item.ValidateId))
            {
                await JSRuntime.InvokeVoidAsync(null, "bb_tooltip", item.ValidateId, "dispose");
            }
            if (IsSingle)
            {
                DefaultFileList?.Clear();
            }
            else
            {
                DefaultFileList?.Remove(item);
            }
        }
        return ret;
    }
}
