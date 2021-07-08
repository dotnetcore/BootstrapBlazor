// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Transition : BootstrapComponentBase
    {
        private ElementReference transition { get; set; }

        private JSInterop<Transition>? Interop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected virtual string? ClassString => CssBuilder
            .Default("animate__animated")
            .AddClass(Name.ToDescriptionString(), Show)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool Show { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Animate Name { get; set; } = Animate.FadeIn;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool Once { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 动画执行完成回调
        /// </summary>
        [Parameter]
        public Action? Transitioned { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JSInvokable]
        public Task TransitionAsync()
        {
            Transitioned?.Invoke();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                Interop = new JSInterop<Transition>(JSRuntime);
                await Interop.InvokeVoidAsync(this, transition, "bb_transition");
            }
        }
    }
}
