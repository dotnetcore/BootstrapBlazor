using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// 获得/设置 是否允许过滤数据 默认为 false
        /// </summary>
        bool Filterable { get; set; }

        /// <summary>
        /// 获得/设置 列宽
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// 获得/设置 绑定列类型
        /// </summary>
        Type? FieldType { get; set; }

        /// <summary>
        /// 获得/设置 模板
        /// </summary>
        RenderFragment<object>? Template { get; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        [Parameter]
        string? FormatString { get; set; }

        /// <summary>
        /// 获得/设置 列格式化回调委托
        /// </summary>
        [Parameter]
        Func<object?, Task<string>>? Formatter { get; set; }
    }
}
