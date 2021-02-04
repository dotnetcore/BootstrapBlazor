// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ButtonUploadBase : MultipleUploadBase
    {
        /// <summary>
        /// 获得/设置 是否仅上传一次 默认 false
        /// </summary>
        [Parameter]
        public bool IsSingle { get; set; }

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
        /// 获得/设置 设置文件格式图标回调委托
        /// </summary>
        [Parameter]
        public Func<string, string>? OnGetFileFormat { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 上传文件夹时 开启 Multiple 属性
            if (IsDirectory) IsMultiple = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override async Task OnFileChange(InputFileChangeEventArgs args)
        {
            if (IsMultiple)
            {
                var items = args.GetMultipleFiles(args.FileCount).Select(f => new UploadFile()
                {
                    OriginFileName = f.Name,
                    Size = f.Size,
                    File = f,
                    Uploaded = OnChange == null,
                    UpdateCallback = Update
                });
                UploadFiles.AddRange(items);
                if (OnChange != null)
                {
                    foreach (var item in items)
                    {
                        await OnChange(item);
                        item.Uploaded = true;
                    }
                }
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
                UploadFiles.Add(file);
                if (OnChange != null)
                {
                    await OnChange(file);
                }
                file.Uploaded = true;
            }
        }

        private void Update(UploadFile file)
        {
            if (GetShowProgress(file))
            {
                StateHasChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetFileFormatClassString(UploadFile item)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<string, object> GetUploadAdditionalAttributes()
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
