// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 获得/设置 最大上传个数 默认为最大值 <see cref="int.MaxValue"/>
    /// </summary>
    [Parameter]
    public int Max { get; set; } = int.MaxValue;

    /// <summary>
    /// 是否显示上传组件
    /// </summary>
    protected bool CheckCanUpload()
    {
        var count = GetUploadFiles().Count;
        return IsSingle ? count < 1 : count < Max;
    }

    /// <summary>
    /// 获得当前图片集合
    /// </summary>
    /// <returns></returns>
    protected virtual List<UploadFile> GetUploadFiles()
    {
        var ret = new List<UploadFile>();
        if (IsSingle)
        {
            if (DefaultFileList != null && DefaultFileList.Count != 0)
            {
                ret.Add(DefaultFileList.First());
            }
            if (ret.Count == 0 && UploadFiles.Count != 0)
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
        if (ret)
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
                await RemoveValidResult(item.ValidateId);
            }
            RemoveItem();
        }

        void RemoveItem()
        {
            if (DefaultFileList != null)
            {
                if (IsSingle)
                {
                    DefaultFileList.Clear();
                }
                else
                {
                    DefaultFileList.Remove(item);
                }
            }
        }
        return ret;
    }
}
