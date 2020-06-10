using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class LayoutPage
    {
        private IEnumerable<SelectedItem> SideBarItems { get; set; } = new SelectedItem[]
        {
            new SelectedItem("left-right", "左右结构"),
            new SelectedItem("top-bottom", "上下结构")
        };

        private string? StyleString => CssBuilder.Default()
            .AddClass($"height: {Height * 100}px", Height > 0)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool IsFixedHeader { get; set; }

        /// <summary>
        /// 获得/设置 是否固定页脚
        /// </summary>
        [Parameter]
        public bool IsFixedFooter { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏是否外置
        /// </summary>
        [Parameter]
        public bool IsFullSide { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<bool, bool, bool, bool, Task>? OnLayoutChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            SideBarItems.ElementAt(IsFullSide ? 0 : 1).Active = true;
        }

        private async Task OnFooterChanged(CheckboxState state, bool val)
        {
            if (OnLayoutChanged != null) await OnLayoutChanged.Invoke(IsFullSide, IsFixedHeader, IsFixedFooter, ShowFooter);
        }

        private async Task OnHeaderStateChanged(CheckboxState state, bool val)
        {
            if (OnLayoutChanged != null) await OnLayoutChanged.Invoke(IsFullSide, IsFixedHeader, IsFixedFooter, ShowFooter);
        }

        private async Task OnFooterStateChanged(CheckboxState state, bool val)
        {
            if (OnLayoutChanged != null) await OnLayoutChanged.Invoke(IsFullSide, IsFixedHeader, IsFixedFooter, ShowFooter);
        }

        private async Task OnSideChanged(CheckboxState state, SelectedItem item)
        {
            IsFullSide = item.Value == "left-right";
            if (OnLayoutChanged != null) await OnLayoutChanged.Invoke(IsFullSide, IsFixedHeader, IsFixedFooter, ShowFooter);
        }
    }
}
