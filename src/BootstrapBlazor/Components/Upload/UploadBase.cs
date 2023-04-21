// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// Upload 组件基类
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor/modules/upload.js", Relative = false)]
public abstract class UploadBase<TValue> : ValidateBase<TValue>, IUpload
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 
    /// </summary>
    protected UploadFile? CurrentFile { get; set; }

    /// <summary>
    /// 获得/设置 上传文件集合
    /// </summary>
    protected List<UploadFile> UploadFiles { get; } = new List<UploadFile>();

    List<UploadFile> IUpload.UploadFiles { get => UploadFiles; }

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
    /// 获得/设置 点击删除按钮时回调此方法
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task<bool>>? OnDelete { get; set; }

    /// <summary>
    /// 获得/设置 点击浏览按钮时回调此方法
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnChange { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        Inherit = true;

        base.OnInitialized();
    }

    /// <summary>
    /// 显示/隐藏验证结果方法
    /// </summary>
    /// <param name="results"></param>
    /// <param name="validProperty">是否对本属性进行数据验证</param>
    public override void ToggleMessage(IEnumerable<ValidationResult> results, bool validProperty)
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
    /// 
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
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected virtual Task OnFileChange(InputFileChangeEventArgs args)
    {
        // 判定可为空
        var type = NullableUnderlyingType ?? typeof(TValue);
        if (type.IsAssignableTo(typeof(IBrowserFile)))
        {
            CurrentValue = (TValue)args.File;
        }
        if (type.IsAssignableTo(typeof(List<IBrowserFile>)))
        {
            CurrentValue = (TValue)(object)UploadFiles.Select(f => f.File).ToList();
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
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
    /// 清空上传列表方法
    /// </summary>
    public virtual void Reset()
    {
        UploadFiles.Clear();
        StateHasChanged();
    }
}
