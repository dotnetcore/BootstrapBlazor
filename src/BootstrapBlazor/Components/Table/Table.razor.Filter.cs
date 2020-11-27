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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 过滤小图标样式
        /// </summary>
        protected string? GetFilterClassString(string fieldName) => CssBuilder.Default("fa fa-fw fa-filter")
            .AddClass("active", Filters.ContainsKey(fieldName))
            .Build();

        /// <summary>
        /// 获得/设置 表头过滤时回调方法
        /// </summary>
        public Func<Task>? OnFilterAsync { get; private set; }

        /// <summary>
        /// 获得 过滤集合
        /// </summary>
        public Dictionary<string, IFilterAction> Filters { get; } = new Dictionary<string, IFilterAction>();

        /// <summary>
        /// 点击 过滤小图标方法
        /// </summary>
        /// <param name="col"></param>
        protected EventCallback<MouseEventArgs> OnFilterClick(ITableColumn col) => EventCallback.Factory.Create<MouseEventArgs>(this, () =>
        {
            col.Filter?.Show();
        });
    }
}
