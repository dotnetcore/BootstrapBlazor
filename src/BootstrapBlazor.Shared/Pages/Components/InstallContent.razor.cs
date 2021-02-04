// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class InstallContent
    {
        /// <summary>
        /// 获得/设置 版本号字符串
        /// </summary>
        private string Version { get; set; } = "latest";

        /// <summary>
        ///
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "服务器端渲染模式";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string HostFile { get; set; } = "Pages/_Host.cshtml";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            Version = await VersionManager.GetVersionAsync();
        }
    }
}
