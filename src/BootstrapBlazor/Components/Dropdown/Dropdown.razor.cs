// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Dropdown<TItem>
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
        protected string? ActiveItem(SelectedItem item) => CssBuilder.Default("dropdown-item")
            .AddClass("active", () => item.Value == CurrentValueAsString)
            .Build();

        /// <summary>
        /// 是否开启分裂式
        /// </summary>
        [Parameter]
        public bool ShowSplit { get; set; }

        /// <summary>
        /// 获取菜单对齐方式
        /// </summary>
        [Parameter]
        public Alignment MenuAlignment { get; set; }

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

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Items ??= Enumerable.Empty<SelectedItem>();

            SelectedItem = Items.FirstOrDefault(i => i.Value == CurrentValueAsString)
                ?? Items.FirstOrDefault(i => i.Active)
                ?? Items.FirstOrDefault();
        }

        /// <summary>
        /// 下拉框选项点击时调用此方法
        /// </summary>
        protected async Task OnItemClick(SelectedItem item)
        {
            if (!item.IsDisabled)
            {
                var i = Items.FirstOrDefault(i => i.Active);
                if (i != null)
                {
                    i.Active = false;
                }
                item.Active = true;

                SelectedItem = item;
                CurrentValueAsString = item.Value;

                // 触发 SelectedItemChanged 事件
                if (OnSelectedItemChanged != null)
                {
                    await OnSelectedItemChanged.Invoke(SelectedItem);
                }
            }
        }
    }
}
