using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 下拉框组件基类
    /// </summary>
    public abstract class DropdownBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 按钮弹出方向集合
        /// </summary>
        /// <returns></returns>
        protected string? DirectionClassName => CssBuilder.Default()
            .AddClass($"btn-group", DropdownType == DropdownType.ButtonGroup)
            .AddClass($"{Direction.ToDescriptionString()}", DropdownType == DropdownType.DropdownMenu)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ButtonClassName => CssBuilder.Default("btn")
            .AddClass("dropdown-toggle", !ShowSplit)
            .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .Build();

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("btn dropdown-toggle")
          .AddClass("dropdown-toggle-split")
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .Build();

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item.Value == Value?.Value)
            .Build();

        /// <summary>
        /// 获得 是否分裂式按钮
        /// </summary>
        protected string? DropdownToggle => !ShowSplit ? "dropdown" : null;

        /// <summary>
        /// 菜单对齐方式样式
        /// </summary>
        protected string? MenuAlignmentClass => CssBuilder.Default("dropdown-menu")
            .AddClass($"dropdown-menu-{MenuAlignment.ToDescriptionString()}", MenuAlignment == Alignment.Right)
            .Build();

        /// <summary>
        /// 是否开启分裂式
        /// </summary>
        [Parameter]
        public bool ShowSplit { get; set; }

        /// <summary>
        /// 获取菜单对齐方式
        /// </summary>
        [Parameter] public Alignment MenuAlignment { get; set; }

        /// <summary>
        /// 下拉选项方向 
        /// </summary>
        [Parameter]
        public Direction Direction { get; set; }

        /// <summary>
        /// 组件尺寸
        /// </summary>
        [Parameter]
        public Size Size { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 下拉框渲染类型
        /// </summary>
        [Parameter]
        public DropdownType DropdownType { get; set; }

        /// <summary>
        /// 获得/设置 绑定数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 选中项实例
        /// </summary>
        [Parameter]
        public SelectedItem? Value { get; set; }

        /// <summary>
        /// Gets or sets an expression that identifies the bound value.
        /// </summary>
        [Parameter] public Expression<Func<SelectedItem>>? ValueExpression { get; set; }

        /// <summary>
        /// 获得/设置 选中项改变回调方法
        /// </summary>
        [Parameter]
        public EventCallback<SelectedItem> ValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 控件值变化时触发此事件
        /// </summary>
        [Parameter]
        public Action<SelectedItem>? OnValueChanged { get; set; }

        /// <summary>
        /// 下拉框子项点击时调用此方法
        /// </summary>
        protected void OnClick(SelectedItem item)
        {
            Value = item;
            OnValueChanged?.Invoke(Value);
        }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 设置数据集合后 Value 设置默认值
            if (ValueExpression == null && Value == null && Items != null) Value = Items.First();
        }
    }
}
