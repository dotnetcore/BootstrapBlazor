using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Select 组件基类
    /// </summary>
    public abstract class SelectBase<TItem> : ValidateInputBase<TItem>
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
        protected string? ArrowClassName => CssBuilder.Default("form-select-append")
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
        /// 获得 按钮 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得/设置 Select 内部 Input 组件 Id
        /// </summary>
        protected string? InputId => string.IsNullOrEmpty(Id) ? null : $"{Id}_input";

        /// <summary>
        /// 获得 当前选项显示文本
        /// </summary>
        protected string CurrentTextAsString => SelectedItem?.Text ?? "";

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.None;

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// SelectedItemChanged 方法
        /// </summary>
        [Parameter]
        public EventCallback<SelectedItem> OnSelectedItemChanged { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // 双向绑定其他组件更改了数据源值时
            if (Items != null && SelectedItem != null && SelectedItem.Value != CurrentValueAsString)
            {
                SelectedItem = Items.FirstOrDefault(i => i.Value == CurrentValueAsString);
            }

            // 设置数据集合后 SelectedItem 设置默认值
            if (SelectedItem == null || !(Items?.Any(i => i.Value == SelectedItem.Value && i.Text == SelectedItem.Text) ?? false))
            {
                var item = Items?.FirstOrDefault(i => i.Active);
                if (item == null) item = Items?.FirstOrDefault(i => i.Value == CurrentValueAsString) ?? Items?.FirstOrDefault();
                if (item != null)
                {
                    SelectedItem = item;
                    if (Value != null && CurrentValueAsString != SelectedItem.Value)
                    {
                        item = Items.FirstOrDefault(i => i.Text == CurrentValueAsString);
                        if (item != null) SelectedItem = item;
                    }
                    CurrentValueAsString = SelectedItem.Value;
                }
            }
        }

        /// <summary>
        /// 失去焦点时触发此方法
        /// </summary>
        protected virtual void OnBlur()
        {
            if (FieldIdentifier != null) EditContext?.NotifyFieldChanged(FieldIdentifier.Value);
        }

        /// <summary>
        /// 下拉框选项点击时调用此方法
        /// </summary>
        protected void OnItemClick(SelectedItem item)
        {
            if (SelectedItem != null) SelectedItem.Active = false;
            SelectedItem = item;
            SelectedItem.Active = true;

            // ValueChanged
            CurrentValueAsString = SelectedItem.Value;

            // 触发 SelectedItemChanged 事件
            if (OnSelectedItemChanged.HasDelegate) OnSelectedItemChanged.InvokeAsync(item);
        }

        /// <summary>
        /// 客户端检查完成时调用此方法
        /// </summary>
        /// <param name="valid"></param>
        protected override void OnValidate(bool valid)
        {
            base.OnValidate(valid);
            Color = valid ? Color.Success : Color.Danger;
        }

        /// <summary>
        /// 调用 Tooltip 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void InvokeTooltip(bool firstRender)
        {
            if (firstRender) JSRuntime.Tooltip(InputId);
            else JSRuntime.Tooltip(InputId, "show");
        }

        /// <summary>
        /// 获得 分组数据
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<IGrouping<string, SelectedItem>> GetSelectedItems() => (Items ?? new SelectedItem[0]).GroupBy(i => i.GroupName);

        /// <summary>
        /// 更改组件数据源方法
        /// </summary>
        /// <param name="items"></param>
        public void SetItems(IEnumerable<SelectedItem> items)
        {
            Items = items;
            SelectedItem = Items.FirstOrDefault(i => i.Active);
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) JSRuntime.Tooltip(InputId, "dispose");

        }
    }
}
