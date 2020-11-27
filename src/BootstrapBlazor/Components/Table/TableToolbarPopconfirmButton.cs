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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TableToolbarPopconfirmButton<TItem> : PopConfirmButtonBase, IToolbarButton<TItem>
    {
        /// <summary>
        /// 
        /// </summary>
        public Func<IEnumerable<TItem>, Task>? OnClickCallback { get; set; }

        /// <summary>
        /// 获得/设置 Table Toolbar 实例
        /// </summary>
        [CascadingParameter]
        protected TableToolbar<TItem>? Toolbar { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Toolbar?.AddButton(this);
        }
    }
}
