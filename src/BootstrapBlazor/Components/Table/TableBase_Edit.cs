using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class TableBase<TItem>
    {
        /// <summary>
        /// 获得 Checkbox 样式表集合
        /// </summary>
        /// <returns></returns>
        protected string? ButtonColumnClass => CssBuilder.Default("table-th-button")
            .Build();

        /// <summary>
        /// 获得/设置 删除按钮提示弹框实例
        /// </summary>
        protected PopoverConfirm? DeleteConfirm { get; set; }

        /// <summary>
        /// 获得/设置 删除按钮提示弹框实例
        /// </summary>
        protected PopoverConfirm? ButtonDeleteConfirm { get; set; }

        /// <summary>
        /// 获得/设置 编辑弹窗 Title 文字
        /// </summary>
        protected string? EditModalTitleString { get; set; }

        /// <summary>
        /// 获得/设置 被选中数据集合
        /// </summary>
        /// <value></value>
        protected List<TItem> SelectedItems { get; set; } = new List<TItem>();

        /// <summary>
        /// 获得/设置 被选中的数据集合
        /// </summary>
        public IEnumerable<TItem> SelectedRows => SelectedItems;

        /// <summary>
        /// 获得/设置 编辑数据弹窗实例
        /// </summary>
        protected Modal? EditModal { get; set; }

        /// <summary>
        /// 获得/设置 编辑数据弹窗 Title
        /// </summary>
        [Parameter] public string EditModalTitle { get; set; } = "编辑数据窗口";

        /// <summary>
        /// 获得/设置 新建数据弹窗 Title
        /// </summary>
        [Parameter] public string AddModalTitle { get; set; } = "新建数据窗口";

        /// <summary>
        /// 获得/设置 新建数据弹窗 Title
        /// </summary>
        [Parameter] public string ColumnButtonTemplateHeaderText { get; set; } = "操作";

        /// <summary>
        /// 获得/设置 EditTemplate 实例
        /// </summary>
        [Parameter] public RenderFragment<TItem?>? EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 RowButtonTemplate 实例
        /// </summary>
        [Parameter] public RenderFragment<TItem>? RowButtonTemplate { get; set; }

        /// <summary>
        /// 获得/设置 EditModel 实例
        /// </summary>
        [Parameter] public TItem? EditModel { get; set; }

        /// <summary>
        /// 获得/设置 单选模式下点击行即选中本行 默认为 true
        /// </summary>
        [Parameter]
        public bool ClickToSelect { get; set; } = true;

        /// <summary>
        /// 获得/设置 单选模式下双击即编辑本行 默认为 false
        /// </summary>
        [Parameter]
        public bool DoubleClickToEdit { get; set; }

        /// <summary>
        /// 单选模式下选择行时调用此方法
        /// </summary>
        /// <param name="val"></param>
        protected virtual Task SelectRow(TItem val)
        {
            SelectedItems.Clear();
            SelectedItems.Add(val);

            // TODO: 性能问题此处重新渲染整个 DataGrid
            // 合理做法是将 tbody 做成组件仅渲染 tbody 即可，后期优化此处 
            StateHasChanged();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 检查当前行是否被选中方法
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected virtual bool CheckActive(TItem val)
        {
            var ret = false;
            if (!IsMultipleSelect && ClickToSelect)
            {
                ret = SelectedItems.Contains(val);
            }
            return ret;
        }

        /// <summary>
        /// 查询按钮调用此方法
        /// </summary>
        /// <returns></returns>
        public async Task QueryAsync()
        {
            await QueryData();
            StateHasChanged();
        }

        /// <summary>
        /// 调用 OnQuery 回调方法获得数据源
        /// </summary>
        protected async Task QueryData()
        {
            SelectedItems.Clear();

            QueryData<TItem>? queryData = null;
            if (OnQueryAsync != null)
            {
                queryData = await OnQueryAsync(new QueryPageOptions()
                {
                    PageIndex = PageIndex,
                    PageItems = PageItems,
                    SearchText = SearchText,
                    SortOrder = SortOrder,
                    SortName = SortName,
                    Filters = Filters.Values,
                    Searchs = Searchs
                });
            }
            if (queryData != null)
            {
                Items = queryData.Items;
                TotalCount = queryData.TotalCount;
                IsFiltered = queryData.IsFiltered;
                IsSorted = queryData.IsSorted;
                IsSearch = queryData.IsSearch;

                // 外部未过滤，内部自行过滤
                if (!IsFiltered && Filters.Any())
                {
                    Items = Items.Where(Filters.Values.GetFilterFunc<TItem>());
                    TotalCount = Items.Count();
                }

                // 外部未处理排序，内部自行排序
                if (!IsSorted && SortOrder != SortOrder.Unset && !string.IsNullOrEmpty(SortName))
                {
                    var invoker = SortLambdaCache.GetOrAdd(typeof(TItem), key => Items.GetSortLambda().Compile());
                    Items = invoker(Items, SortName, SortOrder);
                }
            }
        }

        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<TItem>, string, SortOrder, IEnumerable<TItem>>> SortLambdaCache = new ConcurrentDictionary<Type, Func<IEnumerable<TItem>, string, SortOrder, IEnumerable<TItem>>>();

        /// <summary>
        /// 行尾列编辑按钮点击回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected Task ClickEditButton(TItem item)
        {
            SelectedItems.Clear();
            SelectedItems.Add(item);

            if (OnSaveAsync != null || OnAddAsync != null)
            {
                // 更新行选中状态
                Edit();
                StateHasChanged();
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 双击行回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected async Task DoubleClickRow(TItem item)
        {
            if (DoubleClickToEdit)
            {
                await ClickEditButton(item);
            }

            await OnDoubleClickRowCallback.Invoke(item);
        }

        /// <summary>
        /// 行尾列按钮点击回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected Task ClickDeleteButton(TItem item)
        {
            SelectedItems.Clear();
            SelectedItems.Add(item);
            StateHasChanged();

            return Task.CompletedTask;
        }

        #region AutoEdit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected RenderFragment AutoGenerateTemplate(ITableColumn col, TItem? model) => builder =>
        {
            var fieldType = col.FieldType;
            if (fieldType != null && model != null)
            {
                // GetDisplayName
                var displayName = col.GetDisplayName();
                var fieldName = col.GetFieldName();

                // FieldValue
                var valueInvoker = GetPropertyValueLambdaCache.GetOrAdd((typeof(TItem), fieldName), key => model.GetPropertyValueLambda<TItem, object?>(key.FieldName).Compile());
                var fieldValue = valueInvoker.Invoke(model);

                // ValueChanged
                var valueChangedInvoker = CreateLambda(fieldType).Compile();
                var fieldValueChanged = valueChangedInvoker(model, fieldName);

                // ValueExpression
                var body = Expression.Property(Expression.Constant(model), typeof(TItem), fieldName);
                var tDelegate = typeof(Func<>).MakeGenericType(fieldType);
                var valueExpression = Expression.Lambda(tDelegate, body);

                var index = 0;
                var componentType = GenerateComponent(fieldType);
                builder.OpenComponent(index++, componentType);
                builder.AddAttribute(index++, "DisplayText", displayName);
                builder.AddAttribute(index++, "Value", fieldValue);
                builder.AddAttribute(index++, "ValueChanged", fieldValueChanged);
                builder.AddAttribute(index++, "ValueExpression", valueExpression);
                builder.AddMultipleAttributes(index++, CreateMultipleAttributes(fieldType));
                builder.CloseComponent();
            }
        };

        private IEnumerable<KeyValuePair<string, object>> CreateMultipleAttributes(Type fieldType)
        {
            var ret = new List<KeyValuePair<string, object>>();
            var type = Nullable.GetUnderlyingType(fieldType) ?? fieldType;
            if (type.IsEnum)
            {
                // 枚举类型
                // 通过字符串转化为枚举类实例
                var items = type.ToSelectList();
                if (items != null) ret.Add(new KeyValuePair<string, object>("Items", items));
            }
            else
            {
                switch (type.Name)
                {
                    case nameof(String):
                        ret.Add(new KeyValuePair<string, object>("placeholder", "请输入 ..."));
                        break;
                    default:
                        break;
                }
            }

            if (IsSearch)
            {
                ret.Add(new KeyValuePair<string, object>("ShowLabel", true));
            }
            return ret;
        }

        private Type GenerateComponent(Type fieldType)
        {
            Type? ret = null;
            var type = (Nullable.GetUnderlyingType(fieldType) ?? fieldType);
            if (type.IsEnum)
            {
                ret = typeof(Select<>).MakeGenericType(fieldType);
            }
            else
            {
                switch (type.Name)
                {
                    case nameof(Boolean):
                        ret = typeof(Checkbox<>).MakeGenericType(fieldType);
                        break;
                    case nameof(DateTime):
                        ret = typeof(DateTimePicker<>).MakeGenericType(fieldType);
                        break;
                    case nameof(Int32):
                    case nameof(Double):
                    case nameof(Decimal):
                        ret = typeof(BootstrapInput<>).MakeGenericType(fieldType);
                        break;
                    case nameof(String):
                        ret = typeof(BootstrapInput<>).MakeGenericType(typeof(string));
                        break;
                }
            }
            return ret ?? typeof(BootstrapInput<>).MakeGenericType(typeof(string));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="model"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        protected EventCallback<TType> CreateCallback<TType>(TItem model, string fieldName)
        {
            return EventCallback.Factory.Create<TType>(this, t =>
            {
                if (model != null)
                {
                    var invoker = SetPropertyValueLambdaCache.GetOrAdd((typeof(TItem), fieldName), key => model.SetPropertyValueLambda<TItem, object?>(key.FieldName).Compile());
                    invoker.Invoke(model, t);
                }
            });
        }

        private Expression<Func<TItem, string, object>> CreateLambda(Type fieldType)
        {
            var exp_p1 = Expression.Parameter(typeof(TItem));
            var exp_p2 = Expression.Parameter(typeof(string));
            var method = GetType().GetMethod("CreateCallback", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(fieldType);
            var body = Expression.Call(Expression.Constant(this), method, exp_p1, exp_p2);

            return Expression.Lambda<Func<TItem, string, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2);
        }

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Func<TItem, object?>> GetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Func<TItem, object?>>();

        private static ConcurrentDictionary<(Type ModelType, string FieldName), Action<TItem, object?>> SetPropertyValueLambdaCache { get; } = new ConcurrentDictionary<(Type, string), Action<TItem, object?>>();
        #endregion
    }
}
