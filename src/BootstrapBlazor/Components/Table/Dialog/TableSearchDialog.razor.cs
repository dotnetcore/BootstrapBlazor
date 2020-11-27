// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableSearchDialog<TModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Parameter]
        [NotNull]
        public Func<Task>? OnResetSearchClick { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Parameter]
        [NotNull]
        public Func<Task>? OnSearchClick { get; set; }

        /// <summary>
        /// 重置按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ResetButtonText { get; set; }

        /// <summary>
        /// 查询按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? QueryButtonText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<TableSearchDialog<TModel>>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ResetButtonText ??= Localizer[nameof(ResetButtonText)];
            QueryButtonText ??= Localizer[nameof(QueryButtonText)];

            if (OnSearchClick == null) OnSearchClick = () => Task.CompletedTask;
            if (OnResetSearchClick == null) OnResetSearchClick = () => Task.CompletedTask;
        }
    }
}
