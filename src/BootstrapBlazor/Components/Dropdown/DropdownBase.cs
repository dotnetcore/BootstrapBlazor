using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazor.Utils;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 下拉框组件基类
    /// </summary>
    public abstract class DropdownBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 按钮颜色/大小集合
        /// </summary>
        /// <returns></returns>
        protected string ClassColor => CssBuilder.Default("btn")
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .Build();

        /// <summary>
        /// 获得 按钮颜色/大小集合
        /// </summary>
        /// <returns></returns>
        protected string ClassResponsive => CssBuilder.Default("dropdown-menu")
            .AddClass(Responsive)
            .Build();

        /// <summary>
        /// 获得 按钮弹出方向集合
        /// </summary>
        /// <returns></returns>
        protected string ClassDirection => CssBuilder.Default()
          .AddClass($"{MenuType.ToDescriptionString()}", MenuType != 0)
          .AddClass($"{Direction.ToDescriptionString()}", Direction != Direction.None)
          .Build();

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        protected string ClassName => CssBuilder.Default("btn")
          .AddClass("dropdown-toggle")
          .AddClass("dropdown-toggle-split", ShowSplit)
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .AddClass(Class)
          .Build();

        /// <summary>
        /// 组件Id
        /// </summary>
        [Parameter]
        public string Id { get; set; } = "";
        /// <summary>
        /// 默认渲染元素 
        /// </summary>
        [Parameter]
        public string TagName { get; set; } = "button";

        /// <summary>
        /// 菜单项
        /// </summary>
        [Parameter]
        public string MenuItem { get; set; } = "a";

        /// <summary>
        /// 是否开启分裂式
        /// </summary>
        [Parameter]
        public bool ShowSplit { get; set; } = false;

        /// <summary>
        /// 下拉选项方向 
        /// </summary>
        [Parameter]
        public Direction Direction { get; set; } = Direction.None;

        /// <summary>
        /// 标题子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Css样式
        /// </summary>
        [Parameter]
        public string Class { get; set; } = "";

        /// <summary>
        /// 开启响应式
        /// </summary>
        [Parameter]
        public string Responsive { get; set; } = "";

        /// <summary>
        /// 组件尺寸
        /// </summary>
        [Parameter]
        public Size Size { get; set; } = Size.None;

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Danger;

        /// <summary>
        /// 下拉框渲染类型
        /// </summary>
        [Parameter]
        public MenuType MenuType { get; set; } = MenuType.Dropmenu;

        /// <summary>
        /// 获得/设置 绑定数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<SelectedItem> Items { get; set; } = new SelectedItem[0];

        /// <summary>
        /// 获得/设置 选中项实例
        /// </summary>
        [Parameter]
        public SelectedItem Value { get; set; } = new SelectedItem();

        /// <summary>
        /// 获得/设置 选中项改变回调方法
        /// </summary>
        [Parameter]
        public EventCallback<SelectedItem> ValueChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected void OnClick(SelectedItem item)
        {
            Value = item;
            if (!ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
        }

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string ActiveItem(SelectedItem item)
        {
            return CssBuilder.Default("dropdown-item")
                .AddClass("active", () => item.Value == Value?.Value)
                .Build();
        }
    }
}
