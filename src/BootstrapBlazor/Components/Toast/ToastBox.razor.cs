// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ToastBox
    {
        private MarkupString MarkupContent => string.IsNullOrEmpty(Content) ? new MarkupString() : new MarkupString(Content);
        /// <summary>
        /// ToastBox HTML 实例引用
        /// </summary>
        protected ElementReference ToastBoxElement { get; set; }

        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        protected string? AutoHide => !IsAutoHide ? "false" : null;

        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        protected string? ClassName => CssBuilder.Default("toast fade")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 进度条样式
        /// </summary>
        protected string? ProgressClass => CssBuilder.Default("toast-progress")
            .AddClass("bg-success", Category == ToastCategory.Success)
            .AddClass("bg-info", Category == ToastCategory.Information)
            .AddClass("bg-danger", Category == ToastCategory.Error)
            .Build();

        /// <summary>
        /// 获得/设置 图标样式
        /// </summary>
        protected string? IconString => CssBuilder.Default("fa")
            .AddClass("fa-check-circle text-success", Category == ToastCategory.Success)
            .AddClass("fa-exclamation-circle text-info", Category == ToastCategory.Information)
            .AddClass("fa-times-circle text-danger", Category == ToastCategory.Error)
            .Build();

        /// <summary>
        /// 获得/设置 弹出框自动关闭时长
        /// </summary>
        protected string? DelayString => IsAutoHide ? Convert.ToString(Delay + 200) : null;

        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        [Parameter] public ToastCategory Category { get; set; }

        /// <summary>
        /// 获得/设置 显示标题 默认为 未设置
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 Toast Body 子组件
        /// </summary>
        [Parameter]
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        [Parameter]
        public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏时间间隔
        /// </summary>
        [Parameter]
        public int Delay { get; set; } = 4000;

        /// <summary>
        /// 获得/设置 Toast 实例
        /// </summary>
        /// <value></value>
        [CascadingParameter]
        public Toast? Toast { get; set; }

        private JSInterop<Toast>? Interop { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            // 执行客户端动画
            if (firstRender)
            {
                if (Toast != null)
                {
                    Interop = new JSInterop<Toast>(JSRuntime);
                    await Interop.Invoke(Toast, ToastBoxElement, "showToast", nameof(Toast.Clear));
                }
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Interop?.Dispose();
                Interop = null;
            }
        }
    }
}
