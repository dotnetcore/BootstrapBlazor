// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class PageLayout
    {
        private bool IsOpen { get; set; }

        private string Theme { get; set; } = "";

        /// <summary>
        /// 获得/设置 是否固定页头
        /// </summary>
        [Parameter]
        public bool IsFixedHeader { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否固定页脚
        /// </summary>
        [Parameter]
        public bool IsFixedFooter { get; set; } = true;

        /// <summary>
        /// 获得/设置 侧边栏是否外置
        /// </summary>
        [Parameter]
        public bool IsFullSide { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示页脚
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否开启多标签模式
        /// </summary>
        [Parameter]
        public bool UseTabSet { get; set; } = true;

        [Inject]
        [NotNull]
        private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("$.tooltip");
            }
        }

        /// <summary>
        /// 更新组件方法
        /// </summary>
        public void Update() => StateHasChanged();

        private IEnumerable<MenuItem> GetIconSideMenuItems() => new List<MenuItem>
        {
            new MenuItem() { Text = "返回组件库", Icon = "fa fa-fw fa-home", Url = "layouts" },
            new MenuItem() { Text = "布局网页", Icon = "fa fa-fw fa-desktop", Url = "layout-page" },
            new MenuItem() { Text = "示例网页", Icon = "fa fa-fw fa-laptop", Url = "Layout-demo" }
        };

        private void ToggleDrawer()
        {
            IsOpen = !IsOpen;
        }
    }
}
