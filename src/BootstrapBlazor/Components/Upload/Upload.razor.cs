// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Upload
    {
        [NotNull]
        private string? FileTooLargeText { get; set; }

        [NotNull]
        private string? AllowFileTypeErrorMessage { get; set; }

        private JSInterop<UploadBase>? Interop { get; set; }

        private ElementReference UploaderElement { get; set; }

        private string? PreviewClassString => CssBuilder.Default("upload-prev")
            .AddClass("is-load is-upload is-valid", !string.IsNullOrEmpty(ImageUrl))
            .Build();

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("upload")
            .AddClass("is-circle", IsCircle)
            .AddClass("is-prev", ShowPreview)
            .AddClass("is-wall", IsPhotoWall)
            .AddClass("is-card", IsCard)
            .AddClass("is-stack", IsStack)
            .AddClass("is-progress", ShowProgress)
            .AddClass("is-disabled", IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 预览框 Style 属性
        /// </summary>
        private string? PrevStyleString => CssBuilder.Default()
            .AddClass($"width: {Width}px;", !IsStack && Width > 0)
            .AddClass($"height: {Height}px;", !IsStack && Height > 0 && !IsCircle)
            .AddClass($"height: {Width}px;", !IsStack && IsCircle)
            .Build();

        /// <summary>
        /// 获得/设置 圆形进度半径
        /// </summary>
        private string CircleDiameter => $"{Width / 2}";

        /// <summary>
        /// 获得/设置 半径
        /// </summary>
        private string CircleR => $"{Width / 2 - 2}";

        /// <summary>
        /// 获得 圆形周长
        /// </summary>
        private string CircleLength => $"{Math.Round(Width * Math.PI, 2)}";

        /// <summary>
        /// 获得 是否允许多文件上传 默认不允许 IsStack 模式下允许多文件上传
        /// </summary>
        private string? MultipleString => (IsMultiple || IsStack) ? "multiple" : null;

        /// <summary>
        /// 获得 组件是否被禁用属性值
        /// </summary>
        private string? DisabledString => IsDisabled ? "disabled" : null;

        private string? RemoveButtonClassString => CssBuilder.Default("btn")
            .AddClass(DeleteButtonClass)
            .Build();

        private string? UploadButtonClassString => CssBuilder.Default("btn")
            .AddClass(UploadButtonClass)
            .Build();

        private string? BrowserButtonClassString => CssBuilder.Default("btn btn-browser")
            .AddClass(BrowserButtonClass)
            .Build();

        private bool IsDeleteButtonDisabled => IsDisabled || string.IsNullOrEmpty(FileName);

        /// <summary>
        /// 获得/设置 上传组件模式 默认为 Normal 正常模式多用于表单中
        /// </summary>
        [Parameter]
        public UploadStyle Style { get; set; }

        /// <summary>
        /// 获得/设置 是否显示删除按钮 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowDeleteButton { get; set; }

        /// <summary>
        /// 获得/设置 PlaceHolder 占位符文本
        /// </summary>
        [Parameter]
        public string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 删除按钮样式 默认 btn-danger
        /// </summary>
        [Parameter]
        public string DeleteButtonClass { get; set; } = "btn-danger";

        /// <summary>
        /// 获得/设置 浏览按钮样式 默认 btn-success
        /// </summary>
        [Parameter]
        public string UploadButtonClass { get; set; } = "btn-success";

        /// <summary>
        /// 获得/设置 上传按钮样式 默认 btn-primary
        /// </summary>
        [Parameter]
        public string BrowserButtonClass { get; set; } = "btn-primary";

        /// <summary>
        /// 获得/设置 删除按钮图标 默认 fa fa-trash-o
        /// </summary>
        [Parameter]
        public string DeleteButtonIcon { get; set; } = "fa fa-trash-o";

        /// <summary>
        /// 获得/设置 上传按钮图标 默认 fa fa-cloud-upload
        /// </summary>
        [Parameter]
        public string UploadButtonIcon { get; set; } = "fa fa-cloud-upload";

        /// <summary>
        /// 获得/设置 浏览按钮图标 默认 fa fa-folder-open-o
        /// </summary>
        [Parameter]
        public string BrowserButtonIcon { get; set; } = "fa fa-folder-open-o";

        /// <summary>
        /// 获得/设置 上传按钮图标
        /// </summary>
        [Parameter]
        public string Icon { get; set; } = "fa fa-cloud-upload";

        /// <summary>
        /// 获得/设置 上传文件名
        /// </summary>
        [Parameter]
        public string? FileName { get; set; }

        /// <summary>
        /// 获得/设置 是否显示预览 默认不预览
        /// </summary>
        [Parameter]
        public bool ShowPreview { get; set; }

        /// <summary>
        /// 获得/设置 是否为堆砌效果 默认不预览
        /// </summary>
        [Parameter]
        public bool IsStack { get; set; }

        /// <summary>
        /// 获得/设置 是否显示上传进度 默认不显示
        /// </summary>
        [Parameter]
        public bool ShowProgress { get; set; }

        /// <summary>
        /// 获得/设置 是否显示重置按钮
        /// </summary>
        [Parameter]
        public bool ShowReset { get; set; }

        /// <summary>
        /// 获得/设置 文件预览框宽度
        /// </summary>
        [Parameter]
        public int Width { get; set; } = 100;

        /// <summary>
        /// 获得/设置 文件预览框高度
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 100;

        /// <summary>
        /// 获得/设置 是否允许多文件上传 默认不允许
        /// </summary>
        [Parameter]
        public bool IsMultiple { get; set; }

        /// <summary>
        /// 获得/设置 是否圆形图片框 默认为 false
        /// </summary>
        [Parameter]
        public bool IsCircle { get; set; }

        /// <summary>
        /// 获得/设置 是否卡片式预览 默认为 false
        /// </summary>
        [Parameter]
        public bool IsCard { get; set; }

        /// <summary>
        /// 获得/设置 是否为照片墙效果 默认为 false
        /// </summary>
        [Parameter]
        public bool IsPhotoWall { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 允许上传文件扩展名集合
        /// </summary>
        [Parameter]
        public string? AllowFileType { get; set; }

        /// <summary>
        /// 获得/设置 允许上传文件最大值 默认为 0 不限制
        /// </summary>
        [Parameter]
        public int MaxFileLength { get; set; }

        /// <summary>
        /// 获得/设置 上传失败后回调委托
        /// </summary>
        [Parameter]
        public Func<InputFileChangeEventArgs, Task<string>>? OnChange { get; set; }

        /// <summary>
        /// 获得/设置 点击删除按钮时回调此方法
        /// </summary>
        [Parameter]
        public Func<string, Task<bool>>? OnDelete { get; set; }

        /// <summary>
        /// 获得/设置 上传按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? UploadButtonText { get; set; }

        /// <summary>
        /// 获得/设置 重置按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ResetText { get; set; }

        /// <summary>
        /// 获得/设置 重置按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? DeleteButtonText { get; set; }

        /// <summary>
        /// 获得/设置 浏览按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? BrowserButtonText { get; set; }

        /// <summary>
        /// 获得/设置 默认初始化图片地址
        /// </summary>
        [Parameter]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private IStringLocalizer<Upload>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DeleteButtonText ??= Localizer[nameof(DeleteButtonText)];
            BrowserButtonText ??= Localizer[nameof(BrowserButtonText)];
            UploadButtonText ??= Localizer[nameof(UploadButtonText)];
            ResetText ??= Localizer[nameof(ResetText)];
            FileTooLargeText ??= Localizer[nameof(FileTooLargeText)];
            AllowFileTypeErrorMessage ??= Localizer[nameof(AllowFileTypeErrorMessage)];
        }

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

        private async Task OnFileDelete()
        {
            if (OnDelete != null && !string.IsNullOrEmpty(FileName))
            {
                var ret = await OnDelete(FileName);
                if (ret)
                {
                    FileName = null;
                }
            }
        }

        private Task OnFileBrowser()
        {
            FileName = "";
            return Task.CompletedTask;
        }

        private async Task OnFileChange(InputFileChangeEventArgs args)
        {
            FileName = args.File.Name;

            if (OnChange == null)
            {
                var format = args.File.ContentType;
                var imageFile = await args.File.RequestImageFileAsync(format, 640, 480);

                using var fileStream = imageFile.OpenReadStream();
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);

                ImageUrl = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
            }
            else
            {
                ImageUrl = await OnChange(args);
            }
        }

        /// <summary>
        /// 组件复位方法
        /// </summary>
        public EventCallback<MouseEventArgs> Reset() => EventCallback.Factory.Create<MouseEventArgs>(this, async () => await JSRuntime.InvokeVoidAsync(UploaderElement, "uploader", nameof(Reset)));

        /// <summary>
        /// 文件上传前检查文件扩展名时回调此方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public object CheckFiles(string fileName, string fileType, long fileSize)
        {
            var result = true;
            string? message = null;

            if (MaxFileLength > 0)
            {
                result = MaxFileLength > fileSize;
                message = result ? null : FileTooLargeText;
            }

            if (result)
            {
                // check file extensions
                if (AllowFileType?.Contains("image", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    result = fileType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
                    message = result ? null : AllowFileTypeErrorMessage;
                }
            }

            return new { result, message };
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) Interop?.Dispose();
        }
    }
}
