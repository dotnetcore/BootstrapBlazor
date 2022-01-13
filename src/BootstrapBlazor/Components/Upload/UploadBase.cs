// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// Upload 组件基类
/// </summary>
public abstract class UploadBase<TValue> : ValidateBase<TValue>, IUpload
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 Upload 组件实例
    /// </summary>
    protected ElementReference UploaderElement { get; set; }

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
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && !IsDisabled)
        {
            await JSRuntime.InvokeVoidAsync(UploaderElement, "bb_upload");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected static string? GetFileName(UploadFile? item = null) => item?.OriginFileName ?? item?.FileName;

    /// <summary>
    /// 触发客户端验证方法
    /// </summary>
    protected void ValidateFile()
    {
        if (ValidateForm != null && EditContext != null && FieldIdentifier.HasValue)
        {
            EditContext.NotifyFieldChanged(FieldIdentifier.Value);
        }
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
            if (messages.Any() && CurrentFile != null)
            {
                ErrorMessage = messages.FirstOrDefault(m => m.MemberNames.Any(f => f.Equals(CurrentFile.ValidateId, StringComparison.OrdinalIgnoreCase)))?.ErrorMessage;
                IsValid = string.IsNullOrEmpty(ErrorMessage);

                if (IsValid.HasValue && !IsValid.Value)
                {
                    TooltipMethod = validProperty ? "show" : "enable";
                }
            }
            else
            {
                ErrorMessage = null;
                IsValid = true;
                TooltipMethod = "dispose";
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
            CurrentValue = (TValue)(object)UploadFiles;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual Task OnFileBrowser() => Task.CompletedTask;

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
