// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Upload
    {
        private ElementReference UploaderElement { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("upload")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 预览框 Style 属性
        /// </summary>
        private string? PrevStyleString => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width > 0)
            .AddClass($"height: {Height}px;", Height > 0 && !IsCircle)
            .AddClass($"height: {Width}px;", IsCircle)
            .Build();

        private string? GetUploadItemClassString(UploadFile item) => CssBuilder.Default("upload-item")
            .AddClass("is-valid", item.Code == 0 && item.Uploaded)
            .AddClass("is-invalid", item.Code != 0)
            .AddClass("is-circle", IsCircle && Style == UploadStyle.Avatar)
            .Build();

        private static bool IsImage(UploadFile item)
        {
            return item.File?.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase)
                ?? Path.GetExtension(item.OriginFileName ?? item.FileName)?.ToLowerInvariant() switch
                {
                    ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" => true,
                    _ => false
                };
        }

        private string? GetFileFormatClassString(UploadFile item)
        {
            var builder = CssBuilder.Default("fa");
            var fileExtension = Path.GetExtension(item.OriginFileName ?? item.FileName)?.ToLowerInvariant() ?? "";
            string icon = OnGetFileFormat?.Invoke(fileExtension) ?? fileExtension switch
            {
                ".csv" or ".xls" or ".xlsx" => "fa-file-excel-o",
                ".doc" or ".docx" or ".dot" or ".dotx" => "fa-file-word-o",
                ".ppt" or ".pptx" => "fa-file-powerpoint-o",
                ".wav" or ".mp3" => "fa-file-audio-o",
                ".mp4" or ".mov" or ".mkv" => "fa-file-video-o",
                ".cs" or ".html" or ".vb" => "fa-file-code-o",
                ".pdf" => "fa-file-pdf-o",
                ".zip" or ".rar" or ".iso" => "fa-file-archive-o",
                ".txt" or ".log" or ".iso" => "fa-file-text-o",
                ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" => "fa-file-image-o",
                _ => "fa-file-o"
            };
            builder.AddClass(icon);
            return builder.Build();
        }

        private string? GetUploadItemClassString() => CssBuilder.Default("upload-item")
            .AddClass("is-circle", IsCircle && Style == UploadStyle.Avatar)
            .Build();

        private static string? GetDiabledString(UploadFile item) => (item.Uploaded && item.Code == 0) ? null : "disabled";

        private static string? GetDeleteButtonDiabledString(UploadFile item) => (item.Uploaded) ? null : "disabled";

        private string? RemoveButtonClassString => CssBuilder.Default("btn")
            .AddClass(DeleteButtonClass)
            .Build();

        private string? BrowserButtonClassString => CssBuilder.Default("btn btn-browser")
            .AddClass(BrowserButtonClass)
            .Build();

        private bool IsDeleteButtonDisabled => IsDisabled || !UploadFiles.Any();

        private bool IsUploadButtonDisabled => IsSingle && UploadFiles.Any();

        private string? GetFileName(UploadFile? item = null)
        {
            var file = item ?? UploadFiles.FirstOrDefault();
            return file?.OriginFileName ?? file?.FileName;
        }

        [NotNull]
        private List<UploadFile>? UploadFiles { get; set; }

        /// <summary>
        /// 获得/设置 设置文件格式图标回调委托
        /// </summary>
        [Parameter]
        public Func<string, string>? OnGetFileFormat { get; set; }

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
        /// 获得/设置 上传接收的文件格式 默认为 null 接收任意格式
        /// </summary>
        [Parameter]
        public string? Accept { get; set; }

        /// <summary>
        /// 获得/设置 删除按钮样式 默认 btn-danger
        /// </summary>
        [Parameter]
        public string DeleteButtonClass { get; set; } = "btn-danger";

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
        /// 获得/设置 浏览按钮图标 默认 fa fa-folder-open-o
        /// </summary>
        [Parameter]
        public string BrowserButtonIcon { get; set; } = "fa fa-folder-open-o";

        /// <summary>
        /// 获得/设置 已上传文件集合
        /// </summary>
        [Parameter]
        [NotNull]
        public List<UploadFile>? DefaultFileList { get; set; }

        /// <summary>
        /// 获得/设置 最大上传文件数量 默认为 10
        /// </summary>
        [Parameter]
        public int MaxFileCount { get; set; } = 10;

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
        /// 获得/设置 是否允许多文件上传 默认 false 不允许
        /// </summary>
        [Parameter]
        public bool IsMultiple { get; set; }

        /// <summary>
        /// 获得/设置 是否仅上传一次 默认 false
        /// </summary>
        [Parameter]
        public bool IsSingle { get; set; }

        /// <summary>
        /// 获得/设置 是否圆形图片框 Avatar 模式时生效 默认为 false
        /// </summary>
        [Parameter]
        public bool IsCircle { get; set; }

        /// <summary>
        /// 获得/设置 是否上传整个目录 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDirectory { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 是否显示上传进度 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowProgress { get; set; }

        /// <summary>
        /// 获得/设置 上传失败后回调委托
        /// </summary>
        [Parameter]
        public Func<IEnumerable<UploadFile>, Task>? OnChange { get; set; }

        /// <summary>
        /// 获得/设置 点击删除按钮时回调此方法
        /// </summary>
        [Parameter]
        public Func<string, Task<bool>>? OnDelete { get; set; }

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

            if (Style != UploadStyle.Normal)
            {
                UploadFiles ??= new List<UploadFile>();
            }

            UploadFiles ??= new List<UploadFile>();

            if (DefaultFileList != null) UploadFiles.AddRange(DefaultFileList);

            // 上传文件夹时 开启 Multiple 属性
            if (IsDirectory) IsMultiple = true;
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

        private async Task OnFileDelete(UploadFile? item)
        {
            if (OnDelete != null && item != null)
            {
                var fileName = item.OriginFileName ?? item.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    var ret = await OnDelete(fileName);
                    if (ret)
                    {
                        UploadFiles.Remove(item);
                    }
                }
            }
        }

        private Task OnFileBrowser()
        {
            UploadFiles.Clear();
            return Task.CompletedTask;
        }

        private async Task OnFileChange(InputFileChangeEventArgs args)
        {
            var files = new List<UploadFile>();
            if (IsMultiple)
            {
                files.AddRange(args.GetMultipleFiles(MaxFileCount).Select(f => new UploadFile()
                {
                    OriginFileName = f.Name,
                    Size = f.Size,
                    File = f,
                    Uploaded = false,
                    UpdateCallback = Update
                }));
            }
            else
            {
                var file = new UploadFile()
                {
                    OriginFileName = args.File.Name,
                    Size = args.File.Size,
                    File = args.File,
                    Uploaded = false,
                    UpdateCallback = Update
                };
                files.Add(file);
            }

            UploadFiles.AddRange(files);

            if (OnChange != null)
            {
                await OnChange(files);
            }

            files.ForEach(f => f.Uploaded = true);
        }

        private void Update(UploadFile file)
        {
            if (ShowProgress)
            {
                StateHasChanged();
            }
        }

        private static string GetFileSize(long fileSize) => fileSize switch
        {
            > 1024 and < 1024 * 1024 => $"{Math.Round(fileSize / 1024D, 0, MidpointRounding.AwayFromZero)} KB",
            > 1024 * 1024 and < 1024 * 1024 * 1024 => $"{Math.Round(fileSize / 1024 / 1024D, 0, MidpointRounding.AwayFromZero)} MB",
            > 1024 * 1024 * 1024 => $"{Math.Round(fileSize / 1024 / 1024 / 1024D, 0, MidpointRounding.AwayFromZero)} GB",
            _ => $"{fileSize} B"
        };

        private IDictionary<string, object> GetUploadAdditionalAttributes()
        {
            var ret = new Dictionary<string, object>
            {
                { "hidden", "hidden" }
            };

            if (!string.IsNullOrEmpty(Accept)) ret.Add("accept", Accept);
            if (IsMultiple) ret.Add("multiple", "multiple");
            if (IsDirectory)
            {
                ret.Add("directory", "dicrectory");
                ret.Add("webkitdirectory", "webkitdirectory");
            }
            return ret;
        }
    }
}
