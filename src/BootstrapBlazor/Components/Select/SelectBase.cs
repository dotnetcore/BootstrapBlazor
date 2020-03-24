using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Select 组件基类
    /// </summary>
    public abstract class SelectBase<TItem> : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string ClassName => CssBuilder.Default("form-select dropdown")
            .AddClass("is-disabled", IsDisabled)
            .AddClass(Class)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string InputClassName => CssBuilder.Default("form-control form-select-input")
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string ArrowClassName => CssBuilder.Default("form-select-append")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
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
        protected string? Disabled => IsDisabled ? "disabled" : null;

        /// <summary>
        /// 获得 当前组件 Id
        /// </summary>
        [Parameter] public string? Id { get; set; }

        /// <summary>
        /// 获得/设置 自定义样式
        /// </summary>
        [Parameter] public string Class { get; set; } = "";

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
        public EventCallback<SelectedItem> SelectedItemChanged { get; set; }

        /// <summary>
        /// 下拉框选项点击时调用此方法
        /// </summary>
        protected void OnItemClick(SelectedItem item)
        {
            SelectedItem = item;
            SelectedItem.Active = true;

            // 触发 SelectedItemChanged 事件
            if (SelectedItemChanged.HasDelegate) SelectedItemChanged.InvokeAsync(item);
        }

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string ActiveItem(SelectedItem item)
        {
            return CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item.Value == SelectedItem?.Value)
            .Build();
        }
    }
}
