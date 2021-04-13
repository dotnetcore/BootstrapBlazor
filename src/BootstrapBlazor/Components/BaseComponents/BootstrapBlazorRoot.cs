// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// WIP
    /// </summary>
    internal class BootstrapBlazorRoot : IComponent
    {
        [Inject]
        [NotNull]
        private IServiceProvider? Provider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderHandle"></param>
        public void Attach(RenderHandle renderHandle) => ServiceProviderHelper.RegisterProviderRoot(Provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task SetParametersAsync(ParameterView parameters) => Task.CompletedTask;
    }
}
