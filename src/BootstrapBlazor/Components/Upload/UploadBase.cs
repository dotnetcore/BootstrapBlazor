// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 文件上传组件基类
    /// </summary>
    public abstract class UploadBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("upload")
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
        protected string? PrevStyleString => CssBuilder.Default()
            .AddClass($"width: {Width}px;", !IsStack && Width > 0)
            .AddClass($"height: {Height}px;", !IsStack && Height > 0 && !IsCircle)
            .AddClass($"height: {Width}px;", !IsStack && IsCircle)
            .Build();

        /// <summary>
        /// 获得/设置 圆形进度半径
        /// </summary>
        protected string CircleDiameter => $"{Width / 2}";

        /// <summary>
        /// 获得/设置 半径
        /// </summary>
        protected string CircleR => $"{Width / 2 - 2}";

        /// <summary>
        /// 获得 圆形周长
        /// </summary>
        protected string CircleLength => $"{Math.Round(Width * Math.PI, 2)}";

        /// <summary>
        /// 获得 是否允许多文件上传 默认不允许 IsStack 模式下允许多文件上传
        /// </summary>
        protected string? MultipleString => (IsMultiple || IsStack) ? "multiple" : null;

        /// <summary>
        /// 获得 组件是否被禁用属性值
        /// </summary>
        protected string? DisabledString => IsDisabled ? "disabled" : null;

        /// <summary>
        /// 获得/设置 上传按钮图标
        /// </summary>
        [Parameter]
        public string Icon { get; set; } = "fa fa-cloud-upload";

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
        /// 获得/设置 上传接口地址 默认值为 "api/Upload"
        /// </summary>
        [Parameter]
        public string? UploadUrl { get; set; }

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
        /// 获得/设置 成功上传后回调委托
        /// </summary>
        [Parameter]
        public Func<string, string, Task>? OnUploaded { get; set; }

        /// <summary>
        /// 获得/设置 成功删除后回调委托
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnRemoved { get; set; }

        /// <summary>
        /// 获得/设置 上传失败后回调委托
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnFailed { get; set; }

        /// <summary>
        /// 获得/设置 设置请求头回调委托
        /// </summary>
        [Parameter]
        public Func<IEnumerable<UploadHeader>>? OnSetHeaders { get; set; }

        /// <summary>
        /// 文件上传成功后回调此方法
        /// </summary>
        [JSInvokable]
        public async Task Completed(string fileName, string prevUrl)
        {
            if (OnUploaded != null) await OnUploaded.Invoke(fileName, prevUrl);
        }

        /// <summary>
        /// 文件删除成功后回调此方法
        /// </summary>
        [JSInvokable]
        public async Task Removed(string fileName)
        {
            if (OnRemoved != null) await OnRemoved.Invoke(fileName);
        }

        /// <summary>
        /// 文件上传失败后回调此方法
        /// </summary>
        [JSInvokable]
        public async Task Failed(string fileName)
        {
            if (OnFailed != null) await OnFailed.Invoke(fileName);
        }

        /// <summary>
        /// 设置 请求头方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public IEnumerable<UploadHeader> SetHeaders()
        {
            return OnSetHeaders?.Invoke() ?? new UploadHeader[0];
        }
    }
}
