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
    [Obsolete("已弃用 通过 IsMultiple 参数实现此功能; Deprecated. please use IsMultiple parameter.")]
    [ExcludeFromCodeCoverage]
    public bool IsSingle { get; set; }

    /// <summary>
    /// 获得/设置 最大上传个数 默认为最大值 <see cref="int.MaxValue"/>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用 通过 MaxFileCount 参数实现此功能; Deprecated. please use MaxFileCount parameter.")]
    [ExcludeFromCodeCoverage]
    public int Max { get; set; } = int.MaxValue;

    /// <summary>
    /// 获得/设置 最大上传个数 默认为 null
    /// </summary>
    [Parameter]
    public int? MaxFileCount { get; set; }

    /// <summary>
    /// 获得/设置 所有文件上传完毕回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<IReadOnlyCollection<UploadFile>, Task>? OnAllFileUploaded { get; set; }

    /// <summary>
    /// 获得/设置 已上传文件集合，可用于组件初始化
    /// </summary>
    [Parameter]
    public List<UploadFile>? DefaultFileList { get; set; }

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
    /// 获得/设置 是否上传整个目录 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDirectory { get; set; }

    /// <summary>
    /// 获得/设置 是否允许多文件上传 默认 false 不允许
    /// </summary>
    [Parameter]
    public bool IsMultiple { get; set; }

    /// <summary>
    /// 获得/设置 点击删除按钮时回调此方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task<bool>>? OnDelete { get; set; }

    /// <summary>
    /// 获得/设置 点击浏览按钮时回调此方法，如果多文件上传此回调会触发多次 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnChange { get; set; }

    /// <summary>
    /// 获得/设置 已上传文件集合，此集合中数据是用户上传文件集合
    /// </summary>
    public List<UploadFile> UploadFiles { get; } = [];

    /// <summary>
    /// Gets the collection of files to be uploaded.
    /// </summary>
    protected List<UploadFile> Files => GetUploadFiles();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? FormatValueAsString(TValue? value)
    {
        if (value is null)
        {
            return null;
        }
        else if (value is IEnumerable<IBrowserFile> files)
        {
            return string.Join(";", files.Select(i => i.Name));
        }
        else if (value is IBrowserFile file)
        {
            return file.Name;
        }
        else if (value is IEnumerable<string> strings)
        {
            return string.Join(";", strings);
        }
        else
        {
            return base.FormatValueAsString(value);
        }
    }

    /// <summary>
    /// User selects files callback method
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected async Task OnFileChange(InputFileChangeEventArgs args)
    {
        var fileCount = MaxFileCount ?? args.FileCount;
        var items = args.GetMultipleFiles(fileCount).Select(f =>
        {
            var file = new UploadFile()
            {
                OriginFileName = f.Name,
                Size = f.Size,
                File = f,
                FileCount = args.FileCount,
                Uploaded = false,
                UpdateCallback = Update
            };
            file.ValidateId = $"{Id}_{file.GetHashCode()}";
            return file;
        }).ToList();

        foreach (var item in items)
        {
            UploadFiles.Add(item);

            // trigger OnChange event callback
            // 回调给用户，用于存储文件并生成预览地址给 PreUrl
            if (OnChange != null)
            {
                await OnChange(item);
            }
            item.Uploaded = true;
            StateHasChanged();
        }

        // trigger OnAllFileUploaded event callback
        if (OnAllFileUploaded != null)
        {
            await OnAllFileUploaded(items);
        }

        if (ValueType.IsAssignableTo(typeof(IEnumerable<IBrowserFile>)))
        {
            CurrentValue = (TValue)(object)items.Select(f => f.File).ToList();
        }
        else if (ValueType.IsAssignableTo(typeof(IEnumerable<string>)))
        {
            CurrentValue = (TValue)(object)string.Join(";", items.Select(f => f.OriginFileName)).ToList();
        }
        else if (ValueType == typeof(IBrowserFile))
        {
            CurrentValue = (TValue)items[0].File!;
        }
        else if (ValueType == typeof(string))
        {
            CurrentValue = (TValue)(object)string.Join(";", items.Select(f => f.OriginFileName));
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
            if (!string.IsNullOrEmpty(item.ValidateId))
            {
                await RemoveValidResult(item.ValidateId);
            }
            UploadFiles.Remove(item);
            DefaultFileList?.Remove(item);
        }
        StateHasChanged();
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

    private List<UploadFile>? _filesCache;
    /// <summary>
    /// Get the files collection.
    /// 获得当前文件集合
    /// </summary>
    /// <returns></returns>
    protected List<UploadFile> GetUploadFiles()
    {
        if (_filesCache == null)
        {
            _filesCache = [];
            if (DefaultFileList != null)
            {
                _filesCache.AddRange(DefaultFileList);
            }
            _filesCache.AddRange(UploadFiles);
        }
        return _filesCache;
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="results"></param>
    public override void ToggleMessage(IEnumerable<ValidationResult> results)
    {
        if (FieldIdentifier != null)
        {
            var messages = results.Where(item => item.MemberNames.Any(m => m == FieldIdentifier.Value.FieldName)).ToList();
            if (messages.Count == 0)
            {
                messages = results.Where(item => item.MemberNames.Any(m =>
                        UploadFiles.Any(f => f.ValidateId?.Equals(m, StringComparison.OrdinalIgnoreCase) ?? false)))
                    .ToList();
            }
            if (messages.Count > 0)
            {
                ErrorMessage = messages.First().ErrorMessage;
                IsValid = false;
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
    /// append html attribute method.
    /// </summary>
    /// <returns></returns>
    protected IDictionary<string, object> GetUploadAdditionalAttributes()
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
        if (IsMultiple)
        {
            ret.Add("multiple", "multiple");
        }
        if (IsDirectory)
        {
            ret.Add("directory", "directory");
            ret.Add("webkitdirectory", "webkitdirectory");
        }
        return ret;
    }
}
