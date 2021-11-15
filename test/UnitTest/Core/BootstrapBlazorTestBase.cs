// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit;
using System;
using Xunit;

namespace UnitTest.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Collection("BlazorTestContext")]
    public class BootstrapBlazorTestBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected TestContext Context { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public BootstrapBlazorTestBase(BootstrapBlazorTestHost host)
        {
            Context = host.Context;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CollectionDefinition("BlazorTestContext")]
    public class BootstrapBlazorTestCollection : ICollectionFixture<BootstrapBlazorTestHost>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class BootstrapBlazorTestHost : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        internal BootstrapBlazorTestContext Context { get; }

        public BootstrapBlazorTestHost()
        {
            Context = new BootstrapBlazorTestContext();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
