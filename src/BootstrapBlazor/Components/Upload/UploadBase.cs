using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 文件上传组件基类
    /// </summary>
    public abstract class UploadBase : BootstrapComponentBase
    {
        private JSInterop<UploadBase>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 Captcha DOM 元素实例
        /// </summary>
        protected ElementReference UploaderElement { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassName => CssBuilder.Default("upload")
            .AddClass("is-circle", IsCircle)
            .AddClass("is-prev", ShowPreview)
            .AddClass("is-wall", IsPhotoWall)
            .AddClass("is-card", IsCard)
            .AddClass("is-stack", IsStack)
            .AddClass("is-progress", ShowProgress)
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
        /// 获得/设置 上传按钮显示文字
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "点击上传";

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
        public Action<string>? OnUploaded { get; set; }

        /// <summary>
        /// 获得/设置 成功删除后回调委托
        /// </summary>
        [Parameter]
        public Action<string>? OnRemoved { get; set; }

        /// <summary>
        /// 获得/设置 上传失败后回调委托
        /// </summary>
        [Parameter]
        public Action<string>? OnFailed { get; set; }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                if (Interop == null && JSRuntime != null) Interop = new JSInterop<UploadBase>(JSRuntime);
                Interop?.Invoke(this, UploaderElement, "uploader", nameof(Completed), nameof(CheckFiles), nameof(Removed), nameof(Failed));
            }
        }

        /// <summary>
        /// 文件上传成功后回调此方法
        /// </summary>
        [JSInvokable]
        public void Completed(string fileName)
        {
            OnUploaded?.Invoke(fileName);
        }

        /// <summary>
        /// 文件删除成功后回调此方法
        /// </summary>
        [JSInvokable]
        public void Removed(string fileName)
        {
            OnRemoved?.Invoke(fileName);
        }

        /// <summary>
        /// 文件上传失败后回调此方法
        /// </summary>
        [JSInvokable]
        public void Failed(string fileName)
        {
            OnFailed?.Invoke(fileName);
        }

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
                message = result ? null : "文件太大";
            }

            if(result)
            {
                // check file extensions
                if(AllowFileType?.Contains("image", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    result = fileType.StartsWith("image", StringComparison.OrdinalIgnoreCase);
                    message = result ? null : "只允许选择图片类型文件";
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
