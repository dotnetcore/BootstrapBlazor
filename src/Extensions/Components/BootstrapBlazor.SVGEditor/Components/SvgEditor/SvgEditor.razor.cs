// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SvgEditor component
    /// </summary>
    public partial class SvgEditor
    {
        /// <summary>
        /// 获得/设置 首次加载内容
        /// </summary>
        [Parameter]
        [NotNull]
        public string? PreContent { get; set; }


        /// <summary>
        /// 获得/设置 保存编辑器内容回调
        /// </summary>
        [Parameter]
        [NotNull]
        public Func<string, Task>? OnSaveChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await InvokeVoidAsync("init", Id, PreContent, Interop, nameof(GetContent));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task GetContent(string value)
        {
            if (OnSaveChanged != null)
            {
                await OnSaveChanged(value);
            }
        }
    }
}
