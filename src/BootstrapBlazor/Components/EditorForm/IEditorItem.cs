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

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEditorItem
    {
        /// <summary>
        /// 获得/设置 绑定列类型
        /// </summary>
        Type FieldType { get; }

        /// <summary>
        /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
        /// </summary>
        bool Editable { get; set; }

        /// <summary>
        /// 获得/设置 当前列编辑时是否只读 默认为 false
        /// </summary>
        bool Readonly { get; set; }

        /// <summary>
        /// 获得/设置 表头显示文字
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        RenderFragment<object>? EditTemplate { get; set; }

        /// <summary>
        /// 获取绑定字段显示名称方法
        /// </summary>
        string GetDisplayName();

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        string GetFieldName();
    }
}
