using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITableHeader 接口
    /// </summary>
    public interface ITableColumn
    {
        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        /// <returns></returns>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获取绑定字段显示名称方法
        /// </summary>
        string GetDisplayName();

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        string GetFieldName();

        /// <summary>
        /// 获得/设置 是否允许排序 默认为 false
        /// </summary>
        bool Sortable { get; set; }

        /// <summary>
        /// 获得/设置 模板
        /// </summary>
        RenderFragment<object>? Template { get; set; }
    }
}
