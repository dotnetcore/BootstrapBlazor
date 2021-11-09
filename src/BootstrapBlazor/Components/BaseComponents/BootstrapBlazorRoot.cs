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
    public class BootstrapBlazorRoot : ComponentBase
    {
        [Inject]
        [NotNull]
        private IServiceProvider? Provider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            ServiceProviderFactory.Configure(Provider);
        }

        /// <summary>
        /// BuildRenderTree 方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var index = 0;
            //builder.OpenComponent<Dialog>(index++);
            //builder.CloseComponent();

            builder.OpenComponent<Title>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Message>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Toast>(index++);
            builder.CloseComponent();

            builder.OpenComponent<SweetAlert>(index++);
            builder.CloseComponent();

            builder.OpenComponent<FullScreen>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Print>(index++);
            builder.CloseComponent();

            builder.OpenComponent<PopoverConfirm>(index++);
            builder.CloseComponent();

            builder.OpenComponent<Download>(index++);
            builder.CloseComponent();
        }
    }
}
