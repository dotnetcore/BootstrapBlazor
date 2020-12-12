// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Modal 弹窗组件
    /// </summary>
    public abstract class ModalBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 DOM 元素实例
        /// </summary>
        protected ElementReference ModalElement { get; set; }

        /// <summary>
        /// 获得 样式字符串
        /// </summary>
        protected string? ClassString => CssBuilder.Default("modal")
            .AddClass("fade", IsFade)
            .Build();

        /// <summary>
        /// 获得 后台关闭弹窗设置
        /// </summary>
        protected string? Backdrop => IsBackdrop ? null : "static";

        /// <summary>
        /// 获得 ModalDialog 集合
        /// </summary>
        protected List<ModalDialogBase> Dialogs { get; private set; } = new List<ModalDialogBase>(50);

        /// <summary>
        /// 获得/设置 是否后台关闭弹窗
        /// </summary>
        [Parameter]
        public bool IsBackdrop { get; set; }

        /// <summary>
        /// 获得/设置 是否开启淡入淡出动画 默认为 true 开启动画
        /// </summary>
        [Parameter]
        public bool IsFade { get; set; } = true;

        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 弹窗状态切换方法
        /// </summary>
        public async ValueTask Toggle()
        {
            Dialogs.ForEach(d => d.IsShown = Dialogs.IndexOf(d) == 0);
            await JSRuntime.InvokeVoidAsync(ModalElement, "bb_modal", "toggle");
        }

        /// <summary>
        /// 显示弹窗方法
        /// </summary>
        /// <returns></returns>
        public async ValueTask Show()
        {
            Dialogs.ForEach(d => d.IsShown = Dialogs.IndexOf(d) == 0);
            await JSRuntime.InvokeVoidAsync(ModalElement, "bb_modal", "show");
        }

        /// <summary>
        /// 关闭弹窗方法
        /// </summary>
        /// <returns></returns>
        public async ValueTask Close()
        {
            Dialogs.ForEach(d => d.IsShown = Dialogs.IndexOf(d) == 0);
            await JSRuntime.InvokeVoidAsync(ModalElement, "bb_modal", "hide");
        }

        /// <summary>
        /// 添加对话窗方法
        /// </summary>
        /// <param name="dialog"></param>
        public void AddDialog(ModalDialogBase dialog)
        {
            if (!Dialogs.Any()) dialog.IsShown = true;
            Dialogs.Add(dialog);
        }

        /// <summary>
        /// 显示指定对话框方法
        /// </summary>
        /// <param name="dialog"></param>
        public void ShowDialog(ModalDialogBase dialog)
        {
            Dialogs.ForEach(d => d.IsShown = d == dialog);
            StateHasChanged();
        }
    }
}
