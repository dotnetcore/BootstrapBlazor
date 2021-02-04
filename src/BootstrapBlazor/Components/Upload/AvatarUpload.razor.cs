// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class AvatarUpload
    {
        /// <summary>
        /// 
        /// </summary>
        protected override string? ItemClassString => CssBuilder.Default(base.ItemClassString)
            .AddClass("is-circle", IsCircle)
            .Build();

        /// <summary>
        /// 获得/设置 预览框 Style 属性
        /// </summary>
        private string? PrevStyleString => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width > 0)
            .AddClass($"height: {Height}px;", Height > 0 && !IsCircle)
            .AddClass($"height: {Width}px;", IsCircle)
            .Build();

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
        /// 获得/设置 是否圆形图片框 Avatar 模式时生效 默认为 false
        /// </summary>
        [Parameter]
        public bool IsCircle { get; set; }

        /// <summary>
        /// 获得/设置 是否仅上传一次 默认 false
        /// </summary>
        [Parameter]
        public bool IsSingle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override async Task OnFileChange(InputFileChangeEventArgs args)
        {
            CurrentValue = args.File;

            var file = new UploadFile()
            {
                OriginFileName = args.File.Name,
                Size = args.File.Size,
                File = args.File,
                Uploaded = false
            };

            UploadFiles.Add(file);

            if (OnChange != null)
            {
                await OnChange(file);
            }
            else
            {
                await file.RequestBase64ImageFileAsync(file.File.ContentType, 320, 240);
            }
        }
    }
}
