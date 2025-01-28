// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// SingleUploadBase 基类
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

    /// <summary>
    /// 更新上传进度方法
    /// </summary>
    /// <param name="file"></param>
    protected void Update(UploadFile file)
    {
        if (GetShowProgress(file))
        {
            StateHasChanged();
        }
    }
}
