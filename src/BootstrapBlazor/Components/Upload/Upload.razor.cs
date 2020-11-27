// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
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

        /// <summary>
        /// 获得/设置 上传按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 重置按钮显示文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ResetText { get; set; }

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

            Text ??= Localizer[nameof(Text)];
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
                if (Interop == null) Interop = new JSInterop<UploadBase>(JSRuntime);
                if (Interop != null) await Interop.Invoke(this, UploaderElement, "uploader", nameof(Completed), nameof(CheckFiles), nameof(Removed), nameof(Failed), nameof(SetHeaders));
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
