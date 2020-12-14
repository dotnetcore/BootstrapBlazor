// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableEditorDialog<TModel>
    {
        /// <summary>
        /// 获得/设置 保存回调委托
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<EditContext, Task>? OnSaveAsync { get; set; }

        /// <summary>
        /// 重置按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? CloseButtonText { get; set; }

        /// <summary>
        /// 查询按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SaveButtonText { get; set; }

        /// <summary>
        /// 关闭弹窗回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnCloseAsync { get; set; }


        [Inject]
        [NotNull]
        private IStringLocalizer<TableEditorDialog<TModel>>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            CloseButtonText ??= Localizer[nameof(CloseButtonText)];
            SaveButtonText ??= Localizer[nameof(SaveButtonText)];
        }

        private async Task OnClose()
        {
            if (OnCloseAsync != null) await OnCloseAsync();
        }
    }
}
