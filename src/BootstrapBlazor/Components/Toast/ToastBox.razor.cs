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
        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        protected string? AutoHide => !IsAutoHide ? "false" : null;

        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        protected string? ClassName => CssBuilder.Default("toast")
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
        /// 获得/设置 显示标题
        /// </summary>
        [Parameter] public string Title { get; set; } = "Toast";

        /// <summary>
        /// 获得/设置 Toast Body 子组件
        /// </summary>
        [Parameter] public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        [Parameter] public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏时间间隔
        /// </summary>
        [Parameter] public int Delay { get; set; } = 4000;

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            // 执行客户端动画
            if (firstRender) JSRuntime.ShowToast(Id);
        }
    }
}
