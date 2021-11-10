// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnitTest.Services;

namespace UnitTest
{
    internal class BlazorTestContext : IDisposable
    {
        private TestContext Context { get; set; }

        public IServiceProvider Services => Context.Services;

        public BlazorTestContext()
        {
            Context = new TestContext();
            Context.JSInterop.Mode = JSRuntimeMode.Loose;
            Context.JSInterop.SetupVoid("$.bb_modal", _ => true);

            Context.Services.AddBootstrapBlazor();
            Context.Services.AddConfiguration();
            Context.Services.AddFallbackServiceProvider(new FallbackServiceProvider());
            Context.Services.RegisterProvider();
            Context.RenderComponent<BootstrapBlazorRoot>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
