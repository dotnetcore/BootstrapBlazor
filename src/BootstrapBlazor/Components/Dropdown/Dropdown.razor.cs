// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Dropdown<TItem>
    {
        /// <summary>
        /// 获得 按钮弹出方向集合
        /// </summary>
        /// <returns></returns>
        private string? DirectionClassName => CssBuilder.Default()
            .AddClass($"btn-group", DropdownType == DropdownType.ButtonGroup)
            .AddClass($"{Direction.ToDescriptionString()}", DropdownType == DropdownType.DropdownMenu)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        private string? ButtonClassName => CssBuilder.Default("btn")
            .AddClass("dropdown-toggle", !ShowSplit)
            .AddClass($"btn-primary", Color == Color.None)
            .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .Build();

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        /// <returns></returns>
        private string? ClassName => CssBuilder.Default("btn dropdown-toggle")
          .AddClass("dropdown-toggle-split")
          .AddClass($"btn-{Color.ToDescriptionString()}", Color != Color.None)
          .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
          .Build();

        /// <summary>
        /// 获得 是否分裂式按钮
        /// </summary>
        private string? DropdownToggle => !ShowSplit ? "dropdown" : null;

        /// <summary>
        /// 菜单对齐方式样式
        /// </summary>
        private string? MenuAlignmentClass => CssBuilder.Default("dropdown-menu")
            .AddClass($"dropdown-menu-{MenuAlignment.ToDescriptionString()}", MenuAlignment == Alignment.Right)
            .Build();

        /// <summary>
        /// 设置当前项是否 Active 方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item.Value == CurrentValueAsString)
            .Build();
    }
}
