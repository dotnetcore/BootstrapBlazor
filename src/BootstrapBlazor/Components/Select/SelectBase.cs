using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Select 组件基类
    /// </summary>
    public abstract class SelectBase<TValue> : ValidateBase<TValue>, ISelect
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("form-select dropdown")
            .AddClass("is-disabled", IsDisabled)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string? InputClassName => CssBuilder.Default("form-control form-select-input")
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string? AppendClassName => CssBuilder.Default("form-select-append")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled)
            .Build();

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item.Value == CurrentValueAsString)
            .Build();

        /// <summary>
        /// 获得 PlaceHolder 属性
        /// </summary>
        protected string? PlaceHolder
        {
            get
            {
                string? placeHolder = "请选择 ...";
                if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var ph) && !string.IsNullOrEmpty(Convert.ToString(ph)))
                {
                    placeHolder = ph.ToString();
                }
                return placeHolder;
            }
        }

        /// <summary>
        /// 当前选择项实例
        /// </summary>
        protected SelectedItem? SelectedItem { get; set; }

        /// <summary>
        /// 获得/设置 Select 内部 Input 组件 Id
        /// </summary>
        protected string? InputId => string.IsNullOrEmpty(Id) ? null : $"{Id}_input";

        /// <summary>
        /// 获得 当前选项显示文本
        /// </summary>
        protected string CurrentTextAsString => SelectedItem?.Text ?? "";

        /// <summary>
        /// 获得/设置 是否初始化完成 默认为 false
        /// </summary>
        protected bool Initialized { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.None;

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// SelectedItemChanged 方法
        /// </summary>
        [Parameter]
        public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }

        /// <summary>
        /// 获得/设置 选项模板支持静态数据
        /// </summary>
        [Parameter]
        public RenderFragment? Options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string? RetrieveId() => InputId;

        /// <summary>
        /// 下拉框选项点击时调用此方法
        /// </summary>
        protected async Task OnItemClick(SelectedItem item)
        {
            if (SelectedItem != null) SelectedItem.Active = false;
            SelectedItem = item;

            SelectedItem.Active = true;

            // ValueChanged
            CurrentValueAsString = SelectedItem.Value;

            // 触发 SelectedItemChanged 事件
            if (OnSelectedItemChanged != null) await OnSelectedItemChanged.Invoke(SelectedItem);
        }

        /// <summary>
        /// 客户端检查完成时调用此方法
        /// </summary>
        /// <param name="valid"></param>
        protected override void OnValidate(bool valid)
        {
            Color = valid ? Color.Success : Color.Danger;
        }

        /// <summary>
        /// 获得 分组数据
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<IGrouping<string, SelectedItem>> GetSelectedItems() => GetItems().GroupBy(i => i.GroupName);

        /// <summary>
        /// 获得 数据源
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<SelectedItem> GetItems()
        {
            var items = Items?.ToList() ?? new List<SelectedItem>();
            items.AddRange(Childs);

            if (items.Any())
            {
                // 双向绑定其他组件更改了数据源值时
                if (SelectedItem != null && SelectedItem.Value != CurrentValueAsString)
                {
                    SelectedItem = items.FirstOrDefault(i => i.Value == CurrentValueAsString);
                }

                // 设置数据集合后 SelectedItem 设置默认值
                if (SelectedItem == null || !items.Any(i => i.Value == SelectedItem.Value && i.Text == SelectedItem.Text))
                {
                    var item = items.FirstOrDefault(i => i.Active);
                    if (item == null) item = items.FirstOrDefault(i => i.Value == CurrentValueAsString) ?? items.FirstOrDefault();
                    if (item != null)
                    {
                        SelectedItem = item;
                        if (Value != null && CurrentValueAsString != SelectedItem.Value)
                        {
                            item = items.FirstOrDefault(i => i.Text == CurrentValueAsString);
                            if (item != null) SelectedItem = item;
                        }
                        CurrentValueAsString = SelectedItem.Value;
                    }
                }
            }
            else
            {
                SelectedItem = null;
            }

            return items;
        }

        /// <summary>
        /// 更改组件数据源方法
        /// </summary>
        /// <param name="items"></param>
        public void SetItems(IEnumerable<SelectedItem> items)
        {
            Items = items;
            SelectedItem = GetItems().FirstOrDefault(i => i.Active);
        }

        private List<SelectedItem> Childs { get; set; } = new List<SelectedItem>();
        /// <summary>
        /// 添加静态下拉项方法
        /// </summary>
        /// <param name="item"></param>
        public void Add(SelectedItem item)
        {
            Childs.Add(item);
        }
    }
}
