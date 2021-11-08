// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class Block : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 是否显示此 Block 默认显示
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<Task<bool>>? OnQueryCondition { get; set; }

        /// <summary>
        /// 获得/设置 子组件内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 符合条件显示的内容
        /// </summary>
        [Parameter]
        public RenderFragment? Authorized { get; set; }

        /// <summary>
        /// 获得/设置 不符合条件显示的内容
        /// </summary>
        [Parameter]
        public RenderFragment? NotAuthorized { get; set; }

        private bool IsShow { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            OnQueryCondition ??= () => Task.FromResult(true);
        }

        /// <summary>
        /// OnParametersSetAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            IsShow = await OnQueryCondition();
        }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (IsShow)
            {
                builder.AddContent(0, Authorized ?? ChildContent);
            }
            else
            {
                builder.AddContent(0, NotAuthorized);
            }
        }
    }
}
