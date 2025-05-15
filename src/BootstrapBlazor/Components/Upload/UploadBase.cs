// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// Upload 组件基类
/// </summary>
[BootstrapModuleAutoLoader(ModuleName = "upload")]
public abstract class UploadBase<TValue> : ValidateBase<TValue>, IUpload
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
    /// 获得/设置 所有文件上传完毕回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<IReadOnlyCollection<UploadFile>, Task>? OnAllFileUploaded { get; set; }

    /// <summary>
    /// 获得/设置 已上传文件集合
    /// </summary>
    [Parameter]
    public List<UploadFile>? DefaultFileList { get; set; }

    /// <summary>
    /// 获得/设置 当前上传文件
    /// </summary>
    protected UploadFile? CurrentFile { get; set; }

    /// <summary>
    /// 获得/设置 上传文件集合
    /// </summary>
    protected List<UploadFile> UploadFiles { get; set; } = [];

    List<UploadFile> IUpload.UploadFiles { get => UploadFiles; }

    /// <summary>
    /// 获得/设置 是否显示上传进度 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowProgress { get; set; }

    /// <summary>
    /// 获得/设置 上传接收的文件格式 默认为 null 接收任意格式
    /// </summary>
    [Parameter]
    public string? Accept { get; set; }

    /// <summary>
    /// 获得/设置 媒体捕获机制的首选面向模式，默认为 null
    /// </summary>
    [Parameter]
    public string? Capture { get; set; }

    /// <summary>
    /// 获得/设置 点击删除按钮时回调此方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task<bool>>? OnDelete { get; set; }

    /// <summary>
    /// 获得/设置 点击浏览按钮时回调此方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnChange { get; set; }

    /// <summary>
    /// 显示/隐藏验证结果方法
    /// </summary>
    /// <param name="results"></param>
    public override void ToggleMessage(IEnumerable<ValidationResult> results)
    {
        if (FieldIdentifier != null)
        {
            var messages = results.Where(item => item.MemberNames.Any(m => UploadFiles.Any(f => f.ValidateId?.Equals(m, StringComparison.OrdinalIgnoreCase) ?? false)));
            if (messages.Any())
            {
                IsValid = false;
                if (CurrentFile != null)
                {
                    var msg = messages.FirstOrDefault(m => m.MemberNames.Any(f => f.Equals(CurrentFile.ValidateId, StringComparison.OrdinalIgnoreCase)));
                    if (msg != null)
                    {
                        ErrorMessage = msg.ErrorMessage;
                    }
                }
            }
            else
            {
                ErrorMessage = null;
                IsValid = true;
            }
            OnValidate(IsValid);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected virtual async Task OnFileChange(InputFileChangeEventArgs args)
    {
        // init UploadFiles
        var items = args.GetMultipleFiles(args.FileCount).Select(f => new UploadFile()
        {
            OriginFileName = f.Name,
            Size = f.Size,
            File = f,
            FileCount = args.FileCount,
            Uploaded = OnChange == null,
            UpdateCallback = Update
        });
        UploadFiles.AddRange(items);

        // trigger OnChange event callback
        if (OnChange != null)
        {
            foreach (var item in items)
            {
                await OnChange(item);
                item.Uploaded = true;
            }
            StateHasChanged();
        }

        // trigger OnAllFileUploaded event callback
        if (OnAllFileUploaded != null)
        {
            await OnAllFileUploaded(UploadFiles);
        }

        var type = NullableUnderlyingType ?? typeof(TValue);
        if (type.IsAssignableTo(typeof(List<IBrowserFile>)))
        {
            CurrentValue = (TValue)(object)UploadFiles.Select(f => f.File).ToList();
        }
    }

    /// <summary>
    /// Delete file method.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected virtual async Task<bool> OnFileDelete(UploadFile item)
    {
        var ret = true;
        if (OnDelete != null)
        {
            ret = await OnDelete(item);
        }
        ErrorMessage = null;

        if (ret)
        {
            UploadFiles.Remove(item);
            if (!string.IsNullOrEmpty(item.ValidateId))
            {
                await RemoveValidResult(item.ValidateId);
            }
            DefaultFileList?.Remove(item);
        }
        return ret;

    }

    /// <summary>
    /// append html attribute method.
    /// </summary>
    /// <returns></returns>
    protected virtual IDictionary<string, object> GetUploadAdditionalAttributes()
    {
        var ret = new Dictionary<string, object>
        {
            { "hidden", "hidden" }
        };
        if (!string.IsNullOrEmpty(Accept))
        {
            ret.Add("accept", Accept);
        }
        if (!string.IsNullOrEmpty(Capture))
        {
            ret.Add("capture", Capture);
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
    /// 清空上传列表方法
    /// </summary>
    public virtual void Reset()
    {
        DefaultFileList?.Clear();
        UploadFiles.Clear();
        StateHasChanged();
    }
}
