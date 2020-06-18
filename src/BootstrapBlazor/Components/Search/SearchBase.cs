using Microsoft.AspNetCore.Components;
using System;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Search 组件基类
    /// </summary>
    public abstract class SearchBase : AutoCompleteBase
    {
        /// <summary>
        /// 获得/设置 搜索按钮颜色
        /// </summary>
        [Parameter]
        public Color SearchButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 搜索按钮图标
        /// </summary>
        [Parameter]
        public string? SearchButtonIcon { get; set; } = "fa fa-fw fa-search";

        /// <summary>
        /// 获得/设置 搜索按钮文字
        /// </summary>
        [Parameter]
        public string? SearchButtonText { get; set; } = "搜索";

        /// <summary>
        /// 获得/设置 点击搜索按钮时回调委托
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnSearch { get; set; }

        /// <summary>
        /// 点击搜索按钮时触发此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClick()
        {
            if (OnSearch != null) await OnSearch(CurrentValueAsString);
        }
    }
}
