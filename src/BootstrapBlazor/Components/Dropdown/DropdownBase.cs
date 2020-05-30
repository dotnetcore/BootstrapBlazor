using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 下拉框组件基类
    /// </summary>
    public abstract class DropdownBase<TItem> : SelectBase<TItem>
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
        protected override string? ClassName => CssBuilder.Default("btn dropdown-toggle")
          .AddClass("dropdown-toggle-split")
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
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
        /// 下拉框渲染类型
        /// </summary>
        [Parameter]
        public DropdownType DropdownType { get; set; }
    }
}
