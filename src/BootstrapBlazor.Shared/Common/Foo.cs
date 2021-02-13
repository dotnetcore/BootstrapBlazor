// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "城市名称")]
        [Required(ErrorMessage = "请选择一个城市")]
        public string Name { get; set; } = "Shanghai";
    }
}
