using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table 组件基类
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class TableBase<TItem> : BootstrapComponentBase where TItem : class, new()
    {
        /// <summary>
        /// 获得 wrapper 样式表集合
        /// </summary>
        protected string? WrapperClassName => CssBuilder.Default("table-wrapper")
            .AddClass("table-bordered", IsBordered)
            .Build();

        /// <summary>
        /// 获得 class 样式表集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("table")
            .AddClass("table-striped", IsStriped)
            .AddClass("table-hover", IsStriped)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Checkbox 样式表集合
        /// </summary>
        /// <returns></returns>
        protected string? CheckboxColumnClass => CssBuilder.Default("table-th-checkbox")
            .AddClass("show-text", ShowCheckboxText)
            .Build();

        /// <summary>
        /// 获得 选择列显示文字
        /// </summary>
        protected string? CheckboxDisplayTextString => ShowCheckboxText ? CheckboxDisplayText : null;

        private List<CheckboxBase<TItem>>? ItemCheckboxs;
        private CheckboxBase<bool>? HeaderCheckbox;

        /// <summary>
        /// 获得/设置 被选中数据集合
        /// </summary>
        /// <value></value>
        protected IEnumerable<TItem> SelectedItems => _selectedItems;

        private List<TItem> _selectedItems = new List<TItem>();

        /// <summary>
        /// 获得 表头 Model 实例
        /// </summary>
        protected TItem HeaderModel => Items?.FirstOrDefault() ?? new TItem();

        /// <summary>
        /// 获得/设置 TableHeader 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? HeaderTemplate { get; set; }

        /// <summary>
        /// 获得/设置 RowTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? RowTemplate { get; set; }

        /// <summary>
        /// 获得/设置 TableFooter 实例
        /// </summary>
        [Parameter]
        public RenderFragment? TableFooter { get; set; }

        /// <summary>
        /// 获得/设置 数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<TItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 是否显示表脚 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; }

        /// <summary>
        /// 获得/设置 是否斑马线样式
        /// </summary>
        [Parameter] public bool IsStriped { get; set; }

        /// <summary>
        /// 获得/设置 是否带边框样式
        /// </summary>
        [Parameter] public bool IsBordered { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 如果未设置 Items 数据源 自动执行查询方法
            if (Items == null)
            {
                QueryData();
                if (Items == null) Items = new TItem[0];
            }
        }

        #region Checkbox
        /// <summary>
        /// 获得/设置 显示选择列
        /// </summary>
        [Parameter] public bool ShowCheckbox { get; set; }

        /// <summary>
        /// 获得/设置 是否显示选择框文字
        /// </summary>
        /// <value></value>
        [Parameter] public bool ShowCheckboxText { get; set; }

        /// <summary>
        /// 获得/设置 显示选择框文字 默认为 选择
        /// </summary>
        /// <value></value>
        [Parameter] public string CheckboxDisplayText { get; set; } = "选择";

        /// <summary>
        /// 点击 Header 选择复选框时触发此方法
        /// </summary>
        /// <param name="state"></param>
        /// <param name="val"></param>
        protected virtual void OnHeaderCheck(CheckboxState state, bool val)
        {
            ItemCheckboxs?.ForEach(checkbox => checkbox.SetState(state));
        }

        /// <summary>
        /// 点击选择复选框时触发此方法
        /// </summary>
        /// <param name="state"></param>
        /// <param name="val"></param>
        protected virtual void OnCheck(CheckboxState state, TItem val)
        {
            if (state == CheckboxState.Checked) _selectedItems.Add(val);
            else _selectedItems.Remove(val);

            if (Items != null)
            {
                var headerCheckboxState = _selectedItems.Count == 0
                    ? CheckboxState.UnChecked
                    : (_selectedItems.Count == Items.Count() ? CheckboxState.Checked : CheckboxState.Mixed);
                HeaderCheckbox?.SetState(headerCheckboxState);
            }
        }

        /// <summary>
        /// 行内选择框初始化回调函数
        /// </summary>
        /// <param name="component"></param>
        protected void OnCheckboxInit(BootstrapComponentBase component)
        {
            HeaderCheckbox = (CheckboxBase<bool>)component;
        }

        /// <summary>
        /// 表头选择框初始化回调函数
        /// </summary>
        /// <param name="component"></param>
        protected void OnItemCheckboxInit(BootstrapComponentBase component)
        {
            if (ItemCheckboxs == null) ItemCheckboxs = new List<CheckboxBase<TItem>>();

            ItemCheckboxs.Add((CheckboxBase<TItem>)component);
        }
        #endregion

        #region Pagination
        /// <summary>
        /// 获得/设置 是否分页
        /// </summary>
        [Parameter] public bool IsPagination { get; set; }

        /// <summary>
        /// 获得/设置 每页数据数量
        /// </summary>
        [Parameter]
        public int PageItems { get; set; } = QueryPageOptions.DefaultPageItems;

        /// <summary>
        /// 获得/设置 每页显示数据数量的外部数据源
        /// </summary>
        [Parameter] public IEnumerable<int>? PageItemsSource { get; set; }

        /// <summary>
        /// 点击翻页回调方法
        /// </summary>
        [Parameter]
        public Func<QueryPageOptions, QueryData<TItem>>? OnQuery { get; set; }

        /// <summary>
        /// 获得/设置 数据总条目
        /// </summary>
        protected int TotalCount { get; set; }

        /// <summary>
        /// 获得/设置 当前页码
        /// </summary>
        protected int PageIndex { get; set; } = 1;

        /// <summary>
        /// 点击页码调用此方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageItems"></param>
        protected void OnPageClick(int pageIndex, int pageItems)
        {
            if (pageIndex != PageIndex)
            {
                PageIndex = pageIndex;
                PageItems = pageItems;
                Query();
            }
        }

        /// <summary>
        /// 每页记录条数变化是调用此方法
        /// </summary>
        protected void OnPageItemsChanged(int pageItems)
        {
            if (OnQuery != null)
            {
                PageIndex = 1;
                PageItems = pageItems;
                Query();
            }
        }
        #endregion

        /// <summary>
        /// 查询按钮调用此方法
        /// </summary>
        public void Query()
        {
            QueryData();
            StateHasChanged();
        }

        /// <summary>
        /// 调用 OnQuery 回调方法获得数据源
        /// </summary>
        protected void QueryData()
        {
            if (OnQuery != null)
            {
                //SelectedItems.Clear();
                var queryData = OnQuery(new QueryPageOptions()
                {
                    PageIndex = PageIndex,
                    PageItems = PageItems,
                    //SearchText = SearchText,
                    //SortOrder = SortOrder,
                    //SortName = SortName
                });
                Items = queryData.Items;
                PageIndex = queryData.PageIndex;
                TotalCount = queryData.TotalCount;
            }
        }
    }
}
