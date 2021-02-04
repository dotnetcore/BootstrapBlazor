// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class LayoutPages
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
        private int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool ShowFooter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool IsFixedHeader { get; set; }

        /// <summary>
        /// 获得/设置 是否固定页脚
        /// </summary>
        private bool IsFixedFooter { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏是否外置
        /// </summary>
        private bool IsFullSide { get; set; }

        /// <summary>
        /// 获得/设置 是否开启多标签模式
        /// </summary>
        private bool UseTabSet { get; set; }

        [CascadingParameter]
        [NotNull]
        private PageLayout? RootPage { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IsFullSide = RootPage.IsFullSide;
            IsFixedHeader = RootPage.IsFixedHeader;
            IsFixedFooter = RootPage.IsFixedFooter;
            ShowFooter = RootPage.ShowFooter;
            UseTabSet = RootPage.UseTabSet;

            SideBarItems.ElementAt(IsFullSide ? 0 : 1).Active = true;
        }

        private async Task OnFooterChanged(CheckboxState state, bool val)
        {
            await UpdateAsync();
        }

        private async Task OnHeaderStateChanged(CheckboxState state, bool val)
        {
            await UpdateAsync();
        }

        private async Task OnFooterStateChanged(CheckboxState state, bool val)
        {
            await UpdateAsync();
        }

        private async Task OnSideChanged(CheckboxState state, SelectedItem item)
        {
            IsFullSide = item.Value == "left-right";
            await UpdateAsync();
        }

        private async Task OnUseTabSetChanged(bool val)
        {
            await UpdateAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync()
        {
            await RootPage.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
            {
                [nameof(RootPage.IsFullSide)] = IsFullSide,
                [nameof(RootPage.IsFixedFooter)] = IsFixedFooter && ShowFooter,
                [nameof(RootPage.IsFixedHeader)] = IsFixedHeader,
                [nameof(RootPage.ShowFooter)] = ShowFooter,
                [nameof(RootPage.UseTabSet)] = UseTabSet
            }));

            // 获得 Razor 示例代码
            RootPage.Update();
        }
    }
}
