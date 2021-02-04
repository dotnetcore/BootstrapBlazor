// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Upload 组件基类
    /// </summary>
    public abstract class UploadBase : ValidateBase<IBrowserFile>
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
        /// 获得/设置 上传接收的文件格式 默认为 null 接收任意格式
        /// </summary>
        [Parameter]
        public string? Accept { get; set; }

        /// <summary>
        /// 获得/设置 点击删除按钮时回调此方法
        /// </summary>
        [Parameter]
        public Func<string, Task<bool>>? OnDelete { get; set; }

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

            if (firstRender)
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
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual async Task<bool> OnFileDelete(UploadFile item)
        {
            var ret = true;
            if (OnDelete != null)
            {
                var fileName = item.OriginFileName ?? item.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    ret = await OnDelete(fileName);
                }
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected abstract Task OnFileChange(InputFileChangeEventArgs args);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnFileBrowser()
        {
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
            if (!string.IsNullOrEmpty(Accept)) ret.Add("accept", Accept);
            return ret;
        }
    }
}
