// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Shared.Common
{
    /// <summary>
    /// 
    /// </summary>
    internal class Foo
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("城市名称")]
        public string Name { get; set; } = "Shanghai";
    }
}
