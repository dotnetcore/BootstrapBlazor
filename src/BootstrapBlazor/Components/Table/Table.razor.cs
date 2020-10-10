using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table 组件基类
    /// </summary>
    public partial class Table<TItem> : BootstrapComponentBase, ITable where TItem : class, new()
    {
        /// <summary>
        /// 获得 Table 组件样式表
        /// </summary>
        protected string? TableClassName => CssBuilder.Default("table-container")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 wrapper 样式表集合
        /// </summary>
        protected string? WrapperClassName => CssBuilder.Default()
            .AddClass("table-bordered", IsBordered)
            .AddClass("table-striped table-hover", IsStriped)
            .AddClass("is-clickable", ClickToSelect || DoubleClickToEdit || OnClickRowCallback != null || OnDoubleClickRowCallback != null)
            .AddClass("table-scroll", !Height.HasValue)
            .AddClass("table-fixed", Height.HasValue)
            .AddClass("table-fixed-column", Columns.Any(c => c.Fixed))
            .Build();

        /// <summary>
        /// 获得 Body 内行样式
        /// </summary>
        /// <param name="item"></param>
        /// <param name="css"></param>
        /// <returns></returns>
        protected string? GetRowClassString(TItem item, string? css = null) => CssBuilder.Default(css)
            .AddClass(SetRowClassFormatter?.Invoke(item))
            .AddClass("active", CheckActive(item))
            .AddClass("is-master", DetailRowTemplate != null)
            .Build();

        /// <summary>
        /// 获得/设置 可过滤表格列集合
        /// </summary>
        protected IEnumerable<ITableColumn>? FilterColumns { get; set; }

        /// <summary>
        /// 获得 起始行号
        /// </summary>
        protected int StarRowIndex { get; set; }

        /// <summary>
        /// 获得/设置 组件是否渲染完毕 默认 false
        /// </summary>
        public bool IsRendered { get; set; }

        /// <summary>
        /// 获得 表头集合
        /// </summary>
        public List<ITableColumn> Columns { get; } = new List<ITableColumn>(50);

        /// <summary>
        /// 获得/设置 明细行模板
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? DetailRowTemplate { get; set; }

        /// <summary>
        /// 获得/设置 TableHeader 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? TableColumns { get; set; }

        /// <summary>
        /// 获得/设置 TableFooter 实例
        /// </summary>
        [Parameter]
        public RenderFragment<IEnumerable<TItem>>? TableFooter { get; set; }

        /// <summary>
        /// 获得/设置 数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<TItem> Items { get; set; } = Enumerable.Empty<TItem>();

        /// <summary>
        /// 获得/设置 是否显示表脚 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; }

        /// <summary>
        /// 获得/设置 是否斑马线样式 默认为 false
        /// </summary>
        [Parameter]
        public bool IsStriped { get; set; }

        /// <summary>
        /// 获得/设置 是否带边框样式 默认为 false
        /// </summary>
        [Parameter]
        public bool IsBordered { get; set; }

        /// <summary>
        /// 获得/设置 单击行回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnClickRowCallback { get; set; }

        /// <summary>
        /// 获得/设置 双击行回调委托方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnDoubleClickRowCallback { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // 初始化每页显示数量
            if (IsPagination)
            {
                PageItems = PageItemsSource.FirstOrDefault();
            }

            // 初始化 EditModel
            if (EditModel == null)
            {
                if (OnAddAsync != null) EditModel = await OnAddAsync();
                else EditModel = new TItem();
            }

            // 设置 OnSort 回调方法
            OnSortAsync = QueryAsync;

            // 设置 OnFilter 回调方法
            OnFilterAsync = QueryAsync;
        }

        private string? methodName;

        /// <summary>
        /// 获得/设置 是否我第一次 Render
        /// </summary>
        protected bool FirstRender { get; set; } = true;

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                FirstRender = false;
                methodName = Height.HasValue ? "fixTableHeader" : "init";

                ScreenSize = await RetrieveWidth();

                ColumnVisibles = Columns.Select(i => new ColumnVisibleItem { FieldName = i.GetFieldName(), Visible = i.Visible }).ToList();

                // set default sortName
                var col = Columns.FirstOrDefault(i => i.Sortable && i.DefaultSort);
                if (col != null)
                {
                    SortName = col.GetFieldName();
                    SortOrder = col.DefaultSortOrder;
                }
                await QueryAsync();
            }

            if (!firstRender) IsRendered = true;

            if (!string.IsNullOrEmpty(methodName) && IsRendered)
            {
                // 固定表头脚本关联
                await JSRuntime.InvokeVoidAsync(TableElement, "bb_table", methodName);
                methodName = null;
            }
        }

        /// <summary>
        /// 获得 Table 组件客户端宽度
        /// </summary>
        /// <returns></returns>
        protected ValueTask<decimal> RetrieveWidth() => JSRuntime.InvokeAsync<decimal>(TableElement, "bb_table", "width", UseComponentWidth);

        /// <summary>
        /// 检查当前列是否显示方法
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        protected bool CheckShownWithBreakpoint(ITableColumn col)
        {
            return col.ShownWithBreakPoint switch
            {
                BreakPoint.Small => ScreenSize >= 576,
                BreakPoint.Medium => ScreenSize >= 768,
                BreakPoint.Large => ScreenSize >= 992,
                BreakPoint.ExtraLarge => ScreenSize >= 1200,
                _ => true
            };
        }

        #region 生成 Row 方法
        /// <summary>
        /// 获得 指定单元格数据方法
        /// </summary>
        /// <param name="col"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected RenderFragment GetValue(ITableColumn col, TItem item) => async builder =>
        {
            if (col.Template != null)
            {
                builder.AddContent(0, col.Template.Invoke(item));
            }
            else
            {
                var content = "";
                var val = GetItemValue(col.GetFieldName(), item);
                if (col.Formatter != null)
                {
                    // 格式化回调委托
                    content = await col.Formatter(val);
                }
                else if (!string.IsNullOrEmpty(col.FormatString))
                {
                    // 格式化字符串
                    content = val?.Format(col.FormatString) ?? "";
                }
                else if (col.FieldType.IsEnum())
                {
                    content = col.FieldType.ToDescriptionString(val?.ToString());
                }
                else
                {
                    content = val?.ToString() ?? "";
                }
                builder.AddContent(0, content);
            }
        };

        private object? GetItemValue(string fieldName, TItem item)
        {
            object? ret = null;
            if (item != null)
            {
                var invoker = GetPropertyCache.GetOrAdd((typeof(TItem), fieldName), key => item.GetPropertyValueLambda<TItem, object>(key.Item2).Compile());
                ret = invoker(item);
            }
            return ret;
        }

        private static readonly ConcurrentDictionary<(Type, string), Func<TItem, object>> GetPropertyCache = new ConcurrentDictionary<(Type, string), Func<TItem, object>>();
        #endregion
    }
}
