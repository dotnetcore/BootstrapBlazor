using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{

    /// <summary>
    /// 
    /// </summary>
    public class AutoGenerateClassInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Alignment? Align { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Editable { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Filterable { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Readonly { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Searchable { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? ShowTips { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Sortable { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? TextEllipsis { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? TextWrap { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Width { get; internal set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AutoGenerateColumnInfo : AutoGenerateClassInfo
    {
        /// <summary>
        /// 获得/设置 显示顺序 ，规则如下：
        /// <para></para>
        /// &gt;0时排前面，1,2,3...
        /// <para></para>
        /// =0时排中间(默认)
        /// <para></para>
        /// &lt;0时排后面，...-3,-2,-1
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// 获得/设置 是否忽略 默认为 false 不忽略
        /// </summary>
        public bool? Ignore { get; set; }

        /// <summary>
        /// 获得/设置 是否为默认排序列 默认为 false
        /// </summary>
        public bool? DefaultSort { get; set; }

        /// <summary>
        /// 获得/设置 是否不进行验证 默认为 false
        /// </summary>
        public bool? SkipValidate { get; set; }

        /// <summary>
        /// 获得/设置 新建时此列只读 默认为 false
        /// </summary>
        public bool? IsReadonlyWhenAdd { get; set; }

        /// <summary>
        /// 获得/设置 编辑时此列只读 默认为 false
        /// </summary>
        public bool? IsReadonlyWhenEdit { get; set; }

        /// <summary>
        /// 获得/设置 是否为默认排序规则 默认为 SortOrder.Unset
        /// </summary>
        public SortOrder? DefaultSortOrder { get; set; }

        /// <summary>
        /// 获得/设置 是否固定本列 默认 false 不固定
        /// </summary>
        public bool? Fixed { get; set; }

        /// <summary>
        /// 获得/设置 列是否显示 默认为 true 可见的
        /// </summary>
        public bool? Visible { get; set; } = true;

        /// <summary>
        /// 获得/设置 列 td 自定义样式 默认为 null 未设置
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// 获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置
        /// </summary>
        public BreakPoint? ShownWithBreakPoint { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        public string? FormatString { get; set; }

        /// <summary>
        /// 获得/设置 placeholder 文本 默认为 null
        /// </summary>
        public string? PlaceHolder { get; set; }


        /// <summary>
        /// 获得/设置 组件类型 默认为 null
        /// </summary>
        public Type? ComponentType { get; set; }


        /// <summary>
        /// 获得/设置 步长 默认为 1
        /// </summary>
        public object? Step { get; set; }

        /// <summary>
        /// 获得/设置 Textarea 行数
        /// </summary>
        public int? Rows { get; set; }


        /// <summary>
        /// 获得/设置 当前属性显示文字 列头或者标签名称
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal string? FieldName { get; set; }


        /// <summary>
        /// 分组
        /// </summary>
        public string? Category { get; set; }
    }
}
