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
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Search 组件基类
    /// </summary>
    public abstract class SearchBase : AutoCompleteBase
    {
        /// <summary>
        /// Clear button color
        /// </summary>
        [Parameter]
        public Color ClearButtonColor { get; set; } = Color.Secondary;

        /// <summary>
        /// Clear button icon
        /// </summary>
        [Parameter]
        public string ClearButtonIcon { get; set; } = "fa fa-trash";

        /// <summary>
        /// Clear button text
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ClearButtonText { get; set; }

        /// <summary>
        /// 获得/设置 搜索按钮颜色
        /// </summary>
        [Parameter]
        public Color SearchButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 搜索按钮图标
        /// </summary>
        [Parameter]
        public string SearchButtonIcon { get; set; } = "fa fa-search";

        /// <summary>
        /// 获得/设置 搜索按钮文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SearchButtonText { get; set; }

        /// <summary>
        /// 获得/设置 点击搜索按钮时回调委托
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnSearch { get; set; }

        /// <summary>
        /// 获得/设置 点击搜索按钮时回调委托
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnClear { get; set; }

        /// <summary>
        /// 点击搜索按钮时触发此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnSearchClick()
        {
            if (OnSearch != null) await OnSearch(CurrentValueAsString);
        }

        /// <summary>
        /// 点击搜索按钮时触发此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClearClick()
        {
            if (OnClear != null) await OnClear(CurrentValueAsString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override async Task OnKeyUp(KeyboardEventArgs args)
        {
            await base.OnKeyUp(args);

            if (!string.IsNullOrEmpty(CurrentValueAsString) && args.Key == "Enter")
            {
                await OnSearchClick();
            }
        }
    }
}
