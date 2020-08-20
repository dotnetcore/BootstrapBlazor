using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表头组件
    /// </summary>
    public class TableColumn<TItem> : BootstrapComponentBase, ITableColumn
    {
        /// <summary>
        /// 获得/设置 绑定列类型
        /// </summary>
        public Type? FieldType { get; set; }

        /// <summary>
        /// 获得/设置 相关过滤器
        /// </summary>
        public IFilter? Filter { get; set; }

#nullable disable
        /// <summary>
        /// 获得/设置 数据绑定字段值
        /// </summary>
        [Parameter]
        public TItem Field { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 ValueExpression 表达式
        /// </summary>
        [Parameter]
        public Expression<Func<TItem>>? FieldExpression { get; set; }

        /// <summary>
        /// 获得/设置 是否排序 默认 false
        /// </summary>
        [Parameter]
        public bool Sortable { get; set; }

        /// <summary>
        /// 获得/设置 是否可过滤数据 默认 false
        /// </summary>
        [Parameter]
        public bool Filterable { get; set; }

        /// <summary>
        /// 获得/设置 是否参与搜索自动生成 默认 false
        /// </summary>
        [Parameter]
        public bool Searchable { get; set; }

        /// <summary>
        /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
        /// </summary>
        [Parameter]
        public bool Editable { get; set; } = true;

        /// <summary>
        /// 获得/设置 表头显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 列宽 默认为 auto
        /// </summary>
        [Parameter]
        public int? Width { get; set; }

        /// <summary>
        /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
        /// </summary>
        [Parameter]
        public string? FormatString { get; set; }

        /// <summary>
        /// 获得/设置 列格式化回调委托
        /// </summary>
        [Parameter]
        public Func<object?, Task<string>>? Formatter { get; set; }

        /// <summary>
        /// 获得/设置 显示模板
        /// </summary>
        [Parameter]
        public RenderFragment<TableColumnContext<object, TItem>>? Template { get; set; }

        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        [Parameter]
        public RenderFragment<object?>? EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 搜索模板
        /// </summary>
        /// <value></value>
        [Parameter]
        public RenderFragment<object?>? SearchTemplate { get; set; }

        /// <summary>
        /// 获得/设置 过滤模板
        /// </summary>
        [Parameter]
        public RenderFragment? FilterTemplate { get; set; }

        /// <summary>
        /// 获得/设置 Table Header 实例
        /// </summary>
        //[CascadingParameter]
        //protected TableColumnCollection? Columns { get; set; }

        [CascadingParameter]
        protected ITable? Table { get; set; }

        /// <summary>
        /// 内部使用负责把 object 类型的绑定数据值转化为泛型数据传递给前端
        /// </summary>
        RenderFragment<object>? ITableColumn.Template
        {
            get => Template == null ? null : new RenderFragment<object>(context => builder =>
            {
                // 此处 context 为行数据
                // 将绑定字段值放入上下文中
                var invoker = GetPropertyCache.GetOrAdd((context.GetType(), GetFieldName()), key => context.GetPropertyValueLambda<object, TItem>(key.FieldName).Compile());
                var value = invoker(context);
                builder.AddContent(0, Template.Invoke(new TableColumnContext<object, TItem>() { Row = context, Value = value }));
            });
        }

        /// <summary>
        /// 组件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            Table?.Columns.Add(this);
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);

            // 获取模型属性定义类型
            FieldType = _fieldIdentifier.Value.Model.GetType().GetProperty(GetFieldName())?.PropertyType;
        }

        private FieldIdentifier? _fieldIdentifier;
        /// <summary>
        /// 获取绑定字段显示名称方法
        /// </summary>
        public string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? "";

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        public string GetFieldName() => _fieldIdentifier?.FieldName ?? "";

        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), Func<object, TItem>> GetPropertyCache = new ConcurrentDictionary<(Type, string), Func<object, TItem>>();
    }
}
